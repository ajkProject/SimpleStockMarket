using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStockMarket
{
    public class GBCE
    {

        public Dictionary<string,BaseStock> stocks { get; set; }

        /// <summary>
        ///  Global Beverage Corporation Exchange - Clas that implments the Exchange
        /// </summary>
        public GBCE()
            {

            this.stocks = new Dictionary<string, BaseStock>();

            }

        /// <summary>
        /// Add a Common stock to the Global Beverage Corporation Exchange 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="lastDividend"></param>
        /// <param name="price"></param>
        /// Throws a BusinessException if the Stock symbol aready exists in the exchange
        public void addCommonStock(string symbol, double lastDividend, double price)
        {
            if (this.stocks.ContainsKey(symbol))

                throw new BusinessException("Stock already exists in the Global Beverage Corporation Exchange");

            this.stocks.Add(symbol, new CommonStock(symbol, lastDividend, price));

        }

        /// <summary>
        /// Add a Preferred stock to the Global Beverage Corporation Exchange 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="lastDividend"></param>
        /// <param name="fixedDividend"></param>
        /// <param name="parValue"></param>
        /// <param name="price"></param>
        /// Throws a BusinessException if the Stock symbol aready exists in the exchange

        public void addPreferredStock(string symbol, double lastDividend, double fixedDividend, double parValue, double price)
        {
            if (stocks.ContainsKey(symbol))

                throw new BusinessException("Stock already exists in the Global Beverage Corporation Exchange");

            this.stocks.Add(symbol, new PreferredStock(symbol, lastDividend, fixedDividend, parValue, price));

        }

        /// <summary>
        /// Remove a stock from Add a Common stock to the Global Beverage Corporation Exchange 
        /// </summary>
        /// <param name="symbol"></param>
        /// Throws a BusinessException if the Stock symbol does not exists in the exchange

        public void removeStock(string symbol)
        {

            if (this.stocks.ContainsKey(symbol))

                this.stocks.Remove(symbol);

            else

                throw new BusinessException("Stock does not exist on the Global Beverage Corporation Exchange");

        }

        /// <summary>
        /// Get the BaseStock class for a stock in the Exchange.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns>BaseStock class describing the stock</returns>
        /// Throws a BusinessException if the Stock symbol does not exists in the exchange
        public BaseStock GetStock( string symbol )
        {

            if (this.stocks.ContainsKey(symbol))

                return this.stocks[symbol];

            else

                throw new BusinessException("Stock does not exist on the Global Beverage Corporation Exchange");

        }


        /// <summary>
        /// Calculate AllShareIndex
        /// 
        /// This will Only work if there arew any stocks and all the StockPrices are positive and Non-zero
        /// All other cases it will throw an exception
        /// 
        /// </summary>
        /// <returns>The AllShareIndex</returns>
        /// Throws a BusinessException if the any stock has Zero or Negative price

        public double AllShareIndex()
        {

            if( this.stocks.Count == 0 )

                throw new BusinessException("No Stocks to calculate All Share Index");

            double stockProduct = 1;

            foreach( KeyValuePair<string, BaseStock> currentStock in this.stocks )
            {

                // Validate the price - If price is Zero then the AllShareIndex will be zero
                //   if the Price is negative then the all share index will not be a real number if there is an even number of stocks - which does not make sense.

                if( currentStock.Value.stockPrice <= 0 )

                    throw new BusinessException("Stock Price zero or Negative - cannot calculate All Share Index");

                stockProduct *= (double)currentStock.Value.stockPrice;


            }

            return (double)Math.Pow(stockProduct, 1.0 / this.stocks.Count);

        }

    }
}
