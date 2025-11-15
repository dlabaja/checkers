namespace Checkers;

public interface IControls
{
    public Dictionary<ConsoleKey, Action> KeyActions { get; }
}
