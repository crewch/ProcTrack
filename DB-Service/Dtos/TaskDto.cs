﻿namespace DB_Service.Dtos
{
    public class TaskDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public TimeSpan? ExpectedTime { get; set; }
        public UserDto? User { get; set; }
        public List<CommentDto>? Comments { get; set; }
    }
}