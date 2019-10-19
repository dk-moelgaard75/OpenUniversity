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

        private string _name;
        private string _teacher;
        private Guid _courseID;
        public string Name { get; set; }
        public string Teacher { get; set; }

    }
}
