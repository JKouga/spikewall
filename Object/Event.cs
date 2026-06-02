namespace spikewall.Object
{
    /// <summary>
    /// Class for Event, an object that contains
    /// information and settings for given Events, used to setup events to the client.
    /// </summary>
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
        SpecialStage,
        RaidBoss,
        CollectObject,
        Gacha,
        Advert,
        Quick,
        BGM
    }

    public enum EventType
    {
        GetAnimals,
        GetRing,
        RunDistance,
        Roulette,
        Character,
        Shop
    }
}
