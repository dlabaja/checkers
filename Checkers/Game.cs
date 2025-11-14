namespace Checkers;

public class Game
{
    public void Play()
    {
        var board = new Board();
        var renderer = new Renderer();
        
        renderer.DisplayBoard(board.GetBoard());
    }
}