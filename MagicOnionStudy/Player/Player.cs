namespace MagicOnionServer
{
    public class Player
    {
        public long UserId { get; set; }
        public Guid ConnectionId { get; set; }
        public string Name { get; set; }
        public Player(long userId, Guid connectionId, string name)
        {
            UserId = userId;
            ConnectionId = connectionId;
            Name = name;
        }

        public Player()
        {

        }
    }
}
