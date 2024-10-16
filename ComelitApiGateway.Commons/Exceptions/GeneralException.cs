using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComelitApiGateway.Commons.Exceptions
{
    public class GeneralException : Exception
    {
        public GeneralException() { }

        public GeneralException(string name) : base(name)
        {

        }
    }
}
