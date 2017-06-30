namespace MyTelegramBot.QuestionHandlers
{
    public class RateAmountQuestionHandler : IQuestionHandler
    {
        public bool Accept(ParticipatingInfo participant)
        {
            return !string.IsNullOrWhiteSpace(participant.Name)
                && participant.Major.HasValue
                && participant.EnglishPercent.HasValue
                && participant.MathPercent.HasValue
                && participant.CircutePercent.HasValue
                && participant.ElectronicsPercent.HasValue
                && participant.SignalPercent.HasValue
                && participant.ControllPercent.HasValue
                && participant.MachinePercent.HasValue
                && participant.MagneticPercent.HasValue
                && participant.RateAmount == null;
        }

        public QuestionRequest Handle(ParticipatingInfo participant)
        {
            participant.LastQuestion = Questions.RateAmount;
            return new QuestionRequest()
            {
                Message = "معدل:"
            };
        }
    }
}