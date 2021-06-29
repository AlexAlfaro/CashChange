using System;
using System.Collections.Generic;
using System.Text;

namespace CashChange.Exceptions
{
    [Serializable]
    public class DenominationsNotSetException:Exception
    {
        public DenominationsNotSetException() { }
        public DenominationsNotSetException(string message) : base(message) { }
    }
}
