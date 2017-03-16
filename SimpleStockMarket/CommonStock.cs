using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStockMarket
{
    public class CommonStock : BaseStock
    {

        /// <summary>
        /// Create a common stock - all data is held in BaseStock
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="lastDividend"></param>
        /// <param name="stockPrice"></param>
        public CommonStock(string symbol, double lastDividend, double stockPrice):
            base( symbol, lastDividend, stockPrice)
        {
        }

        /// <summary>
        /// Given any price as input, calculate the dividend yield - for Common Stock
        /// </summary>
        /// <param name="price">Price to calculate yiels for</param>
        /// <returns>Dividend yield</returns>
        /// Returns BusinessException if there is no dividend
        public override double DividendYield(double price)
        {

            if (price == 0)

                throw new BusinessException("Cannot calculate Dividend Yields for zero priced Stocks");

            return this.lastDividend / price;
        }



    }
}
