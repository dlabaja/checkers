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

        return CurrentPieces.Keys.ToList();
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

    public bool MoveCursorUp()
    {
        var positions = GetValidCellPositions().Where(pos => pos.y < this.cursor.y).ToList();
        if (positions.Count == 0)
        {
            return false;
        }

        var sorted = positions.OrderBy(pos => pos.y).ToList();
        var y = sorted.Last().y;
        var lowestRow = sorted.Where(pos => pos.y == y).Select(pos => pos.x).ToList();
        this.cursor = new Position(y, Utils.FindClosestToX(this.cursor.x, lowestRow));
        return true;
    }

    public bool MoveCursorDown()
    {
        var positions = GetValidCellPositions().Where(pos => pos.y > this.cursor.y).ToList();
        if (positions.Count == 0)
        {
            return false;
        }

        var sorted = positions.OrderBy(pos => pos.y).ToList();
        var y = sorted.First().y;
        var lowestRow = sorted.Where(pos => pos.y == y).Select(pos => pos.x).ToList();
        this.cursor = new Position(y, Utils.FindClosestToX(this.cursor.x, lowestRow, false));
        return true;
    }

    public void Select()
    {
        if (this.selected == this.cursor)
        {
            this.selected = null;
            return;
        }
        this.selected = this.cursor;
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