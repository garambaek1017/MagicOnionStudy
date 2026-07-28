using System.Threading.Tasks;
using MagicOnion;
using MagicOnion.Client;
using MagicOnion.Serialization.MemoryPack;
using Network.Hub;
using Uitility;

public partial class HubClient : MonoBehaviourSingletonTemplate<HubClient>, IChatHubReceiver
{
    private IChatHub _hub;

    public async Task<bool> ConnectAsync(string address)
    {
        var channel = GrpcChannelx.ForAddress($"{address}");

        _hub = await StreamingHubClient.ConnectAsync<IChatHub, IChatHubReceiver>(channel, this,
            serializerProvider: MemoryPackMagicOnionSerializerProvider.Instance);

        if (_hub is null)
        {
            MyLogger.Log("Failed to connect to Hub");
            return false;
        }

        _ = WaitForDisconnectEventAsync();

        async Task WaitForDisconnectEventAsync()
        {
            var reason = await _hub.WaitForDisconnectAsync();
            MyLogger.Log("WaitForDisconnectEventAsync: " + reason.Type);
        }

        return true;
    }

    public async Task DisposeAsync()
    {
        await _hub.DisposeAsync();
    }
}
