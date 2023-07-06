using AuthService.Data;
using AuthService.Dtos;
using AuthService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
//using System.DirectoryServices.AccountManagement;
//using System.DirectoryServices;
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

        public async Task<UserDto> Authorize(AuthDto data)
        {
            //const string LDAP_PATH = "EX://exldap.example.com:5555";
            //const string LDAP_DOMAIN = "exldap.example.com:5555";
            
            //using (var context = new PrincipalContext(ContextType.Domain, LDAP_DOMAIN, "service_acct_user", "service_acct_pswd"))
            //{
            //    if (context.ValidateCredentials(data.Email, data.Password)) {
            //        using (var de = new DirectoryEntry(LDAP_PATH))
            //        using (var ds = new DirectorySearcher(de)) {
            //            // other logic to verify user has correct permissions

            //            // User authenticated and authorized
            //            var identities = new List<ClaimsIdentity> { new ClaimsIdentity("custom auth type") };
            //            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), Options.Scheme);
            //            return Task.FromResult(AuthenticateResult.Success(ticket));
            //        }
            //    }
            //}

            var user = _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.Email == data.Email)
                .FirstOrDefault();
            // TODO: LDAP Password check
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

        //public async Task<UserToken> Login(UserLoginPasswordDto userLogin)
        //{
        //    var user = Authenticate(userLogin);

        //    if (user != null)
        //    {
        //        var token = Generate(user);
        //        var res = new UserToken { Email = userLogin.Email, Token = token };
        //        return  res;
        //    }

        //    return new UserToken();
        //}

        //private string Generate(User user)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var userRoleTitles = _context.Users
        //        .Where(u => u.Id == user.Id)
        //        .SelectMany(u => u.UserRoles)
        //            .Select(ur => ur.Role.Title)
        //        .ToArray();

        //    var claims = new List<Claim>() 
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.LongName),
        //        new Claim(ClaimTypes.Email, user.Email),
        //        new Claim(ClaimTypes.GivenName, user.ShortName),
        //    };
        //    for (int i = 0; i < userRoleTitles.Count(); i++)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, userRoleTitles[i]));
        //    }
        //    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
        //      _configuration["Jwt:Audience"],
        //      claims.ToArray(),
        //      expires: DateTime.Now.AddMinutes(15),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //private User Authenticate(UserLoginPasswordDto userLogin)
        //{
        //    var currentUser = _context.Users.FirstOrDefault(o => o.Email.ToLower() == userLogin.Email.ToLower() && o.Password == userLogin.Password);

        //    if (currentUser != null)
        //    {
        //        return currentUser;
        //    }
        //    return null;
        //}
    }
}
