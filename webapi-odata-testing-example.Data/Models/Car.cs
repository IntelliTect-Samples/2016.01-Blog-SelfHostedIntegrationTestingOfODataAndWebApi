﻿using System.ComponentModel.DataAnnotations;

namespace Example.Data.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public virtual Driver Owner { get; set; }
    }
}