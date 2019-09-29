namespace Snake.Game
{
    public abstract class Trigger
    {
        protected bool isHandled;

        //====== ctors

        protected Trigger (bool isHandled = false)
        {
            this.isHandled = isHandled;
        }

        //====== abstract/virtual

        protected abstract bool Condition { get; }

        protected virtual void AfterHandle () { }

        //====== public methods

        public bool TryHandle ()
        {
            if (isHandled == false && Condition)
            {
                isHandled = true;

                AfterHandle ();

                return true;
            }

            return false;
        }

        //====== override: Object

        public override string ToString () => $"{GetType ().Name}, handled = {isHandled}";
    }
}
