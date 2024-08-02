using MemoryPack;

namespace Shared.Packets
{
    [MemoryPackable]
    public partial class ReqChatPacket
    {
        public string Message { get; set; }
    }

    [MemoryPackable]
    public partial class ResChatPacketResult : BaseResultPacket
    {
        public string Message { get; set; }
    }
}
