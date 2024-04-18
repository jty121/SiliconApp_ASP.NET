using WebApi.Entities;
namespace Infrastructure.Dtos;

public class SubscriberDto
{
    public string Email { get; set; } = null!;
    public bool DailyNewsletter { get; set; } = false;
    public bool AdvertisingUpdates { get; set; } = false;
    public bool WeekinReview { get; set; } = false;
    public bool EventUpdates { get; set; } = false;
    public bool StartupsWeekly { get; set; } = false;
    public bool Podcasts { get; set; } = false;

    public static implicit operator SubscriberDto(SubscribeEntity entity)
    {
        return new SubscriberDto
        {
            Email = entity.Email,
            DailyNewsletter = entity.DailyNewsletter,
            AdvertisingUpdates = entity.AdvertisingUpdates,
            WeekinReview = entity.WeekinReview,
            EventUpdates = entity.EventUpdates,
            StartupsWeekly = entity.StartupsWeekly,
            Podcasts = entity.Podcasts,
        };
    }
}
