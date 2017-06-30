using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using MyTelegramBot.Interfaces;

namespace MyTelegramBot.QuestionHandlers
{
    public class QuestionRequest : IResponce
    {
        public QuestionRequest()
        {
            SelectionList=new EditableList<string>();
        }
        public string UserName { get; set; }
        public string Message { get; set; }
        public bool HasError { get; set; }
        public ResponceType ResponceType { get; set; }
        public List<string> SelectionList { get; set; }
    }

    
}