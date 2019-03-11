using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.Models
{
    public class GymSubscription
    {
        [Key]
        public int SubscriptionId { get; set; }

        public string GymName { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public string UserId { get; set; }

        public SubscriptionType Type { get; set; }

        public virtual FitAppUser Client { get; set; }

    }
}
