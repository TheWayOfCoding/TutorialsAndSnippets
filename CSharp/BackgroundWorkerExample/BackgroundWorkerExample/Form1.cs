using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundWorkerExample
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
    public partial class frmBackgroundWorkerExample : Form
    {
        public frmBackgroundWorkerExample()
        {
            InitializeComponent();
        }

        public enum CurrentStatus
        {
            None,
            Reset,
            Loading,
            Cancelled,
            Success,
            Busy
        }
        
        //used throughout the form to track application status
        CurrentStatus processingStatus = CurrentStatus.None;

        /// <summary>
        /// changes the interface based on current status (processingStatus)
        /// </summary>
        void setFormControlsBasedOnStatus()
        {
            switch (processingStatus)
            {
                case CurrentStatus.None:
                    break;
                case CurrentStatus.Reset:
                    btnStartWorker.Enabled = true;
                    btnStopWorker.Enabled = false;
                    lblLoadingStatus.Visible = false;
                    lblLoadingStatus.Text = "Loading...";
                    pbLoadingGraphic.Visible = false;
                    prgLoadingProgress.Value = 0;
                    break;

                case CurrentStatus.Loading:
                    btnStartWorker.Enabled = false;
                    btnStopWorker.Enabled = true;
                    lblLoadingStatus.Visible = true;
                    lblLoadingStatus.Text = "Loading...";
                    pbLoadingGraphic.Visible = true;
                    prgLoadingProgress.Value = 0;
                    break;

                case CurrentStatus.Cancelled:
                    btnStartWorker.Enabled = true;
                    btnStopWorker.Enabled = false;
                    lblLoadingStatus.Visible = true;
                    lblLoadingStatus.Text = "Cancelled";
                    pbLoadingGraphic.Visible = false;
                    prgLoadingProgress.Value = 0;
                    break;

                case CurrentStatus.Success:
                    btnStartWorker.Enabled = true;
                    btnStopWorker.Enabled = false;
                    lblLoadingStatus.Visible = true;
                    lblLoadingStatus.Text = "Success!";
                    pbLoadingGraphic.Visible = false;
                    prgLoadingProgress.Value = 0;
                    break;

                case CurrentStatus.Busy:
                    btnStartWorker.Enabled = true;
                    btnStopWorker.Enabled = false;
                    lblLoadingStatus.Visible = true;
                    lblLoadingStatus.Text = "The worker is busy.";
                    pbLoadingGraphic.Visible = false;
                    prgLoadingProgress.Value = 0;
                    break;

                default:
                    break;
            }

        }

        /// <summary>
        /// see if the user wants to cancel the background worker
        /// </summary>
        /// <param name="worker">the worker</param>
        /// <param name="e">worker status</param>
        /// <returns>used to tell the caller to exit or not</returns>
        bool isWorkerBeingCancelled(BackgroundWorker worker, DoWorkEventArgs e)
        {
            bool returnValue = false;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                returnValue = true;
                processingStatus = CurrentStatus.Cancelled;
            }

            return returnValue;
        }

        /// <summary>
        /// background worker processing step
        /// </summary>
        /// <param name="sender">worker instance</param>
        /// <param name="e">current status</param>
        private void bwInstance_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            //mimic work and update or cancel the worker as we go
            if (isWorkerBeingCancelled(worker, e)) return; //check for user cancel
            worker.ReportProgress(20);
            System.Threading.Thread.Sleep(250);

            //mimic work and update or cancel the worker as we go
            if (isWorkerBeingCancelled(worker, e)) return; //check for user cancel
            worker.ReportProgress(40);
            System.Threading.Thread.Sleep(250);

            //mimic work and update or cancel the worker as we go
            if (isWorkerBeingCancelled(worker, e)) return; //check for user cancel
            worker.ReportProgress(60);
            System.Threading.Thread.Sleep(250);

            //mimic work and update or cancel the worker as we go
            if (isWorkerBeingCancelled(worker, e)) return; //check for user cancel
            worker.ReportProgress(80);
            System.Threading.Thread.Sleep(250);

            //mimic work and update or cancel the worker as we go
            if (isWorkerBeingCancelled(worker, e)) return; //check for user cancel
            worker.ReportProgress(100);
            System.Threading.Thread.Sleep(250);

            processingStatus = CurrentStatus.Success;
        }

        /// <summary>
        /// updates any progress controls visually
        /// </summary>
        /// <param name="sender">current instance</param>
        /// <param name="e">passed progress</param>
        private void bwInstance_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgLoadingProgress.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// happens once the background worker is completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwInstance_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            setFormControlsBasedOnStatus();
        }

        /// <summary>
        /// a button to start processing
        /// </summary>
        private void btnStartWorker_Click(object sender, EventArgs e)
        {
            if (bwInstance.IsBusy == false)
            {
                processingStatus = CurrentStatus.Loading;
                setFormControlsBasedOnStatus();
                bwInstance.RunWorkerAsync();
            }
            else
            {
                processingStatus = CurrentStatus.Busy;
                setFormControlsBasedOnStatus();
            }
        }

        /// <summary>
        /// a button to stop processing
        /// </summary>
        private void btnStopWorker_Click(object sender, EventArgs e)
        {
            bwInstance.CancelAsync();
        }

        private void frmBackgroundWorkerExample_Load(object sender, EventArgs e)
        {

        }
    }
}
