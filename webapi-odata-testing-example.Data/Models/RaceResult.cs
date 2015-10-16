using System;
using System.ComponentModel.DataAnnotations;

namespace Example.Data.Models
{
    public class RaceResult
    {
        [Key]
        public int Id { get; set; }

        public virtual Race Race { get; set; }

        public virtual Car Car { get; set; }

        public virtual Driver Driver { get; set; }

        public TimeSpan? FinishTimeSpan { get; set; }

        public bool DidFinish { get; set; }
        public bool DidStart { get; set; }

        public int? LapsCompleted { get; set; }

        public int? PitStops { get; set; }
    }
}