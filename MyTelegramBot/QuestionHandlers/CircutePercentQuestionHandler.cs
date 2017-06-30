namespace MyTelegramBot.QuestionHandlers
{
    public class CircutePercentQuestionHandler : IQuestionHandler
    {
        public bool Accept(ParticipatingInfo participant)
        {
            return !string.IsNullOrWhiteSpace(participant.Name)
               && participant.Major.HasValue
                && participant.EnglishPercent.HasValue
                && participant.MathPercent.HasValue
                && participant.CircutePercent == null;
        }

        public QuestionRequest Handle(ParticipatingInfo participant)
        {
            participant.LastQuestion = Questions.CircutePercent;
            return new QuestionRequest()
            {
                Message = "œ—’œ „œ«—:"
            };
        }
    }
}