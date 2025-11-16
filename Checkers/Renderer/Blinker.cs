using System.Timers;

namespace Checkers.Renderer;
using Timer = System.Timers.Timer;

public class Blinker
{
    private readonly Timer blinkTimer;
    private bool cursorIsBlinked;
    
    public Blinker()
    {
        blinkTimer = new Timer(600);
        blinkTimer.Elapsed += OnBlinkTimerElapsed;
        blinkTimer.AutoReset = true;
    }
    
    private void OnBlinkTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        cursorIsBlinked = !cursorIsBlinked;
    }
    
    public bool CursorIsBlinked
    {
        get { return cursorIsBlinked; }
    }
    
    public void Start()
    {
        this.blinkTimer.Enabled = true;
    }
    
    public void Stop()
    {
        this.blinkTimer.Stop();
    }
}
