namespace Checkers.Renderer.Utils;

public static class RenderUtils
{
    public static string Fg(Color color)
    {
        return $"\e[38;5;{(int)color}m";
    }
    
    public static string Bg(Color color)
    {
        return $"\e[48;5;{(int)color}m";
    }

    public static string ColorReset()
    {
        return "\e[0m";
    }
}
