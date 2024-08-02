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
    public partial class BaseResultPacket
    {
        public int Code { get; set; }

        public int UserId { get; set; }

        public string Nickname { get; set; }
    }


}
