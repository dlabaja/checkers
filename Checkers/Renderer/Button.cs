using static Checkers.Renderer.RenderUtils;

namespace Checkers.Renderer;

public class Button
{
    private string text;
    private bool highlighted;

    public Button(string text)
    {
        this.text = text;
    }

    public bool Highlighted
    {
        get { return highlighted; }
        set { highlighted = value; }
    }

    public override string ToString()
    {
        return this.highlighted ? $"{Bg(Color.White)}{Fg(Color.Black)}{text}{ColorReset()}" : text;
    }
}
