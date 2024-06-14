var builder = DistributedApplication.CreateBuilder(args);

var betsDbName = "betsdb";
var userDbName = "userdb";
var db = builder.AddPostgres("db").WithPgAdmin();

var betsDb = db.AddDatabase(betsDbName);
var userDb = db.AddDatabase(userDbName);

var api = builder.AddProject<Projects.HorseBets_Api>("horsebets-api")
    .WithReference(betsDb);

builder.AddProject<Projects.HorseBets>("horsebets-frontend")
    .WithReference(api)
    .WithReference(userDb);

builder.Build().Run();