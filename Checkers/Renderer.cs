using System.Text;

namespace Checkers;

public enum Color
{
    Black = 0,
    White = 15,
    Wooden = 180,
    Dark_Gray = 248
}

public class Renderer
{
    private string Fg(Color color)
    {
        return $"\x1b[38;5;{color}m";
    }
    
    private string Bg(Color color)
    {
        return $"\x1b[48;5;{color}m";
    }

    private string ColorReset()
    {
        return $"{Fg(Color.White)}{Bg(Color.Black)}";
    }

    private void DisplayPiece(Piece piece, StringBuilder buffer)
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
                buffer.Append('⬤');
                break;
            case PieceType.Queen:
                buffer.Append("🜲");
                break;
            default:
                buffer.Append(' ');
                break;
        }
    }

    private void DisplayCell(Piece? piece, bool isLightBackground, StringBuilder buffer)
    {
        buffer.Append(isLightBackground ? Bg(Color.Wooden) : Bg(Color.Dark_Gray));
        if (piece != null)
        {
            DisplayPiece(piece, buffer);
        }
        
        buffer.Append(ColorReset());
    }
    
    public void DisplayBoard(Piece?[,] board)
    {
        var buffer = new StringBuilder();
        for (var y = 0; y < Board.BoardSize; y++)
        {
            for (var x = 0; x < Board.BoardSize; x++)
            {
                var offset = y % 2;
                var isLightBackground = (x + offset) % 2 == 0;
                DisplayCell(board[y, x], isLightBackground, buffer);
            }

            buffer.Append('\n');
        }
        
        Console.WriteLine(buffer.ToString());
    }

    public void Clear()
    {
        Console.Clear();
    }
}