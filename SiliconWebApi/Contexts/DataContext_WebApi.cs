

using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace SiliconWebApi.Contexts;

public class DataContext_WebApi(DbContextOptions<DataContext_WebApi> options) : DbContext(options)
{
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<SubscribeEntity> Subscribers { get; set; }
}

