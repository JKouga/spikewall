namespace spikewall.Object
{
    /// <summary>
    /// Class for Message, an object that contains
    /// a message from users to the client.
    /// </summary>
    public class Message
    {
        public string? ID { get; set; }
        public long Type { get; set; }
        public string? FriendID { get; set; }
        public string? Name { get; set; }
        public string? URL { get; set; }
        public MessageItem[]? MessageItem { get; set; }
        public long ExpireTime { get; set; }
    }

    public class OperatorMessage
    {
        public string? ID { get; set; }
        public string? Content { get; set; }
        public MessageItem[]? MessageItme { get; set; }
        public long ExpireTime { get; set; }
    }
}
