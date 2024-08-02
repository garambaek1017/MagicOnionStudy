using MemoryPack;

namespace Shared.Packets
{
    [MemoryPackable]
    public partial class ReqLoginPacket
    {
        public string Nickname { get; set; }
    }

    [MemoryPackable]
    public partial class ResLoginPacketResult : BaseResultPacket
    {

    }
}
