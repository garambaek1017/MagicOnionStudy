namespace MagicOnionServer.User
{
    // 채팅용 유저 
    public class ChatUser
    {
        public long UserId { get; set; }
        public Guid ConnectionId { get; set; }
        public string Name { get; set; }
        public ChatUser(long userId, Guid connectionId, string name)
        {
            UserId = userId;
            // 서버에서 발행하는 connectionId
            ConnectionId = connectionId;
            Name = name;
        }
    }
}
