using AuthService.Data;
using AuthService.Dtos;
using AuthService.Exceptions;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;

using Novell.Directory.Ldap;

namespace AuthService.Services
{
    public class LoginService : ILoginService
    {
        private readonly AuthContext _context;
        private IConfiguration _configuration;
        private readonly string LDAP_HOST;
        private readonly int LDAP_PORT;

        public LoginService(AuthContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            LDAP_HOST = configuration["LDAP_HOST"];
            LDAP_PORT = int.Parse(configuration["LDAP_PORT"]);
        }

        private bool ValidateUser(string username, string password)
        {
            string domain = "irkut.com";

            try
            {
                using (var connection = new LdapConnection())
                {
                    connection.Connect(LDAP_HOST, LDAP_PORT);
                    connection.Bind(username + "@" + domain, password);
                    if (connection.Bound)
                        return true;
                }
            }
            catch (LdapException ex)
            {
                return false;
            }
            return false;
        }

        public async Task<UserDto> Authorize(AuthDto data)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.ShortName.ToLower() == data.Username.ToLower())
                .FirstOrDefaultAsync();
            
            if (user == null)
            {
                throw new NotFoundException($"User with name {data.Username} not found");
            }

            //if (ValidateUser(data.Username, data.Password))
            //{
                var dto = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    LongName = user.LongName,
                    ShortName = user.ShortName,
                    Roles = user.Roles.Select(r => r.Title).ToList(),
                };
                return dto;   
            //} else
            //{
            //    throw new UnauthorizedException($"User with name {data.Username} not registred");
            //}
        }
    }
}
