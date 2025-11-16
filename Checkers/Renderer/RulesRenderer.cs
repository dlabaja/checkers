namespace Checkers.Renderer;

public class RulesRenderer: MenuRenderer
{
    public static List<Button> buttons = [
        new Button("Back to menu", Controller.RenderMainMenu),
    ];
    private static readonly string text = """
                                          Rules:
                                          TBA
                                          """;
    
    public RulesRenderer(Checkers.Menu menu, Blinker blinker) 
        : base(menu, text, blinker) {}
}
