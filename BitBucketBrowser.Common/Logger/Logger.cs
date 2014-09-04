namespace BitBucketBrowser.Common.Logger
{
    using BitBucketBrowser.Common.Logger.Interfaces;

    using NLog;

    public class Logger : ILogger
    {
        private readonly NLog.Logger logger;

        public Logger(string loggerName)
        {
            this.logger = LogManager.GetLogger(loggerName);
        }

        public void Trace(string message)
        {
            this.logger.Trace(message);
        }

        public void Debug(string message)
        {
            this.logger.Debug(message);
        }

        public void Info(string message)
        {
            this.logger.Info(message);
        }

        public void Warn(string message)
        {
            this.logger.Warn(message);
        }

        public void Error(string message)
        {
            this.logger.Error(message);
        }

        public void Fatal(string message)
        {
            this.logger.Fatal(message);
        }
    }
}
