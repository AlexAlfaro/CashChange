using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashChange.Exceptions;

namespace CashChange
{
    public static class Seller
    {
        public static List<IPayment> SellProduct(decimal total, List<IPayment> customerPayment)
        {
            //calculate the total payment multiplying the number of bills or coins by its denomination
            var totalPayment = customerPayment.Sum(x => (x.Denomination.Value * x.Number));

            if (total > totalPayment)
                throw new InsufficientPaymentException("Insufficient Payment");

            return CalculateChange(totalPayment - total);
        }

        private static List<IPayment> CalculateChange(decimal totalChange)
        {
            List<IPayment> change = new List<IPayment>();

            if (totalChange != 0)
            {
                var listCurrencies = Currency.GetInstance().GetDenominations();
                listCurrencies = listCurrencies.OrderByDescending(x => x.Value).ToList();
                foreach (var currency in listCurrencies)
                {
                    if (totalChange <= 0)
                        break;
                    
                    if (currency.Value > totalChange)
                        continue; 
                    
                    var howMany = (int)(totalChange / currency.Value);
                    totalChange -= (currency.Value * howMany);

                    var billCoin = CashChangeFactory.CreatePayment();
                    billCoin.Number = howMany;
                    billCoin.Denomination = currency;

                    change.Add(billCoin);
                }
            }

            return change;
        }

    }
}
