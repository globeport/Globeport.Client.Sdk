using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ClientModel
{
    public class Answer : ClientObject
    {
        public string QuestionId { get; set; }
        public Entity Entity { get; set; }

        public Answer()
        {
        }

        public Answer(string questionId, Entity entity)
        {
            QuestionId = questionId;
            Entity = entity;
        }

        public AnswerUpload GetUpload()
        {
            return new AnswerUpload(QuestionId, Entity.Id);
        }
    }
}
