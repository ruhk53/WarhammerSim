var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.WarhammerSim_ApiService>("apiservice");

var password = builder.AddParameter("sql-password", secret: true);

var sqlServer = builder.AddAzureSqlServer("sql").AddDatabase("WarhammerSimDB");

builder.AddProject<Projects.WarhammerSim_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
