using MyTelegramBot.AnswerHandlers;

namespace MyTelegramBot.Interfaces
{
    public interface IParticipantsRepository
    {
        IResponce Handle(IRequest message);
        bool IsCompleted(IRequest answerRequest);
    }
}