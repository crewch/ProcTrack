using AuthService.Data;
using AuthService.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public class UserService : IUserService
    {
        private readonly AuthContext _context;

        public UserService(AuthContext context)
        {
            _context = context;
        }

        public Task<UserDto> GetUserById(int id)
        {
            var user = _context.Users
                .Include(u => u.Roles)
                .Where(u => u.Id == id)
                .FirstOrDefault();

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                LongName = user.LongName,
                ShortName = user.ShortName,
                Roles = user.Roles.Select(r => r.Title).ToList(),
            };
            return Task.FromResult(userDto);
        }

        //public Task<UserWithRoles> GetUserByLogin(UserEmailDto data)
        //{
        //    var userWithRoles = _context.Users
        //        .Where(u => u.Email == data.Email)
        //        .Include(u => u.UserRoles)
        //            .ThenInclude(ur => ur.Role)
        //        .FirstOrDefault();
        //    var result = new UserWithRoles()
        //    {
        //        Id = userWithRoles.Id,
        //        Email = userWithRoles.Email,
        //        UserName = userWithRoles.LongName,
        //        Roles = userWithRoles.Roles.Select(ur => ur.Title).ToList()
        //    };
        //    return Task.FromResult(result);
        //}
    }
}
