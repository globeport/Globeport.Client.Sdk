using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ApiModel
{
    public class DataChanges<T> where T : ClientObject
    {
        public List<T> Inserts { get; set; }
        public List<T> Updates { get; set; }
        public List<T> Deletes { get; set; }
        public List<T> LocalItems { get; set; }
        public List<T> RemoteItems { get; set; }
        public CursorModes Mode { get; set; }
        public CursorDirection Direction { get; set; }

        public DataChanges()
        {
            Inserts = new List<T>();
            Updates = new List<T>();
            Deletes = new List<T>();
        }

        public DataChanges(IEnumerable<T> inserts, IEnumerable<T> updates = null, IEnumerable<T> deletes = null)
        {
            Inserts = inserts?.ToList() ?? new List<T>();
            Updates = updates?.ToList() ?? new List<T>();
            Deletes = deletes?.ToList() ?? new List<T>();
        }

        public bool IsEmpty()
        {
            return Updates.IsNullOrEmpty() && Inserts.IsNullOrEmpty() && Deletes.IsNullOrEmpty();
        }

        IEnumerable<T> upserts;
        public IEnumerable<T> Upserts
        {
            get
            {
                if (upserts == null)
                {
                    if (Updates == null) upserts = Inserts;
                    else if (Inserts == null) upserts = Updates;
                    else upserts = Inserts.Concat(Updates);
                }
                return upserts;
            }
        }
    }
}
