using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        public string UserId { get; set; }

        public string Message { get; set; }

        public bool Seen { get; set; }

        public int GymClassId { get; set; }
    }
}
