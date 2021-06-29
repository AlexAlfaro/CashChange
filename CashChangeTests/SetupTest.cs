using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashChange;
using System.Collections.Generic;
using System.Linq;
using CashChange.Exceptions;

namespace CashChangeTests
{
    [TestClass]
    public class SetupTest
    {
        [TestInitialize]
        public void Initialize()
        {
            //reset in case another test created an instance
            Currency.GetInstance().ResetForTesting();
        }

        [TestMethod]
        public void TestSetupUniqueCurrencies()
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

            List<decimal> setUpCurrencies = new List<decimal>();
            foreach(var d in Currency.GetInstance().GetDenominations())
            {
                setUpCurrencies.Add(d.Value);
            }

            Assert.AreEqual(true, setUpCurrencies.All(denominations.Contains));
            
        }

        [TestMethod]
        public void TestSetupDuplicatedCurrencies()
        {
            List<decimal> denominations = new List<decimal>();
            denominations.Add(0.05M);
            denominations.Add(0.10M);
            denominations.Add(0.10M);
            denominations.Add(0.20M);
            denominations.Add(0.50M);
            denominations.Add(1.00M);
            denominations.Add(2.00M);
            denominations.Add(2.00M);
            denominations.Add(5.00M);
            denominations.Add(5.00M);
            denominations.Add(10.00M);
            denominations.Add(20.00M);
            denominations.Add(50.00M);
            denominations.Add(100.00M);
            Setup.SetUpCurrencies(denominations);

            List<decimal> setUpCurrencies = new List<decimal>();
            foreach (var d in Currency.GetInstance().GetDenominations())
            {
                setUpCurrencies.Add(d.Value);
            }

            Assert.AreNotEqual(denominations.Count, setUpCurrencies.Count);
            Assert.AreEqual(false, setUpCurrencies.SequenceEqual(denominations));            
        }

        [TestMethod]
        public void TestSetupNullCurrencies()
        {
            List<decimal> denominations = new List<decimal>();
            
            Assert.ThrowsException<NullCurrenciesException>(()=>Setup.SetUpCurrencies(denominations));            
        }

        [TestMethod]
        public void CurrenciesNotSet()
        {
            Assert.ThrowsException<DenominationsNotSetException>(()=>Currency.GetInstance().GetDenominations());
           
        }

        [TestCleanup]
        public void Reset()
        {
            Currency.GetInstance().ResetForTesting();
        }
    }
}
