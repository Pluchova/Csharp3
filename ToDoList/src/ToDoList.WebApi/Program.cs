var builder = WebApplication.CreateBuilder(args);
{
    //Configure DI
    builder.Services.AddControllers();
}
var app = builder.Build();
{
app.MapControllers();
}

app.Run();
