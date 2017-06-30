using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
    class TelegramService : ITelegramService
    {
        public TelegramService(ITelegramBotClient telegramBotClient)
        {
            TelegramBotClient = telegramBotClient;
        }

        private ITelegramBotClient TelegramBotClient;
        log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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

        private void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            log.Error(receiveErrorEventArgs.ApiRequestException.Message);
            //Debugger.Break();
        }

        private void BotOnChosenInlineResultReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        {
            Console.WriteLine($"Received choosen inline result: {chosenInlineResultEventArgs.ChosenInlineResult.ResultId}");
        }

        private async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            log.InfoFormat("message from {0} : {1}", message.Chat.Username,message.Text);
            if (message == null || message.Type != MessageType.TextMessage) return;
            var repository = Configure.Container.Resolve<IParticipantsRepository>();
            if (message.Text == "$$$7272###")
            {
                await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, repository.GetParticipantsPhoneNumbers(), replyMarkup: new ReplyKeyboardHide());
                return;
            }
            if (message.Text == "$$$727272###")
            {
                await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, repository.GetParticipantsPhoneNumbers(false), replyMarkup: new ReplyKeyboardHide());
                return;
            }
            {
                
                var response = repository.Handle(new AnswerRequest()
                {
                    ChatId = message.Chat.Id.ToString(),
                    UserName = message.Chat.Username.ToString(),
                    Message = Configure.ConvertDigitsToLatin(message.Text),
                    FirstName = message.Chat.FirstName,
                    LastName = message.Chat.LastName,
                    ChatType = message.Chat.Type,
                });
                switch (response.ResponceType)
                {
                    case ResponceType.Text:
                        await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, response.Message, replyMarkup: new ReplyKeyboardHide());
                        await SendCompletionMessage(repository, message);
                        break;
                    case ResponceType.Choose:
                        {
                            var buttons = response.SelectionList.Select(it => new InlineKeyboardButton(it)).ToArray();
                            List<InlineKeyboardButton[]> cc = new List<InlineKeyboardButton[]>();
                            int i = 0;
                            while (i < buttons.Length)
                            {
                                cc.Add(new[] // first row
                                {
                                buttons[i],
                                buttons[i+1],
                            });
                                i = i + 2;

                            }
                            var keyboard = new InlineKeyboardMarkup(cc.ToArray());
                            await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "انتخاب", replyMarkup: keyboard);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

        }

        private async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            try
            {
                var message = callbackQueryEventArgs.CallbackQuery.Message;
                log.InfoFormat("message from {0} : {1}", message.Chat.Username, callbackQueryEventArgs.CallbackQuery.Data);
                if (message == null || message.Type != MessageType.TextMessage) return;
                {
                    var repository = Configure.Container.Resolve<IParticipantsRepository>();
                    var response = repository.Handle(new AnswerRequest()
                    {
                        ChatId = message.Chat.Id.ToString(),
                        UserName = message.Chat.Username.ToString(),
                        Message = Configure.ConvertDigitsToLatin(callbackQueryEventArgs.CallbackQuery.Data),
                        FirstName = message.Chat.FirstName,
                        LastName = message.Chat.LastName,
                        ChatType = message.Chat.Type,
                    });
                    switch (response.ResponceType)
                    {
                        case ResponceType.Text:
                            await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, response.Message, replyMarkup: new ReplyKeyboardHide());
                            await SendCompletionMessage(repository, message);
                            break;
                        case ResponceType.Choose:
                            var keyboard = new InlineKeyboardMarkup(response.SelectionList.Select(it => new InlineKeyboardButton(it)).ToArray());
                            await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "انتخاب", replyMarkup: keyboard);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                //await TelegramBotClient.AnswerCallbackQueryAsync(callbackQueryEventArgs.CallbackQuery.Id, $"Received {callbackQueryEventArgs.CallbackQuery.Data}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task SendCompletionMessage(IParticipantsRepository repository, Message message)
        {
            if (repository.IsCompleted(new AnswerRequest()
            {
                ChatId = message.Chat.Id.ToString(),
                UserName = message.Chat.Username,
            }))
            {
                await TelegramBotClient.SendTextMessageAsync(message.Chat.Id, $"طراحی توسط مهندس امید ریحانی.\n" +
                                                                              $"@bargh_konkur\n" +
                                                                              $"\n" +
                                                                              $"omidraihany@ee.sharif.edu\n",
                    replyMarkup: new ReplyKeyboardHide());
            }
        }
    }
}