namespace MyTelegramBot
{
    public class RateRankMapping
    {
        public MajorType MajorType { get; set; }
        public decimal FromRate { get; set; }
        public decimal ToRate { get; set; }
        public int Rank { get; set; }
    }
}