using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenUniversity
{
    public static class PersonUtility
    {
        private static List<string> _employeeTypes;
        public static List<string> EmployeeTypes
        {
            get
            {
                if (_employeeTypes == null)
                {
                    _employeeTypes = new List<string>();
                    _employeeTypes.Add("Sekretær");
                    _employeeTypes.Add("Pedel");
                    _employeeTypes.Add("Direktør");
                }
                return _employeeTypes;
            }
        }
        
        public static int GetMonthlyHours(string employeeType)
        {
            int monthlyHours = 37; //default secretary hours

            switch (employeeType)
            {
                case "Sekretær":
                    break;
                case "Pedel":
                    monthlyHours = 32;
                    break;
                case "Direktør":
                    monthlyHours = 50;
                    break;
            }
            return monthlyHours;
        }
        public static int GetMonthlySalery(string employeeType)
        {
            int monthlyHours = 30000; //default secretary salary

            switch (employeeType)
            {
                case "Sekretær":
                    break;
                case "Pedel":
                    monthlyHours = 25000;
                    break;
                case "Direktør":
                    monthlyHours = 50000;
                    break;
            }
            return monthlyHours;
        }

    }
}
