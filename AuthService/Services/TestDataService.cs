using AuthService.Data;
using AuthService.Exceptions;
using AuthService.Models;

namespace AuthService.Services
{
    public class TestDataService : ITestDataService
    {
        private readonly AuthContext _context;

        public TestDataService(AuthContext context)
        {
            _context = context;
        }

        public Task CreateTestData()
        {
            if (_context.Types.Any())
            {
                throw new ConflictException($"data is already exists");
            }

            var roles = new Role[]
            {
                new Role{ Title="analyst" },
                new Role{ Title="releaser" },
                new Role{ Title="admin" },
                new Role{ Title="observer" },
                new Role{ Title="inner-observer" },
                new Role{ Title="checker" }
            };

            foreach (var role in roles)
            {
                _context.Roles.Add(role);
            }
            _context.SaveChanges();

            var rights = new Right[]
            {
                new Right{ Title="modification" },
                new Right{ Title="reading" },
                new Right{ Title="deletion" },
                new Right{ Title="matching" },
                new Right{ Title="commenting" }
            };

            foreach (var right in rights)
            {
                _context.Rights.Add(right);
            }
            _context.SaveChanges();

            var types = new Models.Type[]
            {
                new Models.Type { Title="Process" },
                new Models.Type { Title="Stage" }
            };

            foreach (var type in types)
            {
                _context.Types.Add(type);
            }
            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}

