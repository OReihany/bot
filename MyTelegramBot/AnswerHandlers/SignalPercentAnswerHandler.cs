namespace MyTelegramBot.AnswerHandlers
{
    public class SignalPercentAnswerHandler : BasePercentAnswerHandler
    {
        public override bool Accept(ParticipatingInfo participant)
        {
            return participant.LastQuestion == Questions.SignalPercent;
        }

        public override AnswerResponse Handle(ParticipatingInfo participant, AnswerRequest request)
        {
            var result = base.Handle(participant, request);
            if (!result.HasError)
            {
                participant.SignalPercent = decimal.Parse(request.Message);
                return result;
            }
            return result;
        }
    }
}