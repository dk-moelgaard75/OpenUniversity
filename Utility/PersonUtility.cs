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
    }
}
