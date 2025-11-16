namespace Checkers.Renderer;

public class MenuRenderer: IRenderer
{
    private Menu menu;
    private readonly string logo = """
                           ██████╗██╗  ██╗███████╗ ██████╗██╗  ██╗███████╗██████╗ ███████╗
                          ██╔════╝██║  ██║██╔════╝██╔════╝██║ ██╔╝██╔════╝██╔══██╗██╔════╝
                          ██║     ███████║█████╗  ██║     █████╔╝ █████╗  ██████╔╝███████╗
                          ██║     ██╔══██║██╔══╝  ██║     ██╔═██╗ ██╔══╝  ██╔══██╗╚════██║
                          ╚██████╗██║  ██║███████╗╚██████╗██║  ██╗███████╗██║  ██║███████║
                           ╚═════╝╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚══════╝
                          """;

    public MenuRenderer()
    {
        this.menu = new Menu([new Button("Play"), new Button("Rules"), new Button("Exit")]);
    }
    
    public void Start() {}

    public void Render()
    {
        Console.WriteLine(logo);
        Console.WriteLine("--------------Made by Jan Dlabaja as a C# homework--------------");
        Console.WriteLine(this.menu.Buttons[0]);
        Console.WriteLine(this.menu.Buttons[1]);
    }

    public void Dispose() {}
}
