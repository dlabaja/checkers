using Checkers.Controls;
using Checkers.Renderer;
using Checkers.Renderer.Menu;

namespace Checkers;

public static class Controller
{
    private static Renderer.Renderer renderer = new Renderer.Renderer();
    private static Controls.Controls controls = new Controls.Controls();
    private static Blinker blinker = new Blinker();

    public static void Start()
    {
        blinker.Start();
        renderer.Start();
        controls.Start();
        RenderMainMenu();
    }

    public static void RenderMainMenu()
    {
        var menu = new Menu(MainMenuRenderer.buttons);
        renderer.CurrentRenderer = new MainMenuRenderer(menu, blinker);
        controls.CurrentControls = new MenuControls(menu);
    }

    public static void RenderRules()
    {
        var menu = new Menu(RulesRenderer.buttons);
        renderer.CurrentRenderer = new RulesRenderer(menu, blinker);
        controls.CurrentControls = new MenuControls(menu);
    }

    public static void RenderGame()
    {
        var game = new Game();
        renderer.CurrentRenderer = new GameRenderer(game, blinker);
        controls.CurrentControls = new GameControls(game);
    }

    public static void Exit()
    {
        Environment.Exit(0);
    }
}