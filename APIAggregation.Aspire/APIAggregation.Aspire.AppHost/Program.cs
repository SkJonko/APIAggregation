var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.APIAggregation>("APIAggregation");

builder.Build().Run();
