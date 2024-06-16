using System.Numerics;
using System.Xml;

namespace Data
{
    internal class Logger
    {
        private static Logger _instance;
        private static readonly object _lock = new object();

        private Queue<LogEntry> loggerQueue = new Queue<LogEntry>();
        private AutoResetEvent hasNewItems = new AutoResetEvent(false);
        private XmlWriter writerXml;
        private Thread logger;
        private readonly int size = 1000;

        private Logger()
        {
            writerXml = XmlWriter.Create("logger.xml");
            writerXml.WriteStartElement("locationPoolBall");
            logger = new Thread(new ThreadStart(ThreadQueue));
            logger.IsBackground = true;
            logger.Start();
        }

        ~Logger()
        {
            logger.Join();

            lock (writerXml)
            {
                writerXml.WriteEndElement();
                writerXml.Close();
            }
        }

        public static Logger GetInstance()
        {

            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Logger();
                    }
                }
            }
            return _instance;

        }

        public void LogBallPosition(int id, Vector2 position, DateTime time)
        {
            lock (loggerQueue)
            {
                if (loggerQueue.Count >= size)
                {
                    throw new InvalidOperationException("bufor jest zapelniony.");
                    
                }

                loggerQueue.Enqueue(new LogEntry(id, position, time));
            }

            hasNewItems.Set();
        }

        private void ThreadQueue()
        {
            while (true)
            {
                hasNewItems.WaitOne();


                while (loggerQueue.Count > 0)
                {

                        LogEntry logEntry = loggerQueue.Dequeue();
                        if (logEntry != null)
                        {
                            LogBallPositionAsXML(logEntry);
                        }

                }


            }
        }


        private void LogBallPositionAsXML(LogEntry logEntry)
        {
            lock (writerXml)
            {
                writerXml.WriteStartElement("PoolBall");
                writerXml.WriteElementString("id", logEntry.ID.ToString());
                writerXml.WriteElementString("x", logEntry.Position.X.ToString());
                writerXml.WriteElementString("y", logEntry.Position.Y.ToString());
                writerXml.WriteElementString("timestamp", logEntry.Time.ToString("u"));
                writerXml.WriteEndElement();
                writerXml.Flush();
            }
        }
    }

    class LogEntry
    {
        public int ID { get; }
        public Vector2 Position { get; }
        public DateTime Time { get; }

        public LogEntry(int id, Vector2 position, DateTime time)
        {
            ID = id;
            Position = position;
            Time = time;
        }
    }
}
