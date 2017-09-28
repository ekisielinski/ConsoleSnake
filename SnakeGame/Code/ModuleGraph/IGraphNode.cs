using Snake.Game.Core;

namespace Snake.Game
{
    public interface IGraphNode
    {
        GameModule GetInstance ();

        (IGraphNode, ScreenTransitionActivator) GetNextNode (object parameter);
    }
}
