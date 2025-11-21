using Checkers.Renderer.Utils;
using static Checkers.Renderer.Utils.RenderUtils;

namespace Checkers.Renderer;

public class Button
{
    public string Text { get; }
    public Action Action { get; }
    public bool Highlighted { get; set; }

    public Button(string text, Action action)
    {
        this.Text = text;
        this.Action = action;
    }

    public string ToString(bool highlighted)
    {
        var color = highlighted ? $"{Bg(Color.White)}{Fg(Color.Black)}" : $"{Bg(Color.Dark_Gray)}{Fg(Color.Black)}";
        return $"{color}[{Text}]{ColorReset()}";
    }
}
