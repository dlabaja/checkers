namespace Checkers.Controls;

public class GameControls : IControls
{
    private Game game;
    private Dictionary<ConsoleKey, Action> keyActions;

    public GameControls(Game game)
    {
        this.game = game;
        this.keyActions = new Dictionary<ConsoleKey, Action>
        {
            {ConsoleKey.RightArrow, () => this.game.MoveCursorRight()},
            {ConsoleKey.LeftArrow, () => this.game.MoveCursorLeft()}
        };
    }

    public Dictionary<ConsoleKey, Action> KeyActions { get => this.keyActions; }
}
