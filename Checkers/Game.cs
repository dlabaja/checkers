namespace Checkers;

public class Game
{
    private Position cursor;
    private Position? selected;
    private PieceColor currentColor;
    private Board board;

    public Game()
    {
        this.cursor = new Position(7, 0);
        this.currentColor = PieceColor.White;
        this.board = new Board();
    }

    private Dictionary<Position, Piece> CurrentPieces
    {
        get => this.CurrentColor == PieceColor.White ? this.Board.WhitePieces : this.Board.BlackPieces;
    }

    private List<Position> GetValidCellPositions()
    {
        if (selected == null)
        {
            return CurrentPieces.Keys.ToList();
        }

        return new List<Position>();
    }
    
    public bool MoveCursorRight()
    {
        var positions = GetValidCellPositions().Where(pos => pos.y == this.cursor.y && pos.x > this.cursor.x).ToList();
        if (positions.Count == 0)
        {
            return false;
        }

        this.cursor = positions.OrderBy(pos => pos.x).First();
        return true;
    }
    
    public bool MoveCursorLeft()
    {
        var positions = GetValidCellPositions().Where(pos => pos.y == this.cursor.y && pos.x < this.cursor.x).ToList();
        if (positions.Count == 0)
        {
            return false;
        }

        this.cursor = positions.OrderBy(pos => pos.x).Last();
        return true;
    }

    public void Select()
    {
        this.selected = this.cursor;
    }

    public void Deselect()
    {
        this.selected = null;
    }
    
    public Position Cursor
    {
        get { return cursor; }
    }

    public PieceColor CurrentColor
    {
        get { return currentColor; }
    }

    public Board Board
    {
        get { return board; }
    }

    public Position? Selected
    {
        get { return selected; }
    }
}