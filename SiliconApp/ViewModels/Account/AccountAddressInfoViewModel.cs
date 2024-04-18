using System.ComponentModel.DataAnnotations;

namespace SiliconApp.ViewModels.Account;

public class AccountAddressInfoViewModel
{
    [Display(Name = "Address line 1", Prompt = "Enter your address line", Order = 0)]
    [Required(ErrorMessage = "First name is required")]
    public string StreetName { get; set; } = null!;


    [Display(Name = "Address line 2", Prompt = "Enter your second address line", Order = 1)]
    public string? SecondStreetName { get; set; }


    [Display(Name = "PostalCode", Prompt = "Enter your postalCode", Order = 2)]
    [Required(ErrorMessage = "PostalCode is required")]
    [DataType(DataType.PostalCode)]
    public string PostalCode { get; set; } = null!;


    [Display(Name = "City", Prompt = "Enter your city", Order = 3)]
    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = null!;
}
