namespace MyTelegramBot.AnswerHandlers
{
    public class MagneticsPercentAnswerHandler : BasePercentAnswerHandler
    {
        public override string Message => "درصد الکترومغناطیس عددی بین -33 تا 100 می باشد";

        public override bool Accept(ParticipatingInfo participant)
        {
            return participant.LastQuestion == Questions.MagneticPercent;
        }

        public override AnswerResponse Handle(ParticipatingInfo participant, AnswerRequest request)
        {
            var result = base.Handle(participant, request);
            if (!result.HasError)
            {
                participant.MagneticPercent = decimal.Parse(request.Message);
                return result;
            }
            return result;
        }
    }
}