namespace Checkers;

public class Game
{
    public event Action<PieceColor>? OnGameOver;
    private readonly Position defaultCursorPosition = new Position(7, 0);
    private Position cursor;
    private Position? selected;
    private PieceColor currentColor;
    private Board board;

    public Game()
    {
        this.cursor = defaultCursorPosition;
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

    private void SwitchCurrentColor()
    {
        this.currentColor = this.currentColor == PieceColor.White ? PieceColor.Black : PieceColor.White;
    }

    private void ResetCursor()
    {
        this.cursor = this.CurrentPieces.First().Key;
    }

    private bool IsGameOver()
    {
        return this.Board.WhitePieces.Count == 0 || this.Board.BlackPieces.Count == 0;
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
        if (this.board.Pieces.ContainsKey(this.cursor))
        {
            this.selected = this.cursor;
            return true;
        }

        return false;
    }

    private bool Move(out Piece? captured)
    {
        captured = null;
        var pieceWhichHasToCapture = this.board.GetPieceWhichHasToCapture(this.currentColor);
        if (this.selected == null)
        {
            return false;
        }
        
        if (this.Board.Move(this.selected.Value, this.cursor, out captured))
        {
            this.selected = null;
        }
        
        if (captured == null && pieceWhichHasToCapture.HasValue)
        {
            this.Board.CapturePiece(pieceWhichHasToCapture.Value);
        }

        return true;
    }
    
    public bool MakeTurn(out Piece? captured)
    {
        if (Move(out captured))
        {
            if (IsGameOver())
            {
                this.OnGameOver?.Invoke(this.currentColor);
                return true;
            }
            SwitchCurrentColor();
            this.ResetCursor();
            return true;
        }

        return false;
    }

    public void Deselect()
    {
        this.selected = null;
    }
    
    private Dictionary<Position, Piece> CurrentPieces
    {
        get => this.CurrentColor == PieceColor.White ? this.Board.WhitePieces : this.Board.BlackPieces;
    }
    
    public Position Cursor => cursor;

    public PieceColor CurrentColor => currentColor;

    public Board Board => board;

    public Position? Selected => selected;
}