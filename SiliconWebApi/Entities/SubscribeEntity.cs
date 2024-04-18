using Infrastructure.Dtos;

namespace WebApi.Entities;
public class SubscribeEntity 
{
    public int Id { get; set; } 
    public string Email { get; set; } = null!;
    public bool DailyNewsletter { get; set; }
    public bool AdvertisingUpdates { get; set; }
    public bool WeekinReview {  get; set; }
    public bool EventUpdates { get; set; }
    public bool StartupsWeekly { get; set; }
    public bool Podcasts { get; set; }

    public static implicit operator SubscribeEntity(SubscriberDto dto)
    {
        return new SubscribeEntity
        {
            Email = dto.Email,
            DailyNewsletter = dto.DailyNewsletter,
            AdvertisingUpdates = dto.AdvertisingUpdates,
            WeekinReview = dto.WeekinReview,
            EventUpdates = dto.EventUpdates,
            StartupsWeekly = dto.StartupsWeekly,
            Podcasts = dto.Podcasts,
        };
    }
}
