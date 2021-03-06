﻿namespace MyTelegramBot.AnswerHandlers
{
    public partial class EnglishPercentAnswerHandler : BasePercentAnswerHandler
    {
        public override string Message
        {
            get { return "درصد زبان عددی بین -33 تا 100 می باشد"; }
        }
        public override bool Accept(ParticipatingInfo participant)
        {
            return participant.LastQuestion == Questions.EnglishPercent;
        }

        public override AnswerResponse Handle(ParticipatingInfo participant, AnswerRequest request)
        {
            var result = base.Handle(participant, request);
            if (!result.HasError)
            {
                participant.EnglishPercent = decimal.Parse(request.Message);
                return result;
            }
            return result;
        }
    }
}