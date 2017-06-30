namespace MyTelegramBot.QuestionHandlers
{
    public class MathPercentQuestionHandler : IQuestionHandler
    {
        public bool Accept(ParticipatingInfo participant)
        {
            return !string.IsNullOrWhiteSpace(participant.Name)
                && participant.Major.HasValue
                && participant.EnglishPercent.HasValue
                && participant.MathPercent == null;
        }

        public QuestionRequest Handle(ParticipatingInfo participant)
        {
            participant.LastQuestion = Questions.MathPercent;
            return new QuestionRequest()
            {
                Message = "درصد ریاضی:"
            };
        }
    }
}