namespace Checkers;

public struct Position
{
    public int x;
    public int y;

    public Position(int y, int x)
    {
        this.y = y;
        this.x = x;
    } 
}

public class Board
{
    public const byte BoardSize = 8;
    private readonly Dictionary<Position, Piece> pieces = new();
    
    public Board()
    {
        CreateBoard();
    }

    private void CreateRow(PieceColor color, int offset, int rowIndex)
    {
        for (var x = 0; x < BoardSize; x++)
        {
            if ((x + offset) % 2 == 0)
            {
                pieces.Add(new Position(rowIndex, x), new Piece(PieceType.Pawn, color));
            }
        }
    }

    private void CreateBoard()
    {
        for (var y = 0; y < BoardSize; y++)
        {
            var color = y < 3 ? PieceColor.Black : PieceColor.White;
            var offset = y % 2 == 0 ? 1 : 0; 
            if (y is 3 or 4)
            {
                continue;
            }
            
            CreateRow(color, offset, y);
        }
    }

    public bool Move(Piece piece, out List<Piece> captured)
    {
        // evolve
        captured = new List<Piece>();
        return false;
    }

    public Piece?[,] GetBoard()
    {
        var board = new Piece?[8,8];
        foreach (var (pos, piece) in pieces)
        {
            board[pos.y, pos.x] = piece;
        }

        return board;
    }
}