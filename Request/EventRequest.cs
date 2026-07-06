using spikewall.Object;

namespace spikewall.Request
{
    /// <summary>
    /// This class will grab the corresponding Event based on the Event's ID that is currently running
    /// </summary>
    public class EventRequest : BaseRequest
    {
        public int EventID { get; set; }
    }

    public class EventActStartRequest : BaseRequest
    {
        public Item[] Modifier { get; set; }
        public long EventID { get; set; }
        public long RaidBossID { get; set; }
        public long EnergyExpend { get; set; }
    }

    public class EventPostGameResultsRequest : BaseRequest
    {
        public long EventID { get; set; }
        public long NumRaidBossRings { get; set; }
    }

    public class EventUpdateGameResultsRequest : BaseRequest
    {
        public long Score { get; set; }
        public long Rings { get; set; }
        public long RedRings { get; set; }
        public long Distance { get; set; }
        public long DailyChallengeValue { get; set; }
        public long DailyChallengeComplete { get; set; }
        public long Animals { get; set; }
        public long MaxCombo { get; set; }
        public long Closed { get; set; }
        public long EventID { get; set; }
        public long EventValue { get; set; }
        public long RaidBossID { get; set; }
        public long RaidBossDamage { get; set; }
        public long RaidBossBeatFlg { get; set; }
    }
}
