using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetForms : ApiRequest
    {
        public List<string> Forms { get; set; }
        public bool Dependencies { get; set; }

        public GetForms()
        {
        }

        public GetForms(IEnumerable<string> forms, bool dependencies = true)
        {
            Forms = forms.ToList();
            Dependencies = dependencies;
        }

        public override string GetPath()
        {
            return $"forms/{string.Join(",",Forms)}?dependencies={Dependencies}";
        }

        public override string GetLogContent()
        {
            return new { Forms = Forms, Dependencies = Dependencies }.Serialize();
        }
    }

    public class GetFormsResponse : DataResponse
    {
        public List<Form> Forms { get; set; }
        public Dictionary<string, DateTimeOffset> Models { get; set; }
        public Dictionary<string, DateTimeOffset> Classes { get; set; }

        public GetFormsResponse()  
        {
        }

        public GetFormsResponse(IEnumerable<Form> forms)
        {
            Forms = forms.ToList();
        }

        public GetFormsResponse(IEnumerable<Form> forms, IEnumerable<KeyValuePair<string, DateTimeOffset>> models, IEnumerable<KeyValuePair<string, DateTimeOffset>> classes)
        {
            Forms = forms.ToList();
            Models = models.ToDictionary(i=>i.Key, i=>i.Value);
            Classes = models.ToDictionary(i => i.Key, i => i.Value);
        }
    }
}
