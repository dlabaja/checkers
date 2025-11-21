using Checkers.Renderer;

namespace Checkers;

public class Menu
{
    private int head = 0;
    public List<Button> Buttons { get; }
    public bool ChangedHighlightedButton { get; set; }

    public Menu(List<Button> buttons)
    {
        this.Buttons = buttons;
        if (this.Buttons.Count > 0)
        {
            this.Buttons[0].Highlighted = true;
        }
    }

    public void HighlightPrevButton()
    {
        if (this.Buttons.Count == 0)
        {
            return;
        }
        
        this.Buttons[head].Highlighted = false;
        this.head--;
        if (head < 0)
        {
            this.head = this.Buttons.Count - 1;
        }
        this.Buttons[head].Highlighted = true;
        this.ChangedHighlightedButton = true;
    }

    public void HighlightNextButton()
    {
        if (this.Buttons.Count == 0)
        {
            return;
        }
        
        this.Buttons[head].Highlighted = false;
        this.head++;
        if (this.head >= this.Buttons.Count)
        {
            this.head = 0;
        }
        this.Buttons[head].Highlighted = true;
        this.ChangedHighlightedButton = true;
    }

    public void SelectButton()
    {
        this.Buttons[head].Action();
    }
}
