using System.ComponentModel.DataAnnotations;

namespace WebApplicationMachineTest.VieModels
{
    public class AddStudentViewModel
    {
        [Required]
        [StringLength(30)]
        public string FullName { get; set; }
    }
}
