namespace Checkers.Controls;

public class Controls
{
    private Thread thread;
    private readonly CancellationTokenSource cts = new CancellationTokenSource();
    public IControls? CurrentControls { get; set; }

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
            if (CurrentControls != null && CurrentControls.KeyActions.TryGetValue(key, out Action? value))
            {
                value();
            }
        }
    }
}
