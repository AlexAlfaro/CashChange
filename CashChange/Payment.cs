using System;
using System.Collections.Generic;
using System.Text;

namespace CashChange
{
    public class Payment : IPayment
    {
        public int Number { get; set; }
        public IDenomination Denomination { get; set; }
    }
}
