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

    private List<Position> GetAllowedPositionsOnDiagonal(int n, Position position, int yOffset, int xOffset, Position[] opponentPositions, Position[] playerPositions, List<Position> list)
    {
        var nextPosition = new Position(position.y + yOffset, position.x + xOffset);
        if (playerPositions.Contains(position))
        {
            return list;
        }

        if (opponentPositions.Contains(position))
        {
            if (!playerPositions.Contains(nextPosition) && !opponentPositions.Contains(nextPosition))
            {
                return list.Append(nextPosition).ToList();
            }

            return list;
        }

        return n == 0 ? list.Append(position).ToList() : GetAllowedPositionsOnDiagonal(n - 1, nextPosition, xOffset, yOffset, opponentPositions, playerPositions, list);
    }
    

    public List<Position> GetPieceAllowedPositions(Position position, Piece piece)
    {
        var isQueen = piece.Type == PieceType.Queen;
        var n = isQueen ? 20 : 0;
        var opponentPositions = GetPiecesByColor(piece.Color == PieceColor.White ? PieceColor.Black : PieceColor.White).Keys.ToArray();
        var playerPositions = GetPiecesByColor(piece.Color).Keys.ToArray();
        var list1 = isQueen ? GetAllowedPositionsOnDiagonal(n, new Position(position.y + 1, position.x + 1), 1, 1, opponentPositions, playerPositions, []) : [];
        var list2 = isQueen ? GetAllowedPositionsOnDiagonal(n, new Position(position.y + 1, position.x - 1), 1, -1, opponentPositions, playerPositions, []) : [];
        var list3 = GetAllowedPositionsOnDiagonal(n, new Position(position.y - 1, position.x + 1), -1, 1, opponentPositions, playerPositions, []);
        var list4 = GetAllowedPositionsOnDiagonal(n, new Position(position.y - 1, position.x - 1), -1, -1, opponentPositions, playerPositions, []);
        return list1.Concat(list2).Concat(list3).Concat(list4).Append(position).ToList();
    }

    public Dictionary<Position, Piece> GetPiecesByColor(PieceColor color)
    {
        return color == PieceColor.White ? WhitePieces : BlackPieces;
    }
    
    public Dictionary<Position, Piece> WhitePieces
    {
        get => this.pieces.Where(x => x.Value.Color == PieceColor.White).ToDictionary();
    }
    
    public Dictionary<Position, Piece> BlackPieces
    {
        get => this.pieces.Where(x => x.Value.Color == PieceColor.Black).ToDictionary();
    }
    
    public Dictionary<Position, Piece> Pieces
    {
        get { return pieces; }
    }
}