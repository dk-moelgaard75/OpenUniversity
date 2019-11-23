using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenUniversity.CustomExceptions
{
    class SocialSecurityNumberIsUse : Exception
    {
        public override string Message
        {
            get
            {
                return "Personen er allerede registreret";
            }
        }

    }
}
