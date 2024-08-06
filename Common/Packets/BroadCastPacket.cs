using MemoryPack;

namespace Shared.Packets
{
    [MemoryPackable]
    public partial class BroadCastPacket
    {
        public int UserId { get;set; }
        public string Sender { get; set; }
        public string BroadCastMessage { get; set; }
    }
}
