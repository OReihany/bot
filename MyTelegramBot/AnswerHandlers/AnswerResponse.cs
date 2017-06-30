using System.Collections.Generic;
using MyTelegramBot.Interfaces;

namespace MyTelegramBot.AnswerHandlers
{
    public class AnswerResponse : IResponce
    {
        public AnswerResponse()
        {
            Message = "";
        }
        public AnswerResponse(string message)
        {
            Message = message;
        }

        public AnswerResponse(string message,bool hasError)
        {
            Message = message;
            HasError = hasError;
        }

        public string UserName { get; set; }
        public string Message { get; set; }
        public bool HasError { get; set; }
        public ResponceType ResponceType { get; set; }
        public List<string> SelectionList { get; set; }
    }
}