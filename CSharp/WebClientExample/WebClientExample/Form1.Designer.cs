namespace WebClientExample
{
    partial class frmWebClientExample
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtGoogleResult = new System.Windows.Forms.TextBox();
            this.btnStartExample = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtYahooResult = new System.Windows.Forms.TextBox();
            this.bwProcessor = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtGoogleResult
            // 
            this.txtGoogleResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGoogleResult.Location = new System.Drawing.Point(3, 3);
            this.txtGoogleResult.Multiline = true;
            this.txtGoogleResult.Name = "txtGoogleResult";
            this.txtGoogleResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGoogleResult.Size = new System.Drawing.Size(386, 267);
            this.txtGoogleResult.TabIndex = 0;
            this.txtGoogleResult.WordWrap = false;
            // 
            // btnStartExample
            // 
            this.btnStartExample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartExample.Location = new System.Drawing.Point(12, 12);
            this.btnStartExample.Name = "btnStartExample";
            this.btnStartExample.Size = new System.Drawing.Size(773, 23);
            this.btnStartExample.TabIndex = 1;
            this.btnStartExample.Text = "Request the past 10 days of historic stock data on the ticker DIS.";
            this.btnStartExample.UseVisualStyleBackColor = true;
            this.btnStartExample.Click += new System.EventHandler(this.btnStartExample_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 41);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtGoogleResult);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtYahooResult);
            this.splitContainer1.Size = new System.Drawing.Size(773, 273);
            this.splitContainer1.SplitterDistance = 392;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // txtYahooResult
            // 
            this.txtYahooResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtYahooResult.Location = new System.Drawing.Point(3, 3);
            this.txtYahooResult.Multiline = true;
            this.txtYahooResult.Name = "txtYahooResult";
            this.txtYahooResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtYahooResult.Size = new System.Drawing.Size(371, 267);
            this.txtYahooResult.TabIndex = 1;
            this.txtYahooResult.WordWrap = false;
            // 
            // bwProcessor
            // 
            this.bwProcessor.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwProcessor_DoWork);
            this.bwProcessor.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwProcessor_RunWorkerCompleted);
            // 
            // frmWebClientExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 326);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnStartExample);
            this.Name = "frmWebClientExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebClient Example - TheWayOfCoding.com";
            this.Load += new System.EventHandler(this.frmWebClientExample_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtGoogleResult;
        private System.Windows.Forms.Button btnStartExample;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtYahooResult;
        private System.ComponentModel.BackgroundWorker bwProcessor;
    }
}

