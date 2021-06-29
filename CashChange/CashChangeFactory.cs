using System;
using System.Collections.Generic;
using System.Text;

namespace CashChange
{
    public static class CashChangeFactory
    {
        public static IDenomination CreateDenomination(decimal value)
        {
            return new Denomination(value);
        }

        public static IPayment CreatePayment()
        {
            return new Payment();
        }
    }
}
