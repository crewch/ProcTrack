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
        private readonly LdapConnection _connection;

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
                var ldapServer = Environment.GetEnvironmentVariable("LDAP_HOST");
                if (int.TryParse(Environment.GetEnvironmentVariable("LDAP_PORT"), out int ldapPort)) {
                    using (var connection = new LdapConnection {SecureSocketLayer = false})
                    {
                        connection.Connect(ldapServer, ldapPort);
                        connection.Bind(userDn, password);
                        if (connection.Bound)
                            return true;
                    } 
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
    }
}
