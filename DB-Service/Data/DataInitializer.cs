using DB_Service.Models;

namespace DB_Service.Data
{
    public class DataInitializer
    {
        public static void Initialize(DataContext context)
        {
            if (context.Processes.Any())
            {
                return;
            }
            
            var priorities = new Priority[]
            {
                new Priority{Id=1, Title="Средняя важность"},
            };
            foreach (var priority in priorities)
            {
                context.Priorities.Add(priority);
            }
            context.SaveChanges();

            var statuses = new Status[]
            {
                new Status{Id=1, Title="Не начат"},
                new Status{Id=2, Title="Отменен"},
                new Status{Id=3, Title="Отправлен на проверку"},
                new Status{Id=4, Title="Принят на проверку"},
                new Status{Id=5, Title="Согласовано-Блокировано"},
                new Status{Id=6, Title="Согласовано"},
                new Status{Id=7, Title="Остановлен"},
            };
            foreach (var stat in statuses)
            {
                context.Statuses.Add(stat);
            }
            context.SaveChanges();

            var types = new Models.Type[]
            {
                new Models.Type{Id=1, Title="КД"}
            };
            foreach (var type in types)
            {
                context.Types.Add(type);
            }
            context.SaveChanges();

            var tasks = new Models.Task[]
            {
                new Models.Task
                {
                    Id=1,
                    Title="СтИ",
                    ApprovedAt = DateTime.Now.AddDays(1),
                    ExpectedTime = TimeSpan.FromHours(2),
                    SignId = 1,
                },
                new Models.Task
                {
                    Id=2,
                    Title="Стоимость",
                    ExpectedTime = TimeSpan.FromDays(1)
                },
                new Models.Task
                {
                    Id=3,
                    Title="ГОСТ",
                    ApprovedAt = DateTime.Now.AddDays(2),
                    ExpectedTime = TimeSpan.FromHours(4)
                },
                new Models.Task
                {
                    Id=4,
                    Title="Прохождение по ТТХ",
                    ApprovedAt = DateTime.Now.AddDays(2),
                    ExpectedTime = TimeSpan.FromHours(4)
                },
            };
            foreach (var task in tasks)
            {
                context.Tasks.Add(task);
            }
            context.SaveChanges();

            

            var processes = new Process[]
            {
                new Process
                {
                    Id=1,
                    Title = "Шаблон КД",
                    Priority = priorities[0],
                    Type = types[0],
                    //Head = 1,
                    //Tail = 4,
                    CreatedAt = DateTime.Now,
                    IsTemplate = true,
                    ExpectedTime = TimeSpan.FromDays(1)
                }
            };
            foreach(var process in processes)
            {
                context.Processes.Add(process);
            }
            context.SaveChanges();

            var stages = new Stage[]
            {
                new Stage {Id=1, Title="Технологическое отделение", Addenable=false, Status=statuses[0], Process=processes[0]},
                new Stage {Id=2, Title="Финансовое отделение", Addenable=false, Status=statuses[0], Process=processes[0]},
                new Stage {Id=3, Title="Соответствие нормам", Addenable=false, Status=statuses[0], Process=processes[0]},
                new Stage {Id=4, Title="Опытные работы", Addenable=false, Status=statuses[0], Process=processes[0]},
            };
            foreach (var stage in stages)
            {
                context.Stages.Add(stage);
            }
            context.SaveChanges();

            var upPocess = context.Processes.Find(1);

            upPocess.Head = 1;
            upPocess.Tail = 4;
            context.SaveChanges();

            var edges = new Models.Edge[]
            {
                new Edge {StartStage=stages[0], EndStage=stages[1]},
                new Edge {StartStage=stages[0], EndStage=stages[2]},
                new Edge {StartStage=stages[1], EndStage=stages[3]},
            };
            foreach (var edge in edges)
            {
                context.Edges.Add(edge);
            }
            context.SaveChanges();

            var dependences = new Dependence[]
            {
                new Dependence {FirstStage=stages[1], SecondStage=stages[2]},
            };
            foreach (var dependence in dependences)
            {
                context.Dependences.Add(dependence);
            }
            context.SaveChanges();
        }
    }
}
