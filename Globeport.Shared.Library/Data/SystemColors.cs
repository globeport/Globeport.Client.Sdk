using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class SystemColors
    {
        Random random = new Random();
        public static SystemColors Instance { get; } = new SystemColors();
        public Dictionary<string, string> AllColors { get; private set; }
        public List<string> PrimaryColors { get; private set; }
        public static Dictionary<string, string> PortalColors { get; } = new Dictionary<string, string>
        {
            { "Profile", "" },
            { "Public", "Blue" },
            { "Contacts", "Teal" },
            { "Friends", "Amber" },
            { "Family", "Purple" },
            { "Work", "BlueGray" }
        };

        public string Red { get; } = "#FFF44336";
        public string Pink { get; } = "#FFE91E63";
        public string Purple { get; } = "#FF9C27B0";
        public string DeepPurple { get; } = "#FF673AB7";
        public string Indigo { get; } = "#FF6A00FF";
        public string Blue { get; } = "#FF2196F3";
        public string LightBlue { get; } = "#FF03A9F4";
        public string Cyan { get; } = "#FF00BCD4";
        public string Teal { get; } = "#FF009688";
        public string Green { get; } = "#FF4CAF50";
        public string LightGreen { get; } = "#FF8BC34A";
        public string Lime { get; } = "#FFCDDC39";
        public string Yellow { get; } = "#FFFFEB3B";
        public string Amber { get; } = "#FFFFC107";
        public string Orange { get; } = "#FFFF9800";
        public string DeepOrange { get; } = "#FFFF5722";
        public string Brown { get; } = "#FF795548";
        public string Gray { get; } = "#FF9E9E9E";
        public string BlueGray { get; } = "#FF607D8B";
        public string Black { get; } = "#FF000000";
        public string White { get; } = "#FFFFFFFF";
        public string DarkGray { get; } = "#FF282D32";
        public string LightGray { get; } = "#FFE6E7E8";

        public SystemColors()
        {
            PrimaryColors = new List<string> { Red, Pink, Purple, DeepPurple, Indigo, Blue, LightBlue, Cyan, Teal, Green, LightGreen, Lime, Yellow, Amber, Orange, DeepOrange, Brown, Gray, BlueGray };
            AllColors = this.GetProperties<string>().ToDictionary(i => i.Name, i => (string)i.GetValue(this));
        }

        public string GetRandomPrimaryColor()
        {
            return PrimaryColors[random.Next(PrimaryColors.Count)];
        }
    }
}
