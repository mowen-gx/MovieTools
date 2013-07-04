using System.Collections.Generic;

namespace MovieLink.Service
{
    public class DbMsg
    {
        private static readonly object ObjForLock = new object();
        private static List<string> _msgs = new List<string>();
        private static int _hasGet = 0;

        /// <summary>
        /// 获取消息
        /// </summary>
        public static List<string> CurrentMsgs
        {
            get
            {
                lock (ObjForLock)
                {
                    List<string> temp = new List<string>();
                    int count = _msgs.Count;
                    for (int i = _hasGet; i < count; i++)
                    {
                        temp.Add(_msgs[i]);
                    }
                    _hasGet = count;
                    return temp;
                }
            }
        }

        /// <summary>
        /// 设置消息
        /// </summary>
        /// <param name="msg"></param>
        public static void SetMsg(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
                lock (ObjForLock)
                {
                    _msgs.Add(msg);
                }
        }
    }
}
