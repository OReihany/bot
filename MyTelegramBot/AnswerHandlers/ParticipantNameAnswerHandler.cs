namespace MyTelegramBot.AnswerHandlers
{
    public class ParticipantNameAnswerHandler : IAnswerHandler
    {
        public bool Accept(ParticipatingInfo participant)
        {
            return participant.LastQuestion == Questions.Name;
        }

        public AnswerResponse Handle(ParticipatingInfo participant, AnswerRequest request)
        {
            if (participant.Answers.ContainsKey(participant.LastQuestion))
                participant.Answers[participant.LastQuestion] = request.Message;
            else
                participant.Answers.Add(participant.LastQuestion, request.Message);
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return new AnswerResponse("نام و نام خانوادگی نمی تواند خالی باشد",true);
            }
            if (request.Message.Contains(@"\") || request.Message.Contains(@"/") || request.Message.Contains(@"*") || request.Message.Contains(@"#") || request.Message.Contains(@"@"))
            {
                return new AnswerResponse(@"نام و نام خانوادگی نمی تواند دارای کاراکتر های /\*#@ باشد", true);
            }
            participant.Name = request.Message;
            return new AnswerResponse();
        }
    }
}