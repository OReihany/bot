namespace MyTelegramBot.QuestionHandlers
{
    public class ParticipantNameQuestionHandler : IQuestionHandler
    {
        public bool Accept(ParticipatingInfo participant)
        {
            return string.IsNullOrWhiteSpace(participant.Name);
        }

        public QuestionRequest Handle(ParticipatingInfo participant)
        {
            participant.LastQuestion= Questions.Name;
            return new QuestionRequest()
            {
                Message = "نام و نام خانوادگی:"
            };
        }
    }
}