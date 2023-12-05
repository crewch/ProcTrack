using System.ComponentModel.DataAnnotations;

namespace DB_Service.Models
{
    public class BaseEntity
    {
        [Key]

        public int Id { get; set; }
    }
}
