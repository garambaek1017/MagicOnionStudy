using MemoryPack;

namespace Shared.Packets
{
    [MemoryPackable]
    public partial class ReqChatPacket : BasePacket
    {
        public string Message { get; set; }
    }

    [MemoryPackable]
    public partial class ResChatPacketResult : BaseResultPacket
    {
        public string Sender { get; set; }

        public string Message { get; set; }
    }
}
