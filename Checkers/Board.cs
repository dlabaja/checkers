namespace Checkers;

public record struct Position(int y, int x)
{
    public readonly int x = x;
    public readonly int y = y;
}

public class Board
{
    public const byte BoardSize = 8;
    public Action? OnCapture;
    public Dictionary<Position, Piece> Pieces { get; } = new Dictionary<Position, Piece>();
    public Position? LatestDeath { get; private set; }

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
                Pieces.Add(new Position(rowIndex, x), new Piece(PieceType.Pawn, color));
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

    private bool CanEvolve(Position position, Piece piece)
    {
        var evolutionRow = piece.Color == PieceColor.White ? 0 : BoardSize - 1;
        if (position.y == evolutionRow)
        {
            return true;
        }

        return false;
    }

    private List<(Position allowedPosition, Position? captured)> GetAllowedPositionsOnDiagonal(int n, Position position, int yOffset, int xOffset, Position[] opponentPositions, Position[] playerPositions, List<(Position allowedPosition, Position? captured)> list)
    {
        var nextPosition = new Position(position.y + yOffset, position.x + xOffset);
        if (PositionOutOfBounds(position) || playerPositions.Contains(position))
        {
            return list;
        }

        if (opponentPositions.Contains(position))
        {
            if (!PositionOutOfBounds(nextPosition) && !playerPositions.Contains(nextPosition) && !opponentPositions.Contains(nextPosition))
            {
                return list.Append((nextPosition, position)).ToList();
            }

            return list;
        }

        if (n == 0 || PositionOutOfBounds(nextPosition))
        {
            list.Add((position, null));
            return list;
        }

        list.Add((position, null));
        return GetAllowedPositionsOnDiagonal(n - 1, nextPosition, yOffset, xOffset, opponentPositions, playerPositions, list);
    }

    private List<(Position allowedPosition, Position? captured)> GetPieceAllowedPositionsAndCapturables(Position position, Piece piece)
    {
        var isPawn = piece.Type == PieceType.Pawn;
        var isBlackPawn = isPawn && piece.Color == PieceColor.Black;
        var isWhitePawn = isPawn && piece.Color == PieceColor.White;
        var n = isPawn ? 0 : 20;
        var opponentPositions = GetPiecesByColor(piece.Color == PieceColor.White ? PieceColor.Black : PieceColor.White).Keys.ToArray();
        var playerPositions = GetPiecesByColor(piece.Color).Keys.ToArray();
        var list1 = isWhitePawn ? [] : GetAllowedPositionsOnDiagonal(n, new Position(position.y + 1, position.x + 1), 1, 1, opponentPositions, playerPositions, []);
        var list2 = isWhitePawn ? [] : GetAllowedPositionsOnDiagonal(n, new Position(position.y + 1, position.x - 1), 1, -1, opponentPositions, playerPositions, []);
        var list3 = isBlackPawn ? [] : GetAllowedPositionsOnDiagonal(n, new Position(position.y - 1, position.x + 1), -1, 1, opponentPositions, playerPositions, []);
        var list4 = isBlackPawn ? [] : GetAllowedPositionsOnDiagonal(n, new Position(position.y - 1, position.x - 1), -1, -1, opponentPositions, playerPositions, []);
        return list1.Concat(list2).Concat(list3).Concat(list4).Append((position, null)).ToList();
    }

    private static bool PositionOutOfBounds(Position position)
    {
        return !Utils.NumberInRange(position.x, 0, BoardSize) || !Utils.NumberInRange(position.y, 0, BoardSize);
    }

    public void CapturePiece(Position position)
    {
        this.Pieces.Remove(position);
        this.OnCapture?.Invoke();
        this.LatestDeath = position;
    }

    public bool Move(Position start, Position end, out Piece? captured)
    {
        captured = null;
        if (!this.Pieces.ContainsKey(start))
        {
            return false;
        }

        var allowedPositionsAndCaptures = this.GetPieceAllowedPositionsAndCapturables(start, this.Pieces[start]);
        var allowedPositions = allowedPositionsAndCaptures.Select(x => x.allowedPosition).ToList();
        if (!allowedPositions.Contains(end))
        {
            return false;
        }

        var capturedPosition = allowedPositionsAndCaptures.Find(x => x.allowedPosition == end).captured;
        if (capturedPosition != null)
        {
            captured = this.Pieces[capturedPosition.Value];
            CapturePiece(capturedPosition.Value);
        }

        this.Pieces[end] = this.Pieces[start];
        this.Pieces.Remove(start);

        if (CanEvolve(end, this.Pieces[end]))
        {
            this.Pieces[end].Evolve();
        }

        return true;
    }

    public Piece?[,] GetBoard()
    {
        var board = new Piece?[8, 8];
        foreach (var (pos, piece) in Pieces)
        {
            board[pos.y, pos.x] = piece;
        }

        return board;
    }

    public List<Position> GetPieceAllowedPositions(Position position, Piece piece)
    {
        return GetPieceAllowedPositionsAndCapturables(position, piece).Select(x => x.allowedPosition).ToList();
    }

    public List<Position> GetPieceCapturables(Position position, Piece piece)
    {
        return GetPieceAllowedPositionsAndCapturables(position, piece).Select(x => x.captured).OfType<Position>().ToList();
    }

    public Position? GetPieceWhichHasToCapture(PieceColor color)
    {
        var piecesByColor = GetPiecesByColor(color);
        foreach (var (position, piece) in piecesByColor)
        {
            if (GetPieceCapturables(position, piece).Count > 0)
            {
                return position;
            }
        }

        return null;
    } 

    private Dictionary<Position, Piece> GetPiecesByColor(PieceColor color)
    {
        return color == PieceColor.White ? WhitePieces : BlackPieces;
    }

    public Dictionary<Position, Piece> WhitePieces
    {
        get => this.Pieces.Where(x => x.Value.Color == PieceColor.White).ToDictionary();
    }

    public Dictionary<Position, Piece> BlackPieces
    {
        get => this.Pieces.Where(x => x.Value.Color == PieceColor.Black).ToDictionary();
    }
}
