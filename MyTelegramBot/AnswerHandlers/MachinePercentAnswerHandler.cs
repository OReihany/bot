namespace MyTelegramBot.AnswerHandlers
{
    public class MachinePercentAnswerHandler : BasePercentAnswerHandler
    {
        public override bool Accept(ParticipatingInfo participant)
        {
            return participant.LastQuestion == Questions.MachinePercent;
        }

        public override AnswerResponse Handle(ParticipatingInfo participant, AnswerRequest request)
        {
            var result = base.Handle(participant, request);
            if (!result.HasError)
            {
                participant.MachinePercent = decimal.Parse(request.Message);
                return result;
            }
            return result;
        }
    }
}