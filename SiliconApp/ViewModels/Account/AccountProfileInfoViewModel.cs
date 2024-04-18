namespace SiliconApp.ViewModels.Account;

public class AccountProfileInfoViewModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string ProfileImageUrl = "Avatar.jpg";
    public bool IsExternalAccount { get; set; }
}

