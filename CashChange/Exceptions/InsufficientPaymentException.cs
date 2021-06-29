using System;
using System.Collections.Generic;
using System.Text;

namespace CashChange.Exceptions
{
    [Serializable]
    public class InsufficientPaymentException : Exception
    {
        public InsufficientPaymentException() { }
        public InsufficientPaymentException(string message): base(message) { }

    }
}
