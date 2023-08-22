using DB_Service.Clients.Http;
using DB_Service.Dtos;
using DB_Service.Models;
using DB_Service.Tools;

namespace DB_Service.Services
{
    public class MailService : IMailService
    {
        private readonly IMailDataClient _mailClient;

        public MailService(IMailDataClient mailClient)
        {
            _mailClient = mailClient;
        }

        public void SendProcessMailToChecker(Process process, UserDto user, GroupDto group, Stage stage)
        {
            System.Threading.Tasks.Task.Run(async () => await _mailClient.SendMail(new MailDto
                        {
                            To = user.Email,
                            Body = $"Уважаемый(ая) {user.LongName.Split(' ').ToList()[1]} {user.LongName.Split(' ').ToList()[2]},<br><br>" +
                                   $"Процесс согласования КД \"{process.Title}\", находящийся на этапе согласования " +
                                   $"\"{stage.Title}\" <br> отправлен на проверку в Ваше подразделение \"{group.Title}\" <br><br>" +
                                   $"ProcTrack, Система отслеживания процессов согласования, <br>" +
                                   $"{DateParser.Parse(DateTime.Now.AddHours(3))}",
                            Subject = $"Процесс согласования КД {process.Title}"
                        }));
        }

        public void SendProcessMailToReleaser(Process process, Stage stage, UserDto user)
        {
            System.Threading.Tasks.Task.Run(async () => await _mailClient.SendMail(new MailDto
                {
                    To = user.Email,
                    Body = $"Уважаемый(ая) {user.LongName.Split(' ').ToList()[1]} " +
                           $"{user.LongName.Split(' ').ToList()[2]},<br><br>" +
                           $"Процесс согласования КД \"{process.Title}\", находящийся на этапе согласования " +
                           $"\"{stage.Title}\" {stage.Status.Title} <br><br>" +
                           $"ProcTrack, Система отслеживания процессов согласования, <br>" +
                           $"{DateParser.Parse(DateTime.Now.AddHours(3))}",
                    Subject = $"Процесс согласования КД {process.Title}"
                }));
        }
    }
}
