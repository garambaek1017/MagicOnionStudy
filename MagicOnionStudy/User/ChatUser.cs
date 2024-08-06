namespace MagicOnionServer
{
    public class ChatUser
    {
        public long UserId { get; set; }
        public Guid ConnectionId { get; set; }
        public string Name { get; set; }
        public ChatUser(long userId, Guid connectionId, string name)
        {
            UserId = userId;
            ConnectionId = connectionId;
            Name = name;
        }
    }
}
