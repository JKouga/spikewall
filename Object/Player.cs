namespace spikewall.Object
{
    public class Player
    {
        public string? ID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? MigrationPassword { get; set; }
        public string? Key { get; set; }
        public long? LastLogin { get; set; }
        public long? Language { get; set; }
        public PlayerState? PlayerState { get; set; }
        public Character[]? CharacterState { get; set; }
        public Chao[]? ChaoState { get; set; }
        public MileageMapState? MileageMapState { get; set; }
        public OptionUserResult? OptionUserResult { get; set; }
        public WheelOptions? LastWheelOptions { get; set; }
        public RouletteInfo? RouletteInfo { get; set; }
        public ChaoRouletteGroup? ChaoRouletteGroup { get; set; }
        public Message[]? Messages { get; set; }
        public OperatorMessage[]? OperatorMessages { get; set; }
        public LoginBonusStatus? LoginBonusStatus { get; set; }
        public bool? InRun { get; set; }
        public EventState? EventState { get; set; }
        public long? ResetCount { get; set; } //Incremented automatically when Debug_ResetPlayer is executed on player; this is for bookkeeping purposes
        public bool? DisallowInactivePurge { get; set; }
        public long? LastLoginPlatformID { get; set; }
    }
}
