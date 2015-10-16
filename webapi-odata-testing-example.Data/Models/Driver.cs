using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Example.Data.Models
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; } 
    }
}