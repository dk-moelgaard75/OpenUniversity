using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenUniversity.CustomExceptions
{
    class InvalidSocialSecurityNumberException :Exception
    {
        public override string Message
        {
            get
            {
                return "Personnummeret er ikke korrekt";
            }
        }
    }
}
