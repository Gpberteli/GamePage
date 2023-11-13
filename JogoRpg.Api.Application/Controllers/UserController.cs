using JogoRpg.Api.Application.Models;
using JogoRpg.Domain.Entities;
using JogoRpg.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace JogoRpg.Api.Application.Controllers;

[ApiController]
    [Route("v1/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

    /// <summary>
    /// Retorna lista de usuários
    /// </summary>
    /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users =  _userService.Get();
                return Ok(new ResultResponse { Success = true, Data = users });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultResponse { Error = ex.Message });
            }
        }

    /// <summary>
    /// Retorna usuário pelo id
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
                var user = await _userService.Get(id);
                return Ok(new ResultResponse { Success = true, Data = user });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultResponse { Error = ex.Message });
            }
        }

    /// <summary>
    /// Cria usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                var newUser = await _userService.Add(user);
                return StatusCode(StatusCodes.Status201Created, new ResultResponse { Success = true, Data = newUser });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultResponse { Error = ex.Message });
            }
        }

    /// <summary>
    /// Atualiza usuário
    /// </summary>
    /// <param name="id"></param>
    /// <param name="user"></param>
    /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] User user)
        {
            try
            {
                user.UserId = id;
                var updatedUser = await _userService.Update(user);
                return StatusCode(StatusCodes.Status202Accepted, new ResultResponse { Success = true, Data = updatedUser });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultResponse { Error = ex.Message });
            }
        }

    /// <summary>
    /// Deleta Usuario
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
                var user = await _userService.Get(id);
                if (user == null)
                    return NotFound();

                await _userService.Remove(user);
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultResponse { Success = false, Error = ex.Message });
            }
        }
    }







