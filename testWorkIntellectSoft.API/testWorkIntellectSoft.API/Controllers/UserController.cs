﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testWorkIntellectSoft.API.Data;
using testWorkIntellectSoft.API.Methods;
using testWorkIntellectSoft.API.Models.DTO;

namespace testWorkIntellectSoft.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAnswerDTO>>> GetUsers(int? page = null, string? search = null)
        {
            if (_context.Users == null)
                return NotFound();

            try
            {
                page = page ?? 0;
                var userWork = new UserListWork(_context);
                var maxPage = userWork.GetCountPages(search);
                if (page > maxPage)
                    throw new Exception("Страница за пределами поиска");

                var users = await userWork.GetUsers(page.Value, search);
                return (from a in users
                        select new UserAnswerDTO
                        {
                            CurentPage = page.Value,
                            TotalPage = maxPage,
                            Users = users
                        }).ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("edit")]
        public async Task<ActionResult<UserDTO>> GetUserByID(int? id = null)
        {
            if (_context.Users == null)
                return NotFound();

            try
            {
                if (id == null)
                    return new UserDTO();

                var userWork = new UserWork(_context);
                return await userWork.GetUser(id.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserDTO userStruct)
        {
            try
            {
                if (_context.Users == null)
                    throw new Exception("Нет подключения к базе данных");

                var userWork = new UserWork(_context);
                await userWork.CreateUser(userStruct);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UserDTO userStruct)
        {
            try
            {
                if (_context.Users == null)
                    throw new Exception("Нет подключения к базе данных");

                var userWork = new UserWork(_context);
                await userWork.EditUser(userStruct);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserStruct(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            try
            {
                var userWork = new UserWork(_context);
                await userWork.DeleteUser(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}