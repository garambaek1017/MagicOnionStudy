using System.Threading.Tasks;
using MagicOnion;

namespace Network.Service
{
    /// <summary>
    /// Client -> ServerAPI 
    /// </summary>
    public interface IChatService : IService<IChatService>
    {
        ValueTask<string> Login(string name);
    }
}
