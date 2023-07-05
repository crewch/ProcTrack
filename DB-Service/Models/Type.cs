using System.ComponentModel.DataAnnotations.Schema;

namespace DB_Service.Models
{
    [Table("ProcessType")]
    public class Type: BaseEntity
    {
        public string Title { get; set; }
        public ICollection<Process> Processes { get; set; } = new List<Process>();
    }
}
