using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Globeport.Shared.Library.Components
{
    public class ObjectPool<T>
    {
        ConcurrentBag<T> objects;
        Func<Task<T>> objectGenerator;
        SemaphoreSlim poolLock;

        public ObjectPool(int maxSize, Func<Task<T>> objectGenerator)
        {
            if (objectGenerator == null) throw new ArgumentNullException("objectGenerator");
            objects = new ConcurrentBag<T>();
            this.objectGenerator = objectGenerator;
            poolLock = new SemaphoreSlim(maxSize);
        }

        public async Task<T> GetObject()
        {
            await poolLock.WaitAsync().ConfigureAwait(false);
            T item;
            if (objects.TryTake(out item)) return item;
            return await objectGenerator().ConfigureAwait(false);
        }

        public void PutObject(T item)
        {
            objects.Add(item);
            poolLock.Release();
        }
    }
}
