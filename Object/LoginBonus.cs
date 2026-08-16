namespace spikewall.Object
{
    public class LoginBonusStatus
    {
        // Number of logins.
        public long? numLogin { get; set; }
        
        // Number of bonuses?
        public long? numBonus { get; set; }

        // Last time a bonus had been obtained.
        public long? lastBonusTime { get; set; }
    }

    public class LoginBonusReward
    {
        public SelectReward[]? selectRewardList { get; set; }
    }

    public class LoginBonusState
    {
        // Current login day of Dash Debut Login Bonus; this does not reset when login bonus resets
        public long? CurrentFirstLoginBonusDay { get; set; }

        // Current login day of Weekly Login Bonus; this does reset when a new day/week arrives
        public long? CurrentLoginBonusDay { get; set; }
        public long? LastLoginBonusTime { get; set; }
        public long? NextLoginBonusTime { get; set; }
        public long? LoginBonusStartTime { get; set; }
        public long? LoginBonusEndTime { get; set; }
    }
}
