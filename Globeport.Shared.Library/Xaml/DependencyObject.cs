using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

using Globeport.Shared.Library.Components;
using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Xaml
{
    public class DependencyObject : Observable, IDependencyObject
    {
        public event EventHandler<MethodEventArgs> MethodCalled;
        public string PropertyUpdated { get; set; }
        public Bindings Bindings { get; set; }
        public AttachedProperties AttachedProperties { get; set; }
        public IXamlHost Host { get; set; }
        public object Native { get; set; }
        public int Id { get; set; }
        public virtual string Type => nameof(DependencyObject);

        public DependencyObject()
        {
        }

        public virtual void Load(IXamlHost host)
        {
            Host = host;
        }

        public object CallFunction(string function, params object[] args)
        {
            if (Host == null) return null;
            args = args.Prepend(function).ToArray();
            return Host.CallFunction($"Hosts[{Host.Id}].Elements[{this.Id}].Execute", args);
        }

        public virtual List<DependencyObject> GetElements()
        {
            return new List<DependencyObject> { this };
        }

        public void SetValue(string propertyName, object value)
        {
            if (AttachedProperties == null) AttachedProperties = new AttachedProperties();
            AttachedProperties[propertyName] = value;
            OnPropertyChanged(propertyName);
        }

        public object GetValue(string propertyName)
        {
            return AttachedProperties?.GetValue(propertyName);
        }

        public virtual object Clone()
        {
            var element = new DependencyObject();
            element.CopyFrom(this);
            return element;
        }

        public virtual void CopyFrom(DependencyObject element)
        {
            Name = element.name;
            Tag = element.Tag;
            DataContext = element.DataContext;
            PropertyUpdated = element.PropertyUpdated;
            Bindings = element.Bindings;
            AttachedProperties = element.AttachedProperties;
        }

        protected virtual void OnMethodCalled(MethodEventArgs args)
        {
            MethodCalled?.Invoke(this, args);
        }

        string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        object dataContext;
        public object DataContext
        {
            get
            {
                return dataContext;
            }
            set
            {
                if (dataContext != value)
                {
                    dataContext = value;
                    OnPropertyChanged(nameof(DataContext));
                }
            }
        }


        object tag;
        public object Tag
        {
            get
            {
                return tag;
            }
            set
            {
                if (value != tag)
                {
                    tag = value;
                    OnPropertyChanged(nameof(Tag));
                }
            }
        }
    }
}
