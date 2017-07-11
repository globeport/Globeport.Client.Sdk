using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Collections.Specialized;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class Selector : ItemsControl, ISelector
    {
        public override string Type => nameof(Selector);
        public string SelectionChanged { get; set; }

        public Selector()
        {
        }

        public override object Clone()
        {
            var element = new Selector();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (Selector)element;
            SelectionChanged = source.SelectionChanged;
            SelectedItem = source.SelectedItem;
            SelectedIndex = source.SelectedIndex;
            SelectedValue = source.SelectedValue;
            SelectedValuePath = source.SelectedValuePath;
            base.CopyFrom(source);
        }

        object selectedItem;
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if (selectedItem!=value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        long selectedIndex = -1;
        public long SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;
                    OnPropertyChanged(nameof(SelectedIndex));
                }
            }
        }

        object selectedValue;
        public object SelectedValue
        {
            get
            {
                return selectedValue;
            }
            set
            {
                if (selectedValue != value)
                {
                    selectedValue = value;
                    OnPropertyChanged(nameof(SelectedValue));
                }
            }
        }

        string selectedValuePath;
        public string SelectedValuePath
        {
            get
            {
                return selectedValuePath;
            }
            set
            {
                if (value != selectedValuePath)
                {
                    selectedValuePath = value;
                    OnPropertyChanged(nameof(SelectedValuePath));
                }
            }
        }
    }
}
