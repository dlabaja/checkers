namespace Checkers.Controls;

public class MenuControls: IControls
{
    private Dictionary<ConsoleKey, Action> keyActions;
    private Menu menu;

    public MenuControls(Menu menu)
    {
        this.menu = menu;
        this.keyActions = new Dictionary<ConsoleKey, Action>
        {
            {ConsoleKey.UpArrow, menu.HighlightPrevButton},
            {ConsoleKey.DownArrow, menu.HighlightNextButton},
            {ConsoleKey.W, menu.HighlightPrevButton},
            {ConsoleKey.S, menu.HighlightNextButton},
            //{ConsoleKey.Enter, OnEnter}
        };
    }

    public Dictionary<ConsoleKey, Action> KeyActions => this.keyActions;
}
