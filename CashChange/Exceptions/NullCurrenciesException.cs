using System;
using System.Collections.Generic;
using System.Text;

namespace CashChange.Exceptions
{
    [Serializable]
    public class NullCurrenciesException: Exception
    {
        public NullCurrenciesException() { }
        public NullCurrenciesException(string message) : base(message) { }
    }
}
