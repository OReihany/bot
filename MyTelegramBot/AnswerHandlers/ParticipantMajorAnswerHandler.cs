using System.Linq;

namespace MyTelegramBot.AnswerHandlers
{
    public class ParticipantMajorAnswerHandler : IAnswerHandler
    {
        public bool Accept(ParticipatingInfo participant)
        {
            return participant.LastQuestion == Questions.Major;
        }

        public AnswerResponse Handle(ParticipatingInfo participant, AnswerRequest request)
        {
            if (participant.Answers.ContainsKey(participant.LastQuestion))
                participant.Answers[participant.LastQuestion] = request.Message;
            else
                participant.Answers.Add(participant.LastQuestion, request.Message);
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return new AnswerResponse("گرایش نمی تواند خالی باشد",true);
            }
            if (request.Message.Contains(@"\") || request.Message.Contains(@"/") || request.Message.Contains(@"*") || request.Message.Contains(@"#") || request.Message.Contains(@"@"))
            {
                return new AnswerResponse(@"'گرایش نمی تواند دارای کاراکتر های /\*#@ باشد", true);
            }
            if(Configure.MajorTypeString.All(it => it.Value.Trim() != request.Message.Trim()))
                return new AnswerResponse(@"'گرایش مورد نظر یافت نشد", true);
            participant.Major = (MajorType)Configure.MajorTypeString.First(it => it.Value.Trim() == request.Message.Trim()).Key;
            return new AnswerResponse();
        }
    }
}