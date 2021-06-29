using CashChange;
using CashChange.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            SetUpEnv();  
            RunService();

        }

        static void RunService()
        {
            Console.WriteLine("Press ENTER to register a new purchase");
            if(Console.ReadKey().Key == ConsoleKey.Enter)
            {
                NewPurchase();
                RunService();
            }
        }

        static void SetUpEnv()
        {
            try
            {
                Console.WriteLine("Setting POS system...");
                List<decimal> denominations = new List<decimal>();
                denominations.Add(0.05M);
                denominations.Add(0.10M);
                denominations.Add(0.20M);
                denominations.Add(0.50M);
                denominations.Add(1.00M);
                denominations.Add(2.00M);
                denominations.Add(5.00M);
                denominations.Add(10.00M);
                denominations.Add(20.00M);
                denominations.Add(50.00M);
                denominations.Add(100.00M);
                Setup.SetUpCurrencies(denominations);

                Console.WriteLine("------Set Up Finished------");
            }
            catch (NullCurrenciesException)
            {
                Console.WriteLine("Error setting up the currencies");
            }
        }

        static void NewPurchase()
        {
            try
            {
                Console.WriteLine("Insert total: ");
                var total = Convert.ToDecimal(Console.ReadLine());

                Console.WriteLine("Insert customer payment");
                var customerPayment = CustomerPayment();
                Console.WriteLine();

                try
                {
                    var change = Seller.SellProduct(total, customerPayment);
                    if (change.Count == 0)
                        Console.WriteLine("No Change");
                    else
                    {
                        Console.WriteLine("Change to return to customer: ");
                        foreach (var c in change)
                        {

                            Console.WriteLine($"{c.Number} Bills/Coins of {c.Denomination.Value}");
                        }
                    }
                }
                catch (InsufficientPaymentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            catch(FormatException)
            {
                Console.WriteLine("Please insert only numbers");
                NewPurchase();
            }
        }

        static List<IPayment> CustomerPayment()
        {
            List<IPayment> customerPayment = new List<IPayment>();
            AddBillsAndCoins(customerPayment);

            return customerPayment;
        }

        static void AddBillsAndCoins(List<IPayment> paymentsLines)
        {
           
            Console.WriteLine("Insert Currency: (.50,1,20)");            
            var paymentLine = CashChangeFactory.CreatePayment();
            paymentLine.Denomination = CashChangeFactory.CreateDenomination(Convert.ToDecimal(Console.ReadLine()));
            Console.WriteLine("How many bills/coins of this currency did you received from customer? (Numbers only)");
            paymentLine.Number = Convert.ToInt32(Console.ReadLine());
            paymentsLines.Add(paymentLine);
            Console.WriteLine("Do you want to add a new line? Y/n");

            if(Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                AddBillsAndCoins(paymentsLines);
            }

            
        }
    }
}
