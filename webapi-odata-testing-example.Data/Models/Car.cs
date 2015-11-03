using System.ComponentModel.DataAnnotations;

namespace Example.Data.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1886, 2020)]
        [Required]
        public int Year { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public virtual Driver Owner { get; set; }
    }
}