using AuthService.Data;
using AuthService.Dtos;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

//         public class LdapAuthenticationService : IAuthenticationService  
// {  
//     private const string DisplayNameAttribute = "DisplayName";  
//     private const string SAMAccountNameAttribute = "SAMAccountName";  
      
//     private readonly LdapConfig config;  
          
//     public LdapAuthenticationService(IOptions<LdapConfig> config)  
//     {  
//         this.config = config.Value;  
//     }  
//     public User Login(string userName, string password)  
//     {  
//         try  
//         {  
//             using (DirectoryEntry entry = new DirectoryEntry(config.Path, config.UserDomainName + "\\" + userName, password))  
//             {  
//                 using (DirectorySearcher searcher = new DirectorySearcher(entry))  
//                 {  
//                     searcher.Filter = String.Format("({0}={1})", SAMAccountNameAttribute, userName);  
//                     searcher.PropertiesToLoad.Add(DisplayNameAttribute);  
//                     searcher.PropertiesToLoad.Add(SAMAccountNameAttribute);  
//                     var result = searcher.FindOne();  
//                     if (result != null)  
//                     {  
//                         var displayName = result.Properties[DisplayNameAttribute];  
//                         var samAccountName = result.Properties[SAMAccountNameAttribute];  
                          
//                         return new User  
//                         {  
//                             DisplayName = displayName == null || displayName.Count <= 0 ? null : displayName[0].ToString(),  
//                             UserName = samAccountName == null || samAccountName.Count <= 0 ? null : samAccountName[0].ToString()  
//                         };  
//                     }  
//                 }  
//             }  
//         }  
//         catch (Exception ex)  
//         {  
//             // if we get an error, it means we have a login failure.  
//             // Log specific exception  
//         }  
//         return null;  
//     }  
// }  
    }
}
