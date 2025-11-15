using Checkers;
using Checkers.Controls;
using Checkers.Renderer;

var game = new Game();
var renderer = new Renderer();
var controls = new Controls();

renderer.Start();
controls.Start();

renderer.CurrentRenderer = new GameRenderer(game);
controls.CurrentControls = new GameControls(game);

while (true)
{
    
}