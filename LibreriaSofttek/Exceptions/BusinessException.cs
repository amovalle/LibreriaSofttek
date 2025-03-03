using System;

namespace LibreriaSofttek.Exceptions
{
    public class BusinessException : Exception
    {
        // Base para excepciones propias del negocio

        public BusinessException() : base() { }

        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, Exception innerException) : base(message, innerException) { }
    }
}