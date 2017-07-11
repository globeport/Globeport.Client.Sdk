using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public static class ResourceTypes
    {
        public const string Table = nameof(Table);
        public const string Script = nameof(Script);
        public const string Xaml = nameof(Xaml);
        public const string Jaml = nameof(Jaml);
        public const string Schema = nameof(Schema);

        public static string GetFileExtension(string type)
        {
            switch (type)
            {
                case Table:
                case Schema:
                case Jaml:
                    return "json";
                case Script:
                    return "js";
                case Xaml:
                    return "xaml";
            }
            throw new ArgumentException();
        }

        public static string GetFilename(string fileId, string type)
        {
            switch (type)
            {
                case MediaTypes.Image:
                case MediaTypes.Ink:
                    return fileId;
                default:
                    return string.Join(".", fileId, ResourceTypes.GetFileExtension(type), "gz");
            }
        }

        public static string GetColor(string type)
        {
            switch (type)
            {
                case Script:
                    return "Amber";
                case Table:
                    return "Blue";
                case Xaml:
                    return "Red";
            }
            return null;
        }

        public static string GetImage(string type)
        {
            switch (type)
            {
                case Script:
                    return SystemImages.Script;
                case Table:
                    return SystemImages.Table;
                case Xaml:
                    return SystemImages.Xaml;
            }
            return null;
        }

        public static bool IsHidden(string type, string name)
        {
            var isHiddenScript = type == Script && SystemScripts.All.ContainsKey(name);
            var isHiddenTable = type == Table && name == SystemTables.Strings;
            var isHiddenXaml = type == Xaml && SystemXaml.All.ContainsKey(name);
            return type.In(Jaml, Schema) || isHiddenScript || isHiddenTable || isHiddenXaml;
        }
    }
}
