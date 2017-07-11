using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Components
{
    public static class Dispatcher
    {
        public static SynchronizationContext Context { get; private set; }

        public static void Init()
        {
            Context = SynchronizationContext.Current;
            DataObject.Context = SynchronizationContext.Current;
        }

        public static void Invoke(Action action)
        {
            if (Context != SynchronizationContext.Current)
            {
                Context.Post(d => action(), null);
            }
            else
            {
                action();
            }
        }

        public static Task InvokeAsync(Action action)
        {
            var completion = new TaskCompletionSource<object>();
            if (Context != SynchronizationContext.Current)
            {
                Context.Post(d => { action(); completion.SetResult(null); }, null);
            }
            else
            {
                action();
                completion.SetResult(null);
            }
            return completion.Task;
        }


        public static Task<T> InvokeAsync<T>(Func<T> action)
        {
            var completion = new TaskCompletionSource<T>();
            if (Context != SynchronizationContext.Current)
            {
                Context.Post(d =>completion.SetResult(action()), null);
            }
            else
            {
                completion.SetResult(action());
            }
            return completion.Task;
        }

        public static Task InvokeAsync(Func<Task> task)
        {
            if (Context != SynchronizationContext.Current)
            {
                var completion = new TaskCompletionSource<object>();
                Context.Post(async d => { await task().ConfigureAwait(false); completion.SetResult(null); }, null);
                return completion.Task;
            }
            else
            {
                return task();
            }
        }

        public static Task<T> InvokeAsync<T>(Func<Task<T>> task)
        {
            if (Context != SynchronizationContext.Current)
            {
                var completion = new TaskCompletionSource<T>();
                Context.Post(async d => completion.SetResult(await task().ConfigureAwait(false)), null);
                return completion.Task;
            }
            else
            {
                return task();
            }
        }
    }
}
