var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Web>("web");
builder.AddProject<Projects.ProcessApi>("processapi");

builder.Build().Run();
