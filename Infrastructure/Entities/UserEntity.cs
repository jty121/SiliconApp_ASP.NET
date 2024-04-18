
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities;

public class UserEntity : IdentityUser  //ärver från identity, lägg till de delar som du vill addera för en User
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

    [ProtectedPersonalData]
    public string? Biography { get; set; }

    [ProtectedPersonalData]
    public string? ProfileImageUrl { get; set; } //kan ha en profilbild, men behöver inte. 
    public int? AddressId { get; set; }
    public AddressEntity? Address { get; set; }

    public bool IsExternalAccount { get; set; } = false; //extern autentisering
}

