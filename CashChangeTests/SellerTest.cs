using System;
using System.Collections.Generic;
using CashChange;
using CashChange.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CashChangeTests
{
    [TestClass]
    public class SellerTest
    {

        public SellerTest()
        {
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

        }

        [TestMethod]
        public void CustomerPaymentLowerThanPrice()
        {
            List<IPayment> customerPayment = new List<IPayment>();
            var payment = CashChangeFactory.CreatePayment();
            payment.Number = 1;
            payment.Denomination = CashChangeFactory.CreateDenomination(20);
            customerPayment.Add(payment);

            decimal total = 100M;

            Assert.ThrowsException<InsufficientPaymentException>(() => Seller.SellProduct(total, customerPayment));
        }

        public void EmpyCustomerPayment()
        {
            List<IPayment> customerPayment = new List<IPayment>();            
            decimal total = 100M;

            Assert.ThrowsException<InsufficientPaymentException>(() => Seller.SellProduct(total, customerPayment));
        }

        [TestMethod]
        public void CustomerPaymentIsExact()
        {
            List<IPayment> customerPayment = new List<IPayment>();
            var payment = CashChangeFactory.CreatePayment();
            payment.Number = 1;
            payment.Denomination = CashChangeFactory.CreateDenomination(20);
            customerPayment.Add(payment);

            decimal total = 20M;

            Assert.AreEqual(0, Seller.SellProduct(total, customerPayment).Count);
        }

        [TestMethod]
        public void CorrectChange()
        {
            decimal total = 100M;

            List<IPayment> customerPayment = new List<IPayment>();
            var payment = CashChangeFactory.CreatePayment();
            payment.Number = 2;
            payment.Denomination = CashChangeFactory.CreateDenomination(100);
            customerPayment.Add(payment);

            var result = Seller.SellProduct(total, customerPayment);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Number);
            Assert.AreEqual(100, result[0].Denomination.Value);
        }
    }
}
