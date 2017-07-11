using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.Data
{
    public class QuestionUpload
    { 
        public string ModelId { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }

        public QuestionUpload()
        {
        }

        public QuestionUpload(string modelId, string label, string description, bool isRequired)
        {
            ModelId = modelId;
            Label = label;
            Description = description;
            IsRequired = isRequired;
        }
    }
}
