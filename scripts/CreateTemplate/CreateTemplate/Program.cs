// See https://aka.ms/new-console-template for more information

using CreateTemplate;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
class Program
{
    static void Main(string[] args)
    {
        var allowType = new List<string> { "ИИ" };
        var allowGroup = new List<GroupDto>
            {
                new GroupDto
                {
                    Id = 4,
                    Title = "Отдел управления технологичностью изделия"
                },
                new GroupDto
                {
                    Id = 5,
                    Title = "Отдел управления электронными макетами изделий"
                },
                new GroupDto
                {
                    Id = 6,
                    Title = "Отдел управления проектными данными"
                },
                new GroupDto
                {
                    Id = 7,
                    Title = "Отдел управления конфигурацией"
                },
                new GroupDto
                {
                    Id = 8,
                    Title = "Отделение электронного макета и конфигураций самолетов"
                }
            };

        var newTemplate = new TemplateDto();
        newTemplate.Process = new ProcessDto();
        newTemplate.Stages = new List<StageDto>();
        newTemplate.Links = new LinkDto();
        newTemplate.Tasks = new List<TaskDto>();


        Console.WriteLine("Введите название шаблона:");
        newTemplate.Process.Title = Console.ReadLine().ToString();
        Console.WriteLine("Выберите тип КД:");
        for (int i = 0; i < allowType.Count; i++)
        {
            Console.WriteLine($"{i}: {allowType[i]}");
        }
        int choose = int.Parse(Console.ReadLine().ToString());
        newTemplate.Process.Type = allowType[choose];

        newTemplate.Process.ExpectedTime = TimeSpan.Parse("7.00:00:00.0"); // TODO: сделать поинтереснее

        Console.WriteLine("Введите число этапов шаблона:");
        int n = int.Parse(Console.ReadLine().ToString());
        for (int i = 1; i <= n; i++)
        {
            var Stage = new StageDto();
            Stage.Holds = new List<HoldDto>();
            Stage.Id = i;

            Console.WriteLine("Введите название этапа:");
            Stage.Title = Console.ReadLine().ToString();

            Console.WriteLine("Пометить этап если он может быть добавляем (y/n):");
            if (Console.ReadLine().ToString().ToLower() == "y")
            {
                Stage.Mark = true;
            }
            else
            {
                Stage.Mark = false;
            }
            Console.WriteLine("Пропускать автоматически? (y/n):");
            if (Console.ReadLine().ToString().ToLower() == "y")
            {
                Stage.Pass = true;
            }
            else
            {
                Stage.Pass = false;
            }
            Console.WriteLine("Выберите назначенную группу:");
            for (int j = 0; j < allowGroup.Count; j++)
            {
                Console.WriteLine($"{j}: {allowGroup[j].Title}");
            }
            choose = int.Parse(Console.ReadLine().ToString());
            Stage.Holds.Add(new HoldDto
            {
                DestId = i,
                Type = "Stage",
                Groups = new List<GroupDto>()
                {
                    allowGroup[choose]
                }
            });

            newTemplate.Stages.Add(Stage);

            Console.WriteLine("Выберите число заданий этапа:");
            int m = int.Parse(Console.ReadLine().ToString());
            for (int j = 1; j <= m; j++)
            {
                Console.WriteLine("Введите имя задания:");
                string title = Console.ReadLine().ToString();
                var task = new TaskDto
                {
                    Id = j,
                    ExpectedTime = TimeSpan.Parse("2.00:00:00.0"),
                    Title = title,
                    StageId = i};
                newTemplate.Tasks.Add(task);
            }
        }

        Console.WriteLine("Выберите в каких этапах может редактировать другие этапы главный согласующий:");
        for (int i = 1; i <= n; i++)
        {
            Console.Write($"{i}: {newTemplate.Stages[i - 1].Title}; ");
        }
        Console.WriteLine("");

        for (int i = 1; i <= n; i++)
        {
            var listStages = new List<int>();
            Console.WriteLine("Сколько значений будет введено?");
            choose = int.Parse(Console.ReadLine().ToString());
            for (int j = 1; j <= choose; j++)
            {
                listStages.Add(int.Parse(Console.ReadLine().ToString()));
            }
        }
        Console.WriteLine("Выберете начальный этап:");
        newTemplate.StartStage = int.Parse(Console.ReadLine());
        Console.WriteLine("Выберете конечный этап:");
        newTemplate.EndStage = int.Parse(Console.ReadLine());

        Console.WriteLine("Сколько связей будет в маршруте:");
        n = int.Parse(Console.ReadLine().ToString());
        Console.WriteLine("Введите пары ключ-значение:");
        for (int i = 1; i <= n; i++)
        {
            newTemplate.Links.Edges.Add(new Tuple<int, int>(int.Parse(Console.ReadLine().ToString()), int.Parse(Console.ReadLine().ToString())));
        }

        Console.WriteLine("Сколько зависимостей будет в маршруте:");
        n = int.Parse(Console.ReadLine().ToString());
        Console.WriteLine("Введите пары ключ-значение:");
        for (int i = 1; i <= n; i++)
        {
            newTemplate.Links.Edges.Add(new Tuple<int, int>(int.Parse(Console.ReadLine().ToString()), int.Parse(Console.ReadLine().ToString())));
        }

        string json = JsonConvert.SerializeObject(newTemplate);
        Console.WriteLine(json);
    }
}

