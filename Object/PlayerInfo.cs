namespace spikewall.Object
{
    public class PlayerInfo : Player
    {
        public long? SuspendedUntil { get; set; }
        public long? SuspendReason { get; set; }
        public string? LastLoginDevice { get; set; }
        public long? LastLoginPlatform { get; set; }
        public long? LastLoginVersionID { get; set; }
        public long[]? AcceptedOpeMessageIDs { get; set; }
    }

    public class StoredPlayerInfo : PlayerInfo
    {

    }
}
