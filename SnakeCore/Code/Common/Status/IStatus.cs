using System.Collections.Generic;

namespace Snake.Core
{
    public interface IStatus
    {
        IReadOnlyList<StatusItem> GetStatus ();
    }
}
