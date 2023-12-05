using DB_Service.Dtos;

namespace DB_Service.Services.Passport.CRUD
{
    public interface IPassportService
    {
        Task<int> Create(
            int processId,
            string title, 
            string message
            );

        Task<int> Update(
            int passportId,
            string? title,
            string? message
            );

        Task<int> Delete(int passportId);

        Task<Models.Passport> Exist(int passportId);

        Task<PassportDto> Get(int passportId);

    }
}
