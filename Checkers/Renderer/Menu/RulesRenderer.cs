namespace Checkers.Renderer.Menu;

public class RulesRenderer: MenuRenderer
{
    public static List<Button> Buttons
    {
        get =>
        [
            new Button("Back to menu", Controller.RenderMainMenu),
        ];
    }
    private static readonly string text = """
                                          Rules:
                                          Check it here: https://www.deskovehry.info/pravidla/ceska-dama.htm
                                          """;
    
    public RulesRenderer(Checkers.Menu menu, Blinker blinker) 
        : base(menu, text, blinker) {}
}
