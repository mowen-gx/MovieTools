namespace MovieLink.MovieInfoWin
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbParse = new System.Windows.Forms.ListBox();
            this.bgwMtime = new System.ComponentModel.BackgroundWorker();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lbDb = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbHasDone = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbParse
            // 
            this.lbParse.FormattingEnabled = true;
            this.lbParse.ItemHeight = 12;
            this.lbParse.Location = new System.Drawing.Point(14, 15);
            this.lbParse.Name = "lbParse";
            this.lbParse.Size = new System.Drawing.Size(592, 376);
            this.lbParse.TabIndex = 0;
            // 
            // bgwMtime
            // 
            this.bgwMtime.WorkerReportsProgress = true;
            this.bgwMtime.WorkerSupportsCancellation = true;
            this.bgwMtime.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwMtime_DoWork);
            this.bgwMtime.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwMtime_ProgressChanged);
            this.bgwMtime.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwMtime_RunWorkerCompleted);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 422);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(174, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "导入时光网电影信息";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(1176, 422);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lbDb
            // 
            this.lbDb.FormattingEnabled = true;
            this.lbDb.ItemHeight = 12;
            this.lbDb.Location = new System.Drawing.Point(612, 15);
            this.lbDb.Name = "lbDb";
            this.lbDb.Size = new System.Drawing.Size(639, 376);
            this.lbDb.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 400);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "进度：";
            // 
            // lbHasDone
            // 
            this.lbHasDone.AutoSize = true;
            this.lbHasDone.Location = new System.Drawing.Point(50, 400);
            this.lbHasDone.Name = "lbHasDone";
            this.lbHasDone.Size = new System.Drawing.Size(0, 12);
            this.lbHasDone.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 457);
            this.Controls.Add(this.lbHasDone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbDb);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lbParse);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbParse;
        private System.ComponentModel.BackgroundWorker bgwMtime;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ListBox lbDb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbHasDone;
    }
}

