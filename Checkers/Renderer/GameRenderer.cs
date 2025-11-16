using System.Text;
using System.Timers;
using static Checkers.Renderer.RenderUtils;
using Timer = System.Timers.Timer;

namespace Checkers.Renderer;

public class GameRenderer : IRenderer
{
    private Game game;
    private bool cursorIsBlinked;
    private readonly Timer blinkTimer;

    public GameRenderer(Game game)
    {
        this.game = game;
        blinkTimer = new Timer(700);
        blinkTimer.Elapsed += OnBlinkTimerElapsed;
        blinkTimer.AutoReset = true;
    }

    private void OnBlinkTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        cursorIsBlinked = !cursorIsBlinked;
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
        buffer.Append(Bg(background));
        if (piece != null)
        {
            DisplayPiece(piece, buffer);
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
                return this.cursorIsBlinked ? Color.Purple : Color.Light_Purple;
            }
            return this.cursorIsBlinked ? Color.Red : Color.Light_Red;
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
        if (allowedPositions.Count > 0)
        {
            Console.WriteLine("");
        }
        var buffer = new StringBuilder();
        for (var y = 0; y < Board.BoardSize; y++)
        {
            for (var x = 0; x < Board.BoardSize; x++)
            {
                DisplayCell(board[y, x], GetBackgroundForCell(new Position(y, x), allowedPositions), buffer);
            }

            buffer.Append('\n');
        }

        Console.WriteLine(buffer.ToString());
    }

    public void Start()
    {
        this.blinkTimer.Enabled = true;
    }

    public void Render()
    {
        DisplayBoard(this.game.Board.GetBoard());
    }

    public void Dispose()
    {
        this.blinkTimer.Stop();
        this.blinkTimer.Dispose();
    }
}
