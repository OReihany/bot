using Telegram.Bot.Types.Enums;

namespace MyTelegramBot.Interfaces
{
    public interface IRequest
    {
        string UserName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        ChatType ChatType { get; set; }
        string ChatId { get; set; }
        string Message { get; set; }
    }
}