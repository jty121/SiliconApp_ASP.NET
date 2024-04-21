using Microsoft.EntityFrameworkCore;
using SiliconWebApi.Configurations;
using SiliconWebApi.Contexts;
using WebApi.Repositories;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext_WebApi>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDatabase_WebApi")));

//h�r k�r du dina dependecy injections. m�ste deklarera dom h�r innan du k�r programmet. 
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<SubscribeRepository>();
builder.Services.AddScoped<SubscribeService>();

builder.Services.RegisterSwagger();



var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Silicon Web Api v1"));
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
