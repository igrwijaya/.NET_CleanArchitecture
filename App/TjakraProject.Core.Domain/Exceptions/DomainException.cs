using System;

namespace TjakraProject.Core.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string propertyName, string message)
            : base($"{propertyName} - {message}")
        {
        }
    }
}