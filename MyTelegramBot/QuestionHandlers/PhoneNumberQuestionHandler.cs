namespace MyTelegramBot.QuestionHandlers
{
    public class PhoneNumberQuestionHandler : IQuestionHandler
    {
        public bool Accept(ParticipatingInfo participant)
        {
            return !string.IsNullOrWhiteSpace(participant.Name)
                   && participant.Major.HasValue
                   && participant.EnglishPercent.HasValue
                   && participant.MathPercent.HasValue
                   && participant.CircutePercent.HasValue
                   && participant.ElectronicsPercent.HasValue
                   && participant.SignalPercent.HasValue
                   && participant.ControllPercent.HasValue
                   && participant.MachinePercent.HasValue
                   && participant.MagneticPercent.HasValue
                   && participant.RateAmount.HasValue
                   && string.IsNullOrWhiteSpace(participant.PhoneNumber);
        }

        public QuestionRequest Handle(ParticipatingInfo participant)
        {
            participant.LastQuestion = Questions.PhoneNumber;
            return new QuestionRequest()
            {
                Message = "‘„«—Â  „«”:"
            };
        }
    }
}