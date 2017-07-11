using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Globeport.Shared.Library.Components
{
    public abstract class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Observable()
        {
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            Dispatcher.Invoke(()=>PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
        }
    }
}
