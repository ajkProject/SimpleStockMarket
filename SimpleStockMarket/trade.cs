using System;

namespace SimpleStockMarket
{
    public class trade
    {
        public bool sell { get; set; }
        public int shareQuantity { get; set; }
        public double tradePrice { get; set; }

        public trade( int shareQuantity, bool sell, double tradePrice)
        {
            this.shareQuantity = shareQuantity;
            this.sell = sell;
            this.tradePrice = tradePrice;
        }
    }
}