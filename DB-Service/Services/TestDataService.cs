using DB_Service.Data;
using DB_Service.Models;

namespace DB_Service.Services
{
    public class TestDataService : ITestDataService
    {
        private readonly DataContext _context;

        public TestDataService(DataContext context)
        {
            _context = context;
        }

        public System.Threading.Tasks.Task CreateTestData()
        {
            if (_context.Types.Any())
            {
                return System.Threading.Tasks.Task.CompletedTask;
            }

            var priorities = new Priority[]
            {
               new Priority { Title="Высокая важность" },
               new Priority { Title="Средняя важность" },
               new Priority { Title="Низкая важность" }
            };
            foreach (var priority in priorities)
            {
               _context.Priorities.Add(priority);
            }
            _context.SaveChanges();

            var statuses = new Status[]
            {
               new Status { Title="Не начат", Value=2 },
               new Status { Title="Отменен", Value=0 },
               new Status { Title="Отправлен на проверку", Value=3 },
               new Status { Title="Принят на проверку", Value=3 },
               new Status { Title="Согласовано-Блокировано", Value=3 },
               new Status { Title="Согласовано", Value=3 },
               new Status { Title="Остановлен", Value=1 },
            };
            foreach (var stat in statuses)
            {
               _context.Statuses.Add(stat);
            }
            _context.SaveChanges();

            var types = new Models.Type[]
            {
               new Models.Type { Title="КД" },
               new Models.Type { Title="ИИ" }
            };
            foreach (var type in types)
            {
               _context.Types.Add(type);
            }
            _context.SaveChanges();

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
