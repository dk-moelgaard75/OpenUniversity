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
        //DB setup due to inheritance: https://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-2-table-per-type-tpt
        //General setup for MVVM projects (https://stackoverflow.com/questions/22514584/in-which-class-should-i-load-my-data-when-using-mvvm)
        //ProjectName
        //      Converters
        //      DataAccess
        //      DataTypes
        //      Images
        //      ViewModels
        //      Views

        public OpenUniversityDbContext() : base("name=OpenUniversityDBEntities")
        {
            
            //Database.SetInitializer<OpenUniversityDbContext>(new OpenUniversityDbInitializer());
            Database.SetInitializer(new OpenUniversityDbInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonModel>().ToTable("Persons");
            modelBuilder.Entity<EmployeeModel>().ToTable("Employees");
            modelBuilder.Entity<StudentModel>().ToTable("Students");
            modelBuilder.Entity<CourseModel>().ToTable("Courses");

        }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<PersonModel> Persons { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        
    }
}
