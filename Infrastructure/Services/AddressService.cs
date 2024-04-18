using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class AddressService(AddressRepository addressRepo)
{
    private readonly AddressRepository _addressRepo = addressRepo;

    public async Task<bool> CreateAddressAsync(AddressEntity entity)
    {
        try
        {
            await _addressRepo.CreateAsync(entity);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return false;
    }

    public async Task<AddressEntity> GetAddressAsync(string userId)
    {
        try
        {
            var addressEntity = await _addressRepo.GetOneAsync(x => x.UserId == userId);
            if (addressEntity != null)
            {
                return addressEntity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<bool> UpdateAddressAsync(AddressEntity entity)
    {
        try
        {
            var exsist = await _addressRepo.GetOneAsync(x => x.UserId == entity.UserId);
            if (exsist != null)
            {
                exsist.StreetName = entity.StreetName;
                exsist.SecondStreetName = entity.SecondStreetName;
                exsist.PostalCode = entity.PostalCode;
                exsist.City = entity.City;

                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return false;
    }
}
