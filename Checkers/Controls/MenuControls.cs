namespace Checkers.Controls;

public class MenuControls: IControls
{
    private Menu menu;
    public Dictionary<ConsoleKey, Action> KeyActions { get; }

    public MenuControls(Menu menu)
    {
        this.menu = menu;
        this.KeyActions = new Dictionary<ConsoleKey, Action>
        {
            {ConsoleKey.UpArrow, menu.HighlightPrevButton},
            {ConsoleKey.DownArrow, menu.HighlightNextButton},
            {ConsoleKey.W, menu.HighlightPrevButton},
            {ConsoleKey.S, menu.HighlightNextButton},
            {ConsoleKey.Enter, menu.SelectButton}
        };
    }
}
