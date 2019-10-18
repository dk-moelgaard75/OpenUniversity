using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenUniversity.Models;

namespace OpenUniversity.Data
{
    class OpenUniversityDbContext : DbContext
    {
        public OpenUniversityDbContext() : base("name=OpenUniversityDBEntities")
        {

        }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<PersonModel> Persons { get; set; }
        public DbSet<StudentModel> Students { get; set; }
    }
}
