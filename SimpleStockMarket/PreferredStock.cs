using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStockMarket
{
    public class PreferredStock : BaseStock
    {

        public double fixedDividend { get; set; }
        public double parValue { get; set; }

        /// <summary>
        /// Create a peferred stock - this class hold information specific to Peferred stock, all common information held in BaseStock
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="lastDividend"></param>
        /// <param name="fixedDividend"></param>
        /// <param name="parValue"></param>
        /// <param name="stockPrice"></param>
        public PreferredStock(string symbol, double lastDividend, double fixedDividend, double parValue, double stockPrice):
            base( symbol, lastDividend, stockPrice)
        {
            this.fixedDividend = fixedDividend;
            this.parValue = parValue;
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

            return this.fixedDividend * this.parValue / ( price * 100); //Divide by 100 as fixedDividend is a percentage
        }

    }
}
