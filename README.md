# SimpleStockMarket

To Build:

Use MSBuild.exe

MSBuild SimpleStockMarket.sln

Assumptions - try to assume as little as possible and stick to the requirements.

Numbers - Price, Dividends etc I could have assumed that these should be positive, but decided just to apply conditions 
to them to ensure calculations did no fail and throw the exception at the point of doing the calculation.

Decimal places - Numbers would normally be displayed to a fixed number of decimal places in the case of stocks, dividends etc, but
remove rounding errors numbers are not rounded at any point - this would be the job of the calling program to do when 
displaying the numbers.

I did wonder if Volume Weighted Stock Price for a Stock and the Stock Price used for the All Share Index were the same thing, 
but as it was not specified I used a separate Stock Price.
