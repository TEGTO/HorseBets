var builder = DistributedApplication.CreateBuilder(args);

//var db = builder.AddPostgres("db").WithPgAdmin().WithDataVolume();

//var betsDb = db.AddDatabase("betsdb");
//var userDb = db.AddDatabase("userdb");

var cache = builder.AddRedis("cache");

var api = builder.AddProject<Projects.HorseBets_Api>("horsebets-api");

builder.AddProject<Projects.HorseBets>("horsebets-frontend")
    .WithReference(api)
    .WithReference(cache);

builder.Build().Run();