
using System.ComponentModel.DataAnnotations;

namespace SiliconWebApp.ViewModels;

public class SubscriberViewModel
{
    [Display(Name = "Email address", Prompt = "Your email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Your email address is invalid")]
    public string Email { get; set; } = null!;


    [Display(Name = "DailyNewsletter")]
    public bool DailyNewsletter { get; set; } = false;


    [Display(Name = "AdvertisingUpdates")]
    public bool AdvertisingUpdates { get; set; } = false;


    [Display(Name = "WeekinReview")]
    public bool WeekinReview { get; set; } = false;


    [Display(Name = "StartupsWeekly")]
    public bool StartupsWeekly { get; set; } = false;


    [Display(Name = "EventUpdates")]
    public bool EventUpdates { get; set; } = false;


    [Display(Name = "Podcasts")]
    public bool Podcasts { get; set; } = false;
}

//modell för formuläret 