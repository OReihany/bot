namespace MyTelegramBot.AnswerHandlers
{
    public class RateAmountAnswerHandler : IAnswerHandler
    {
        public bool Accept(ParticipatingInfo participant)
        {
            return participant.LastQuestion == Questions.RateAmount;
        }
        public AnswerResponse Handle(ParticipatingInfo participant, AnswerRequest request)
        {
            if (participant.Answers.ContainsKey(participant.LastQuestion))
                participant.Answers[participant.LastQuestion] = request.Message;
            else
                participant.Answers.Add(participant.LastQuestion, request.Message);
            decimal val;
            if (!decimal.TryParse(request.Message, out val))
            {
                return new AnswerResponse("عدد وارد کنید!!!",true);
            }
            if (val < 0 || val > 20)
            {
                return new AnswerResponse("معدل بین 0 تا 20 می باشد!!!", true);
            }
            participant.RateAmount = decimal.Parse(request.Message);
            return new AnswerResponse();
        }
    }
}