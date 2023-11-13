using JogoRpg.Data.Context;
using JogoRpg.Domain.Entities;
using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Interface.Repositories;
using JogoRpg.Domain.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using static Dapper.SqlMapper;

namespace JogoRpg.Data.Repositories;

public class CharacterRepository : BaseRepository<Character>, ICharacterRepository
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

    public async Task<Character> CreateCharacter(long userId, Character character)
    {
        try
        {
            // Tenta encontrar um usuário com o userId especificado no banco de dados.
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                //Chama o método de validação para garantir que as entradas do personagem sejam válidas.
                ValidateCharacterInputs(character);

                //Define o status do personagem e chama a classe Ex:( 1 = Assassin)
                character.UserId = userId;
                var characterClassType = GetCharacterClassType(character.ClassType);
                var characterInfo = Activator.CreateInstance(characterClassType) as CharactersInfo;
                characterInfo.InitializeStats();
                character.CharStatus = characterInfo;

                _context.Characters.Add(character);
                await _context.SaveChangesAsync();

                return character;
            }

            return null;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Erro em criar personagem.");
            throw;
        }

    }

    private void ValidateCharacterInputs(Character character)
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

    public async Task<IEnumerable<Character>> Get()
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
            return await connection.QueryAsync<Character>(query);
        }

    }

    public override async Task<Character> GetAsync(long charId)
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
            return await connection.QueryFirstOrDefaultAsync<Character>(query, new { CharId = charId });
        }
    }

    public virtual async Task<Character> UpdateAsync(Character character)
    {
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


    public async Task<Character> RemoveAsync(Character character)
    {
        string query = @" Delete From Charact Where CharId = @CharId";


        using (var connection = _dapperContext.CreateConnection())
        {
            await connection.ExecuteAsync(query, character);
            return character;
        }
    }

}
