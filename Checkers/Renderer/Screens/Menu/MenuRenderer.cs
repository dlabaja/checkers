using Checkers.Renderer.Interfaces;

namespace Checkers.Renderer.Screens.Menu;

public class MenuRenderer: IRenderer
{
    private readonly Checkers.Menu menu;
    private readonly Blinker blinker;
    private readonly string text;

    public MenuRenderer(Checkers.Menu menu, string text, Blinker blinker)
    {
        this.menu = menu;
        this.text = text;
        this.blinker = blinker;
    }
    
    public void Render()
    {
        Console.WriteLine(this.text);
        foreach (var button in menu.Buttons)
        {
            Console.WriteLine(button.ToString((blinker.CursorIsBlinked || this.menu.ChangedHighlightedButton) && button.Highlighted));
        }

        this.menu.ChangedHighlightedButton = false;
    }
}
