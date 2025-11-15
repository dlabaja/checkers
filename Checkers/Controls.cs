namespace Checkers;

public class Controls
{
    private Thread thread;
    private Game game;
    private readonly CancellationTokenSource cts = new CancellationTokenSource();
    private Dictionary<ConsoleKey, Action> keyActions = new Dictionary<ConsoleKey, Action>
    {
        {ConsoleKey.RightArrow, () => }
    };

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
            if (Console.ReadKey().Key == ConsoleKey.RightArrow)
            {
                Console.Beep();
            }
        }
    }
}
