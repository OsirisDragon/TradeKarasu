using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using System;

namespace TradeUtility.Serilog
{
    public static class SinkExtensions
    {
        public static LoggerConfiguration DbLogfSink(
                  this LoggerSinkConfiguration loggerConfiguration,
                  IFormatProvider formatProvider = null,
                  LogEventLevel restrictedToMinimumLevel = LogEventLevel.Verbose)
        {
            return loggerConfiguration.Sink(new DbLogfSink(formatProvider, restrictedToMinimumLevel));
        }
    }
}
