namespace Checkers;

public record struct Position(int y, int x)
{
    public readonly int x = x;
    public readonly int y = y;
}

public class Board
{
    public const byte BoardSize = 8;
    private readonly Dictionary<Position, Piece> pieces = new Dictionary<Position, Piece>();
    
    public Board()
    {
        CreateBoard();
        this.pieces[new Position(0, 1)].Evolve();
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
    
    public Dictionary<Position, Piece> WhitePieces
    {
        get => this.pieces.Where(x => x.Value.Color == PieceColor.White).ToDictionary();
    }
    
    public Dictionary<Position, Piece> BlackPieces
    {
        get => this.pieces.Where(x => x.Value.Color == PieceColor.Black).ToDictionary();
    }
}