using System.ComponentModel.DataAnnotations;

namespace WebApplicationMachineTest.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(30)]
        public string FullName { get; set; }

        public string RollNumber { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
