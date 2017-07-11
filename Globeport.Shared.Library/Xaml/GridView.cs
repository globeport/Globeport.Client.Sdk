using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class GridView : ListView, IGridView
    {
        public override string Type => nameof(GridView);

        public override object Clone()
        {
            var element = new GridView();
            element.CopyFrom(this);
            return element;
        }
    }
}
