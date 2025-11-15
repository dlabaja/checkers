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

    private List<Position> GetAllowedPositionsOnDiagonal(int n, Position position, int xOffset, int yOffset, Position[] opponentPieces, List<Position> list)
    {
        var cellIsEmpty = !opponentPieces.Contains(position);
        var nextPosition = new Position(position.y + yOffset, position.x + xOffset);
        if (n == 0)
        {
            var isNextEmpty = !opponentPieces.Contains(nextPosition);
            if (cellIsEmpty)
            {
                list.Add(position);
            }
            else if (isNextEmpty)
            {
                list.Add(position);
                list.Add(nextPosition);
            }

            return list;
        }
        list.Add(position);
        return GetAllowedPositionsOnDiagonal(n - 1, nextPosition, xOffset, yOffset, opponentPieces, list);
    }
    

    public List<Position> GetPieceAllowedPositions(Position position, Piece piece)
    {
        var n = piece.Type == PieceType.Pawn ? 1 : 20;
        var oppositePieces = GetPiecesByColor(piece.Color == PieceColor.White ? PieceColor.Black : PieceColor.White).Keys.ToArray();
        var list1 = GetAllowedPositionsOnDiagonal(n, position, 1, 1, oppositePieces, []);
        var list2 = GetAllowedPositionsOnDiagonal(n, position, -1, 1, oppositePieces, []);
        var list3 = GetAllowedPositionsOnDiagonal(n, position, 1, -1, oppositePieces, []);
        var list4 = GetAllowedPositionsOnDiagonal(n, position, -1, -1, oppositePieces, []);
        return list1.Concat(list2).Concat(list3).Concat(list4).Distinct().ToList();
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
}