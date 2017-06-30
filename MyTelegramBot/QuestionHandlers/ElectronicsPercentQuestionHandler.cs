namespace MyTelegramBot.QuestionHandlers
{
    public class ElectronicsPercentQuestionHandler : IQuestionHandler
    {
        public bool Accept(ParticipatingInfo participant)
        {
            return !string.IsNullOrWhiteSpace(participant.Name)
                && participant.Major.HasValue
                && participant.EnglishPercent.HasValue
                && participant.MathPercent.HasValue
                && participant.CircutePercent.HasValue
                && participant.ElectronicsPercent == null;
        }

        public QuestionRequest Handle(ParticipatingInfo participant)
        {
            participant.LastQuestion = Questions.ElectronicsPercent;
            return new QuestionRequest()
            {
                Message = "درصد الکترونیک:"
            };
        }
    }
}