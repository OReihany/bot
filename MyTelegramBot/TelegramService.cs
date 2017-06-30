using System;
using System.Diagnostics;
using System.Linq;
using Castle.Windsor;
using MyTelegramBot.AnswerHandlers;
using MyTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyTelegramBot
{
    class TelegramService: ITelegramService
    {
        public TelegramService(ITelegramBotClient telegramBotClient)
        {
            TelegramBotClient = telegramBotClient;
        }

        private ITelegramBotClient TelegramBotClient;
        //public static List<ParticipatingInfo> Users = new List<ParticipatingInfo>();
        public void Start()
        {
            TelegramBotClient.OnCallbackQuery += BotOnCallbackQueryReceived;
            TelegramBotClient.OnMessage += BotOnMessageReceived;
            TelegramBotClient.OnMessageEdited += BotOnMessageReceived;
            TelegramBotClient.OnInlineQuery += BotOnInlineQueryReceived;
            TelegramBotClient.OnInlineResultChosen += BotOnChosenInlineResultReceived;
            TelegramBotClient.OnReceiveError += BotOnReceiveError;

            var me = TelegramBotClient.GetMeAsync().Result;

            Console.Title = me.Username;

            TelegramBotClient.StartReceiving();
            Console.ReadLine();
            TelegramBotClient.StopReceiving();
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Debugger.Break();
        }

        private static void BotOnChosenInlineResultReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        {
            Console.WriteLine($"Received choosen inline result: {chosenInlineResultEventArgs.ChosenInlineResult.ResultId}");
        }

        private async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;
            //Configure.Container.AddChildContainer(new WindsorContainer());
            //using (var builder = Configure.Container.GetChildContainer(message.MessageId.ToString()))
            {
                var repository = Configure.Container.Resolve<IParticipantsRepository>();
                var response=repository.Handle(new AnswerRequest()
                {
                    ChatId = message.Chat.Id.ToString(),
                    UserName = message.Chat.Username.ToString(),
                    Message = message.Text,
                    FirstName = message.Chat.FirstName,
                    LastName = message.Chat.LastName,
                    ChatType = message.Chat.Type,
                });
                switch (response.ResponceType)
                {
                    case ResponceType.Text:
                        await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, response.Message);
                        break;
                    case ResponceType.Choose:
                        var keyboard = new ReplyKeyboardMarkup(response.SelectionList.Select(it => new KeyboardButton(it)).ToArray());
                        await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "انتخاب", replyMarkup: keyboard);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                //await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, response.Message);
            }
            //if (Users.Any(it => it.ChatId == message.Chat.))
            //{

            //    Handler(message);
            //    return;
            //}
            //else if (message.Text.StartsWith("/start"))
            //{
            //    var uu = Users.SingleOrDefault(it => it.ChatId == message.Chat.Username);
            //    if (uu != null)
            //    {
            //        Users.Remove(uu);
            //    }
            //    var usr = new ParticipatingInfo() { ChatId = message.Chat.Username };
            //    Users.Add(usr);
            //    Handler(message);
            //    return;
            //}
            //else if (message.Text.StartsWith("/done"))
            //{
            //    //save in db;
            //    return;
            //}
            //else if (message.Text.StartsWith("/exit"))
            //{
            //    //remove data from db;
            //    return;
            //}
            //else
            //{
            //    var usage = @"
            //        /start   - شروع
            //        /done - تایید اطلاعات
            //        /exit    - خروج و حذف اطلاعات
            //        ";
            //    await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, usage,
            //        replyMarkup: new ReplyKeyboardHide());
            //}
        }

        //private async void Handler(Message message)
        //{
        //    var user = Users.Single(it => it.ChatId == message.Chat.Username);
        //    decimal val;
        //    if (!decimal.TryParse(message.Text, out val) && (int)user.LastQuestion > 2)
        //    {
        //        await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "عدد وارد کنید!!!");
        //        return;
        //    }
        //    else if ((int)user.LastQuestion > 2 && (int)user.LastQuestion < 10 && (val < -33 || val > 100))
        //    {
        //        await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "درصد بین -33 و 100 می باشد!!!");
        //        return;
        //    }
        //    else if ((int)user.LastQuestion == 10 && (val < 0 || val > 20))
        //    {
        //        await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "معدل بین 0 تا 20 می باشد!!!");
        //        return;
        //    }
        //    else if (user.LastQuestion == Questions.None)
        //    {

        //    }
        //    else
        //    {
        //        user.Answers.Add(user.LastQuestion, message.Text);
        //    }
        //    switch (user.LastQuestion)
        //    {
        //        case Questions.Name:

        //            await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "گرایش:");
        //            user.LastQuestion = Questions.Major;
        //            break;
        //        case Questions.Major:
        //            await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "درصد زبان:");
        //            user.LastQuestion = Questions.EnglishPercent;
        //            break;
        //        case Questions.EnglishPercent:
        //            await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "درصد ریاضی:");
        //            user.LastQuestion = Questions.MathPercent;
        //            break;
        //        case Questions.MathPercent:
        //            await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "درصد مدار:");
        //            user.LastQuestion = Questions.CircutePercent;
        //            break;
        //        case Questions.CircutePercent:
        //            await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "درصد الکترونیک:");
        //            user.LastQuestion = Questions.ElectronicsPercent;
        //            break;
        //        case Questions.ElectronicsPercent:
        //            await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "درصد سیگنال:");
        //            user.LastQuestion = Questions.SignalPercent;
        //            break;
        //        case Questions.SignalPercent:
        //            await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "درصد کنترل:");
        //            user.LastQuestion = Questions.ControllPercent;
        //            break;
        //        case Questions.ControllPercent:
        //            await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "درصد ماشین:");
        //            user.LastQuestion = Questions.MachinePercent;
        //            break;
        //        case Questions.MachinePercent:
        //            await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "معدل:");
        //            user.LastQuestion = Questions.RateAmount;
        //            break;
        //        case Questions.RateAmount:
        //            if (!user.Answers.Any(it => it.Key == user.LastQuestion))
        //                user.Answers.Add(user.LastQuestion, message.Text);
        //            var usage = @"
        //            /done - تایید اطلاعات
        //            /exit    - خروج و حذف اطلاعات
        //            ";
        //            await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, usage,
        //                replyMarkup: new ReplyKeyboardHide());
        //            break;
        //        default:
        //            await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "نام و نام خانوادگی:");
        //            user.LastQuestion = Questions.Name;
        //            break;
        //    }
        //}
        private async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            try
            {
                await TelegramBotClient.AnswerCallbackQueryAsync(callbackQueryEventArgs.CallbackQuery.Id, $"Received {callbackQueryEventArgs.CallbackQuery.Data}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}