using Dapper;
using JogoRpg.Data.Context;
using JogoRpg.Data.Repositories;
using JogoRpg.Domain.DTO;
using JogoRpg.Domain.Entities;
using JogoRpg.Domain.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

    public IEnumerable<UserDTO> Get()
    {
        string query = "SELECT UserId, UserName, NickName, UserEmail FROM Users";

        using (var connection = _dapperContext.CreateConnection())
        {
            return connection.Query<UserDTO>(query);
        }
    }

    public UserDTO Get(long userId)
    {
        string query = "SELECT UserId, UserName, NickName, UserEmail FROM Users WHERE UserId = @UserId";

        using (var connection = _dapperContext.CreateConnection())
        {
            return connection.QueryFirstOrDefault<UserDTO>(query, new { UserId = userId });
        }
    }


    public async Task<UserDTO> Add(UserCreateDTO userCreateDTO)
    {
        if (userCreateDTO == null)
        {
            throw new ArgumentNullException(nameof(userCreateDTO), "Objeto de usuário para adição não pode ser nulo.");
        }

        // Hash da senha
        string hashedPassword = HashAndSaltPassword(userCreateDTO.UserPassword);

        string query = @"INSERT INTO Users (UserName, NickName, UserEmail, UserPassword)
                         VALUES (@UserName, @NickName, @UserEmail, @UserPassword);
                         SELECT SCOPE_IDENTITY();";

        using (var connection = _dapperContext.CreateConnection())
        {
            // Execute o comando SQL e obtenha o ID do usuário recém-adicionado
            long newUserId = await connection.ExecuteScalarAsync<long>(query, new
            {
                UserName = userCreateDTO.UserName,
                NickName = userCreateDTO.NickName,
                UserEmail = userCreateDTO.UserEmail,
                UserPassword = hashedPassword
            });

            // Retorne o DTO com o ID gerado
            return new UserDTO
            {
                UserId = newUserId,
                UserName = userCreateDTO.UserName,
                NickName = userCreateDTO.NickName,
                UserEmail = userCreateDTO.UserEmail
            };
        }
    }

    public override async Task<UserDTO> Update(UserDTO user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "Objeto de usuário para atualização não pode ser nulo.");
        }

        string query = @"UPDATE Users
                     SET UserName = @UserName,
                         NickName = @NickName,
                         UserEmail = @UserEmail
                     WHERE UserId = @UserId";

        using (var connection = _dapperContext.CreateConnection())
        {
            await connection.ExecuteAsync(query, new
            {
                UserName = user.UserName,
                NickName = user.NickName,
                UserEmail = user.UserEmail,
                UserId = user.UserId
            });

            return user;
        }
    }

    // Adicione outros métodos conforme necessário, utilizando Dapper para as operações no banco de dados.

    public async Task<User> Remove(long userId)
    {
        string query = "DELETE FROM Users WHERE UserId = @UserId";

        using (var connection = _dapperContext.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { UserId = userId });
        }

        return null; // Você pode retornar o usuário removido se necessário
    }

    public async Task<User> Authenticate(string username, string password)
    {
        string hashedPassword = HashAndSaltPassword(password);

        string query = "SELECT * FROM Users WHERE UserName = @UserName AND UserPassword = @UserPassword";

        using (var connection = _dapperContext.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<User>(query, new
            {
                UserName = username,
                UserPassword = hashedPassword
            });
        }
    }

    private string HashAndSaltPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}