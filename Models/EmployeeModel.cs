using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenUniversity.Models
{
    class EmployeeModel : PersonModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        public int MonthlySalary { get; set; }
        public int MonthlyHours { get; set; }
        public string MonthlySalaryText 
        { 
            get
            {
                return MonthlySalary.ToString() + " kr";
            }
        }
        public string MonthlyHoursText
        { 
            get
            {
                return MonthlyHours.ToString() + " timer";
            }
        }

    }
}
