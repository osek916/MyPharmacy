using System;

namespace MyPharmacy.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message)
        {

        }
    }
}
