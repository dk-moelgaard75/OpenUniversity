using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenUniversity.Models
{
    class PersonModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public string Type { get; set; }

        private Guid _guid;
        public Guid PersonID
        {
            get
            {
                if (_guid == null || _guid == Guid.Empty)
                {
                    _guid = Guid.NewGuid();
                }
                return _guid;
            }
            set { _guid = value; }

        }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

    }
}
