namespace Checkers.Controls;

public class GameControls : IControls
{
    private readonly Game game;
    public Dictionary<ConsoleKey, Action> KeyActions { get; }

    public GameControls(Game game)
    {
        this.game = game;
        this.KeyActions = new Dictionary<ConsoleKey, Action>
        {
            {ConsoleKey.UpArrow, game.MoveCursorUp},
            {ConsoleKey.DownArrow, game.MoveCursorDown},
            {ConsoleKey.RightArrow, game.MoveCursorRight},
            {ConsoleKey.LeftArrow, game.MoveCursorLeft},
            {ConsoleKey.W, game.MoveCursorUp},
            {ConsoleKey.S, game.MoveCursorDown},
            {ConsoleKey.D, game.MoveCursorRight},
            {ConsoleKey.A, game.MoveCursorLeft},
            {ConsoleKey.P, () => Controller.RenderDraw(game)},
            {ConsoleKey.Enter, OnEnter}
        };
    }

    private void OnEnter()
    {
        if (this.game.Selected == null)
        {
            this.game.Select();
            return;
        }
        
        if (this.game.Selected == this.game.Cursor)
        {
            this.game.Deselect();
            return;
        }

        if (!this.game.Board.Pieces.ContainsKey(this.game.Cursor))
        {
            this.game.MakeTurn(out Piece? piece);
        }
    }
}
