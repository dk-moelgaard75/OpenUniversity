using OpenUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenUniversity.Repository
{
    interface IEmployeeRepository
    {
        IEnumerable<EmployeeModel> GetAll();
        EmployeeModel GetByID(Guid id);

    }
}
