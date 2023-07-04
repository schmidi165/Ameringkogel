﻿using Microsoft.AspNetCore.Mvc;
using UserManagement.Models.DTOs;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPut, Route("{id:Int}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO user)
        {
            user.Id = id;
            var result = await _userService.UpdateUser(user);
            return Ok(result);
        }
    }
}
