using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.Data
{
    public class GroupInfo
    {
        //to do: remove this when cassandra supports it
        [IgnoreDataMember]
        public IEnumerable<string> AdministratorsEnumerable
        {
            get { return Administrators; }
            set { Administrators = value?.ToList(); }
        }
        public List<string> Administrators { get; set; }
        public DateTimeOffset? MemberFrom { get; set; }
        public DateTimeOffset? MemberTo { get; set; }

        public GroupInfo()
        {
        }

        public GroupInfo(params string[] administrators)
        {
            Administrators = administrators.ToList();
        }
    }
}
