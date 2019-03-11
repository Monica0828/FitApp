using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.Models
{

    public enum UserType
    {
        Trainer,
        Client
    }

    public enum ResponseStatus
    {
        Ok,
        Failed
    }

    public enum SubscriptionType
    {
        DayTime,
        FullTime
    }
}
