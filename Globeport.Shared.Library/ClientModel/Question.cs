using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ClientModel
{
    public class Question : ClientObject
    {
        public string ModelId { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
        public string AnswerId { get; set; }

        public Question()
        {
        }

        public Question(string modelId, string label, string description, bool isRequired)
        {
            ModelId = modelId;
            Label = label;
            Description = description;
            IsRequired = isRequired;
        }

        public Question(string id, string modelId, string label, string description, bool isRequired) 
            : this(modelId, label, description, isRequired)
        {
            Id = id;
        }

        public QuestionUpload GetUpload()
        {
            return new QuestionUpload(ModelId, Label, Description, IsRequired);
        }
    }
}
