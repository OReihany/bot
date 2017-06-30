using System.Collections.Generic;

namespace MyTelegramBot.Interfaces
{
    public interface IResponce
    {
        string UserName { get; set; }
        string Message { get; set; }
        bool HasError { get; set; }
        ResponceType ResponceType { get; set; }
        List<string> SelectionList { get; set; }
    }
    public enum ResponceType
    {
        Text = 0,
        Choose = 1
    }
}