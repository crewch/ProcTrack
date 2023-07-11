using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateTemplate
{
    public class UserDto
    {
        public int Id { get; set; } = 0;
        public string Email { get; set; } = "string";
        public string LongName { get; set; } = "string";
        public string ShortName { get; set; } = "string";
        public List<string>? Roles { get; set; } = new List<string>() { "string" };
    }
    public class GroupDto
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = "string";
        public string? Description { get; set; } = "string";
        public UserDto Boss { get; set; } = new UserDto();
    }
    public class HoldDto
    {
        public int Id { get; set; } = 0;
        public int DestId { get; set; } = 0;
        public string Type { get; set; } = "string";
        public List<string>? Rights { get; set; } = new List<string>() { "string" };
        public List<UserDto>? Users { get; set; } = new List<UserDto>();
        public List<GroupDto>? Groups { get; set; } = new List<GroupDto>();
    }
    public class ProcessDto
    {
        public int? Id { get; set; } = 0;
        public string? Title { get; set; } = "string";
        public string? Priority { get; set; } = "string";
        public string? Status { get; set; } = "string";
        public string? Type { get; set; } = "string";
        public string? CreatedAt { get; set; } = "string";
        public string? ApprovedAt { get; set; } = "string";
        public TimeSpan? ExpectedTime { get; set; } = TimeSpan.Zero;
        public List<HoldDto>? Hold { get; set; } = new List<HoldDto>();
    }
    public class CommentDto
    {
        public int? Id { get; set; } = 0;
        public string Text { get; set; } = "string";
        public string? FileRef { get; set; } = "string";
        public UserDto User { get; set; } = new UserDto();
        public string? CreatedAt { get; set; } = "string";
    }
    public class TaskDto
    {
        public int? Id { get; set; }
        public string Title { get; set; } = "string";
        public int StageId { get; set; } = 0;
        public int? SignId { get; set; } = 0;
        public string? StartedAt { get; set; } = "string";
        public string? ApprovedAt { get; set; } = "string";
        public TimeSpan? ExpectedTime { get; set; } = TimeSpan.Zero;
        public string? Signed { get; set; } = "string";
        public UserDto? User { get; set; } = new UserDto();
        public List<CommentDto>? Comments { get; set; } = new List<CommentDto>();
        public string? EndVerificationDate { get; set; } = "string";
    }
    public class LinkDto
    {
        public List<Tuple<int, int>> Edges { get; set; } = new List<Tuple<int, int>>();
        public List<Tuple<int, int>>? Dependences { get; set; } = new List<Tuple<int, int>>();
    }
    public class StageDto
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = "string";
        public string? CreatedAt { get; set; } = "string";
        public string? SignedAt { get; set; } = "string";
        public string? ApprovedAt { get; set; } = "string";
        public string Status { get; set; } = "string";
        public int? StatusValue { get; set; } = 0;
        public UserDto? User { get; set; } = new UserDto();
        public List<HoldDto>? Holds { get; set; } = new List<HoldDto>();
        public List<int>? CanCreate { get; set; } = new List<int>();
        public bool? Mark { get; set; } = false;
        public bool? Pass { get; set; } = false;
    }
    public class TemplateDto
    {
        public ProcessDto? Process { get; set; } = new ProcessDto();
        public int? StartStage { get; set; } = 0;
        public int? EndStage { get; set; } = 0;
        public List<StageDto>? Stages { get; set; } = new List<StageDto>();
        public List<TaskDto>? Tasks { get; set; } = new List<TaskDto>();
        public LinkDto? Links { get; set; } = new LinkDto();
    }
    internal class Dtos
    {
    }
}
