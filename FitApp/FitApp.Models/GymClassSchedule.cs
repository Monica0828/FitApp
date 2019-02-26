using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitApp.Models
{
    [Table("GymClassSchedule")]
    public partial class GymClassSchedule
    {
        [Key]
        public int ScheduleId { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public int GymClassId { get; set; }

        public virtual FitAppUser Trainer { get; set; }

        public virtual GymClass Class { get; set; }
    }
}
