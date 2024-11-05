var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);

var postgres = builder.AddPostgres("postgres", username, password);
var postgresdb = postgres.AddDatabase("postgresdb");

var apiService = builder.AddProject<Projects.bjaas_ApiService>("apiservice")
                        .WithReference(postgresdb);

builder.AddProject<Projects.bjaas_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
