using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPayTracker.Models
{
    public class TimeSheet
    { 
        public int ID { get; set; }
        [Display(Name = "Time In")]
        public DateTime TimeIn { get; set; }
        [Display(Name ="Time Out")]
        public DateTime TimeOut { get; set; }
        [Display(Name ="Total Hours")]
        public float HoursWorked { get; set; }    

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        public TimeSheet() { }

    }
}
