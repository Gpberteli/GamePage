using JogoRpg.Data.Context;
using JogoRpg.Domain.DTO;
using JogoRpg.Domain.Entities;
using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Interface.Repositories;
using JogoRpg.Domain.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using static Dapper.SqlMapper;

namespace JogoRpg.Data.Repositories;

public class CharacterRepository : BaseRepository<CharacterDTO>, ICharacterRepository
{
    private readonly ILogger<CharacterRepository> _logger;
    private readonly EntityContext _context;
    private readonly DapperContext _dapperContext;

    public CharacterRepository(EntityContext context, DapperContext dapperContext, ILogger<CharacterRepository> logger) : base(context, dapperContext)
    {
        _logger = logger;
        _context = context;
        _dapperContext = dapperContext;
    }

    public async Task<IEnumerable<CharacterDTO>> Get()
    {
        string query = @"select 
                                o.CharId,
                                o.CharName,
                                o.CharClass,
                                o.CharSex,
                                o.ClassId,
                                o.UserId  
                                from Charact o with (nolock)";
        using (var connection = _dapperContext.CreateConnection())
        {
            return await connection.QueryAsync<CharacterDTO>(query);
        }

    }

    public override async Task<CharacterDTO> Get(long charId)
    {
        string query = @"select 
                                o.CharId,
                                o.CharName,
                                o.CharClass,
                                o.CharSex,
                                o.ClassId,
                                o.UserId
                                from Charact o with (nolock)
                                Where CharId = @CharId";

        using (var connection = _dapperContext.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<CharacterDTO>(query, new { CharId = charId });
        }
    }

    private void ValidateCharacterInputs(CharacterDTO character)
    {
        if (string.IsNullOrWhiteSpace(character.CharName))
        {
            throw new ArgumentException("Character name cannot be null or empty.", nameof(character.CharName));
        }

    }
    private Type GetCharacterClassType(CharacterClassType classType)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes()
            .Where(type => type.IsClass && type.GetCustomAttribute<CharacterClassAttribute>()?.Type == classType)
            .ToList();

        return types.FirstOrDefault();
    }

    private CharactersInfo GetCharacterClassByType(CharacterClassType classType)
    {
        Type characterClassType = GetCharacterClassType(classType);

        if (characterClassType != null)
        {
            var instance = Activator.CreateInstance(characterClassType);
            return (CharactersInfo) instance;
        }

        return null;
    }
    public async Task<CharacterDTO> CreateCharacter(long userId, CharacterDTO character)
    {
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                if (character == null)
                {
                    throw new ArgumentNullException(nameof(character), "Objeto de personagem não pode ser nulo.");
                }

                // Tenta encontrar um usuário com o userId especificado no banco de dados.
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    throw new InvalidOperationException($"Usuário com ID {userId} não encontrado.");
                }

                // Chama o método de validação para garantir que as entradas do personagem sejam válidas.
                ValidateCharacterInputs(character);

                // Define o status do personagem e chama a classe (por exemplo, 1 = Assassin)
                character.UserId = userId;
                var characterClassType = GetCharacterClassType(character.ClassType);
                var characterInfo = Activator.CreateInstance(characterClassType) as CharactersInfo;
                characterInfo.InitializeStats();
                character.CharStatus = new CharacterInfosDTO
                {
                    Strong = characterInfo.Strong,
                    Speed = characterInfo.Speed,
                    Vitality = characterInfo.Vitality,
                    Intelligence = characterInfo.Intelligence,
                    Dexterity = characterInfo.Dexterity,
                    Stamina = characterInfo.Stamina,
                    Description = characterInfo.Description
                };

                // Use comandos SQL para inserir o personagem
                string insertQuery = @"INSERT INTO Charact (CharName, CharClass, CharSex, ClassId, UserId)
                                   VALUES (@CharName, @CharClass, @CharSex, @ClassId, @UserId);
                                   SELECT SCOPE_IDENTITY();";

                using (var connection = _dapperContext.CreateConnection())
                {
                    // Execute o comando SQL e obtenha o ID do personagem recém-criado
                    var parameters = new
                    {
                        CharName = character.CharName,
                        CharClass = character.ClassType.ToString(),
                        CharSex = character.CharSex.ToString(),
                        ClassId = character.ClassId,
                        UserId = character.UserId
                    };

                    long newCharId = await connection.ExecuteScalarAsync<long>(insertQuery, parameters);

                    // Atribua o ID gerado ao objeto Character
                    character.CharId = newCharId;
                }

                // Não é necessário chamar transaction.Commit() explicitamente, pois o using já faz isso ao sair do bloco.

                return character;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "Erro em criar personagem.");
                throw;
            }
        }
    }

    public virtual async Task<CharacterDTO> Update(CharacterDTO character)
    {
        if (character == null)
        {
            throw new ArgumentNullException(nameof(character), "Objeto de personagem para atualização não pode ser nulo.");
        }
        string query = @" Update Charact
                              Set CharName = @CharName,                                  
                                  CharClass = @CharClass,
                                  CharSex = @CharSex,
                                  ClassId = @ClassId,
                                  UserId = @UserId
                                  Where CharId = @CharId";

        using (var connection = _dapperContext.CreateConnection())
        {
            await connection.ExecuteAsync(query, new
            {
                CharName = character.CharName,
                CharClass = character.ClassType.ToString(),
                CharSex = character.CharSex.ToString(), // Converte a enumeração para string
                ClassId = character.ClassId,
                UserId = character.UserId,
                CharId = character.CharId
            });
            return character;
        }
    }


    public async Task<CharacterDTO> Remove(CharacterDTO character)
    {
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                // Utilize comandos SQL para remover o personagem
                string deleteQuery = "DELETE FROM Charact WHERE CharId = @CharId";

                using (var connection = _dapperContext.CreateConnection())
                {
                    await connection.ExecuteAsync(deleteQuery, new { CharId = character.CharId });
                }

                transaction.Commit();
                return character;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }
}
