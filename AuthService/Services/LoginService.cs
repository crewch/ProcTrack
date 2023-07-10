using AuthService.Data;
using AuthService.Dtos;
using AuthService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Novell.Directory.Ldap;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace AuthService.Services
{
    public class LoginService : ILoginService
    {
        private readonly AuthContext _context;
        private IConfiguration _configuration;

        public LoginService(AuthContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public bool ValidateUser(string domainName, string username, string password)
        {
            string userDn = $"{username}@{domainName}";
            try
            {
                using (var connection = new LdapConnection {SecureSocketLayer = false})
                {
                    connection.Connect(domainName, LdapConnection.DefaultPort);
                    connection.Bind(userDn, password);
                    if (connection.Bound)
                        return true;
                }
            }
            catch (LdapException ex)
            {
                // Log exception
            }
            return false;
        }

        public async Task<UserDto> Authorize(AuthDto data)
        {
            var user = _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.Email == data.Email)
                .FirstOrDefault();

            

            var dto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                LongName = user.LongName,
                ShortName = user.ShortName,
                Roles = user.Roles.Select(r => r.Title).ToList(),
            };
            return dto;
        }

        public async Task<EnvDto> GetEnv()
        {
            var res = new EnvDto
            {
                LDAP_HOST = _configuration["LDAP_HOST"],
                LDAP_PORT = int.Parse(_configuration["LDAP_PORT"]),
                LDAP_LOGIN = _configuration["LDAP_LOGIN"],
                LDAP_PASSWORD = _configuration["LDAP_PASSWORD"]
            };
            return res;
        }
    }
}
