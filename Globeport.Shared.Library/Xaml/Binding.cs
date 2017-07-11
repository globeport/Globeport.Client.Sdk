using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class Binding : IBinding
    {
        static Regex PropertyPathRegex = new Regex("^(([a-zA-Z]+[a-zA-Z0-9]*){1}(\\[[a-zA-Z0-9]+\\]){0,1}|(\\[[a-zA-Z0-9]+\\]){1})$");
        public Binding()
        {
        }

        public Binding(string path)
        {
            Path = path;
        }

        string ParsePath(string path)
        {
            if (path.Contains(".[")) return null;

            if (path.Split('.').Any(i => !PropertyPathRegex.IsMatch(i))) return null;

            return path;
        }


        string path;
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = ParsePath(value);
            }
        }

        string mode = "OneTime";
        public string Mode
        {
            get
            {
                return mode;
            }
            set
            {
                if (value != null && value!=mode && typeof(BindingModes).GetConstants().ContainsKey(value))
                {
                    mode = value;
                }
            }
        }

        string converter;
        public string Converter
        {
            get
            {
                return converter;
            }
            set
            {
                converter = value;
            }
        }

        string converterParameter;
        public string ConverterParameter
        {
            get
            {
                return converterParameter;
            }
            set
            {
                converterParameter = value;
            }
        }

        string propertyName;
        public string PropertyName
        {
            get
            {
                return propertyName;
            }
            set
            {
                propertyName = value;
            }
        }

        string targetNullValue;
        public string TargetNullValue
        {
            get
            {
                return targetNullValue;
            }
            set
            {
                targetNullValue = value;
            }
        }
    }
}
