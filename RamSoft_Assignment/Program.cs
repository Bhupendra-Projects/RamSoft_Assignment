using Microsoft.EntityFrameworkCore;
using RamSoft_Assignment.Data;
using RamSoft_Assignment.Repositories;
using RamSoft_Assignment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TaskManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<IColumnRepository, ColumnRepository>();
builder.Services.AddTransient<IColumnService, ColumnService>();
builder.Services.AddTransient<ITaskService, TaskService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
