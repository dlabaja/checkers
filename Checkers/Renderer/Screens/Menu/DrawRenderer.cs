namespace Checkers.Renderer.Screens.Menu;

public class DrawRenderer: MenuRenderer
{
    public static List<Button> Buttons(Game game)
    {
        return [
            new Button("No", () => Controller.RenderGame(game)),
            new Button("Yes", () => Controller.RenderGameOver(null)),
        ];
    }
    private const string text = """
                                Do you really want to offer a draw?
                                """;
    
    public DrawRenderer(Checkers.Menu menu, Blinker blinker) 
        : base(menu, text, blinker) {}
}
