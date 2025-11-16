namespace Checkers.Renderer;

public class MenuRenderer: IRenderer
{
    private Checkers.Menu menu;
    private Blinker blinker;
    private string text;

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
            Console.WriteLine(button.ToString(blinker.CursorIsBlinked && button.Highlighted));
        }
    }
}
