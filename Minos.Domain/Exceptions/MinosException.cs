using System;
using System.Collections.Generic;
using System.Text;

namespace Minos.Domain.Exceptions
{
    class MinosException : Exception
    {
        public MinosException()
        {

        }

        public MinosException(string message)
            : base(message)
        {

        }

        public MinosException(string message, Exception exception)
            : base(message, exception)
        {

        }
    }
}
