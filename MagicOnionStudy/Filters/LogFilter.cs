using MagicOnion.Server.Filters;
using MagicOnion.Server.Hubs;

namespace MagicOnionServer.Filters
{

    public class LogFilter : IStreamingHubFilter
    {
        public async ValueTask Invoke(StreamingHubContext context, Func<StreamingHubContext, ValueTask> next)
        {
            var methodName = context.Path.Split('/')[1];

            Logger.Log($"[REQ_{methodName}] {context.Path}, connectionId: {context.ConnectionId}");
            await next(context);
            Logger.Log($"[RES_{methodName}] {context.Path}");
        }
    }
}
