using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.Models
{
    public class UserLoginHistory
    {
        [Key]
        public int UserLoginHistoryId { get; set; }

        public string UserId { get; set; }

        public DateTime LastLoggenOn { get; set; }

    }
}
