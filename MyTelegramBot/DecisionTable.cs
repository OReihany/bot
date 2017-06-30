using System.Collections.Generic;

namespace MyTelegramBot
{
    public static class DecisionTable
    {
        public static List<RateRankMapping> GetRateRankMapping()
        {
            return new List<RateRankMapping>
            {
                new RateRankMapping {MajorType = MajorType.Electronic, FromRate = 44, ToRate = 100, Rank = 1},
                new RateRankMapping {MajorType = MajorType.Electronic, FromRate = 40, ToRate = 43, Rank = 5},
            };
        }
    }
}