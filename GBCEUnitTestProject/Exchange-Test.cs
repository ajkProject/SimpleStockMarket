using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleStockMarket;

namespace GBCEUnitTestProject
{
    /// <summary>
    /// Summary description for Exchange_Test
    /// </summary>
    [TestClass]
    public class Exchange_Test
    {
        public Exchange_Test()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Test_ExchangeAddCommonStock()
        {

            string stock = "POP";
            double lastDividend = 8;
            double stockPrice = 25;

            GBCE exchange = new GBCE();

            exchange.addCommonStock(stock, lastDividend, stockPrice);


            Assert.AreEqual(1, exchange.stocks.Count); //Verify there is one stock in the exchange

        }

        [TestMethod]
        public void Test_ExchangeAddPreferredStock()
        {

            string stock = "GIN";
            double lastDividend = 8;
            double fixedDividend = 2;
            double parValue = 100;
            double stockPrice = 25;

            GBCE exchange = new GBCE();

            exchange.addPreferredStock(stock, lastDividend, fixedDividend, parValue, stockPrice);


            Assert.AreEqual(1, exchange.stocks.Count); //Verify there is one stock in the exchange

        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void Test_ExchangeAddStockDuplicateException()
        {

            string stock = "POP";
            double lastDividend = 8;
            double stockPrice = 25;

            GBCE exchange = new GBCE();

            exchange.addCommonStock(stock, lastDividend, stockPrice);

            exchange.addCommonStock(stock, lastDividend, stockPrice);

        }

        [TestMethod]
        public void Test_ExchangeRemoveStock()
        {

            string stock = "GIN";
            double lastDividend = 8;
            double fixedDividend = 2;
            double parValue = 100;
            double stockPrice = 25;

            GBCE exchange = new GBCE();

            exchange.addPreferredStock(stock, lastDividend, fixedDividend, parValue, stockPrice);

            stock = "POP";
            lastDividend = 8;
            stockPrice = 25;

            exchange.addCommonStock(stock, lastDividend, stockPrice);

            //Now remove a stock
            Assert.AreEqual(2, exchange.stocks.Count); //Verify there are two stocks in the exchange

            exchange.removeStock(stock);

            Assert.AreEqual(1, exchange.stocks.Count); //Verify there is one stock in the exchange

        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void Test_ExchangeRemoveStockException()
        {

            string stock = "GIN";
            double lastDividend = 8;
            double fixedDividend = 2;
            double parValue = 100;
            double stockPrice = 25;

            GBCE exchange = new GBCE();

            exchange.addPreferredStock(stock, lastDividend, fixedDividend, parValue, stockPrice);

            stock = "POP";

            exchange.removeStock(stock);
        }

        [TestMethod]
        public void Test_ExchangeFindStock()
        {

            string stock = "GIN";
            double lastDividend = 8;
            double fixedDividend = 2;
            double parValue = 100;
            double stockPrice = 25;

            GBCE exchange = new GBCE();

            exchange.addPreferredStock(stock, lastDividend, fixedDividend, parValue, stockPrice);

            stock = "POP";
            lastDividend = 4;
            stockPrice = 50;

            exchange.addCommonStock(stock, lastDividend, stockPrice);

            Assert.AreEqual(2, exchange.stocks.Count); //Verify there are two stocks in the exchange

            BaseStock selectedStock = exchange.GetStock(stock);

            Assert.AreEqual(50, selectedStock.stockPrice); //Verify it has the correct stock price

        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void Test_ExchangeFindStockException()
        {

            string stock = "GIN";
            double lastDividend = 8;
            double fixedDividend = 2;
            double parValue = 100;
            double stockPrice = 25;

            GBCE exchange = new GBCE();

            exchange.addPreferredStock(stock, lastDividend, fixedDividend, parValue, stockPrice);

            stock = "POP";

            BaseStock selectedStock = exchange.GetStock(stock);

        }

        [TestMethod]
        public void Test_ExchangeAllShareIndex()
        {

            string stock = "GIN";
            double lastDividend = 8;
            double fixedDividend = 2;
            double parValue = 100;
            double stockPrice = 25;

            GBCE exchange = new GBCE();

            exchange.addPreferredStock(stock, lastDividend, fixedDividend, parValue, stockPrice);

            stock = "TEA";
            lastDividend = 0;
            stockPrice = 75;

            exchange.addCommonStock(stock, lastDividend, stockPrice);

            stock = "POP";
            lastDividend = 4;
            stockPrice = 50;

            exchange.addCommonStock(stock, lastDividend, stockPrice);

            stock = "ALE";
            lastDividend = 23;
            stockPrice = 67;

            exchange.addCommonStock(stock, lastDividend, stockPrice);

            stock = "JOE";
            lastDividend = 13;
            stockPrice = 250;

            exchange.addCommonStock(stock, lastDividend, stockPrice);

            double expectedValue = Math.Pow((25 * 75 * 50 * 67 * 250), 1.0 / 5);

            Assert.AreEqual(5, exchange.stocks.Count); //Verify there are five stocks in the exchange

            double allShareIndex = exchange.AllShareIndex();

            Assert.AreEqual(expectedValue, allShareIndex); //Verify it has the correct index value

        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void Test_ExchangeAllShareIndexException()
        {

            string stock = "GIN";
            double lastDividend = 8;
            double fixedDividend = 2;
            double parValue = 100;
            double stockPrice = 25;

            GBCE exchange = new GBCE();

            exchange.addPreferredStock(stock, lastDividend, fixedDividend, parValue, stockPrice);

            stock = "TEA";
            lastDividend = 0;
            stockPrice = 75;

            exchange.addCommonStock(stock, lastDividend, stockPrice);

            stock = "POP";
            lastDividend = 4;
            stockPrice = 50;

            exchange.addCommonStock(stock, lastDividend, stockPrice);

            stock = "ALE";
            lastDividend = 23;
            stockPrice = -67; //-ve share price will cause exception on all share index

            exchange.addCommonStock(stock, lastDividend, stockPrice);

            stock = "JOE";
            lastDividend = 13;
            stockPrice = 250;

            exchange.addCommonStock(stock, lastDividend, stockPrice);

            Assert.AreEqual(5, exchange.stocks.Count); //Verify there are five stocks in the exchange

            double allShareIndex = exchange.AllShareIndex();

        }
    }


}
