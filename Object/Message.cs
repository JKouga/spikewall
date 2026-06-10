namespace spikewall.Object
{
    /// <summary>
    /// Class for Message, an object that contains
    /// a message from users to the client.
    /// </summary>
    public class Message
    {
        public string? Id { get; set; }
        public long Type { get; set; }
        public string? FriendId { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public Item[]? MessageItem { get; set; }
        public long ExpireTime { get; set; }
    }
}
