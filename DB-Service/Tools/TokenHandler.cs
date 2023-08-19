using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DB_Service.Tools
{
    public class TokenHandler
    {
        public static int GetIdFromToken(string? token)
        {
            if (token == null)
            {
                return -1;
            }

            var handler = new JwtSecurityTokenHandler();
            var parsedToken = handler.ReadToken(token) as JwtSecurityToken;

            bool validated = parsedToken.ValidTo > DateTime.Now;

            if (!validated)
            {
                return -1;
            }

            int UserId = int.Parse(parsedToken.Claims
                .Where(c => c.Type == ClaimTypes.Sid)
                .FirstOrDefault()?.ToString().Split(" ")[1]);

            return UserId;
        }
    }
}
