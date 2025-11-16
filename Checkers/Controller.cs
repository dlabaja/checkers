using Checkers.Controls;
using Checkers.Renderer;

namespace Checkers;

public class Controller
{
    private Renderer.Renderer renderer;
    private Controls.Controls controls;
    
    public Controller()
    {
        this.renderer = new Renderer.Renderer();
        this.controls = new Controls.Controls();
    }

    public void Start()
    {
        this.renderer.Start();
        this.controls.Start();
        this.renderer.CurrentRenderer = new MenuRenderer();
        this.controls.CurrentControls = new MenuControls();
    }
}