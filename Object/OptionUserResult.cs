namespace spikewall.Object
{
    public class OptionUserResult
    {
        //Highest Total Score Recorded for Story Mode
        public long? TotalSumHighScore { get; set; }
        //Highest Total Score Recorded for Timed Mode
        public long? QuickTotalSumHighScore { get; set; }
        //Total Number of Rings Acquired
        public long? NumTakeAllRings { get; set; }
        //Total Number of Red Star Rings Acquired
        public long? NumTakeAllRedRings { get; set; }
        //Total Times the Chao Roulette was spun
        public long? NumChaoRoulette { get; set; }
        //Total Times the Item Roulette was spun
        public long? NumItemRoulette { get; set; }
        //Total Number of Jackpots won all-time
        public long? NumJackpot { get; set;  }
        //Highest Jackpot won all-time
        public long? NumMaximumJackpotRings { get; set; }
        public long? NumSupport { get; set; }
    }
}
