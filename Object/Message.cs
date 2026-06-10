namespace spikewall.Object
{
    public class Message
    {
        public string? ID { get; set; }
        public long Type { get; set; }
        public string? FriendID { get; set; }
        public string? Name { get; set; }
        public string? URL { get; set; }
        public Item[]? MessageItem { get; set; }
        public long ExpireTime { get; set; }
    }
}
