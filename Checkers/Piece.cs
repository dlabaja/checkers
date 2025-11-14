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
    private PieceType type;
    private PieceColor color;
    
    public Piece(PieceType type, PieceColor color)
    {
        this.type = type;
        this.color = color;
    }

    public bool Evolve()
    {
        if (this.type == PieceType.Pawn)
        {
            this.type = PieceType.Queen;
            return true;
        }

        return false;
    }
}