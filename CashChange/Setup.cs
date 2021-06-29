using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashChange.Exceptions;

namespace CashChange
{
    public static class Setup
    {
        public static void SetUpCurrencies(List<decimal> insertedCurrencies)
        {
            if (insertedCurrencies.Count == 0)
                throw new NullCurrenciesException("Can not set null currencies");

            List<IDenomination> currencies = new List<IDenomination>();
            foreach (var currency in insertedCurrencies.Distinct().ToList()) //remove repeated values
            {
                var den = CashChangeFactory.CreateDenomination(currency);
                currencies.Add(den);

            }

            //create the single instance of currency setting the global denominations
            Currency.GetInstance(currencies);

        }
    }
}
