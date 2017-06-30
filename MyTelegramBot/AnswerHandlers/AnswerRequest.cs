using MyTelegramBot.Interfaces;
using Telegram.Bot.Types.Enums;

namespace MyTelegramBot.AnswerHandlers
{
    public class AnswerRequest : IRequest
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ChatType ChatType { get; set; }
        public string ChatId { get; set; }
        public string Message { get; set; }
    }
}