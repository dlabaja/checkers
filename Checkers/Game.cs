namespace Checkers;

public class Game
{
    public event Action<PieceColor>? OnGameOver;
    private readonly Position defaultCursorPosition = new Position(7, 0);
    public PieceColor CurrentColor { get; private set; }
    public Board Board { get; }
    public Position Cursor { get; private set; }
    public Position? Selected { get; private set; }

    public Game()
    {
        this.Cursor = defaultCursorPosition;
        this.CurrentColor = PieceColor.White;
        this.Board = new Board();
    }
    
    private List<Position> GetValidCursorPositions()
    {
        if (!Selected.HasValue)
        {
            return CurrentPieces.Keys.ToList();
        }

        if (this.CurrentPieces.TryGetValue(this.Selected.Value, out Piece? piece))
        {
            return this.Board.GetPieceAllowedPositions(this.Selected.Value, piece);
        }
        
        return CurrentPieces.Keys.ToList();
    }

    private void SwitchCurrentColor()
    {
        this.CurrentColor = this.CurrentColor == PieceColor.White ? PieceColor.Black : PieceColor.White;
    }

    private void ResetCursor()
    {
        this.Cursor = this.CurrentPieces.First().Key;
    }

    private bool IsGameOver()
    {
        return this.Board.WhitePieces.Count == 0 || this.Board.BlackPieces.Count == 0;
    }
    
    public void MoveCursorRight()
    {
        var positions = GetValidCursorPositions().Where(pos => pos.y == this.Cursor.y && pos.x > this.Cursor.x).ToList();
        if (positions.Count == 0)
        {
            return;
        }

        this.Cursor = positions.OrderBy(pos => pos.x).First();
    }
    
    public void MoveCursorLeft()
    {
        var positions = GetValidCursorPositions().Where(pos => pos.y == this.Cursor.y && pos.x < this.Cursor.x).ToList();
        if (positions.Count == 0)
        {
            return;
        }

        this.Cursor = positions.OrderBy(pos => pos.x).Last();
    }

    public void MoveCursorUp()
    {
        var positions = GetValidCursorPositions().Where(pos => pos.y < this.Cursor.y).ToList();
        if (positions.Count == 0)
        {
            return;
        }

        var sorted = positions.OrderBy(pos => pos.y).ToList();
        var y = sorted.Last().y;
        var lowestRow = sorted.Where(pos => pos.y == y).Select(pos => pos.x).ToList();
        this.Cursor = new Position(y, Utils.FindClosestToX(this.Cursor.x, lowestRow));
    }

    public void MoveCursorDown()
    {
        var positions = GetValidCursorPositions().Where(pos => pos.y > this.Cursor.y).ToList();
        if (positions.Count == 0)
        {
            return;
        }

        var sorted = positions.OrderBy(pos => pos.y).ToList();
        var y = sorted.First().y;
        var lowestRow = sorted.Where(pos => pos.y == y).Select(pos => pos.x).ToList();
        this.Cursor = new Position(y, Utils.FindClosestToX(this.Cursor.x, lowestRow, false));
    }

    public void Select()
    {
        if (this.Board.Pieces.ContainsKey(this.Cursor))
        {
            this.Selected = this.Cursor;
        }
    }

    private bool Move(out Piece? captured)
    {
        captured = null;
        var pieceWhichHasToCapture = this.Board.GetPieceWhichHasToCapture(this.CurrentColor);
        if (this.Selected == null)
        {
            return false;
        }
        
        if (this.Board.Move(this.Selected.Value, this.Cursor, out captured))
        {
            this.Selected = null;
        }
        
        if (captured == null && pieceWhichHasToCapture.HasValue)
        {
            this.Board.CapturePiece(pieceWhichHasToCapture.Value);
        }

        return true;
    }
    
    public void MakeTurn(out Piece? captured)
    {
        if (Move(out captured))
        {
            if (IsGameOver())
            {
                this.OnGameOver?.Invoke(this.CurrentColor);
                return;
            }
            SwitchCurrentColor();
            this.ResetCursor();
        }

    }

    public void Deselect()
    {
        this.Selected = null;
    }
    
    private Dictionary<Position, Piece> CurrentPieces
    {
        get => this.CurrentColor == PieceColor.White ? this.Board.WhitePieces : this.Board.BlackPieces;
    }
}