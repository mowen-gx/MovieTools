using System.Threading;
using MovieLink.Service.Interface.Worker;

namespace MovieLink.Service.Impl.Worker.Piaohua
{
    public class Worker : IWork
    {
        public void Run()
        {
            PageParserWorker pageWorker = new PageParserWorker();
            Thread pageParserThread = new Thread(pageWorker.Run);
            pageParserThread.IsBackground = true;
            pageParserThread.Start();

            for (int i = 0; i < 5; i++)
            {
                MovieParseWorker detaiWorker = new MovieParseWorker();
                Thread detaiParserThread = new Thread(detaiWorker.Run);
                detaiParserThread.IsBackground = true;
                detaiParserThread.Start();
            }

            DataProcessor.DataProcessor processor = new DataProcessor.DataProcessor();
            Thread processorThread = new Thread(processor.Run);
            processorThread.IsBackground = true;
            processorThread.Start();
        }
    }
}
