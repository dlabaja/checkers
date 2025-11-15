namespace Checkers.Controls;

public class GameControls : IControls
{
    private Dictionary<ConsoleKey, Action> keyActions;

    public GameControls(Game game)
    {
        this.keyActions = new Dictionary<ConsoleKey, Action>
        {
            {ConsoleKey.UpArrow, () => game.MoveCursorUp()},
            {ConsoleKey.DownArrow, () => game.MoveCursorDown()},
            {ConsoleKey.RightArrow, () => game.MoveCursorRight()},
            {ConsoleKey.LeftArrow, () => game.MoveCursorLeft()},
            {ConsoleKey.W, () => game.MoveCursorUp()},
            {ConsoleKey.S, () => game.MoveCursorDown()},
            {ConsoleKey.D, () => game.MoveCursorRight()},
            {ConsoleKey.A, () => game.MoveCursorLeft()}
        };
    }

    public Dictionary<ConsoleKey, Action> KeyActions
    {
        get => this.keyActions;
    }
}
