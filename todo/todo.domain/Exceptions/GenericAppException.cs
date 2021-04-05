using System;
using System.Collections.Generic;
using System.Text;

namespace todo.domain.Exceptions
{
    public class GenericAppException : ApplicationException
    {

        public GenericAppException()
        {

        }

        public GenericAppException(string message) : base(message)
        {

        }

        public GenericAppException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
