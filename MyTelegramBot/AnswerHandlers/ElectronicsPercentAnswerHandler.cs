namespace MyTelegramBot.AnswerHandlers
{
    public class ElectronicsPercentAnswerHandler : BasePercentAnswerHandler
    {
        public override string Message
        {
            get { return "درصد الکترونیک عددی بین -33 تا 100 می باشد"; }
        }
        public override bool Accept(ParticipatingInfo participant)
        {
            return participant.LastQuestion == Questions.ElectronicsPercent;
        }

        public override AnswerResponse Handle(ParticipatingInfo participant, AnswerRequest request)
        {
            var result = base.Handle(participant, request);
            if (!result.HasError)
            {
                participant.ElectronicsPercent = decimal.Parse(request.Message);
                return result;
            }
            return result;
        }
    }
}