using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MovieLink.Service;

namespace MovieLink.MovieInfoWin
{
    public partial class Form1 : Form
    {
        private static readonly object ObjForLock = new object();
        public Form1()
        {
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();
        }

        private void bgwMtime_DoWork(object sender, DoWorkEventArgs e)
        {
            Service.Data.Reset(Util.MovieType.Mtime);
            Service.Impl.Worker.Mtime.Worker worker = new Service.Impl.Worker.Mtime.Worker();
            worker.Run();

            while (!Service.Data.IsDoneDetailLinkFinish(Util.MovieType.Mtime) && !Service.Data.IsDoneMovieFinish && !Service.Data.IsCanceled)
            {
                MovieMsg msg = new MovieMsg();
                msg.ParserMsgs = ParserMsg.CurrentMsgs;
                msg.DbMsgs = DbMsg.CurrentMsgs;
                msg.CurrentGetWebDetailLink = Service.Data.HasGetWebDetailInfoLinkCount();
                msg.CurrentDbNotParseDetailLink = Service.Data.GetNotParseMovieInfoLinks();
                msg.CurrentDoneDetailLink = Service.Data.GetDoneMovieInfoLinks();
                msg.CurrentDoneMovie = Service.Data.GetDoneMovieCount();
                bgwMtime.ReportProgress(0, msg);
                Thread.Sleep(100);
            }
            bgwMtime.ReportProgress(100, null);
        }

        private void bgwMtime_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lock (ObjForLock)
            {
                MovieMsg msgObj = ((MovieMsg)e.UserState);
                if (msgObj != null)
                {
                    msgObj.ParserMsgs = msgObj.ParserMsgs ?? new List<string>();
                    msgObj.DbMsgs = msgObj.DbMsgs ?? new List<string>();
                    foreach (string msg in msgObj.ParserMsgs)
                    {
                        if (!string.IsNullOrEmpty(msg))
                            lbParse.Items.Add(msg);
                    }
                    if (lbParse.Items.Count > 0 && msgObj.ParserMsgs.Count > 0)
                    {
                        lbParse.SelectedIndex = lbParse.Items.Count - 1;
                        lbParse.SelectedIndex = -1;
                    }

                    foreach (string msg in msgObj.DbMsgs)
                    {
                        if (!string.IsNullOrEmpty(msg))
                            lbDb.Items.Add(msg);
                    }
                    if (lbDb.Items.Count > 0 && msgObj.DbMsgs.Count > 0)
                    {
                        lbDb.SelectedIndex = lbDb.Items.Count - 1;
                        lbDb.SelectedIndex = -1;
                    }

                    lbHasDone.Text = "获取电影链接" + msgObj.CurrentGetWebDetailLink + "部，已解析库中" + msgObj.CurrentDoneDetailLink + "部，库中尚未解析的链接有：" + msgObj.CurrentDbNotParseDetailLink + "部，添加到数据库" + msgObj.CurrentDoneMovie +
                                     "部";
                }
            }
        }

        private void bgwMtime_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (bgwMtime.IsBusy)
            {
                return;
            }
            Service.Data.IsCancel = false;
            lbParse.Items.Clear();
            lbParse.Items.Add("时光网电影信息导入任务开始执行");
            bgwMtime.RunWorkerAsync();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            lbParse.Items.Add("任务取消中");
            Service.Data.IsCancel = true;
            btnStop.Enabled = false;
        }
    }
}
