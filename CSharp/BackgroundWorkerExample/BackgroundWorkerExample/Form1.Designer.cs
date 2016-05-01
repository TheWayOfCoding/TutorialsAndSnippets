namespace BackgroundWorkerExample
{
    partial class frmBackgroundWorkerExample
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
            this.bwInstance = new System.ComponentModel.BackgroundWorker();
            this.pbLoadingGraphic = new System.Windows.Forms.PictureBox();
            this.lblLoadingStatus = new System.Windows.Forms.Label();
            this.prgLoadingProgress = new System.Windows.Forms.ProgressBar();
            this.btnStartWorker = new System.Windows.Forms.Button();
            this.btnStopWorker = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoadingGraphic)).BeginInit();
            this.SuspendLayout();
            // 
            // bwInstance
            // 
            this.bwInstance.WorkerReportsProgress = true;
            this.bwInstance.WorkerSupportsCancellation = true;
            this.bwInstance.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwInstance_DoWork);
            this.bwInstance.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwInstance_ProgressChanged);
            this.bwInstance.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwInstance_RunWorkerCompleted);
            // 
            // pbLoadingGraphic
            // 
            this.pbLoadingGraphic.Image = global::BackgroundWorkerExample.Properties.Resources.Loading_2_transparent;
            this.pbLoadingGraphic.Location = new System.Drawing.Point(413, 12);
            this.pbLoadingGraphic.Name = "pbLoadingGraphic";
            this.pbLoadingGraphic.Size = new System.Drawing.Size(70, 70);
            this.pbLoadingGraphic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLoadingGraphic.TabIndex = 0;
            this.pbLoadingGraphic.TabStop = false;
            this.pbLoadingGraphic.Visible = false;
            // 
            // lblLoadingStatus
            // 
            this.lblLoadingStatus.AutoSize = true;
            this.lblLoadingStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadingStatus.Location = new System.Drawing.Point(12, 51);
            this.lblLoadingStatus.Name = "lblLoadingStatus";
            this.lblLoadingStatus.Size = new System.Drawing.Size(134, 31);
            this.lblLoadingStatus.TabIndex = 1;
            this.lblLoadingStatus.Text = "Loading...";
            this.lblLoadingStatus.Visible = false;
            // 
            // prgLoadingProgress
            // 
            this.prgLoadingProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgLoadingProgress.Location = new System.Drawing.Point(12, 110);
            this.prgLoadingProgress.Name = "prgLoadingProgress";
            this.prgLoadingProgress.Size = new System.Drawing.Size(471, 23);
            this.prgLoadingProgress.TabIndex = 2;
            // 
            // btnStartWorker
            // 
            this.btnStartWorker.Location = new System.Drawing.Point(12, 12);
            this.btnStartWorker.Name = "btnStartWorker";
            this.btnStartWorker.Size = new System.Drawing.Size(75, 23);
            this.btnStartWorker.TabIndex = 3;
            this.btnStartWorker.Text = "Start Worker";
            this.btnStartWorker.UseVisualStyleBackColor = true;
            this.btnStartWorker.Click += new System.EventHandler(this.btnStartWorker_Click);
            // 
            // btnStopWorker
            // 
            this.btnStopWorker.Enabled = false;
            this.btnStopWorker.Location = new System.Drawing.Point(94, 12);
            this.btnStopWorker.Name = "btnStopWorker";
            this.btnStopWorker.Size = new System.Drawing.Size(75, 23);
            this.btnStopWorker.TabIndex = 4;
            this.btnStopWorker.Text = "Stop Worker";
            this.btnStopWorker.UseVisualStyleBackColor = true;
            this.btnStopWorker.Click += new System.EventHandler(this.btnStopWorker_Click);
            // 
            // frmBackgroundWorkerExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 145);
            this.Controls.Add(this.btnStopWorker);
            this.Controls.Add(this.btnStartWorker);
            this.Controls.Add(this.prgLoadingProgress);
            this.Controls.Add(this.lblLoadingStatus);
            this.Controls.Add(this.pbLoadingGraphic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmBackgroundWorkerExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Background Worker Example - TheWayOfCoding.com";
            this.Load += new System.EventHandler(this.frmBackgroundWorkerExample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbLoadingGraphic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bwInstance;
        private System.Windows.Forms.PictureBox pbLoadingGraphic;
        private System.Windows.Forms.Label lblLoadingStatus;
        private System.Windows.Forms.ProgressBar prgLoadingProgress;
        private System.Windows.Forms.Button btnStartWorker;
        private System.Windows.Forms.Button btnStopWorker;
    }
}

