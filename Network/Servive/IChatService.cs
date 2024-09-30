using System.Threading.Tasks;
using MagicOnion;

namespace Shared.Service
{
    /// <summary>
    /// Client -> ServerAPI 
    /// </summary>
    public interface IChatService : IService<IChatService>
    {
        ValueTask<string> Login(string name);
    }
}
