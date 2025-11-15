using System.Text;
using static Checkers.Renderer.RenderUtils;

namespace Checkers.Renderer;

public class GameRenderer: IRenderer
{
    private Game game;
    private bool cursorIsBlinked;
    
    public GameRenderer(Game game)
    {
        this.game = game;
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

    private static void DisplayCell(Piece? piece, Color background, StringBuilder buffer)
    {
        buffer.Append(RenderUtils.Bg(background));
        if (piece != null)
        {
            DisplayPiece(piece, buffer);
        }
        else
        {
            buffer.Append("   ");
        }
        
        buffer.Append(RenderUtils.ColorReset());
    }

    private Color GetBackgroundForCell(Position position)
    {
        if (this.game.Selected != null)
        {
            return this.game.Selected == position ? Color.Purple : Color.Light_Purple;
        }

        if (this.cursorIsBlinked && this.game.Cursor == position)
        {
            return Color.Red;
        }

        var offset = position.y % 2;
        var isLightBackground = (position.x + offset) % 2 == 0;
        return isLightBackground ? Color.Wooden : Color.Dark_Gray;
    }

    private void DisplayBoard(Piece?[,] board)
    {
        var buffer = new StringBuilder();
        for (var y = 0; y < Board.BoardSize; y++)
        {
            for (var x = 0; x < Board.BoardSize; x++)
            {
                DisplayCell(board[y, x], GetBackgroundForCell(new Position(y, x)), buffer);
            }

            buffer.Append('\n');
        }
        
        Console.WriteLine(buffer.ToString());
    }

    public void Render()
    {
        DisplayBoard(this.game.Board.GetBoard());
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
