using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenUniversity.Models
{
    class CourseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        private Guid _courseID;
        public Guid CourseID
        {
            get
            {
                if (_courseID == null || _courseID == Guid.Empty)
                {
                    _courseID = Guid.NewGuid();
                }
                return _courseID;
            }
            set { _courseID = value; }
        }
        public string Name { get; set; }
        public EmployeeModel Teacher { get; set; }

        public int Price { get; set; }

        public int MaxNrOfStudents { get; set; }

    }
}
