using MyTelegramBot.AnswerHandlers;

namespace MyTelegramBot.Interfaces
{
    public interface IParticipantsRepository
    {
        IResponce Handle(IRequest message);
        bool IsCompleted(IRequest answerRequest);
        string GetParticipantsPhoneNumbers(bool all = true);
    }
}