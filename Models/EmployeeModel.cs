using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenUniversity.Models
{
    class EmployeeModel : PersonModel
    {
        public int MonthlySalary { get; set; }
        public int MonthlyHours { get; set; }

    }
}
