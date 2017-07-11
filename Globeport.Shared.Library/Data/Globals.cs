using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;

namespace Globeport.Shared.Library.Data
{
    public static class Globals
    {
        public const string JsonSchemaLicence = "3318-I34W8hExlKEtm1QeLhmhvhb0X9b1uDi6i5w7705RckCMiKuSIGhu5uLprnFSEt5FOoV/nsR0IzqEtGyJwxwNfPCD0erRpjBLaUfe2wb+9TGgaTS7AJQdwHENBAT2o5XCxnZG2eAC18zDiU1Lv4Eayvq74lgx8S1RAzoAIERt7RV7IklkIjozMzE4LCJFeHBpcnlEYXRlIjoiMjAxNy0xMi0wOFQxMTo1MDowMi42MjgxMTE5WiIsIlR5cGUiOiJKc29uU2NoZW1hSW5kaWUifQ==";

        public const string CurrentVersion = "1.0.342.0";

        public const string MinimumClientVersion = "1.0.340.0";

        public const string MaximumClientVersion = CurrentVersion;

        public readonly static string Culture = CultureInfo.CurrentCulture.ToString();

        public readonly static string CountryCode = new RegionInfo(CultureInfo.CurrentCulture.Name).TwoLetterISORegionName;

        public readonly static DateTimeOffset Epoch = new DateTimeOffset(2016, 6, 1, 0, 0, 0, TimeSpan.Zero);

        public const int MessageBatchSize = 1000;

        public const int PreKeyBatchSize = 100;

        public const int MinPageSize = 1;

        public const int MaxPageSize = 100;

        public const int DefaultPageSize = 20;

        public const string SrpPrime = "AC6BDB41324A9A9BF166DE5E1389582FAF72B6651987EE07FC3192943DB56050A37329CBB4A099ED8193E0757767A13DD52312AB4B03310DCD7F48A9DA04FD50E8083969EDB767B0CF6095179A163AB3661A05FBD5FAAAE82918A9962F0B93B855F97993EC975EEAA80D740ADBF4FF747359D041D5C33EA71D281E446B14773BCA97B43A23FB801676BD207A436C6481F1D2B9078717461A5B9D32E688F87748544523B524B0D57D5EA77A2775D2ECFA032CFBDBF52FB3786160279004E57AE6AF874E7303CE53299CCC041C7BC308D82A5698F3A8D0C38271AE35F8E9DBFBB694B5C803D89F7AE435DE236D525F54759B65E372FCD68EF20FA7111F9E4AFF73";

        public const int SrpGenerator = 2;

        public const int MaxGetCount = 20;

        public const int MaxDeleteCount = 20;

        public const string DomainName = "globeport.io";

        public const int MaxContacts = 100;

        public const int MaxLists = 10;

        public const int MaxGroups = 10;

        public const int MaxSessions = 10;

        public const int MaxModelEntitiesCount = 10;

        public const int MaxMediaCount = 100;

        public const int MaxQuestionsCount = 10;
    }
}
