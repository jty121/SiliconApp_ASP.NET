using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class DataContext_WebApp(DbContextOptions<DataContext_WebApp> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<AddressEntity> Addresses { get; set; }
}
//här behöver du inte registrera tabeller som har med identity att göra. ENDAST de övriga som du skapar utöver.