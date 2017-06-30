namespace MyTelegramBot.AnswerHandlers
{
    public class MachinePercentAnswerHandler : BasePercentAnswerHandler
    {
        public override string Message
        {
            get { return "درصد ماشین عددی بین -33 تا 100 می باشد"; }
        }
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