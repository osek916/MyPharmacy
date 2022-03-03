using System;

namespace MyPharmacy.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
