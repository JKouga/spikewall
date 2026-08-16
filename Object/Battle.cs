namespace spikewall.Object
{
    public class BattleData
    {
        public string? UserID { get; set; }
        public string? Name { get; set; }
        public long? MaxScore { get; set; }
        public long? League { get; set; }
        public long? LoginTime { get; set; }
        public string? MainChaoID { get; set; }
        public long? MainChaoLevel { get; set; }
        public string? SubChaoID { get; set; }
        public long? SubChaoLevel { get; set; }
        public long? Rank { get; set; }
        public string? MainCharacterID { get; set; }
        public long? MainCharacterLevel { get; set; }
        public string? SubCharacterID { get; set; }
        public long? SubCharacterLevel { get; set; }
        public long? WinStreak { get; set; }
        public bool? IsEnergySent { get; set; }
        public long? Language { get; set; }
    }

    public class BattlePair
    {
        public long? StartTime { get; set; }
        public long? EndTime { get; set; }
        public BattleData? BattleData { get; set; }
        public BattleData? RivalBattleData { get; set; }
    }
    public class RewardBattlePair : BattlePair
    {

    }

    public class BattleState
    {
        public bool? ScoreRecordedToday { get; set; }
        public long? DailyBattleHighScore { get; set; }
        public long? PrevDailyBattleHighScore { get; set; }
        public long? BattleStart { get; set; }
        public long? BattleEnd { get; set; }
        public bool? MatchedWithRival { get; set; }
        public string? RivalID { get; set; }
        public long? Wins { get; set; }
        public long? Losses { get; set; }
        public long? Failures { get; set; }
        public long? WinStreak { get; set; }
        public long? LossStreak { get; set; }
        public BattlePair[]? BattleHistory { get; set; }
        public bool? PendingReward { get; set; }
        public RewardBattlePair? PendingRewardData { get; set; }
    }

    public class BattleStatus
    {
        public long? Wins { get; set; }
        public long? Losses { get; set; }
        public long? Ties { get; set; }
        public long? Failures { get; set; }
        public long? WinStreak { get; set; }
        public long? LossStreak { get; set; }
    }
}
