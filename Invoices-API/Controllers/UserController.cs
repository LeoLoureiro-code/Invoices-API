using Invoices_API.DataAccess.EF.DTO;
using Invoices_API.DataAccess.EF.Models;
using Invoices_API.DataAccess.EF.Repositories.Interfaces;
using Invoices_API.DataAccess.EF.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoices_API.Controllers
{
    [Route("Invoices/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public UserController(IUserRepository userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        [Authorize]
        [HttpGet("all-users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllusers()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                if (users == null || !users.Any())
                {
                    return NotFound("No users Found");
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    title: "An error occurred while fetching users.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpGet("find-by-id/")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);

                if (user == null)
                {
                    return NotFound(
                        new
                        {
                            Message = $"User with ID {id} was not found."

                        });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    title: "An error occurred while fetching user.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpPost("find-by-credentials")]

        public async Task<ActionResult<User>> GetUserByCredentials([FromBody] LoginDTO login)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByName(login.Email);

                if (existingUser == null)
                {
                    throw new Exception("User not found.");
                }

                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                return Problem(
                   detail: ex.Message,
                   title: "An error occurred while fetching user.",
                   statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("create-user")]
        public async Task<ActionResult> CreateUser([FromBody] UserDTO user)
        {


            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        Message = "Email and password are required."
                    });
                }
                var createdUser = await _userRepository.CreateUser(user);

                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.InnerException?.Message ?? ex.Message,
                    title: "An error occurred while creating the user.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }

        }

        [Authorize]
        [HttpPut("update-user/{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                if (user.Id != 0 && user.Id != id)
                    return BadRequest("User ID in the body does not match URL.");

                if (string.IsNullOrWhiteSpace(user.Email))
                    return BadRequest("Email cannot be empty.");


                var existingUser = await _userRepository.GetUserById(id);
                if (existingUser == null)
                    return NotFound($"User with ID {id} not found.");


                if (!string.IsNullOrWhiteSpace(user.Password) &&
                    user.Password != existingUser.Password)
                {

                    existingUser.Password = _passwordService.HashPassword(user.Password);
                }

                existingUser.Email = user.Email;

                await _userRepository.UpdateUser(existingUser.Id, existingUser.Email, existingUser.Password);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    title: "An error occurred while updating the user.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpDelete("delete-user/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                await _userRepository.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    title: "An error occurred while deleting the user.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
