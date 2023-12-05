using AuthService.Dtos.Hold;

namespace AuthService.Services.Hold
{
    public interface IHoldService
    {
        // проверить существование
        Task<Models.Hold> Exist(int HoldId);
        // создание нового нового объекта
        Task<int> Create(string Type, int DestId);
        // добавить пользователя к объекту
        Task<int> AddUser(int HoldId, int UserId, int StatusId);
        // добавить группу к объекту
        Task<int> AddGroup(int HoldId, int GroupId, int StatusMemberId, int StatusBossId);
        // найти объект
        Task<int> Find(string Type, int DestId);
        // получить объект
        Task<HoldDto> Get(int HoldId);
        // получить id связаных групп
        Task<List<GroupHoldDto>> Groups(int HoldId);
        // получить id связаных пользователей
        Task<List<UserHoldDto>> Users(int HoldId);
        // копировать права от одного объекта другому
        Task<int> Copy(int HoldId, int NewHoldId);
        // получение прав для объекта относительно человека
        Task<List<string>> UserRights(int HoldId, int UserId);
    }
}
