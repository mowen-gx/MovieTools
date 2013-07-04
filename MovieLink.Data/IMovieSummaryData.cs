namespace MovieLink.Data
{
    public interface IMovieSummaryData
    {
        /// <summary>
        /// 添加电影简介到数据库
        /// </summary>
        /// <param name="movieGuid"></param>
        /// <param name="summary"></param>
        /// <returns></returns>
        void AddSummary(string movieGuid, string summary);
    }
}