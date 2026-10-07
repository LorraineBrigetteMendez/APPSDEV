using System.ComponentModel.DataAnnotations;

namespace StudentRosterApi.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Course { get; set; }

        [Required]
        [Range(1, 4)]
        public int YearLevel { get; set; }
    }
}