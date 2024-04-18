
namespace Infrastructure.Entities;

public class AddressEntity
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string StreetName { get; set; } = null!;
    public string? SecondStreetName { get; set; }
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public ICollection<UserEntity> Users { get; set; } = [];
}
