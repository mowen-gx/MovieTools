using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using MovieLink.Service;

namespace MovieLink.Win
{
    public partial class Form1 : Form
    {
        private static readonly object ObjForLock = new object();
        public Form1()
        {
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();
        }

        /// <summary>
        /// 导入飘花电影
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPiaohua_Click(object sender, EventArgs e)
        {
            if (bgwPiaoHua.IsBusy)
            {
                return;
            }
            Service.Data.IsCancel = false;
            lbParse.Items.Clear();
            lbParse.Items.Add("飘花电影导入任务开始执行");
            bgwPiaoHua.RunWorkerAsync();
            btnPiaohua.Enabled = false;
            btnDy2018.Enabled = false;
            btnStop.Enabled = true;
        }
        
        /// <summary>
        /// 飘花后台任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwPiaoHua_DoWork(object sender, DoWorkEventArgs e)
        {
            Service.Data.Reset(Util.MovieType.Piaohua);
            Service.Impl.Worker.Piaohua.Worker worker = new Service.Impl.Worker.Piaohua.Worker();
            worker.Run();

            while (!Service.Data.IsDoneDetailLinkFinish(Util.MovieType.Piaohua) && !Service.Data.IsDoneMovieFinish && !Service.Data.IsCanceled)
            {
                MovieMsg msg = new MovieMsg();
                msg.ParserMsgs = ParserMsg.CurrentMsgs;
                msg.DbMsgs = DbMsg.CurrentMsgs;
                msg.CurrentGetWebDetailLink = Service.Data.HasGetWebDetailLinkCount();
                msg.CurrentDbNotParseDetailLink = Service.Data.GetNotParseDetailLinkFromDb(Util.MovieType.Piaohua);
                msg.CurrentDoneDetailLink = Service.Data.GetDoneDetailLinkCount();
                msg.CurrentDoneMovie = Service.Data.GetDoneMovieCount();
                bgwPiaoHua.ReportProgress(0, msg);
                Thread.Sleep(100);
            }
            bgwPiaoHua.ReportProgress(100, null);
        }

        /// <summary>
        /// 飘花电影导入进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwPiaoHua_ProgressChanged(object sender, ProgressChangedEventArgs e)
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

        /// <summary>
        /// 飘花电影导入完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwPiaoHua_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (!Service.Data.IsCancel)
            {
                lbParse.Items.Add("飘花电影导入完成");
            }
            else
            {
                lbParse.Items.Add("任务取消完成");
                lbDb.Items.Add("任务取消完成");
            }
            lbParse.SelectedIndex = lbParse.Items.Count - 1;
            lbParse.SelectedIndex = -1;
            lbDb.SelectedIndex = lbDb.Items.Count - 1;
            lbDb.SelectedIndex = -1;
            btnPiaohua.Enabled = true;
            btnDy2018.Enabled = true;
            btnStop.Enabled = true;
        }

        /// <summary>
        /// 电影天堂电影导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDy2018_Click(object sender, EventArgs e)
        {
            if (bgwDy2018.IsBusy)
            {
                return;
            }
            Service.Data.IsCancel = false;
            lbParse.Items.Clear();
            lbParse.Items.Add("电影天堂电影导入任务开始执行");
            bgwDy2018.RunWorkerAsync();
            btnPiaohua.Enabled = false;
            btnDy2018.Enabled = false;
            btnStop.Enabled = true;
        }

        /// <summary>
        /// 电影天堂电影后台任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwDy2018_DoWork(object sender, DoWorkEventArgs e)
        {
            Service.Data.Reset(Util.MovieType.Dy2018);
            Service.Impl.Worker.Dy2018.Worker worker = new Service.Impl.Worker.Dy2018.Worker();
            worker.Run();

            while (!Service.Data.IsDoneDetailLinkFinish(Util.MovieType.Dy2018) && !Service.Data.IsDoneMovieFinish && !Service.Data.IsCanceled)
            {
                MovieMsg msg = new MovieMsg();
                msg.ParserMsgs = ParserMsg.CurrentMsgs;
                msg.DbMsgs = DbMsg.CurrentMsgs;
                msg.CurrentGetWebDetailLink = Service.Data.HasGetWebDetailLinkCount();
                msg.CurrentDbNotParseDetailLink = Service.Data.GetNotParseDetailLinkFromDb(Util.MovieType.Dy2018);
                msg.CurrentDoneDetailLink = Service.Data.GetDoneDetailLinkCount();
                msg.CurrentDoneMovie = Service.Data.GetDoneMovieCount();
                bgwDy2018.ReportProgress(0, msg);
                Thread.Sleep(100);
            }
            bgwDy2018.ReportProgress(100, null);
        }

        /// <summary>
        /// 电影天堂后台任务进度切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwDy2018_ProgressChanged(object sender, ProgressChangedEventArgs e)
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

        /// <summary>
        /// 电影天堂后台任务完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwDy2018_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!Service.Data.IsCancel)
            {
                lbParse.Items.Add("电影天堂电影导入完成");
            }
            else
            {
                lbParse.Items.Add("任务取消完成");
                lbDb.Items.Add("任务取消完成");
            }
            lbParse.SelectedIndex = lbParse.Items.Count - 1;
            lbParse.SelectedIndex = -1;
            lbDb.SelectedIndex = lbDb.Items.Count - 1;
            lbDb.SelectedIndex = -1;
            btnDy2018.Enabled = true;
            btnPiaohua.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            lbParse.Items.Add("任务取消中");
            lbDb.Items.Add("任务取消中");
            Service.Data.IsCancel = true;
            btnStop.Enabled = false;
        }
    }
}
