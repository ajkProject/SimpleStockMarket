using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleStockMarket;

namespace GBCEUnitTestProject
{
    /// <summary>
    /// Summary description for Stock_Test
    /// </summary>
    [TestClass]
    public class Stock_Test
    {
        public Stock_Test()
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
        public void Test_StocksCommonYield()
        {
            string stock = "POP";
            double lastDividend = 8;
            double stockPrice = 25;
            double price = 64;
            double expected = lastDividend / price;

            CommonStock testStock = new CommonStock(stock, lastDividend, stockPrice);


            double dividendYield = testStock.DividendYield(price);

            Assert.AreEqual(expected, dividendYield);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void Test_StocksCommonYieldException()
        {
            string stock = "POP";
            double lastDividend = 8;
            double stockPrice = 25;
            double price = 0;


            CommonStock testStock = new CommonStock(stock, lastDividend, stockPrice);


            double dividendYield = testStock.DividendYield(price);

        }
        [TestMethod]
        public void Test_StocksPreferredYield()
        {
            string stock = "GIN";
            double lastDividend = 8;
            double fixedDividend = 2;
            double parValue = 100;
            double stockPrice = 25;
            double price = 64;
            double expected = ((fixedDividend / 100 )* parValue ) / price;

            PreferredStock testStock = new PreferredStock(stock, lastDividend, fixedDividend, parValue, stockPrice);


            double dividendYield = testStock.DividendYield(price);

            Assert.AreEqual(expected, dividendYield);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void Test_StocksPreferredYieldException()
        {
            string stock = "GIN";
            double lastDividend = 8;
            double fixedDividend = 2;
            double parValue = 100;
            double stockPrice = 25;
            double price = 0;


            PreferredStock testStock = new PreferredStock(stock, lastDividend, fixedDividend, parValue, stockPrice);


            double dividendYield = testStock.DividendYield(price);

        }
        [TestMethod]
        public void Test_StocksPERatio()
        {
            string stock = "GIN";
            double lastDividend = 8;
            double fixedDividend = 2;
            double parValue = 100;
            double stockPrice = 25;
            double price = 64;
            double expected = price / lastDividend;

            PreferredStock testStock = new PreferredStock(stock, lastDividend, fixedDividend, parValue, stockPrice);


            double PEratio = testStock.PERatio(price);

            Assert.AreEqual(expected, PEratio);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void Test_StocksPERatioException()
        {
            string stock = "GIN";
            double lastDividend = 0;
            double fixedDividend = 2;
            double parValue = 100;
            double stockPrice = 25;
            double price = 200;


            PreferredStock testStock = new PreferredStock(stock, lastDividend, fixedDividend, parValue, stockPrice);


            double PEratio = testStock.PERatio(price);

        }
        [TestMethod]
        public void Test_StocksAddTrades()
        {
            string stock = "GIN";
            double lastDividend = 0;
            double stockPrice = 25;


            CommonStock testStock = new CommonStock(stock, lastDividend, stockPrice);

            //Add 5 trades

            testStock.addTrade(DateTime.Now.AddMinutes(-3), 100, true, 250); //Sell 100 shares now at 250
            testStock.addTrade(DateTime.Now.AddMinutes(-1), 300, false, 260); //Buy 300 shares now at 260
            testStock.addTrade(DateTime.Now.AddMinutes(-6), 10, true, 240); //Sell 10 shares now at 240
            testStock.addTrade(DateTime.Now, 400, true, 230); //Sell 400 shares now at 230
            testStock.addTrade(DateTime.Now.AddMinutes(-8), 1000, false, 235); //Sell 1000 shares now at 235

            Assert.AreEqual(5, testStock.stockTrades.Count); //Check there are 5 trades
            Assert.AreEqual(230, testStock.stockTrades.Values[0].tradePrice); //Check the first trade in the list has a price of 235 - i.e. the most recent trade



        }

        [TestMethod]
        public void Test_StocksVolumeWeightedStockPrice()
        {
            string stock = "GIN";
            double lastDividend = 0;
            double stockPrice = 25;


            CommonStock testStock = new CommonStock(stock, lastDividend, stockPrice);

            //Add 10 trades

            testStock.addTrade(DateTime.Now.AddMinutes(-3), 100, true, 250); //Sell 100 shares now at 250
            testStock.addTrade(DateTime.Now.AddMinutes(-1), 300, false, 260); //Buy 300 shares now at 260
            testStock.addTrade(DateTime.Now.AddMinutes(-6), 10, true, 240); //Sell 10 shares now at 240
            testStock.addTrade(DateTime.Now, 400, true, 230); //Sell 400 shares now at 230
            testStock.addTrade(DateTime.Now.AddMinutes(-8), 1000, false, 235); //Sell 1000 shares now at 235
            testStock.addTrade(DateTime.Now.AddMinutes(-30), 100, true, 250); //Sell 100 shares now at 250
            testStock.addTrade(DateTime.Now.AddMinutes(-16), 300, false, 260); //Buy 300 shares now at 260
            testStock.addTrade(DateTime.Now.AddMinutes(-5), 10, true, 240); //Sell 10 shares now at 240
            testStock.addTrade(DateTime.Now.AddMinutes(-2), 400, true, 230); //Sell 400 shares now at 230
            testStock.addTrade(DateTime.Now.AddMinutes(-7), 1000, false, 235); //Sell 1000 shares now at 235

            double expectedResult = (100 * 250.0 + 300 * 260 + 10 * 240 + 400 * 230 + 1000 * 235 + 10 * 240 + 400 * 230 + 1000 * 235) / (100 + 300 + 10 + 400 + 1000 + 10 + 400 + 1000);

            double vwsp = testStock.volumeWeightedStockPrice();

            Assert.AreEqual(expectedResult, vwsp);



        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void Test_StocksVolumeWeightedStockPriceException()
        {
            string stock = "GIN";
            double lastDividend = 0;
            double stockPrice = 25;


            CommonStock testStock = new CommonStock(stock, lastDividend, stockPrice);

            //Add 10 trades

            testStock.addTrade(DateTime.Now.AddMinutes(-23), 100, true, 250); //Sell 100 shares now at 250
            testStock.addTrade(DateTime.Now.AddMinutes(-21), 300, false, 260); //Buy 300 shares now at 260
            testStock.addTrade(DateTime.Now.AddMinutes(-26), 10, true, 240); //Sell 10 shares now at 240
            testStock.addTrade(DateTime.Now.AddMinutes(-22), 400, true, 230); //Sell 400 shares now at 230
            testStock.addTrade(DateTime.Now.AddMinutes(-28), 1000, false, 235); //Sell 1000 shares now at 235
            testStock.addTrade(DateTime.Now.AddMinutes(-30), 100, true, 250); //Sell 100 shares now at 250
            testStock.addTrade(DateTime.Now.AddMinutes(-16), 300, false, 260); //Buy 300 shares now at 260
            testStock.addTrade(DateTime.Now.AddMinutes(-25), 10, true, 240); //Sell 10 shares now at 240
            testStock.addTrade(DateTime.Now.AddMinutes(-42), 400, true, 230); //Sell 400 shares now at 230
            testStock.addTrade(DateTime.Now.AddMinutes(-17), 1000, false, 235); //Sell 1000 shares now at 235

            double vwsp = testStock.volumeWeightedStockPrice();



        }
    }
}
