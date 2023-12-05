using DB_Service.Dtos.Auth.Hold;
using DB_Service.Dtos.Auth.User;
using NuGet.Protocol;

namespace DB_Service.Dtos.Stage
{
    public class StageDto
    {
        public int Id { get; set; }
        public int? ProcessId { get; set; }
        public string? ProcessName { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        public string? CreatedAt { get; set; }
        public DateTime? CreatedAtUnparsed { get; set; }
        public string? SignedAt { get; set; }
        public string? ApprovedAt { get; set; }
        public string Status { get; set; } //TODO: удалить StatusValue на фронте
        public List<UserDto>? Users { get; set; }
        public List<GroupDto>? Groups { get; set; }
        public HoldDto? Hold { get; set; }
        public List<int>? CanCreate { get; set; }
        public bool? Mark { get; set; }
        public bool? Pass { get; set; }
        public FilterStageDto? Filter { get; set; }
    }
}
