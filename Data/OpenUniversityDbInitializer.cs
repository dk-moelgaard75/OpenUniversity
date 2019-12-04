using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OpenUniversity.Data
{
    public class OpenUniversityDbInitializer : DropCreateDatabaseIfModelChanges<OpenUniversityDbContext>
    {
        protected override void Seed(OpenUniversityDbContext context)
        {
            base.Seed(context);
        }
    }
}
