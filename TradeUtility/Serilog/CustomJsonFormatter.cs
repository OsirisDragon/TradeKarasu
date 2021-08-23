using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using System;
using System.Collections;
using System.IO;

namespace TradeUtility.Serilog
{
    public class CustomJsonFormatter : ITextFormatter
    {
        readonly JsonValueFormatter _valueFormatter;

        public CustomJsonFormatter(JsonValueFormatter valueFormatter = null)
        {
            _valueFormatter = valueFormatter ?? new JsonValueFormatter(typeTagName: "$type");
        }

        public void Format(LogEvent logEvent, TextWriter output)
        {
            FormatEvent(logEvent, output, _valueFormatter);
            output.WriteLine();
        }

        public static void FormatEvent(LogEvent logEvent, TextWriter output, JsonValueFormatter valueFormatter)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            if (output == null) throw new ArgumentNullException(nameof(output));
            if (valueFormatter == null) throw new ArgumentNullException(nameof(valueFormatter));

            output.Write("{\"@t\":\"");
            output.Write(logEvent.Timestamp.ToString("yyyy/MM/dd HH:mm:ss"));

            output.Write("\",\"@mt\":");
            JsonValueFormatter.WriteQuotedJsonString(logEvent.MessageTemplate.Text, output);

            output.Write(",\"@l\":\"");
            output.Write(logEvent.Level);
            output.Write('\"');

            if (logEvent.Exception != null)
            {
                output.Write(",\"@x\":");
                JsonValueFormatter.WriteQuotedJsonString(logEvent.Exception.ToString(), output);
            }

            // 我不要顯示這些
            ArrayList excludeProperties = new ArrayList() { "ActionId", "RequestId", "CorrelationId", "ConnectionId" };

            foreach (var property in logEvent.Properties)
            {
                var name = property.Key;

                if (excludeProperties.Contains(name)) continue;

                if (name.Length > 0 && name[0] == '@')
                {
                    // Escape first '@' by doubling
                    name = '@' + name;
                }

                output.Write(',');
                JsonValueFormatter.WriteQuotedJsonString(name, output);
                output.Write(':');
                valueFormatter.Format(property.Value, output);
            }

            output.Write('}');
        }
    }
}
