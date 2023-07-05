namespace AuthService.Models
{
    public class Type: BaseEntity
    {
        public string Title { get; set; }
        public ICollection<Hold> Holds { get; set; } = new List<Hold>();
    }
}
