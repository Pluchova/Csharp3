var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello!");
app.MapGet("/nazdarSvete", () => "Hello World!");
app.Run();