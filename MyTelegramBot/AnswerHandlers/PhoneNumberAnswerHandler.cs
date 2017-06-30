using System.Text.RegularExpressions;

namespace MyTelegramBot.AnswerHandlers
{
    public class PhoneNumberAnswerHandler : IAnswerHandler
    {
        public bool Accept(ParticipatingInfo participant)
        {
            return participant.LastQuestion == Questions.PhoneNumber;
        }

        public AnswerResponse Handle(ParticipatingInfo participant, AnswerRequest request)
        {
            if (participant.Answers.ContainsKey(participant.LastQuestion))
                participant.Answers[participant.LastQuestion] = request.Message;
            else
                participant.Answers.Add(participant.LastQuestion, request.Message);
            if (!Regex.IsMatch(request.Message, @"(0|\+98)?([ ]|,|-|[()]){0,2}9[1|2|3|4]([ ]|,|-|[()]){0,2}(?:[0-9]([ ]|,|-|[()]){0,2}){8}"))
            {
                return new AnswerResponse("الگوی شماره تماس صحیح نمی باشد",true);
            }
            participant.PhoneNumber = request.Message;
            return new AnswerResponse();
        }
    }
}