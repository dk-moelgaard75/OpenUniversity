﻿using OpenUniversity.CustomExceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenUniversity.Models
{
    class StudentModel : PersonModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        public string Address { get; set; }
        public int? ZipCode { get; set; }
        public string Hometown { get; set; }
        public string Phonenumber { get; set; }
        public string SocialSecurityNumber { get; set; }

        private StudentModel()
        {
            //empty default constructor with pricat accessor ensures that 
            //an object is allways created with a socialsecuritynumber
        }
        public StudentModel(string socialSecurityNumber)
        {
            //do a modulus 11 control (https://cpr.dk/cpr-systemet/personnumre-uden-kontrolciffer-modulus-11-kontrol/) 
            //certan numbers may not pass
            if (!DoModulus11Validation(socialSecurityNumber))
            {
                throw new InvalidSocialSecurityNumberException();
            }

        }

        private bool DoModulus11Validation(string socialSecurityNumber)
        {
            //How to mock configurationmanager data
            //https://stackoverflow.com/questions/24307679/configurationmanager-appsettings-returns-null-in-unit-test-project
            
            //Do an inital test to se how this part act, if no value is set in app.config
            string isModulus11ValidationEnables = ConfigurationManager.AppSettings["modulus11"];
            if (isModulus11ValidationEnables.Equals("false"))
            {
                return true;
            }
            else
            {
                return PersonUtility.ValidateSocialSecurityNumber(socialSecurityNumber);
            }
        }
    }
}
