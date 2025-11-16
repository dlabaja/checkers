namespace Checkers.Renderer.Screens.Menu;

public class ControlsRenderer: MenuRenderer
{
    public static List<Button> Buttons
    {
        get =>
        [
            new Button("Back to menu", Controller.RenderMainMenu),
        ];
    }
    private const string text = """
                                Controls:
                                - Move cursor:        Arrows/WASD
                                - Select/Move piece:  Enter
                                - Make a Draw         P
                                """;
    
    public ControlsRenderer(Checkers.Menu menu, Blinker blinker) 
        : base(menu, text, blinker) {}
}
