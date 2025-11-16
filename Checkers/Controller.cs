using Checkers.Controls;
using Checkers.Renderer;
using Checkers.Renderer.Screens;
using Checkers.Renderer.Screens.Menu;

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
        var menu = new Menu(MainMenuRenderer.Buttons);
        renderer.CurrentRenderer = new MainMenuRenderer(menu, blinker);
        controls.CurrentControls = new MenuControls(menu);
    }

    public static void RenderRules()
    {
        var menu = new Menu(RulesRenderer.Buttons);
        renderer.CurrentRenderer = new RulesRenderer(menu, blinker);
        controls.CurrentControls = new MenuControls(menu);
    }
    
    public static void RenderControls()
    {
        var menu = new Menu(ControlsRenderer.Buttons);
        renderer.CurrentRenderer = new ControlsRenderer(menu, blinker);
        controls.CurrentControls = new MenuControls(menu);
    }

    public static void RenderDraw(Game game)
    {
        var menu = new Menu(DrawRenderer.Buttons(game));
        renderer.CurrentRenderer = new DrawRenderer(menu, blinker);
        controls.CurrentControls = new MenuControls(menu);
    }

    public static void RenderGameOver(PieceColor? winner)
    {
        var menu = new Menu(GameOverRenderer.Buttons);
        renderer.CurrentRenderer = new GameOverRenderer(winner, menu, blinker);
        controls.CurrentControls = new MenuControls(menu);
    }

    public static void RenderGame(Game game)
    {
        game.OnGameOver += color => RenderGameOver(color); 
        renderer.CurrentRenderer = new GameRenderer(game, blinker);
        controls.CurrentControls = new GameControls(game);
    }

    public static void Exit()
    {
        Environment.Exit(0);
    }
}