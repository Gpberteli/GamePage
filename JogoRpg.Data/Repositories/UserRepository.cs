using Dapper;
using JogoRpg.Data.Context;
using JogoRpg.Data.Repositories;
using JogoRpg.Domain.DTO;
using JogoRpg.Domain.Entities;
using JogoRpg.Domain.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


public class UserRepository : BaseRepository<UserDTO>, IUserRepository
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

    public async Task<UserDTO> GetUserWithCharacters(long userId)
    {
        string query = @"
        SELECT *
        FROM Users u
        LEFT JOIN Characters c ON u.UserId = c.UserId
        WHERE u.UserId = @UserId;
    ";

        using (var connection = _dapperContext.CreateConnection())
        {
            var userDictionary = new Dictionary<long, UserDTO>();
            var result = await connection.QueryAsync<UserDTO, CharacterDTO, UserDTO>(
                query,
                (user, character) =>
                {
                    if (!userDictionary.TryGetValue(user.UserId, out var currentUser))
                    {
                        currentUser = user;
                        currentUser.Characters = new List<CharacterDTO>();
                        userDictionary.Add(currentUser.UserId, currentUser);
                    }

                    currentUser.Characters.Add(character);
                    return currentUser;
                },
                new { UserId = userId },
                splitOn: "CharacterId"
            );

            return result.FirstOrDefault();
        }
    }

    public async Task<UserDTO> Add(string userName, string nickName, string userEmail, string userPassword)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userPassword))
        {
            throw new ArgumentException("O nome de usuário e a senha são obrigatórios para a criação de um usuário.");
        }

        // Hash da senha
        string hashedPassword = HashAndSaltPassword(userPassword);

        string query = @"INSERT INTO Users (UserName, NickName, UserEmail, UserPassword)
                     VALUES (@UserName, @NickName, @UserEmail, @UserPassword);
                     SELECT SCOPE_IDENTITY();";

        using (var connection = _dapperContext.CreateConnection())
        {
            // Execute o comando SQL e obtenha o ID do usuário recém-adicionado
            long newUserId = await connection.ExecuteScalarAsync<long>(query, new
            {
                UserName = userName,
                NickName = nickName,
                UserEmail = userEmail,
                UserPassword = hashedPassword
            });

            // Retorne o DTO com o ID gerado
            return new UserDTO
            {
                UserId = newUserId,
                UserName = userName,
                NickName = nickName,
                UserEmail = userEmail
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

    public async Task<UserDTO> Remove(long userId)
    {
        string query = "DELETE FROM Users WHERE UserId = @UserId";

        using (var connection = _dapperContext.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { UserId = userId });
        }

        return null; // Você pode retornar o usuário removido se necessário
    }

    public async Task<UserDTO> Authenticate(string username, string password)
    {
        string hashedPassword = HashAndSaltPassword(password);

        string query = "SELECT * FROM Users WHERE UserName = @UserName AND UserPassword = @UserPassword";

        using (var connection = _dapperContext.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<UserDTO>(query, new
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