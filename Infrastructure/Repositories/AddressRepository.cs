using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class AddressRepository(DataContext_WebApp context) : Repo<AddressEntity>(context)
{
    private readonly DataContext_WebApp _context = context;
}
