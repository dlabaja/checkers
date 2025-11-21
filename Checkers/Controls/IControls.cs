namespace Checkers.Controls;

public interface IControls
{
    public Dictionary<ConsoleKey, Action> KeyActions { get; }
}
