namespace Checkers.Controls;

public class Controls
{
    private Thread thread;
    private IControls? currentControls;
    private readonly CancellationTokenSource cts = new CancellationTokenSource();

    public void Start()
    {
        this.thread = new Thread(() => this.HandleControls(this.cts.Token));
        this.thread.Start();
    }

    public void Stop()
    {
        this.cts.Cancel();
    }

    private void HandleControls(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var key = Console.ReadKey().Key;
            if (currentControls != null && currentControls.KeyActions.TryGetValue(key, out Action? value))
            {
                value();
            }
        }
    }
    
    public IControls? CurrentControls
    {
        get { return currentControls; }
        set { currentControls = value; }
    }
}
