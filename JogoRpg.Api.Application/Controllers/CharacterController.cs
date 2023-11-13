using JogoRpg.Api.Application.Models;
using JogoRpg.Domain.Entities;
using JogoRpg.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace JogoRpg.Api.Application.Controllers;

[ApiController]
    [Route("v1/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

    /// <summary>
    /// Retorna Tabela de personagens
    /// </summary>
    /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var characters = await _characterService.Get();
                return Ok(new ResultResponse { Success = true, Data = characters });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultResponse { Error = ex.Message });
            }
        }

    /// <summary>
    /// Retorna o personagem pelo id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var character = await _characterService.Get(id);
                return Ok(new ResultResponse { Success = true, Data = character });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultResponse { Error = ex.Message });
            }
        }

    /// <summary>
    /// Cria Personagem
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="character"></param>
    /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> CreateCharacter(long userId, [FromBody] Character character)
    {
        var createdCharacter = await _characterService.CreateCharacter(userId, character);

        if (createdCharacter != null)
        {
            return Ok(createdCharacter);
        }

        return BadRequest("Usuário não encontrado."); 
    }

    /// <summary>
    /// Atualiza Personagem
    /// </summary>
    /// <param name="id"></param>
    /// <param name="character"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Character character)
        {
            try
            {
                character.CharId = id;
                var updatedCharacter = await _characterService.Update(character);
                return StatusCode(StatusCodes.Status202Accepted, new ResultResponse { Success = true, Data = updatedCharacter });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultResponse { Error = ex.Message });
            }
        }

    /// <summary>
    /// Deleta personagem
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var character = await _characterService.Get(id);
                if (character == null)
                    return NotFound();

                await _characterService.Remove(character);
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultResponse { Success = false, Error = ex.Message });
            }
        }
    }