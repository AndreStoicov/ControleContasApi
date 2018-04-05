using System;
using log4net;

namespace HubFintech.ControleContas.Api.Configuration.Factories
{
    public interface ILogFactory
    {
        string CorrelationId();
        ILog Log();
    }

    public class LogFactory : ILogFactory
    {
        private string _correlationId;

        public LogFactory()
        {
            GlobalContext.Properties["HOSTNAME"] = Environment.MachineName;
        }

        public string CorrelationId()
        {
            if (string.IsNullOrWhiteSpace(_correlationId))
                _correlationId = Guid.NewGuid().ToString("N");

            return _correlationId;
        }

        public ILog Log()
        {
            return LogManager.GetLogger(CorrelationId());
        }
    }
}