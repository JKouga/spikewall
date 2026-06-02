namespace spikewall.Object
{
    public class Event
    {
        public long? eventId { get; set; }
        public long? eventType { get; set; }
        public long? eventStartTime { get; set; }
        public long? eventEndTime { get; set; }
        public long? eventCloseTime { get; set; }
    }

    /// <summary>
    /// Enum that contains all the Event IDs
    /// </summary>
    public enum EventID
    {
        SpecialStage = 100000000,
        RaidBoss,
        CollectObject,
        Gacha,
        Advert,
        Quick,
        BGM
    }

    /// <summary>
    /// Enum that contains all the Event Types in the game
    /// </summary>
    public enum EventType
    {
        SpecialStage,
        RaidBoss,
        CollectObject,
        Gacha,
        Advert,
        Quick,
        BGM
    }

    /// <summary>
    /// Enum that contains the Advert Event Types
    /// </summary>
    public enum AdvertEventType
    {
        Roulette,
        Character,
        Shop
    }

    /// <summary>
    /// Evnum that contains the Collect Event Types
    /// </summary>
    public enum CollectEventType
    {
        GetAnimals,
        GetRings,
        RunDistance
    }
}
