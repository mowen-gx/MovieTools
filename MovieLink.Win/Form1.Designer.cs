namespace MovieLink.Win
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
            this.bgwPiaoHua = new System.ComponentModel.BackgroundWorker();
            this.bgwDy2018 = new System.ComponentModel.BackgroundWorker();
            this.btnPiaohua = new System.Windows.Forms.Button();
            this.btnDy2018 = new System.Windows.Forms.Button();
            this.lbDb = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbHasDone = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbParse
            // 
            this.lbParse.FormattingEnabled = true;
            this.lbParse.ItemHeight = 12;
            this.lbParse.Location = new System.Drawing.Point(12, 12);
            this.lbParse.Name = "lbParse";
            this.lbParse.Size = new System.Drawing.Size(637, 400);
            this.lbParse.TabIndex = 0;
            // 
            // bgwPiaoHua
            // 
            this.bgwPiaoHua.WorkerReportsProgress = true;
            this.bgwPiaoHua.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwPiaoHua_DoWork);
            this.bgwPiaoHua.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwPiaoHua_ProgressChanged);
            this.bgwPiaoHua.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwPiaoHua_RunWorkerCompleted);
            // 
            // bgwDy2018
            // 
            this.bgwDy2018.WorkerReportsProgress = true;
            this.bgwDy2018.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDy2018_DoWork);
            this.bgwDy2018.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwDy2018_ProgressChanged);
            this.bgwDy2018.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwDy2018_RunWorkerCompleted);
            // 
            // btnPiaohua
            // 
            this.btnPiaohua.Location = new System.Drawing.Point(12, 442);
            this.btnPiaohua.Name = "btnPiaohua";
            this.btnPiaohua.Size = new System.Drawing.Size(157, 23);
            this.btnPiaohua.TabIndex = 1;
            this.btnPiaohua.Text = "导入飘花电影";
            this.btnPiaohua.UseVisualStyleBackColor = true;
            this.btnPiaohua.Click += new System.EventHandler(this.btnPiaohua_Click);
            // 
            // btnDy2018
            // 
            this.btnDy2018.Location = new System.Drawing.Point(187, 441);
            this.btnDy2018.Name = "btnDy2018";
            this.btnDy2018.Size = new System.Drawing.Size(159, 23);
            this.btnDy2018.TabIndex = 2;
            this.btnDy2018.Text = "导入电影天堂电影";
            this.btnDy2018.UseVisualStyleBackColor = true;
            this.btnDy2018.Click += new System.EventHandler(this.btnDy2018_Click);
            // 
            // lbDb
            // 
            this.lbDb.FormattingEnabled = true;
            this.lbDb.ItemHeight = 12;
            this.lbDb.Location = new System.Drawing.Point(655, 12);
            this.lbDb.Name = "lbDb";
            this.lbDb.Size = new System.Drawing.Size(637, 400);
            this.lbDb.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 419);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "进度：";
            // 
            // lbHasDone
            // 
            this.lbHasDone.AutoSize = true;
            this.lbHasDone.Location = new System.Drawing.Point(57, 419);
            this.lbHasDone.Name = "lbHasDone";
            this.lbHasDone.Size = new System.Drawing.Size(0, 12);
            this.lbHasDone.TabIndex = 7;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(1216, 441);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 470);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lbHasDone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbDb);
            this.Controls.Add(this.btnDy2018);
            this.Controls.Add(this.btnPiaohua);
            this.Controls.Add(this.lbParse);
            this.Name = "Form1";
            this.Text = "电影导入工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbParse;
        private System.ComponentModel.BackgroundWorker bgwPiaoHua;
        private System.ComponentModel.BackgroundWorker bgwDy2018;
        private System.Windows.Forms.Button btnPiaohua;
        private System.Windows.Forms.Button btnDy2018;
        private System.Windows.Forms.ListBox lbDb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbHasDone;
        private System.Windows.Forms.Button btnStop;
    }
}

