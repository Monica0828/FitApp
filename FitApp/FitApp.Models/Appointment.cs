using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitApp.Models
{
    [Table("Appointment")]
    public partial class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        public string UserId { get; set; }

        public int ScheduleId { get; set; }

        public virtual FitAppUser Client { get; set; }

        public virtual GymClassSchedule ClassSchedule { get; set; }
    }
}
