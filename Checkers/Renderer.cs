using System.Text;

namespace Checkers;

public class Renderer
{
    public void DisplayBoard(Piece?[,] board)
    {
        var buffer = new StringBuilder();
        for (var y = 0; y < Board.BoardSize; y++)
        {
            for (var x = 0; x < Board.BoardSize; x++)
            {
                switch (board[y, x]?.Type)
                {
                    case PieceType.Pawn:
                        buffer.Append('P');
                        break;
                    case PieceType.Queen:
                        buffer.Append('Q');
                        break;
                    default:
                        buffer.Append(' ');
                        break;
                }
            }

            buffer.Append('\n');
        }
        
        Console.WriteLine(buffer.ToString());
    }
}