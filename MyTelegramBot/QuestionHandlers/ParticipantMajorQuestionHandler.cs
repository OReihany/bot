using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using MyTelegramBot.Interfaces;

namespace MyTelegramBot.QuestionHandlers
{
    public class ParticipantMajorQuestionHandler : IQuestionHandler
    {
        public bool Accept(ParticipatingInfo participant)
        {
            return !string.IsNullOrWhiteSpace(participant.Name) && participant.Major==null;
        }
        
        public QuestionRequest Handle(ParticipatingInfo participant)
        {
            participant.LastQuestion= Questions.Major;
            return new QuestionRequest()
            {
                ResponceType = ResponceType.Choose,
                SelectionList = Configure.MajorTypeString.Select(it=>it.Value).ToList(),
                Message = "گرایش:"
            };
        }
    }
}