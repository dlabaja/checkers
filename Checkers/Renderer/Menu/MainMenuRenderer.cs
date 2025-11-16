namespace Checkers.Renderer.Menu;

public class MainMenuRenderer : MenuRenderer
{
    public static List<Button> buttons = [
        new Button("Play", Controller.RenderGame),
        new Button("Rules", Controller.RenderRules), 
        new Button("Exit", Controller.Exit)
    ];
    private static readonly string text = """
                                           ██████╗██╗  ██╗███████╗ ██████╗██╗  ██╗███████╗██████╗ ███████╗
                                          ██╔════╝██║  ██║██╔════╝██╔════╝██║ ██╔╝██╔════╝██╔══██╗██╔════╝
                                          ██║     ███████║█████╗  ██║     █████╔╝ █████╗  ██████╔╝███████╗
                                          ██║     ██╔══██║██╔══╝  ██║     ██╔═██╗ ██╔══╝  ██╔══██╗╚════██║
                                          ╚██████╗██║  ██║███████╗╚██████╗██║  ██╗███████╗██║  ██║███████║
                                           ╚═════╝╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚══════╝
                                          --------------Made by Jan Dlabaja as a C# homework--------------
                                          """;
    
    public MainMenuRenderer(Checkers.Menu menu, Blinker blinker) 
        : base(menu, text, blinker) {}
}
