namespace MyTelegramBot.QuestionHandlers
{
    public interface IQuestionHandler
    {
        bool Accept(ParticipatingInfo participant);
        QuestionRequest Handle(ParticipatingInfo participant);
    }
}