using Checkers.Renderer.Interfaces;
using Checkers.Renderer.Utils;
using System.Text;
using static Checkers.Renderer.Utils.RenderUtils;

namespace Checkers.Renderer.Screens;

public class GameRenderer : IRenderer
{
    private Game game;
    private Blinker blinker;

    public GameRenderer(Game game, Blinker blinker)
    {
        this.game = game;
        this.blinker = blinker;
        this.game.Board.OnCapture += Console.Beep;
    }

    private static void DisplayPiece(Piece piece, StringBuilder buffer)
    {
        switch (piece.Color)
        {
            case PieceColor.White:
                buffer.Append(Fg(Color.White));
                break;
            case PieceColor.Black:
                buffer.Append(Fg(Color.Black));
                break;
        }

        switch (piece.Type)
        {
            case PieceType.Pawn:
                buffer.Append($"{Character.HairSpace}{Character.Pawn}{Character.HairSpace}");
                break;
            case PieceType.Queen:
                buffer.Append($"{Character.HairSpace}{Character.Queen}{Character.HairSpace}");
                break;
        }
    }

    private void DisplayCell(Piece? piece, Position position, Color background, StringBuilder buffer)
    {
        buffer.Append(Bg(background));
        if (piece != null)
        {
            DisplayPiece(piece, buffer);
        }
        else if (this.game.Board.LatestDeath == position)
        {
            buffer.Append($"{Fg(Color.Red)}{Character.HairSpace}{Character.Skull}{Character.HairSpace}");
        }
        else
        {
            buffer.Append("   ");
        }

        buffer.Append(ColorReset());
    }

    private Color GetBackgroundForCell(Position position, List<Position> allowedPositions)
    {
        if (this.game.Cursor == position)
        {
            if (game.Selected != null)
            {
                return this.blinker.CursorIsBlinked ? Color.Purple : Color.Light_Purple;
            }
            return this.blinker.CursorIsBlinked ? Color.Red : Color.Light_Red;
        }
        
        if (this.game.Selected == position)
        {
            return Color.Dark_Purple;
        }

        if (allowedPositions.Contains(position))
        {
            return Color.Dark_Gray;
        }

        var offset = position.y % 2;
        var isLightBackground = (position.x + offset) % 2 == 0;
        return isLightBackground ? Color.Wooden : Color.Light_Gray;
    }

    private void DisplayBoard(Piece?[,] board)
    {
        var selected = this.game.Selected;
        var allowedPositions = selected.HasValue ? this.game.Board.GetPieceAllowedPositions(selected.Value, this.game.Board.Pieces[selected.Value]) : [];
        var buffer = new StringBuilder();
        for (var y = 0; y < Board.BoardSize; y++)
        {
            for (var x = 0; x < Board.BoardSize; x++)
            {
                var position = new Position(y, x);
                DisplayCell(board[y, x], position, GetBackgroundForCell(position, allowedPositions), buffer);
            }

            buffer.Append('\n');
        }

        Console.Write(buffer.ToString());
    }

    private void DisplayText()
    {
        Console.WriteLine($"Currently playing: {this.game.CurrentColor}");
    }

    public void Render()
    {
        DisplayBoard(this.game.Board.GetBoard());
        DisplayText();
    }
}
