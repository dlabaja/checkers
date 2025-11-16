using Checkers.Controls;
using Checkers.Renderer;

namespace Checkers;

public static class Controller
{
    private static Renderer.Renderer renderer = new Renderer.Renderer();
    private static Controls.Controls controls = new Controls.Controls();
    private static Blinker blinker = new Blinker();

    public static void Start()
    {
        var menu = new Menu([new Button("Play"), new Button("Rules"), new Button("Exit")]);
        renderer.CurrentRenderer = new MenuRenderer(menu, "", blinker);
        controls.CurrentControls = new MenuControls(menu);
        blinker.Start();
        renderer.Start();
        controls.Start();
    }

    public static void RenderMainMenu()
    {
        
    }
}