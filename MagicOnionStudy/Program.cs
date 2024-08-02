using MagicOnion.Serialization;
using MagicOnion.Serialization.MemoryPack;
using MagicOnion.Server;
using MagicOnionServer.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Shared.Util;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    // WORKAROUND: Accept HTTP/2 only to allow insecure HTTP/2 connections during development.
    options.ConfigureEndpointDefaults(endpointOptions =>
    {
        endpointOptions.Protocols = HttpProtocols.Http2;
    });
});

MagicOnionSerializerProvider.Default = MemoryPackMagicOnionSerializerProvider.Instance;

// MagicOnion depends on ASP.NET Core gRPC Service.
builder.Services.AddGrpc();
builder.Services.AddMagicOnion(opt =>
{
    opt.GlobalStreamingHubFilters.Add<LogFilter>();
});

var app = builder.Build();
app.MapMagicOnionService();

var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
lifetime.ApplicationStarted.Register(() =>
{
    Logger.Log("#### MagicOnionServer Start ####");
    
});


lifetime.ApplicationStopped.Register(() => { Logger.Log("Server app has stopped."); });

app.Run();

