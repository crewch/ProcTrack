using AuthService.Data;
using AuthService.Models;

namespace AuthService.Data
{
    public class DataInitializer
    {
        public static void Initialize(AuthContext context)
        {
            if (context.Users.Any())
            {
                return;
            }
            
            var roles = new Role[]
            {
                new Role{Id=1, Title="analyst"},
                new Role{Id=2, Title="releaser"},
                new Role{Id=3, Title="admin"}
            };
            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }
            context.SaveChanges();

            var rights = new Right[]
            {
                new Right{Id=1, Title="modification"},
                new Right{Id=2, Title="reading"},
                new Right{Id=3, Title="deletion"},
                new Right{Id=4, Title="matching"},
                new Right{Id=5, Title="commenting"}
            };
            foreach (var right in rights)
            {
                context.Rights.Add(right);
            }
            context.SaveChanges();
            
            var groups = new Group[]
            {
                new Group{Id=1, Title="Технологический контроль", Description="Технологический контроль", BossId=1},
                new Group{Id=2, Title="Отделение клапанов", Description="Отделение клапанов", BossId=2}
            };
            foreach (var group in groups)
            {
                context.Groups.Add(group);
            }
            context.SaveChanges();

            var types = new Models.Type[]
            {
                new Models.Type{Id=1, Title="Process"},
                new Models.Type{Id=2, Title="Stage"}
            };
            foreach (var type in types)
            {
                context.Types.Add(type);
            }
            context.SaveChanges();

            var holds = new Hold[]
            {
                new Hold
                {
                    Id = 1,
                    DestId = 1,
                    Type = types[1],
                    Groups = new List<Group>() { groups[0] },
                    Rights = new List<Right>() { rights[0] }
                },
                new Hold
                {
                    Id = 2,
                    DestId = 2,
                    Type = types[1],
                    Groups = new List<Group>() { groups[1] },
                    Rights = new List<Right>() { rights[0] }
                },
                new Hold
                {
                    Id = 3,
                    DestId = 3,
                    Type = types[1],
                    Groups = new List<Group>() { groups[0] },
                    Rights = new List<Right>() { rights[0] }
                },
                new Hold
                {
                    Id = 4,
                    DestId = 4,
                    Type = types[1],
                    Groups = new List<Group>() { groups[1] },
                    Rights = new List<Right>() { rights[0] }
                }
            };
            foreach (var hold in holds)
            {
                context.Holds.Add(hold);
            }
            context.SaveChanges();

            var users = new User[]
            {
                new User
                {
                    Id = 1,
                    LongName = "Иван Иванов",
                    ShortName = "Иван",
                    Email = "ivan@ivan.com",
                    RefreshToken = "1234",
                    Roles = new List<Role>() { roles[0] },
                    Holds = new List<Hold>() { holds[0] },
                    Groups = new List<Group>() { groups[0] },
                },
                new User
                {
                    Id=2,
                    LongName="Сергей Сергеев",
                    ShortName="Сергей",
                    Email="sergey@sergey.com",
                    RefreshToken="1234",
                    Roles = new List<Role>() { roles[1] },
                    Holds = new List<Hold>() { holds[1] },
                    Groups = new List<Group>() { groups[0] },
                },
                new User
                {
                    Id=3,
                    LongName="Петр Петров",
                    ShortName="Петр",
                    Email="petr@petr.com",
                    RefreshToken="1234",
                    Roles = new List<Role>() { roles[2] },
                    Holds = new List<Hold>() { holds[2] },
                    Groups = new List<Group>() { groups[1] },
                },
                new User
                {
                    Id=4,
                    LongName="Савелий Савельев",
                    ShortName="Савелий",
                    Email="savely@savely.com",
                    RefreshToken="1234",
                    Roles = new List <Role>() { roles[0] },
                    Holds = new List <Hold>() { holds[3] },
                    Groups = new List <Group>() { groups[1] },
                }
            };
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
