using Checkers.Renderer.Utils;
using static Checkers.Renderer.Utils.RenderUtils;

namespace Checkers.Renderer.Screens.Menu;

public class GameOverRenderer: MenuRenderer
{
    public static List<Button> Buttons
    {
        get =>
        [
            new Button("Back to menu", Controller.RenderMainMenu),
        ];
    }
    private const string whiteWon = """
                                    $$\      $$\ $$\       $$\   $$\               
                                    $$ | $\  $$ |$$ |      \__|  $$ |              
                                    $$ |$$$\ $$ |$$$$$$$\  $$\ $$$$$$\    $$$$$$\  
                                    $$ $$ $$\$$ |$$  __$$\ $$ |\_$$  _|  $$  __$$\ 
                                    $$$$  _$$$$ |$$ |  $$ |$$ |  $$ |    $$$$$$$$ |
                                    $$$  / \$$$ |$$ |  $$ |$$ |  $$ |$$\ $$   ____|
                                    $$  /   \$$ |$$ |  $$ |$$ |  \$$$$  |\$$$$$$$\ 
                                    \__/     \__|\__|  \__|\__|   \____/  \_______|
                                                                                   
                                    $$\  $$\  $$\  $$$$$$\  $$$$$$$\               
                                    $$ | $$ | $$ |$$  __$$\ $$  __$$\              
                                    $$ | $$ | $$ |$$ /  $$ |$$ |  $$ |             
                                    $$ | $$ | $$ |$$ |  $$ |$$ |  $$ |             
                                    \$$$$$\$$$$  |\$$$$$$  |$$ |  $$ |             
                                     \_____\____/  \______/ \__|  \__|             
                                    """;
    private const string blackWon = """
                                    $$$$$$$\  $$\                     $$\       
                                    $$  __$$\ $$ |                    $$ |      
                                    $$ |  $$ |$$ | $$$$$$\   $$$$$$$\ $$ |  $$\ 
                                    $$$$$$$\ |$$ | \____$$\ $$  _____|$$ | $$  |
                                    $$  __$$\ $$ | $$$$$$$ |$$ /      $$$$$$  / 
                                    $$ |  $$ |$$ |$$  __$$ |$$ |      $$  _$$<  
                                    $$$$$$$  |$$ |\$$$$$$$ |\$$$$$$$\ $$ | \$$\ 
                                    \_______/ \__| \_______| \_______|\__|  \__|
                                     
                                    $$\  $$\  $$\  $$$$$$\  $$$$$$$\            
                                    $$ | $$ | $$ |$$  __$$\ $$  __$$\           
                                    $$ | $$ | $$ |$$ /  $$ |$$ |  $$ |          
                                    $$ | $$ | $$ |$$ |  $$ |$$ |  $$ |          
                                    \$$$$$\$$$$  |\$$$$$$  |$$ |  $$ |          
                                     \_____\____/  \______/ \__|  \__|          
                                    """;
    private const string draw = """
                                $$$$$$$\                                   
                                $$  __$$\                                  
                                $$ |  $$ | $$$$$$\  $$$$$$\  $$\  $$\  $$\ 
                                $$ |  $$ |$$  __$$\ \____$$\ $$ | $$ | $$ |
                                $$ |  $$ |$$ |  \__|$$$$$$$ |$$ | $$ | $$ |
                                $$ |  $$ |$$ |     $$  __$$ |$$ | $$ | $$ |
                                $$$$$$$  |$$ |     \$$$$$$$ |\$$$$$\$$$$  |
                                \_______/ \__|      \_______| \_____\____/ 
                                """;

    private static string GetText(PieceColor? winner)
    {
        if (!winner.HasValue)
        {
            return draw;
        }

        return winner.Value == PieceColor.White
            ? $"{Bg(Color.White)}{Fg(Color.Black)}{whiteWon}{ColorReset()}"
            : $"{Bg(Color.Black)}{Fg(Color.White)}{blackWon}{ColorReset()}";
    }
    
    public GameOverRenderer(PieceColor? winner, Checkers.Menu menu, Blinker blinker) 
        : base(menu, GetText(winner), blinker) {}
}
