using System.ComponentModel.DataAnnotations.Schema;

namespace DB_Service.Models
{
    public class Type: BaseEntity
    {
        public string Title { get; set; } = String.Empty;
        public ICollection<Process> Processes { get; set; } = new List<Process>();
    }
}
