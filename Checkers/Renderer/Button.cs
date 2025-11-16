using static Checkers.Renderer.RenderUtils;

namespace Checkers.Renderer;

public class Button
{
    private string text;
    private bool highlighted;
    private Action action;

    public Button(string text, Action action)
    {
        this.text = text;
        this.action = action;
    }

    public bool Highlighted
    {
        get { return highlighted; }
        set { highlighted = value; }
    }

    public string ToString(bool highlighted)
    {
        return highlighted ? $"{Bg(Color.White)}{Fg(Color.Black)}[{text}]{ColorReset()}" : $"[{text}]";
    }
    
    public string Text
    {
        get { return text; }
    }
    public Action Action
    {
        get { return action; }
    }
}
