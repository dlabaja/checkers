namespace Checkers;

public enum PieceType
{
    Pawn,
    Queen
}

public enum PieceColor
{
    White,
    Black
}

public class Piece
{
    public PieceType Type { get; private set; }
    public PieceColor Color { get; }
    
    public Piece(PieceType type, PieceColor color)
    {
        this.Type = type;
        this.Color = color;
    }

    public bool Evolve()
    {
        if (this.Type == PieceType.Pawn)
        {
            this.Type = PieceType.Queen;
            return true;
        }

        return false;
    }
}