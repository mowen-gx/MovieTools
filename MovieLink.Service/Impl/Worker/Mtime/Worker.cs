using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MovieLink.Service.Interface.Worker;

namespace MovieLink.Service.Impl.Worker.Mtime
{
    public class Worker : IWork
    {
        public void Run()
        {
            PageParserWorker pageWorker = new PageParserWorker();
            Thread pageParserThread = new Thread(pageWorker.Run);
            pageParserThread.IsBackground = true;
            pageParserThread.Start();

            for (int i = 0; i < 1; i++)
            {
                MovieParseWorker detaiWorker = new MovieParseWorker();
                Thread detaiParserThread = new Thread(detaiWorker.Run);
                detaiParserThread.IsBackground = true;
                detaiParserThread.Start();
            }

            DataProcessor.MovieInfoDataProcessor processor = new DataProcessor.MovieInfoDataProcessor();
            Thread processorThread = new Thread(processor.Run);
            processorThread.IsBackground = true;
            processorThread.Start();
        }
    }
}
