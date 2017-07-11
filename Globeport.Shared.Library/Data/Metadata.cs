using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;
using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Components;

namespace Globeport.Shared.Library.Data
{
    public class Metadata : Observable, IMetadata
    {
        public Metadata()
        {
        }

        public Metadata Initialize()
        {
            Global = Global ?? new DataObject();
            Personal = Personal ?? new DataObject();
            return this;
        }

        public Metadata(string global, string personal)
        {
            Global = global.Deserialize<DataObject>() ?? new DataObject();
            Personal = personal.Deserialize<DataObject>() ?? new DataObject();
        }

        public static Metadata Create()
        {
            return new Metadata(null, null);
        }

        DataObject global;
        public DataObject Global
        {
            get
            {
                return global;
            }
            set
            {
                if (value!=global)
                {
                    global = value;
                    OnPropertyChanged(nameof(Global));
                }
            }
        }


        DataObject personal;
        public DataObject Personal
        {
            get
            {
                return personal;
            }
            set
            {
                if (value != personal)
                {
                    personal = value;
                    OnPropertyChanged(nameof(Personal));
                }
            }
        }
    }
}
