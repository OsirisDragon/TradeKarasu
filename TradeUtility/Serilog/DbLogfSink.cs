using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeUtility.Serilog
{
    public class DbLogfSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly LogEventLevel _restrictedToMinimumLevel;

        public DbLogfSink(IFormatProvider formatProvider, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Verbose)
        {
            _formatProvider = formatProvider;
            _restrictedToMinimumLevel = restrictedToMinimumLevel;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent.Level >= _restrictedToMinimumLevel)
            {
                var message = logEvent.RenderMessage(_formatProvider);
                Console.WriteLine(DateTimeOffset.Now.ToString() + " QQQQQQ " + message);
            }
        }
    }
}
