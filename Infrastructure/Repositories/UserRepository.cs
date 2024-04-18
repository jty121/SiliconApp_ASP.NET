using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class UserRepository(DataContext_WebApp context) : Repo<UserEntity>(context)
{
    private readonly DataContext_WebApp _context = context;
}