using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;

var builder = WebApplication.CreateBuilder(args);
{
    //Configure DI
    builder.Services.AddControllers();
    builder.Services.AddDbContext<ToDoItemsContext>();
    builder.Services.AddScoped<IRepository<ToDoItem>, ToDoItemsRepository>();
}
var app = builder.Build();
{
    app.MapControllers();
}

app.Run();
