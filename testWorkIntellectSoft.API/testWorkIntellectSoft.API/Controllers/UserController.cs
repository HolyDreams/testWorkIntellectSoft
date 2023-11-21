using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testWorkIntellectSoft.API.Data;
using testWorkIntellectSoft.API.Models.DTO;
using testWorkIntellectSoft.API.Services;

namespace testWorkIntellectSoft.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(int page = 0, string? search = null)
        {
            try
            {
                _userService.CheckNull();
                var maxPage = _userService.GetCountPages(search);
                if (page > maxPage)
                    throw new Exception("Страница за пределами поиска");

                var users = await _userService.GetUsersAsync(page, search);
                return Ok(_userService.GetAnswer(users, page, maxPage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> GetUserByID(int? id = null)
        {
            try
            {
                if (id == null)
                    return Ok(new UserDTO());
                _userService.CheckNull();

                return Ok(await _userService.GetUserByIdAsync(id.Value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDTO userStruct)
        {
            try
            {
                _userService.CheckNull();
                if (string.IsNullOrWhiteSpace(userStruct.FirstName) && string.IsNullOrWhiteSpace(userStruct.LastName))
                    throw new Exception("Попытка создать пользователя без имени");

                await _userService.CreateUserAsync(userStruct);

                var users = await _userService.GetUsersAsync();
                return Ok(_userService.GetAnswer(users));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDTO userStruct)
        {
            try
            {
                _userService.CheckNull();
                await _userService.EditUserAsync(userStruct);

                var users = await _userService.GetUsersAsync();
                return Ok(_userService.GetAnswer(users));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                _userService.CheckNull();
                await _userService.DeleteUserAsync(id);

                var users = await _userService.GetUsersAsync();
                return Ok(_userService.GetAnswer(users));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
