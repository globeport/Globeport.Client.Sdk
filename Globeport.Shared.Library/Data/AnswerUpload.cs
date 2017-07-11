using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.Data
{
    public class AnswerUpload
    {
        public string QuestionId { get; set; }
        public string EntityId { get; set; }

        public AnswerUpload()
        {
        }

        public AnswerUpload(string questionId, string entityId)
        {
            QuestionId = questionId;
            EntityId = entityId;
        }
    }
}
