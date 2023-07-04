using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models.Conversions;
using UserManagement.Services;
using UserManagement.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MSDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("UserManagement");
    options.UseSqlServer(connectionString);
});

builder.Services.AddMapper();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IUserGroupService, UserGroupService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IWorkItemService, WorkItemService>();

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
