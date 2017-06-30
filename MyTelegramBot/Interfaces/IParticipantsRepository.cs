namespace MyTelegramBot.Interfaces
{
    public interface IParticipantsRepository
    {
        IResponce Handle(IRequest message);
    }
}