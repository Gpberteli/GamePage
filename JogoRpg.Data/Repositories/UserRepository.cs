using Amazon.Runtime.Internal.Util;
using JogoRpg.Data.Context;
using JogoRpg.Data.Repositories;
using JogoRpg.Domain.Entities;
using JogoRpg.Domain.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Generators;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly EntityContext _context;
    private readonly DapperContext _dapperContext;
    private readonly ILogger<UserRepository> _logger;
    public UserRepository(EntityContext context, DapperContext dapperContext, ILogger<UserRepository> logger)
        : base(context, dapperContext)
    {
        _logger = logger;
        _context = context;
        _dapperContext = dapperContext;
    }

    public IEnumerable<User> Get()
    {
        return _context.Users.ToList();
    }

    public async Task<User> Get(long userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User> GetUserWithCharacters(long userId)
    {
        return _context.Users.Include(u => u.Characters).FirstOrDefault(u => u.UserId == userId);
    }


    public async Task<User> AddAsync(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Erro ao adicionar usuário ao banco de dados.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado ao adicionar usuário ao banco de dados.");
            throw;
        }
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<User> RemoveAsync(long userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        return user;
    }

    public async Task<User> Authenticate(string username, string password)
    {
        var hashedPassword = HashAndSaltPassword(password);
        return _context.Users.FirstOrDefault(u => u.UserName == username && u.UserPassword == hashedPassword);
    }
    private string HashAndSaltPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

}

