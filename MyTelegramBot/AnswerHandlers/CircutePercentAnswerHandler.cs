namespace MyTelegramBot.AnswerHandlers
{
    public class CircutePercentAnswerHandler : BasePercentAnswerHandler
    {
        public override bool Accept(ParticipatingInfo participant)
        {
            return participant.LastQuestion == Questions.CircutePercent;
        }

        public override AnswerResponse Handle(ParticipatingInfo participant, AnswerRequest request)
        {
            var result = base.Handle(participant, request);
            if (!result.HasError)
            {
                participant.CircutePercent = decimal.Parse(request.Message);
                return result;
            }
            return result;
        }
    }
}