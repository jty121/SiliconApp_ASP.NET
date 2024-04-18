using Infrastructure.Dtos;
using System.Diagnostics;
using WebApi.Entities;
using WebApi.Repositories;

namespace WebApi.Services;

public class SubscribeService(SubscribeRepository subscribeRepository)
{
    private readonly SubscribeRepository _subscribeRepository = subscribeRepository;

    public async Task<SubscribeEntity> CreateAsync(SubscriberDto dto)
    {
        try
        {
            if (!await _subscribeRepository.ExistingAsync(x => x.Email == dto.Email))
            {
                var subscribeEntity = await _subscribeRepository.CreateAsync(new SubscribeEntity
                {
                    Email = dto.Email,
                    DailyNewsletter = dto.DailyNewsletter,
                    AdvertisingUpdates = dto.AdvertisingUpdates,
                    WeekinReview = dto.WeekinReview,
                    EventUpdates = dto.EventUpdates,
                    StartupsWeekly = dto.StartupsWeekly,
                    Podcasts = dto.Podcasts,
                });
                if (subscribeEntity != null)
                {
                    return subscribeEntity;
                }  
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<SubscribeEntity> GetOneByIdAsync(int id)
    {
        try
        {
            var subscribeEntity = await _subscribeRepository.GetOneAsync(x => x.Id == id);
            return subscribeEntity;

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<SubscribeEntity> GetOneByEmailAsync(string email)
    {
        try
        {
            var subscribeEntity = await _subscribeRepository.GetOneAsync(x => x.Email == email);
            return subscribeEntity;

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<IEnumerable<SubscribeEntity>> GetAllAsync()
    {
        try
        {
            var subscribeEntities = await _subscribeRepository.GetAllAsync();
            if (subscribeEntities != null)
            {
                return subscribeEntities;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<SubscribeEntity> UpdateAsync(int id, string email)
    {
        try
        {
            var subscribeEntity = await _subscribeRepository.GetOneAsync(x => x.Id == id);
            if (subscribeEntity != null)
            {
                subscribeEntity.Email = email;
                await _subscribeRepository.UpdateOneAsync(subscribeEntity);

                return subscribeEntity;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entityExist = await _subscribeRepository.DeleteAsync(x => x.Id == id);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return false!;
    }

}
