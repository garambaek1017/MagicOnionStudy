using Grpc.Net.Client;
using MagicOnion.Client;
using MagicOnion.Unity;
using Network.Hub;
using UnityEngine;

[MagicOnionClientGeneration(typeof(IChatHub))]
partial class MagicOnionClientInitializer
{
}

class InitialSettings
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void OnRuntimeInitialize()
    {
        GrpcChannelProviderHost.Initialize(
            new GrpcNetClientGrpcChannelProvider(() => new GrpcChannelOptions()
            {
                HttpHandler = new Cysharp.Net.Http.YetAnotherHttpHandler()
                {
                    Http2Only = true,
                }
            }));
    }
}
