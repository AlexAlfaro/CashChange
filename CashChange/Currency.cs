using CashChange.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashChange
{
    public sealed class Currency
    {
        private List<IDenomination> Denominations { get; }

        private static Currency instance = null;

        //set on null to not ask for it on every call since they will only be set once. 
        //denominations MUST NOT BE NULL on first call
        //if they are not set when getting the denominations an exception will be thrown
        public static Currency GetInstance(List<IDenomination> denominations = null)
        {
            if (instance == null)
                instance = new Currency(denominations);

            return instance;
        }

        private Currency(List<IDenomination> denominations)
        {
            Denominations = denominations;
        }

        public List<IDenomination> GetDenominations()
        {
            if (Denominations == null)
                throw new DenominationsNotSetException("Denominations are not set");
            return Denominations;
        }

        public void ResetForTesting()
        {
            instance = null;
        }
    }
}
