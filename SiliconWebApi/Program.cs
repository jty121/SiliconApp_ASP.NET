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

//här kör du dina dependecy injections. måste deklarera dom här innan du kör programmet. 
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<SubscribeRepository>();
builder.Services.AddScoped<SubscribeService>();

builder.Services.RegisterSwagger();
//builder.Services.RegisterJwt(builder.Configuration);



var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Silicon Web Api v1"));
app.UseHttpsRedirection();
/*app.UseAuthentication(); */ // måste ligga före authorization 
app.UseAuthorization();
app.MapControllers();
app.Run();
