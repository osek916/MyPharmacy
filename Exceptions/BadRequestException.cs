using System;

namespace MyPharmacy.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {

        }
    }
}
