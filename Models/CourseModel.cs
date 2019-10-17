using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenUniversity.Models
{
    class CourseModel
    {
        private string _name;
        private string _teacher;
        private Guid _courseID;
        public string Name { get; set; }
        public string Teacher { get; set; }

    }
}
