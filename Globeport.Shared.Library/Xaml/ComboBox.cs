using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{ 
    public class ComboBox : Selector, IComboBox
    {
        public override string Type => nameof(ComboBox);

        public ComboBox()
        {
        }

        public override object Clone()
        {
            var element = new ComboBox();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (ComboBox)element;
            DisplayMemberPath = source.DisplayMemberPath;
            base.CopyFrom(source);
        }

        string displayMemberPath;
        public string DisplayMemberPath
        {
            get
            {
                return displayMemberPath;
            }
            set
            {
                if (value != displayMemberPath)
                {
                    displayMemberPath = value;
                    ItemTemplate = new DataTemplate { Template = new TextBlock { Padding = "0", VerticalAlignment = "Center", Bindings = new Bindings { { "Text", new Binding(value) { PropertyName = "Text" } } } } };
                    OnPropertyChanged(nameof(DisplayMemberPath));
                }
            }
        }
    }
}
