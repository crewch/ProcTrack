using DataLoader.Dtos;
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

// C:\developing\practice_data\1.xlsx

namespace DataLoader
{
    internal class Program
    {

        static async Task RunAsync()
        {
            var client = new HttpClient();

            string path = Console.ReadLine();
            IWorkbook workbook;

            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(fileStream);
            }

            ISheet users = workbook.GetSheetAt(0);

            var rowCount = users.LastRowNum + 1;

            for (int i = 0; i < rowCount; i++)
            {
                IRow row = users.GetRow(i);
                if (row.GetCell(0) == null || row.GetCell(0).CellType == CellType.Blank) 
                {
                    break;
                }
                var count = row.LastCellNum;
                var data = new UserDto
                {
                    Id = 0,
                    LongName = row.GetCell(0).StringCellValue,
                    ShortName = row.GetCell(1).StringCellValue,
                    Email = row.GetCell(1).StringCellValue + "@irkut.com",
                    Roles = new List<string>(),
                };

                for (int j = 2; j < count; j++)
                {
                    var cell = row.GetCell(j);
                    if (cell != null && cell.CellType != CellType.Blank)
                    {
                        data.Roles.Add(row.GetCell(j).StringCellValue);
                    }
                }
                
                Console.WriteLine($"id: {data.Id}");
                Console.WriteLine($"email: {data.Email}");
                Console.WriteLine($"LongName: {data.LongName}");
                Console.WriteLine($"ShortName: {data.ShortName}");
                Console.WriteLine($"Roles:");
                for (int j = 0; j < data.Roles.Count; j++)
                {

                    Console.WriteLine(data.Roles[j]);
                }


                var content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(data),
                    Encoding.UTF8,
                   "application/json");
                HttpResponseMessage response = await client.PostAsync("http://localhost:8000/api/auth/user/create", content);
            }

            ISheet groups = workbook.GetSheetAt(1);

            var rowCount2 = groups.LastRowNum + 1;


            for (int i = 0 ; i < rowCount2; ++i)
            {
                IRow row = groups.GetRow(i);
                if (row.GetCell(0) == null || row.GetCell(0).CellType == CellType.Blank) 
                {
                    break;
                }
                var count = row.LastCellNum;

                var iUsers = new List<UserDto>();

                for (int j = 3; j < count; ++j)
                {
                    var cell = row.GetCell(j);
                    if (cell != null && cell.CellType != CellType.Blank)
                    {
                        iUsers.Add(new UserDto
                        {
                            Id = 0,
                            Email = cell.StringCellValue + "@irkut.com",
                            LongName = "string",
                            ShortName = "string",
                            Roles = new List<string>() { "string" }
                        });
                    }
                }

                var data = new CreateGroupDto
                {
                    Id = 0,
                    Title = row.GetCell(0).StringCellValue,
                    Description = row.GetCell(1).StringCellValue,
                    Users = iUsers,
                    Boss = new UserDto
                    {
                        Id = 0,
                        Email = row.GetCell(2).StringCellValue + "@irkut.com",
                        LongName = "string",
                        ShortName = "string",
                        Roles = new List<string>() { "string" },
                    }
                };

                Console.WriteLine($"{data.Title} {data.Description} {data.Boss.Email}");

                var content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(data),
                     Encoding.UTF8,
                    "application/json");
                HttpResponseMessage response = await client.PostAsync("http://localhost:8000/api/auth/user/groups/create", content);
            }
        }


        static void Main(string[] args)
        {
            Task.Run(RunAsync).GetAwaiter().GetResult();
        }
    }
}