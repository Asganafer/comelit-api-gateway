using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComelitApiGateway.Commons.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() { }

        public UnauthorizedException(string name) : base(name)
        {

        }
    }
}
