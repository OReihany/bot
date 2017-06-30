namespace MyTelegramBot.QuestionHandlers
{
    public class ControllPercentQuestionHandler : IQuestionHandler
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
                && participant.ControllPercent == null;
        }

        public QuestionRequest Handle(ParticipatingInfo participant)
        {
            participant.LastQuestion = Questions.ControllPercent;
            return new QuestionRequest()
            {
                Message = "œ—’œ ò‰ —·:"
            };
        }
    }
}