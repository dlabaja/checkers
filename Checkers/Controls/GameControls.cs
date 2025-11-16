namespace Checkers.Controls;

public class GameControls : IControls
{
    private Dictionary<ConsoleKey, Action> keyActions;
    private Game game;

    public GameControls(Game game)
    {
        this.game = game;
        this.keyActions = new Dictionary<ConsoleKey, Action>
        {
            {ConsoleKey.UpArrow, () => game.MoveCursorUp()},
            {ConsoleKey.DownArrow, () => game.MoveCursorDown()},
            {ConsoleKey.RightArrow, () => game.MoveCursorRight()},
            {ConsoleKey.LeftArrow, () => game.MoveCursorLeft()},
            {ConsoleKey.W, () => game.MoveCursorUp()},
            {ConsoleKey.S, () => game.MoveCursorDown()},
            {ConsoleKey.D, () => game.MoveCursorRight()},
            {ConsoleKey.A, () => game.MoveCursorLeft()},
            {ConsoleKey.Enter, OnEnter}
        };
    }

    private void OnEnter()
    {
        if (this.game.Selected == null)
        {
            this.game.Select();
            return;
        }
        
        if (this.game.Selected == this.game.Cursor)
        {
            this.game.Deselect();
            return;
        }

        if (!this.game.Board.Pieces.ContainsKey(this.game.Cursor))
        {
            this.game.Move(out Piece? piece);
        }
    }

    public Dictionary<ConsoleKey, Action> KeyActions
    {
        get => this.keyActions;
    }
}
