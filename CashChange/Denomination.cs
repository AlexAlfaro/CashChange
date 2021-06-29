using System;
using System.Collections.Generic;
using System.Text;

namespace CashChange
{
    public class Denomination : IDenomination
    {
        public decimal Value { get; set; }
        public Denomination(decimal value)
        {
            Value = value;
        }
    }
}
