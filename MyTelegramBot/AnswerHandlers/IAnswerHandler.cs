namespace MyTelegramBot.AnswerHandlers
{
    public interface IAnswerHandler
    {
        bool Accept(ParticipatingInfo participant);
        AnswerResponse Handle(ParticipatingInfo participant, AnswerRequest message);
    }
}