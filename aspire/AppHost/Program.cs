using MediaInAction.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddForwardedHeaders();

var profile = "https";

// Microservices
var administrationService =
    builder.AddProject<Projects.MediaInAction_AdministrationService_HttpApi_Host>("administrationService", profile);
var identityService = builder
    .AddProject<Projects.MediaInAction_IdentityService_HttpApi_Host>("identityService", profile)
    .WaitFor(administrationService);

var videoService = builder.AddProject<Projects.MediaInAction_VideoService_HttpApi_Host>("videoService", profile)
    .WithEndpoint(
        endpointName: "grpc",
        callback: static endpoint =>
        {
            endpoint.Port = 8181;
            endpoint.UriScheme = "http";
            endpoint.Transport = "http2";
            endpoint.IsProxied = false;
        }
    )
    .WaitFor(identityService)
    .WaitFor(administrationService);

builder.Build().Run();