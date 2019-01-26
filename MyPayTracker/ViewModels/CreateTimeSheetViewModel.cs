using Microsoft.AspNetCore.Mvc.Rendering;
using MyPayTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPayTracker.ViewModels
{
    public class CreateTimeSheetViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Time In")]
        public DateTime TimeIn { get; set; } 
        [Display(Name = "Time Out")]
        public DateTime TimeOut { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public List<SelectListItem> Employees { get; set; }

        public CreateTimeSheetViewModel() { }

        public CreateTimeSheetViewModel(IList<Employee> employees)
        {
            

            Employees = new List<SelectListItem>();


            foreach (var employee in employees)
            {
                Employees.Add(new SelectListItem
                {
                    Value = employee.ID.ToString(),
                    Text = employee.FirstName.ToString()

                });

            }
        }
    }
}