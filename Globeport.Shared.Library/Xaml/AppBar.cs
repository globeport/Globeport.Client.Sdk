using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml
{
    public class AppBar : ContentControl, IAppBar
    {
        public override string Type => nameof(AppBar);

        public AppBar()
        {
        }

        string closedDisplayMode;
        public string ClosedDisplayMode
        {
            get
            {
                return closedDisplayMode;
            }
            set
            {
                if (value!=null && value!=closedDisplayMode && typeof(AppBarClosedDisplayMode).GetConstants().ContainsKey(value))
                {
                    closedDisplayMode = value;
                    OnPropertyChanged(nameof(ClosedDisplayMode));
                }
            }
        }

        public override object Clone()
        {
            var element = new AppBar();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (AppBar)element;
            ClosedDisplayMode = source.ClosedDisplayMode;
            base.CopyFrom(source);
        }
    }
}
