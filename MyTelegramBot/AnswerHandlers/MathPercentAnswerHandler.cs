﻿namespace MyTelegramBot.AnswerHandlers
{
    public class MathPercentAnswerHandler : BasePercentAnswerHandler
    {
        public override string Message
        {
            get { return "درصد ریاضی عددی بین -33 تا 100 می باشد"; }
        }
        public override bool Accept(ParticipatingInfo participant)
        {
            return participant.LastQuestion == Questions.MathPercent;
        }

        public override AnswerResponse Handle(ParticipatingInfo participant, AnswerRequest request)
        {
            var result = base.Handle(participant, request);
            if (!result.HasError)
            {
                participant.MathPercent = decimal.Parse(request.Message);
                return result;
            }
            return result;
        }
    }
}