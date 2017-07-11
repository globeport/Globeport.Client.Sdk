using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using FluentValidation;
using MoreLinq;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Validation
{
    public class CompleteFormValidator: AbstractValidator<CompleteForm>
    {
        public const int MaxAnswersCount = 10;
        public const int MaxMessageCount = 10;
        AnswerUploadValidator AnswerValidator { get; } = new AnswerUploadValidator();
        SignalMessageUploadValidator MessageUploadValidator { get; } = new SignalMessageUploadValidator();

        public CompleteFormValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.FormId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.Answers).NotNull().Must(AreValidAnswers).SetCollectionValidator(AnswerValidator);
            RuleFor(i => i.Messages).NotNull().Must(AreValidMessages).SetCollectionValidator(MessageUploadValidator);
        }

        bool AreValidAnswers(List<AnswerUpload> answers)
        {
            if (answers.Count > MaxAnswersCount) return false;
            if (answers.DistinctBy(i => i.QuestionId).Count() != answers.Count) return false;
            return true;
        }

        bool AreValidMessages(CompleteForm request, List<SignalMessageUpload> messages)
        {
            //1 sender key for each entity and may need sender key distribution to maxsessions + 1
            if (messages.Count > request.Answers.DistinctBy(i => i.EntityId).Count() + Globals.MaxSessions + 1) return false;
            //should be 1 sender key message for each non-public entity
            if (messages.Count(i => i.MessageType == SignalMessageType.SenderKey && i.ContentType == SignalContentType.Entity) > request.Answers.DistinctBy(i => i.EntityId).Count()) return false;

            //check for invalid message types
            foreach (var message in messages)
            {
                if (message.MessageType == SignalMessageType.SenderKey && message.ContentType != SignalContentType.Entity) return false;
                if (message.MessageType == SignalMessageType.PreKey && message.ContentType != SignalContentType.SenderKey) return false;
                if (message.MessageType == SignalMessageType.System) return false;
            }
           
            return true;
        }
    }
}
