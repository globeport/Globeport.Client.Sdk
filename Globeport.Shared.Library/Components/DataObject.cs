using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;

using Newtonsoft.Json;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Components
{
    public class DataObject : INotifyPropertyChanged
    {
        public static SynchronizationContext Context;

        public event PropertyChangedEventHandler PropertyChanged;

        [JsonExtensionData]
        public Dictionary<string, object> Data { get; } = new Dictionary<string, object>();

        public DataObject()
        {
        }

        public DataObject(IDictionary<string, object> data)
        {
            Data = new Dictionary<string, object>(data);
        }

        public DataObject RemoveNulls()
        {
            foreach (var item in Data.Where(i => i.Value == null || i.Value is DataObject).ToList())
            {
                if (item.Value is DataObject)
                {
                    ((DataObject)item.Value).RemoveNulls();
                }
                else
                {
                    Data.Remove(item.Key);
                }
            }
            return this;
        }

        public object this[string key]
        {
            get
            {
                return Data.ContainsKey(key) ? Data[key] : null;
            }
            set
            {
                if (!Data.ContainsKey(key) || Data[key]!=value)
                {
                    Data[key] = value;
                    OnPropertyChanged($"Item[{key}]");
                }
            }
        }

        void Invoke(Action action)
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

        public void SetPropertyByPath(string name, object value)
        {
            var obj = (object)this;
            var paths = name.Split('.');
            foreach (var path in paths)
            {
                if (obj is DataObject)
                {
                    if (path == paths.Last())
                    {
                        ((DataObject)obj)[path] = value;
                    }
                    else
                    {
                        obj = ((DataObject)obj)[path];
                    }
                }
                else if (obj is object[])
                {
                    int index;
                    if (!int.TryParse(path, out index)) return;
                    if (path == paths.Last())
                    {
                        try
                        {
                            (obj as object[])[index] = value;
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        obj = (obj as object[])[index];
                    }
                }
            }
        }

        public object GetPropertyByPath(string name)
        {
            var obj = (object)this;
            var paths = name.Split('.');
            object value = null;
            foreach (var path in paths)
            {
                if (obj is DataObject)
                {
                    if (path == paths.Last())
                    {
                        value = ((DataObject)obj)[path];
                    }
                    else
                    {
                        obj = ((DataObject)obj)[path];
                    }
                }
                else if (obj is object[])
                {
                    int index;
                    if (!int.TryParse(path, out index)) return null;
                    if (path == paths.Last())
                    {
                        try
                        {
                            value = (obj as object[])[index];
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        obj = (obj as object[])[index];
                    }
                }
            }
            return value;
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            Invoke(()=>PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
        }
    }
}
