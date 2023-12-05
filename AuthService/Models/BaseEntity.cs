using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class BaseEntity
    {
        [Key]

        public int Id { get; set; }
    }
}
