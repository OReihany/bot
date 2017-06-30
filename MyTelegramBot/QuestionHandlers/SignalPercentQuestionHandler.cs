namespace MyTelegramBot.QuestionHandlers
{
    public class SignalPercentQuestionHandler : IQuestionHandler
    {
        public bool Accept(ParticipatingInfo participant)
        {
            return !string.IsNullOrWhiteSpace(participant.Name)
                && participant.Major.HasValue
                && participant.EnglishPercent.HasValue
                && participant.MathPercent.HasValue
                && participant.CircutePercent.HasValue
                && participant.ElectronicsPercent.HasValue
                && participant.SignalPercent == null;
        }

        public QuestionRequest Handle(ParticipatingInfo participant)
        {
            participant.LastQuestion = Questions.SignalPercent;
            return new QuestionRequest()
            {
                Message = "درصد سیگنال:"
            };
        }
    }
}