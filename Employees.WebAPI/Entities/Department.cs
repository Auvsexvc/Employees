using System.ComponentModel.DataAnnotations;

namespace Employees.WebAPI.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}