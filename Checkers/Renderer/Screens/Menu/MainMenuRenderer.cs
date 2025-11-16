namespace Checkers.Renderer.Screens.Menu;

public class MainMenuRenderer : MenuRenderer
{
    public static List<Button> Buttons
    {
        get =>
        [
            new Button("Play", Controller.RenderGame),
            new Button("Rules", Controller.RenderRules),
            new Button("Controls", Controller.RenderControls),
            new Button("Exit", Controller.Exit)
        ];
    }
    private const string text = """
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
