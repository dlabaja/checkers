using System.Timers;
using Timer = System.Timers.Timer;

namespace Checkers.Renderer;

public class Renderer
{
    private readonly Timer renderTimer;
    private readonly Timer blinkTimer;
    private readonly IRenderer currentRenderer;

    public Renderer()
    {
        renderTimer = new Timer(200);
        renderTimer.Elapsed += OnRenderTimerElapsed;
        renderTimer.AutoReset = true;
        blinkTimer = new Timer(700);
        blinkTimer.Elapsed += OnBlinkTimerElapsed;
        blinkTimer.AutoReset = true;
    }

    private void OnBlinkTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        cursorIsBlinked = !cursorIsBlinked;
    }

    private void OnRenderTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        Console.Clear();
        if (this.game != null)
        {
            this.DisplayBoard(this.game.Board.GetBoard());
        }
    }
    
    public void Start()
    {
        renderTimer.Enabled = true;
        blinkTimer.Enabled = true;
    }

    public void Stop()
    {
        this.renderTimer.Stop();
        this.renderTimer.Dispose();
        this.blinkTimer.Stop();
        this.blinkTimer.Stop();
    }
    
    public Game Game
    {
        set { game = value; }
    }
}