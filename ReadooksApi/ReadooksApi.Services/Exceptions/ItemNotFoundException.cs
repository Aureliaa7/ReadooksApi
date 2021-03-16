using System;

namespace Readooks.BusinessLogicLayer.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException() : base() { }
        public ItemNotFoundException(string message) : base(message) { }
    }
}
