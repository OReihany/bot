namespace MyTelegramBot.QuestionHandlers
{
    public class EnglishPercentQuestionHandler : IQuestionHandler
    {
        public bool Accept(ParticipatingInfo participant)
        {
            return !string.IsNullOrWhiteSpace(participant.Name)
                && participant.Major.HasValue
                && participant.EnglishPercent == null;
        }

        public QuestionRequest Handle(ParticipatingInfo participant)
        {
            participant.LastQuestion = Questions.EnglishPercent;
            return new QuestionRequest()
            {
                Message = "درصد زبان:"
            };
        }
    }
}