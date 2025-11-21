using Checkers.Renderer.Interfaces;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Checkers.Renderer;

public class Renderer
{
    private readonly Timer renderTimer;
    public IRenderer? CurrentRenderer { get; set; }

    public Renderer()
    {
        renderTimer = new Timer(200);
        renderTimer.Elapsed += OnRenderTimerElapsed;
        renderTimer.AutoReset = true;
    }

    private void OnRenderTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        Console.Clear();
        this.CurrentRenderer?.Render();
    }
    
    public void Start()
    {
        renderTimer.Enabled = true;
    }

    public void Stop()
    {
        this.renderTimer.Stop();
        this.renderTimer.Dispose();
    }
}