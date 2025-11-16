using Checkers.Renderer;

namespace Checkers;

public class Menu
{
    private int head = 0;
    private List<Button> buttons;

    public Menu(List<Button> buttons)
    {
        this.buttons = buttons;
        if (this.buttons.Count > 0)
        {
            this.buttons[0].Highlighted = true;
        }
    }

    public void HighlightPrevButton()
    {
        if (this.buttons.Count == 0)
        {
            return;
        }
        
        this.buttons[head].Highlighted = false;
        this.head--;
        if (head < 0)
        {
            this.head = this.buttons.Count - 1;
        }
        this.buttons[head].Highlighted = true;
    }

    public void HighlightNextButton()
    {
        if (this.buttons.Count == 0)
        {
            return;
        }
        
        this.buttons[head].Highlighted = false;
        this.head++;
        if (this.head >= this.buttons.Count)
        {
            this.head = 0;
        }
        this.buttons[head].Highlighted = true;
    }
    
    public List<Button> Buttons
    {
        get { return buttons; }
    }
}
