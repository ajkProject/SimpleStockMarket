using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleStockMarket
{
    public abstract class BaseStock
    {

        public string symbol { get; set; } //Stock symbol
        public double lastDividend { get; set; } 

        public double stockPrice { get; set; }

        public SortedList< DateTime, trade> stockTrades { get; set; }

        /// <summary>
        /// BaseStock constructor
        /// </summary>
        /// <param name="symbol">Stock Symbol</param>
        /// <param name="lastDividend">Last Dividend Price</param>
        /// <param name="price">Stock Price</param>
        protected BaseStock( string symbol, double lastDividend,  double stockPrice )
        {

            this.symbol = symbol;
            this.lastDividend = lastDividend;
            this.stockPrice = stockPrice;

            this.stockTrades = new SortedList<DateTime, trade>(new DecendingCompare<DateTime>()); //Sorted list with mostrecent trade first

        }

        abstract public double DividendYield(double price);

        /// <summary>
        /// Given any price as input,  calculate the P/E Ratio
        ///
        /// </summary>
        /// <param name="price">Price to calculate the P/E Ratio for</param>
        /// <returns>P/E Ratio</returns>
        /// Through BusinessException if the calculation is not possible.
        public double PERatio( double price)
        {
            if( this.lastDividend == 0)

                throw new BusinessException("Cannot calculate P/E ratio for stock with no earnings");

            return price / this.lastDividend;


        }
        /// <summary>
        /// Add in a new trade for this stock
        /// </summary>
        /// <param name="timeStamp">time of trade</param>
        /// <param name="shareQuantity">Quantity of Shares traded</param>
        /// <param name="sell">If true then this is a Sell trade - if faulse it is a Buy trade</param>
        /// <param name="tradePrice">Price at which the shares are traded</param>
        public void addTrade( DateTime timeStamp, int shareQuantity, bool sell, double tradePrice )
        {

            stockTrades.Add( timeStamp, new trade( shareQuantity, sell, tradePrice));

        }

        /// <summary>
        /// Calculate Volume Weighted Stock Price based on trades in past 15 minutes
        /// </summary>
        /// <returns> Value Weoghted Stock Price</returns>
        public double volumeWeightedStockPrice()
        {

            double priceQuantitySum = 0;
            double quantitySum = 0;
 
            foreach( KeyValuePair<DateTime, trade> currentTrade in stockTrades )
            {

                if (currentTrade.Key < DateTime.Now.AddMinutes(-15))

                    break;

                priceQuantitySum += currentTrade.Value.shareQuantity * currentTrade.Value.tradePrice;
                quantitySum += currentTrade.Value.shareQuantity;

            }

            if( quantitySum == 0)

                throw new BusinessException("Cannot calculate volune Weighted Stock Price - there have been no trades in the last 15 minutes" );

            return priceQuantitySum / quantitySum;


        }
    }
}
