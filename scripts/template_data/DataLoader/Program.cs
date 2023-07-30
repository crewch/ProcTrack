using TemplateCreate.Dtos;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using NPOI.POIFS.Crypt.Dsig;
using NPOI.SS.Formula.Functions;
using System.Net.Http.Json;

// C:\developing\practice_data\1.xlsx

namespace TemplateCreate
{
    internal class Program
    {
        static List<int> ParseStringToList(string input)
        {
            // Разделяем строку на отдельные значения по запятой и пробелу
            string[] stringArray = input.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            List<int> numbersList = new List<int>();

            foreach (string str in stringArray)
            {
                // Преобразуем каждое значение в int и добавляем его в список
                int number;

                if (int.TryParse(str, out number))
                {
                    if (number != 0)
                    {
                        numbersList.Add(number);
                    }
                }
            }

            return numbersList;
        }
        // static async Task<List<GroupDto>> GetGroups()
        // {
        //     var apiUrl = "http://localhost:8000/api/auth/user/group";

        //     try
        //     {
        //         using var httpClient = new HttpClient();
        //         var json = await httpClient.GetStringAsync(apiUrl);
        //         var data = JsonSerializer.Deserialize<List<GroupDto>>(json);
                
        //         List<GroupDto> groups = await response.Content.ReadAsByteArrayAsync<List<GroupDto>>();

        //         return groups;
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
        //         // Логирование ошибки
        //         // logger.LogError(ex, $"Ошибка при выполнении запроса: {ex.Message}");
        //         return null;
        //     }

        // }
        static async Task RunAsync()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:8003/user/groups");
            var resJson = await response.Content.ReadAsStringAsync();
            var allowGroup = JsonConvert.DeserializeObject<List<GroupDto>>(resJson);
            //Console.WriteLine(groupDtos[0].Id);
            //var allowGroup = new List<GroupDto>()
            //{
            //    new GroupDto()
            //    {
            //        Id = 0,
            //        Title = "Первая группа"
            //    }
            //};
            var allowType = new List<string> { "ИИ" };

            var newTemplate = new TemplateDto();
            newTemplate.Process = new ProcessDto();
            newTemplate.Stages = new List<StageDto>();
            newTemplate.Links = new LinkDto();
            newTemplate.Tasks = new List<TaskDto>();


            //string path = "C:\\developing\\template_data\\1.xlsx";
            string path = Console.ReadLine();
            IWorkbook workbook;

            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }
            ISheet process = workbook.GetSheetAt(0);

            IRow rowProcess = process.GetRow(1);

            if (rowProcess.GetCell(0) == null || rowProcess.GetCell(1) == null || rowProcess.GetCell(2) == null || rowProcess.GetCell(3) == null || rowProcess.GetCell(4) == null)
            {
                Console.WriteLine("Вы неправильно заполнили лист процесса!");
                return;
            }

            newTemplate.Process.Title = rowProcess.GetCell(0).StringCellValue;
            newTemplate.Process.Type = rowProcess.GetCell(1).StringCellValue;
            newTemplate.Process.ExpectedTime = TimeSpan.Parse(rowProcess.GetCell(2).StringCellValue);
            

            newTemplate.StartStage = (int) rowProcess.GetCell(3).NumericCellValue;
            newTemplate.EndStage = (int) rowProcess.GetCell(4).NumericCellValue;

            ISheet stages = workbook.GetSheetAt(1);

            IRow rowId = stages.GetRow(0);
            for (int i = 1; i < rowId.LastCellNum; i++)
            {
                if (rowId.GetCell(0) == null || rowId.GetCell(0).CellType == CellType.Blank) 
                {
                    break;
                }
                newTemplate.Stages.Add(new StageDto
                {
                    Id = (int)rowId.GetCell(i).NumericCellValue
                });
            }
            IRow rowTitle = stages.GetRow(1);
            for (int i = 1; i < rowTitle.LastCellNum; i++)
            {
                if (rowTitle.GetCell(0) == null || rowTitle.GetCell(0).CellType == CellType.Blank) 
                {
                    break;
                }
                newTemplate.Stages[i - 1].Title = rowTitle.GetCell(i).StringCellValue;
            }
            IRow rowGroup = stages.GetRow(2);
            for (int i = 1; i < rowGroup.LastCellNum; i++)
            {
                var tmpGroup = new GroupDto
                {
                    Id = 0
                };
                var newGroup = allowGroup.Where(
                    g => g.Title == rowGroup.GetCell(i).StringCellValue
                ).ToList();
                if (newGroup == null)
                {
                    Console.WriteLine($"для {i} столбца не существует группы");
                    return;
                }
                newTemplate.Stages[i - 1].Holds.Add(new HoldDto
                {
                    DestId = newTemplate.Stages[i - 1].Id,
                    Groups = newGroup
                });
            }
            IRow rowPass = stages.GetRow(3);
            for (int i = 1; i < rowPass.LastCellNum; i++)
            {
                newTemplate.Stages[i - 1].Pass = rowPass.GetCell(i).StringCellValue == "Да";
            }
            IRow rowCanCreate = stages.GetRow(4);
            for (int i = 1; i < rowCanCreate.LastCellNum; i++)
            {
                string content;
                if (rowCanCreate.GetCell(i).CellType == CellType.String)
                {
                    content = rowCanCreate.GetCell(i).StringCellValue;
                } else
                {
                    content = rowCanCreate.GetCell(i).NumericCellValue.ToString();
                }
                newTemplate.Stages[i - 1].CanCreate = ParseStringToList(content);
            }
            IRow rowMark = stages.GetRow(5);
            for (int i = 1; i < rowMark.LastCellNum; i++)
            {
                newTemplate.Stages[i - 1].Mark = rowMark.GetCell(i).StringCellValue == "Да";
            }
            IRow rowNext = stages.GetRow(6);
            for (int i = 1; i < rowNext.LastCellNum; i++)
            {
                if (rowNext.GetCell(0) == null || rowNext.GetCell(0).CellType == CellType.Blank) 
                {
                    continue;
                }
                string content;
                if (rowNext.GetCell(i).CellType == CellType.String)
                {
                    content = rowNext.GetCell(i).StringCellValue;
                } else
                {
                    content = rowNext.GetCell(i).NumericCellValue.ToString();
                }
                var listNext = ParseStringToList(content);
                foreach (var next in listNext)
                {
                    if (next != 0)
                    {
                        newTemplate.Links.Edges.Add(
                            new Tuple<int, int>(
                                newTemplate.Stages[i - 1].Id,
                                next
                            )
                        );
                    }
                }
            }
            IRow rowDep = stages.GetRow(7);
            for (int i = 1; i < rowDep.LastCellNum; i++)
            {
                if (rowDep.GetCell(0) == null || rowDep.GetCell(0).CellType == CellType.Blank) 
                {
                    continue;
                }
                string content;
                if (rowDep.GetCell(i).CellType == CellType.String)
                {
                    content = rowDep.GetCell(i).StringCellValue;
                } else
                {
                    content = rowDep.GetCell(i).NumericCellValue.ToString();
                }
                var listDep = ParseStringToList(content);
                foreach (var dep in listDep)
                {
                    if (dep != 0)
                    {
                        newTemplate.Links.Dependences.Add(
                            new Tuple<int, int>(
                                newTemplate.Stages[i - 1].Id,
                                dep
                            )
                        );
                    }
                }
            }
            int l = 8;
            while (true)
            {
                if (l > stages.LastRowNum)
                {
                    break;
                }
                IRow rowTask = stages.GetRow(l);
                if (rowTask.GetCell(0) == null || rowTask.GetCell(0).CellType == CellType.Blank) 
                {
                    break;
                }
                for (int j = 1; j < rowTask.LastCellNum; j++)
                {
                    if (rowTask.GetCell(j) == null || rowTask.GetCell(j).CellType == CellType.Blank) 
                    {
                        break;
                    }
                    newTemplate.Tasks.Add(new TaskDto
                    {
                        Id = j + l,
                        StageId = newTemplate.Stages[j - 1].Id,
                        Title = rowTask.GetCell(j - 1).StringCellValue
                    });
                }
                l++;
            }

            string json = JsonConvert.SerializeObject(newTemplate);
            Console.WriteLine(json);
            var con = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(newTemplate),
                Encoding.UTF8,
                "application/json");

            var resFromServer = await client.PostAsync($"http://localhost:8001/process/template/create", con);
            resJson = await resFromServer.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<TemplateDto>(resJson);
            Console.WriteLine();
            Console.WriteLine(res);
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            Task.Run(RunAsync).GetAwaiter().GetResult();
        }
    }
}