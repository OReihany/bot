﻿namespace MyTelegramBot.AnswerHandlers
{
    public abstract class BasePercentAnswerHandler : IAnswerHandler
    {
        public virtual string Message { get; private set; }
        log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public abstract bool Accept(ParticipatingInfo participant);

        public virtual AnswerResponse Handle(ParticipatingInfo participant, AnswerRequest request)
        {
            log.InfoFormat("answer {0} {1} {2} {3} {4} {5}",request.ChatId,request.UserName,request.Message,request.FirstName,request.LastName,request.ChatType.ToString());
            if (participant.Answers.ContainsKey(participant.LastQuestion))
                participant.Answers[participant.LastQuestion] = request.Message;
            else
                participant.Answers.Add(participant.LastQuestion, request.Message);
            decimal val;
            if (!decimal.TryParse(request.Message, out val))
            {
                return new AnswerResponse(Message, true);
            }
            if (val < -33 || val > 100)
            {
                return new AnswerResponse(Message, true);
            }
            return new AnswerResponse();
        }
    }
}