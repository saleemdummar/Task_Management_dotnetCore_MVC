using System.ComponentModel.DataAnnotations;

namespace Task_Management.Models
{
    public class taskManagement
    {
        [Key]
        public int TaskID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        [MaxLength(20)]
        public string Column { get; set; }
    }
}
