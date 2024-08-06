using MemoryPack;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Packets
{
    [MemoryPackable]
    public partial class BasePacket
    {
        public long UserId { get; set; }
        public string Nickname { get; set; }

    }

    [MemoryPackable]
    public partial class BaseResultPacket
    {
        public ErrorCode Code { get; set; }

        public long UserId { get; set; }

        public string Nickname { get; set; }
    }
}
