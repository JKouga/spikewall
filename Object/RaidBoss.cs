namespace spikewall.Object
{
    public class RaidBossUserState
    {
        public string WrestleID { get; set; }
        public string Name { get; set; }
        public long Grade { get; set; }
        public long NumRank { get; set; }
        public string CharaID { get; set; }
        public long CharaLevel { get; set; }
        public string SubCharaID { get; set; }
        public long SubCharaLevel { get; set; }
        public string MainChaoID { get; set; }
        public long MainChaoLevel { get; set; }
        public string SubChaoID { get; set; }
        public long SubChaoLevel { get; set; }
        public long Language { get; set; }
        public long League { get; set; }
        public long WrestleCount { get; set; }
        public long WrestleDamage { get; set; }
        public long WrestleBeatFlg { get; set; }
    }

    public class EventRaidBossState
    {
        public long ID { get; set; }
        public long Level { get; set; }
        public long Rarity { get; set; }
        public long HP { get; set; }
        public long MaxHP { get; set; }
        public long Status { get; set; }
        public long EsacpeAt { get; set; }
        public string EncounterName { get; set; }
        public long EncounterFlg { get; set; }
        public long ParticipateCount { get; set; }

        /// <summary>
        /// This enum contains the different rarities of raid bosses.
        /// </summary>
        public enum RaidBossRarity
        {
            Normal,
            Rare,
            SuperRare
        }
    }

    public class EventUserRaidBossState
    {
        public long NumRaidBossRings { get; set; }
        public long RaidBossEnergy { get; set; }
        public long RaidBossEnergyBuy { get; set; }
        public long NumBeatedEncounter { get; set; }
        public long NumBeatedEnterprise { get; set; }
        public long NumRaidBossEncountered { get; set; }
        public long EnergyRenewsAt { get; set; }
    }

    public class RaidBossPrize
    {
        public Item ItemID { get; set; }
        public long NumItem { get; set; }
        public long ItemWeight { get; set; }
        public long SpinID { get; set; }
    }

    public class RaidBossWheelOptions
    {
        public string[] Items { get; set; }
        public long[] Item { get; set; }
        public long[] ItemWeight { get; set; }
        public long ItemWon { get; set; }
        public long NextFreeSpin { get; set; }
        public long RouletteRank { get; set; }
        public long NumSpecialEgg { get; set; }
        public long NumRouletteToken { get; set; }
        public long NumJackpotRing { get; set; }
        public long NumRemainingRoulette { get; set; }
        public long SpinID { get; set; }
        public Item[] CostItemList { get; set; }
    }
}
