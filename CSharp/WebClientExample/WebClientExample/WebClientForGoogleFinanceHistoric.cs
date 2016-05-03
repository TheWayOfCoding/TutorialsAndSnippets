using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebClientExample
{
    /*
    By: Scott Waldron
    TheWayOfCoding.com

    - All resources retain copyright (bitmaps, etc).
    - You are not allowed to use these projects 
    as tutorials or samples distributed with your own branding.
    Basically, any use with the same theme as they were 
    intended for with TheWayOfCoding.com is not allowed.

    The MIT License (MIT)

    Copyright (c) 2016 

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
     */
    class WebClientForStockFinanceHistory
    {
        WebClient webConnector;

        //See Google's terms of use before accessing their services
        //https://www.google.com/finance/historical?q=NYSE%3ADIS&ei=7O4nV9GdJcHomAG02L_wCw
        const string googleAddress = 
            "http://www.google.com/finance/historical?q=[-|ticker|-]&startdate=[-|sdate|-]&enddate=[-|edate|-]&num=30&output=csv";

        //See Yahoo's terms of use before accessing their services
        //http://finance.yahoo.com/q/hp?s=DIS+Historical+Prices
        const string yahooAddress = 
            "http://real-chart.finance.yahoo.com/table.csv?s=[-|ticker|-]&[-|sdate|-]&[-|edate|-]&g=d&ignore=.csv";
        
        /// <summary>
        /// request data from google's historic stock page
        /// </summary>
        /// <param name="market">eg. NYSE</param>
        /// <param name="ticker">eg. DIS</param>
        /// <param name="startDate">start date</param>
        /// <param name="endDate">end date</param>
        /// <returns></returns>
        public Dictionary<DateTime, StockDataItem> getStockDataFromGoogle(string market, string ticker, DateTime startDate, DateTime endDate)
        {
            return fillDataDictionary(getData(constructGoogleLink(market, ticker, startDate, endDate)));
        }

        /// <summary>
        /// form a link to request info from google's historic stock data export
        /// </summary>
        /// <param name="market">eg. NYSE</param>
        /// <param name="ticker">eg. DIS</param>
        /// <param name="startDate">start date</param>
        /// <param name="endDate">end date</param>
        /// <returns></returns>
        string constructGoogleLink(string market, string ticker, DateTime startDate, DateTime endDate)
        {
            string constructedUri = googleAddress;

            constructedUri = constructedUri.Replace("[-|ticker|-]", market + "%3A" + ticker);

            string constructedStartDate = startDate.ToString("MMM") + "+" + startDate.Day.ToString() + "%2C+" + startDate.ToString("yyyy");
            string constructedEndDate = endDate.ToString("MMM") + "+" + endDate.Day.ToString() + "%2C+" + endDate.ToString("yyyy");

            constructedUri = constructedUri.Replace("[-|sdate|-]", constructedStartDate);
            constructedUri = constructedUri.Replace("[-|edate|-]", constructedEndDate);

            return constructedUri;
        }

        /// <summary>
        /// request historic stock information from Yahoo
        /// </summary>
        /// <param name="ticker">eg. DIS</param>
        /// <param name="startDate">start date</param>
        /// <param name="endDate">end date</param>
        /// <returns></returns>
        public Dictionary<DateTime, StockDataItem> getStockDataFromYahoo(string ticker, DateTime startDate, DateTime endDate)
        {
            return fillDataDictionary(getData(constructYahooLink(ticker, startDate, endDate)));
        }

        /// <summary>
        /// constructs a html link to request data from Yahoo
        /// </summary>
        /// <param name="ticker">eg. DIS</param>
        /// <param name="startDate">start date</param>
        /// <param name="endDate">end date</param>
        /// <returns></returns>
        string constructYahooLink(string ticker, DateTime startDate, DateTime endDate)
        {
            string constructedUri = yahooAddress;

            constructedUri = constructedUri.Replace("[-|ticker|-]", ticker);

            string constructedStartDate = 
                "a=" + (startDate.Month - 1).ToString() + 
                "&b=" + startDate.Day.ToString() + 
                "&c=" + startDate.Year.ToString();

            string constructedEndDate = 
                "d=" + (endDate.Month - 1).ToString() + 
                "&e=" + endDate.Day.ToString() + 
                "&f=" + endDate.Year.ToString();

            constructedUri = constructedUri.Replace("[-|sdate|-]", constructedStartDate);
            constructedUri = constructedUri.Replace("[-|edate|-]", constructedEndDate);

            return constructedUri;
        }

        /// <summary>
        /// pull data based on a passed url text
        /// </summary>
        /// <param name="webpageUriString"></param>
        /// <returns></returns>
        string getData(string webpageUriString)
        {
            string tempStorageString = "";

            if (webpageUriString != "")
            {
                //create a new instance of the class
                //using should properly close and dispose so we don't have to bother
                using (webConnector = new WebClient())
                {
                    using (Stream responseStream = webConnector.OpenRead(webpageUriString))
                    {
                        using (StreamReader responseStreamReader = new StreamReader(responseStream))
                        {
                            //extract the response we got from the internet server
                            tempStorageString = responseStreamReader.ReadToEnd();

                            //change the unix style line endings so they work here
                            tempStorageString = tempStorageString.Replace("\n", Environment.NewLine);
                        }
                    }

                    //an alternative method (it appeared slower for me)
                    //tempStorageString = webConnector.DownloadString(webpageUriString);
                }
            }

            return tempStorageString;
        }

        /// <summary>
        /// take csv data and push it into a dictionary
        /// </summary>
        /// <param name="csvData">data from Google or Yahoo in CSV format</param>
        /// <returns></returns>
        Dictionary<DateTime, StockDataItem> fillDataDictionary(string csvData)
        {
            Dictionary<DateTime, StockDataItem> parsedStockData = new Dictionary<DateTime, StockDataItem>();

            using (StringReader reader = new StringReader(csvData))
            {
                string csvLine;

                //drop the first row because it is a header we don't need
                reader.ReadLine();

                while ((csvLine = reader.ReadLine()) != null)
                {
                    string[] splitLine = csvLine.Split(',');

                    if (splitLine.Length >= 6)
                    {
                        StockDataItem newItem = new StockDataItem();

                        //parse all values from the downloaded csv file
                        double tempOpen;
                        if (Double.TryParse(splitLine[1], out tempOpen))
                        {
                            newItem.open = tempOpen;
                        }
                        double tempHigh;
                        if (Double.TryParse(splitLine[2], out tempHigh))
                        {
                            newItem.high = tempHigh;
                        }
                        double tempLow;
                        if (Double.TryParse(splitLine[3], out tempLow))
                        {
                            newItem.low = tempLow;
                        }
                        double tempClose;
                        if (Double.TryParse(splitLine[4], out tempClose))
                        {
                            newItem.close = tempClose;
                        }
                        double tempVolume;
                        if (Double.TryParse(splitLine[5], out tempVolume))
                        {
                            newItem.volume = tempVolume;
                        }

                        //if we parse the date successfully, we add the
                        //new StockDataItem to our result set
                        DateTime tempDate;
                        if (DateTime.TryParse(splitLine[0], out tempDate))
                        {
                            parsedStockData.Add(tempDate, newItem);
                        }
                    }
                }
            }

            return parsedStockData;
        }
    }
}
