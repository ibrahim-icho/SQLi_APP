using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sqli_CRM_Web_Application.Context;
using Sqli_CRM_Web_Application.Models;
using Sqli_CRM_Web_Application.Requests;
using Sqli_CRM_Web_Application.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Numerics;
using System;

namespace Sqli_CRM_Web_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly ApplicationDbContext _authContext;
        public AuthController(IAuthService authService, ApplicationDbContext authContext)
        {
            _authService = authService;
            _authContext = authContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            return Ok(result);
        }

        [HttpPost("role")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            return Ok(await _authContext.Users.ToListAsync());
        }




     
      }
    }
