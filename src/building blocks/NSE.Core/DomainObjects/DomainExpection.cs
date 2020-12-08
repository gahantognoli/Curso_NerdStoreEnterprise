using System;

namespace NSE.Core.DomainObjects
{
    public class DomainExpection : Exception
    {
        public DomainExpection()
        {
        }

        public DomainExpection(string message) : base(message)
        {
        }

        public DomainExpection(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
