using AuthService.Data;
using AuthService.Dtos;
using AuthService.Exceptions;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Novell.Directory.Ldap;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public async Task<TokenDto> Authorize(AuthDto data)
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

            //if (!ValidateUser(data.Username, data.Password))
            //    throw new UnauthorizedException($"User with name {data.Username} not registred");
            //}

            //var dto = new UserDto
            //{
            //    Id = user.Id,
            //    Email = user.Email,
            //    LongName = user.LongName,
            //    ShortName = user.ShortName,
            //    Roles = user.Roles.Select(r => r.Title).ToList(),
            //};
            //return dto;

            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            await _context.SaveChangesAsync();

            return new TokenDto
            {
                RefreshToken = refreshToken,
                AccessToken = GenerateAccessToken(user)
            };
        }

        public async Task<TokenDto> RefreshToken(TokenDto data)
        {
            var user = await _context.Users
                .Where(u => u.RefreshToken == data.RefreshToken)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            return new TokenDto
            {
                RefreshToken = user.RefreshToken,
                AccessToken = GenerateAccessToken(user)
            };
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

        private string GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRoleTitles = _context.Users
                .Where(u => u.Id == user.Id)
                .SelectMany(u => u.UserRoles)
                    .Select(ur => ur.Role.Title)
                .ToArray();

            var claims = new List<Claim>() 
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.LongName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.ShortName),
            };

            for (int i = 0; i < userRoleTitles.Count(); i++)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRoleTitles[i]));
            }

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims.ToArray(),
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            string token = Guid.NewGuid().ToString("N");
            return token;
        }
    }
}
