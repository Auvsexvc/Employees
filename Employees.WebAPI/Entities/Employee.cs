using System.ComponentModel.DataAnnotations;

namespace Employees.WebAPI.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Department { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string? PhotoFileName { get; set; }
    }
}
