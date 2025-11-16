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
    
    private List<Position> GetValidCursorPositions()
    {
        if (selected == null)
        {
            return CurrentPieces.Keys.ToList();
        }

        if (this.CurrentPieces.TryGetValue(this.selected.Value, out Piece? piece))
        {
            return this.board.GetPieceAllowedPositions(this.selected.Value, piece);
        }
        
        return CurrentPieces.Keys.ToList();
    }
    
    public bool MoveCursorRight()
    {
        var positions = GetValidCursorPositions().Where(pos => pos.y == this.cursor.y && pos.x > this.cursor.x).ToList();
        if (positions.Count == 0)
        {
            return false;
        }

        this.cursor = positions.OrderBy(pos => pos.x).First();
        return true;
    }
    
    public bool MoveCursorLeft()
    {
        var positions = GetValidCursorPositions().Where(pos => pos.y == this.cursor.y && pos.x < this.cursor.x).ToList();
        if (positions.Count == 0)
        {
            return false;
        }

        this.cursor = positions.OrderBy(pos => pos.x).Last();
        return true;
    }

    public bool MoveCursorUp()
    {
        var positions = GetValidCursorPositions().Where(pos => pos.y < this.cursor.y).ToList();
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
        var positions = GetValidCursorPositions().Where(pos => pos.y > this.cursor.y).ToList();
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

    public bool Select()
    {
        if (this.board.Pieces.TryGetValue(this.cursor, out _))
        {
            this.selected = this.cursor;
            return true;
        }

        return false;
    }

    public void Deselect()
    {
        this.selected = null;
    }

    public bool Move(Position position, out Piece? captured)
    {
        captured = null;
        if (this.selected != null)
        {
            this.board.Pieces[this.cursor] = this.board.Pieces[this.selected.Value];
            this.board.Pieces.Remove(this.selected.Value);
            this.selected = null;
            return true;
        }

        return false;
    }
    
    private Dictionary<Position, Piece> CurrentPieces
    {
        get => this.CurrentColor == PieceColor.White ? this.Board.WhitePieces : this.Board.BlackPieces;
    }
    
    private Dictionary<Position, Piece> OpponentPieces
    {
        get => this.CurrentColor == PieceColor.White ? this.Board.BlackPieces : this.Board.WhitePieces;
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