using System.ComponentModel.DataAnnotations;

namespace WebApplicationMachineTest.Models
{
    public class StudentRecord
    {
        public int RecordId { get; set; }

        public int StudentId { get; set; }

        [Range(1, 100)]
        public byte Maths { get; set; }

        [Range(1, 100)]
        public byte Physics { get; set; }

        [Range(1, 100)]
        public byte Chemistry { get; set; }

        [Range(1, 100)]
        public byte English { get; set; }

        [Range(1, 100)]
        public byte Programming { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
