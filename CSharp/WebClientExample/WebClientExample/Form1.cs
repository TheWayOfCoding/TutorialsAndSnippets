using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    public partial class frmWebClientExample : Form
    {
        public frmWebClientExample()
        {
            InitializeComponent();
        }

        string googleDataResult = "";
        string yahooDataResult = "";

        private void btnStartExample_Click(object sender, EventArgs e)
        {
            //attempt to start the request to the two websites
            googleDataResult = "";
            yahooDataResult = "";
            if(!bwProcessor.IsBusy)
            {
                btnStartExample.Enabled = false;
                txtGoogleResult.Text = "";
                txtYahooResult.Text = "";
                bwProcessor.RunWorkerAsync();
            } else
            {
                MessageBox.Show("The background worker is busy...");
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void bwProcessor_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime tenDaysPast = DateTime.Now.Subtract(TimeSpan.FromDays(10));
            StringBuilder textString = new StringBuilder();
            WebClientForStockFinanceHistory wc1 = new WebClientForStockFinanceHistory();
            Dictionary<DateTime, StockDataItem> downloadedStockData;

            //request data from Google for the past 10 days
            downloadedStockData = wc1.getStockDataFromGoogle("NYSE", "DIS", tenDaysPast, DateTime.Now);

            textString.AppendLine("Data on DIS from Google:");

            foreach (KeyValuePair<DateTime, StockDataItem> singleItem in downloadedStockData)
            {
                textString.AppendLine(singleItem.Key.ToShortDateString() +
                    " [" +
                    singleItem.Value.open.ToString("0.0") + "; " +
                    singleItem.Value.close.ToString("0.0") + "; " +
                    singleItem.Value.high.ToString("0.0") + "; " +
                    singleItem.Value.low.ToString("0.0") + "; " +
                    singleItem.Value.volume +
                    "]");
            }

            //save the Google result to a string on the UI thread
            googleDataResult = textString.ToString();
            
            textString.Clear();
            textString.AppendLine("Data on DIS from Yahoo:");

            //request data from Yahoo for the past 10 days
            downloadedStockData = wc1.getStockDataFromYahoo("DIS", tenDaysPast, DateTime.Now);

            foreach (KeyValuePair<DateTime, StockDataItem> singleItem in downloadedStockData)
            {
                textString.AppendLine(singleItem.Key.ToShortDateString() +
                    " [" +
                    singleItem.Value.open.ToString("0.0") + "; " +
                    singleItem.Value.close.ToString("0.0") + "; " +
                    singleItem.Value.high.ToString("0.0") + "; " +
                    singleItem.Value.low.ToString("0.0") + "; " +
                    singleItem.Value.volume +
                    "]");
            }

            //save the Yahoo result to a string on the UI thread
            yahooDataResult = textString.ToString();
        }

        private void bwProcessor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStartExample.Enabled = true;
            txtGoogleResult.Text = googleDataResult;
            txtYahooResult.Text = yahooDataResult;
        }

        private void frmWebClientExample_Load(object sender, EventArgs e)
        {

        }
    }
}
