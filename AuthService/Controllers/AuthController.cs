using AuthService.Data;
using AuthService.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using AuthService.Dtos;

namespace AuthService.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [EnableCors("cors")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _service;
        
        // debug
        private readonly AuthContext _context;

        public AuthController(ILoginService service, AuthContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpPost]
        public async Task<UserToken> Login(UserLoginPasswordDto userLogin)
        {
            var res = await _service.Login(userLogin);
            return res;
        }

        [Route("debug")]
        [HttpGet]
        public IActionResult GetUsersWithRoles()
        {
            var usersWithRoles = _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ToList();

            var result = new List<UserWithRoles>();

            foreach (var user in usersWithRoles)
            {
                var dto = new UserWithRoles()
                {
                    Id = user.Id,
                    UserName = user.LongName,
                    Email = user.Email,
                    Roles = user.UserRoles.Select(ur => ur.Role.Title).ToList()
                };

                result.Add(dto);
            }

            return Ok(result);
        }
    }
}
