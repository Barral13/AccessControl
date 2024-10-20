using AccessControl.API.Data;
using AccessControl.API.Services;
using AccessControl.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(cnnStr);
});

builder.Services.AddControllers();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ISubgroupService, SubgroupService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Access Control API",
        Version = "v1",
        Description = "API para controle de acessos"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Access Control API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
