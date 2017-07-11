using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Components
{
    public static class Tasks
    {
        public readonly static Task Complete = Task.FromResult<object>(null);
    }

    public class TaskCompletionSource : TaskCompletionSource<object>
    {
        public void SetComplete()
        {
            SetResult(null);
        }
    }
}
