using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MyTelegramBot.AnswerHandlers;
using MyTelegramBot.Interfaces;
using MyTelegramBot.QuestionHandlers;

namespace MyTelegramBot
{
    class ParticipantsRepository : IParticipantsRepository
    {
        private IMongoDatabase MongoDatabase;
        private IMongoCollection<ParticipatingInfo> _collection;
        public ParticipantsRepository(IMongoDatabase mongoDatabase)
        {
            MongoDatabase = mongoDatabase;
            _collection = MongoDatabase.GetCollection<ParticipatingInfo>("ParticipatingInfos");
        }

        public IQueryable<ParticipatingInfo> Query()
        {
            return _collection.AsQueryable();
        }
        public IResponce Handle(IRequest message)
        {
            bool isNew = false;
            var user = Query().SingleOrDefault(it => it.UserName == message.UserName);
            if (user == null)
            {
                isNew = true;
                user = new ParticipatingInfo()
                {
                    Answers = new Dictionary<Questions, string>(),
                    UserName = message.UserName,
                    ChatId = message.ChatId,
                    FirstName=message.FirstName,
                    LastName = message.LastName,
                    ChatType = message.ChatType
                };
            }
            try
            {
                var answerHandler =
                    Configure.Container.ResolveAll<IAnswerHandler>().SingleOrDefault(it => it.Accept(user));
                if (answerHandler == null)
                {
                    IResponce handle;
                    if (Question(message, user, out handle)) return handle;
                    return new AnswerResponse()
                    {
                        Message = @"
                    /done - تایید اطلاعات
                    /exit    - خروج و حذف اطلاعات
                    ",
                        UserName = message.UserName
                    };
                }
                else
                {
                    var response = answerHandler.Handle(user, (AnswerRequest) message);
                    if (response.HasError)
                    {
                        return response;
                    }
                    IResponce handle;
                    if (Question(message, user, out handle)) return handle;
                    return new AnswerResponse();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
            finally
            {
                if(isNew)
                _collection.InsertOne(user);
                else
                {
                    _collection.ReplaceOne(Builders<ParticipatingInfo>.Filter.Eq(it => it.Id , user.Id), user);
                }
            }
        }

        private static bool Question(IRequest message, ParticipatingInfo user, out IResponce handle)
        {
            var qHandler =
                Configure.Container.ResolveAll<IQuestionHandler>().SingleOrDefault(it => it.Accept(user));
            if (qHandler == null)
            {
                {
                    handle = new QuestionRequest()
                    {
                        Message = @"
                    /done - تایید اطلاعات
                    /exit    - خروج و حذف اطلاعات
                    ",
                        UserName = message.UserName
                    };
                    return true;
                }
            }
            handle = qHandler.Handle(user);
            return true;
            return false;
        }
    }
}