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
                    _employeeTypes.Add("Underviser");
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
                case "Underviser":
                    monthlyHours = 40;
                    break;
                case "Direktør":
                    monthlyHours = 50;
                    break;
            }
            return monthlyHours;
        }
        public static int GetMonthlySalery(string employeeType)
        {
            int monthlySalary = 30000; //default secretary salary

            switch (employeeType)
            {
                case "Sekretær":
                    break;
                case "Pedel":
                    monthlySalary = 25000;
                    break;
                case "Underviser":
                    monthlySalary = 40000;
                    break;
                case "Direktør":
                    monthlySalary = 50000;
                    break;
            }
            return monthlySalary;
        }
        public static List<string> Modulus11ExceptionDatesList()
        {
            List<string> list = new List<string>();
            //add exception dates from
            ////do a modulus 11 control (https://cpr.dk/cpr-systemet/personnumre-uden-kontrolciffer-modulus-11-kontrol/) 
            list.Add("0101");

            return list;
        }
        public static bool IsDateModulus11Excption(string date)
        {
            return Modulus11ExceptionDatesList().Contains(date);
        }
        public static bool ValidateSocialSecurityNumber(string number)
        {
            int firstSegment = -1;
            int secondSegment = -1;
            string firstSegmt = number.Substring(0, 6);
            string secondSegmnt = number.Substring(6, 4);
            //First validate if the values are numbers
            bool firstSegmentOk = int.TryParse(firstSegmt, out firstSegment);
            bool secondSegmentOk = int.TryParse(secondSegmnt, out secondSegment);
            //If the first segment is an modules11 exeption date return true
            if (PersonUtility.IsDateModulus11Excption(firstSegmt))
            {
                return true;
            }
            //validate that first the first segment is an actual date
            string day = firstSegmt.Substring(0, 2);
            string month = firstSegmt.Substring(2, 2);
            string year = firstSegmt.Substring(4, 2);

            //DateTime.TryParse();
            //Do modulus11 calculation
            int tmpSum = 0;
            int controlNr = int.Parse(number[9].ToString());
            string factor = "432765432";
            for (int i = 0; i<9; i++)
            {
                //Since we´ve allready checked that the socialsecuritynumber is a number
                //there is no need for a TryParse here
                int val = int.Parse(number[i].ToString());
                int multiplier = int.Parse(factor[i].ToString());
                tmpSum = tmpSum + val * multiplier;
            }
            double finalCheck = 11 - tmpSum % 11;
            if (finalCheck == controlNr || finalCheck == 11)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
