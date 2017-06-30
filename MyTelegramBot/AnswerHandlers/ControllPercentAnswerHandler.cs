namespace MyTelegramBot.AnswerHandlers
{
    public class ControllPercentAnswerHandler : BasePercentAnswerHandler
    {
        public override bool Accept(ParticipatingInfo participant)
        {
            return participant.LastQuestion == Questions.ControllPercent;
        }

        public override AnswerResponse Handle(ParticipatingInfo participant, AnswerRequest request)
        {
            var result = base.Handle(participant, request);
            if (!result.HasError)
            {
                participant.ControllPercent = decimal.Parse(request.Message);
                return result;
            }
            return result;
        }
    }
}