namespace AuthService.Models
{
    public class Right : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<Hold> Holds { get; set; } = new List<Hold>();
        public ICollection<RightHoldMapper> RightHolds = new List<RightHoldMapper>();
    }
}
