using System;
using System.ComponentModel.DataAnnotations;

namespace Example.Data.Models
{
    public class Race
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Laps { get; set; }

        public DateTime StartDateTime { get; set; }
    }
}