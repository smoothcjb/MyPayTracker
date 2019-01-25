using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPayTracker.Models
{
    public class TimeSheet
    {
        public int ID { get; set; }
        public DateTime TimeIn { get; set; }

        
        public Employee Employee { get; set; }
        
    }
}
