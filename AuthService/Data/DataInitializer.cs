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

            var users = new User[]
            {
                new User{Id=1, LongName="Иван Иванов", ShortName="Иван", Email="ivan@ivan.com", Password="1234"},
                new User{Id=2, LongName="Сергей Сергеев", ShortName="Сергей", Email="sergey@sergey.com", Password="1234"},
                new User{Id=3, LongName="Петр Петров", ShortName="Петр", Email="petr@petr.com", Password="1234"},
                new User{Id=4, LongName="Савелий Савельев", ShortName="Савелий", Email="savely@savely.com", Password="1234"}
            };
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();

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
                new Right{Id=3, Title="deletion"}
            };
            foreach (var right in rights)
            {
                context.Rights.Add(right);
            }
            context.SaveChanges();

            var groups = new Group[]
            {
                new Group{Id=1, Title="Технологический контроль", Description="Технологический контроль"},
                new Group{Id=2, Title="Отделение клапанов", Description="Отделение клапанов"}
            };
            foreach (var group in groups)
            {
                context.Groups.Add(group);
            }
            context.SaveChanges();

            var usergroupmappers = new UserGroupMapper[]
            {
                new UserGroupMapper{Id=1, UserId=1, GroupId=1, IsBoss=true},
                new UserGroupMapper{Id=2, UserId=4, GroupId=1, IsBoss=false},
                new UserGroupMapper{Id=3, UserId=2, GroupId=2, IsBoss=true},
                new UserGroupMapper{Id=4, UserId=3, GroupId=2, IsBoss=false}
            };
            foreach (var usergroup in usergroupmappers)
            {
                context.UserGroupMappers.Add(usergroup);
            }
            context.SaveChanges();

            var userrolemappers = new UserRoleMapper[]
            {
                new UserRoleMapper{Id=1, UserId=1, RoleId=1},
                new UserRoleMapper{Id=2, UserId=2, RoleId=2},
                new UserRoleMapper{Id=3, UserId=3, RoleId=3}
            };
            foreach (var userrole in userrolemappers)
            {
                context.UserRoleMappers.Add(userrole);
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
                new Hold{Id=1, DestId=1, TypeId=2},
                new Hold{Id=2, DestId=2, TypeId=2},
                new Hold{Id=3, DestId=3, TypeId=2},
                new Hold{Id=4, DestId=4, TypeId=2},
            };
            foreach (var hold in holds)
            {
                context.Holds.Add(hold);
            }
            context.SaveChanges();

            var rightholdmappers = new RightHoldMapper[]
            {
                new RightHoldMapper{Id=1, HoldId=1, RightId=1},
                new RightHoldMapper{Id=2, HoldId=2, RightId=1},
                new RightHoldMapper{Id=3, HoldId=3, RightId=1},
                new RightHoldMapper{Id=4, HoldId=4, RightId=1}
            };
            foreach (var righthold  in rightholdmappers)
            {
                context.RightHoldMappers.Add(righthold);
            }
            context.SaveChanges();

            var groupholdmappers = new GroupHoldMapper[]
            {
                new GroupHoldMapper{Id=1, HoldId=1, GroupId=1},
                new GroupHoldMapper{Id=2, HoldId=2, GroupId=1},
                new GroupHoldMapper{Id=3, HoldId=3, GroupId=1},
                new GroupHoldMapper{Id=4, HoldId=4, GroupId=1},
            };
        }
    }
}
