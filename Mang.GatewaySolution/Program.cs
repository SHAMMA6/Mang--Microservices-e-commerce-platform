using Mang.GatewaySolution.Extentions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);
builder.AddAppAuthetication();


var app = builder.Build();

app.UseOcelot().GetAwaiter().GetResult();

app.MapGet("/", () => "Hello World!");

app.Run();
