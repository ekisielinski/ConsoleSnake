using Snake.Common.Helpers;
using Snake.Game.Core;
using System;

namespace Snake.Game
{
    public sealed class GraphNode<T> : IGraphNode
    {
        public delegate (IGraphNode node, ScreenTransitionActivator transitionActivator) NodeCreator (T parameter);

        readonly GameModuleActivator moduleActivator;

        NodeCreator nodeCreator = null;

        //====== ctors

        public GraphNode (GameModuleActivator moduleActivator)
        {
            this.moduleActivator = Verify.NotNull (moduleActivator, nameof (moduleActivator));
        }

        //====== public methods

        public void SetNext (NodeCreator nodeCreator)
        {
            Verify.NotNull (nodeCreator, nameof (nodeCreator));

            if (this.nodeCreator != null) throw new InvalidOperationException ("Next module is already set.");

            this.nodeCreator = nodeCreator;
        }

        //====== IModuleNode

        public GameModule GetInstance () => moduleActivator.Invoke ();

        public (IGraphNode, ScreenTransitionActivator) GetNextNode (object parameter)
        {
            if (nodeCreator == null) return (null, null); // no more nodes

            if (parameter == null)
            {
                return nodeCreator (default (T));
            }

            if (parameter is T)
            {
                return nodeCreator ((T) (object) parameter);
            }

            var expectedTypeName = typeof (T).Name;
            var actualTypeName   = parameter.GetType ().Name;

            throw new InvalidOperationException ($"Invalid parameter type. Expected: {expectedTypeName}, actual: {actualTypeName}");
        }
    }
}
