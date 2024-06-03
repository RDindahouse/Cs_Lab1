using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace LogParser
{
    public class Event
    {
        public DateTime Date { get; set; }
        public string Result { get; set; } = string.Empty;
        public string IpFrom { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string UrlTo { get; set; } = string.Empty;
        public int Response { get; set; }
    }

    public class Log
    {
        public List<Event> Events { get; set; }

        public Log()
        {
            Events = new List<Event>();
        }
    }

    public static class LogParser
    {
        public static Log ParseXml(string xml)
        {
            Log log = new Log();

            XDocument doc = XDocument.Parse(xml);
            foreach (XElement eventElement in doc.Descendants("event"))
            {
                Event logEvent = new Event();
                logEvent.Date = DateTime.ParseExact(eventElement.Attribute("date").Value.Trim(), "dd/MMM/yyyy:HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                logEvent.Result = eventElement.Attribute("result").Value.Trim();
                logEvent.IpFrom = eventElement.Element("ip-from")?.Value.Trim() ?? string.Empty;
                logEvent.Method = eventElement.Element("method")?.Value.Trim() ?? string.Empty;
                logEvent.UrlTo = eventElement.Element("url-to")?.Value.Trim() ?? string.Empty;
                logEvent.Response = int.Parse(eventElement.Element("response")?.Value.Trim() ?? "0");

                log.Events.Add(logEvent);
            }

            return log;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string xml = @"
                <log>
                    <event date=""27/May/1999:02:32:46"" result=""success"">
                        <ip-from>195.151.62.18</ip-from>
                        <method>GET</method>
                        <url-to>/mise/</url-to>
                        <response>200</response>
                    </event>
                    <event date=""27/May/1999:02:41:47"" result=""success"">
                        <ip-from>195.209.248.12</ip-from>
                        <method>GET</method>
                        <url-to>soft.htm</url-to>
                        <response>200</response>
                    </event>
                    <event date=""27/May/1999:02:32:46"" result=""success"">
                        <ip-from>195.151.62.18</ip-from>
                        <method>GET</method>
                        <url-to>/mise/</url-to>
                        <response>200</response>
                    </event>
                    <event date=""27/May/1999:02:41:47"" result=""success"">
                        <ip-from>195.209.248.12</ip-from>
                        <method>GET</method>
                        <url-to>soft.htm</url-to>
                        <response>200</response>
                    </event>
                </log>";

            Log log = LogParser.ParseXml(xml);

            Console.WriteLine("Events:");
            foreach (Event logEvent in log.Events)
            {
                Console.WriteLine($"Date: {logEvent.Date}, Result: {logEvent.Result}, IP: {logEvent.IpFrom}, Method: {logEvent.Method}, URL: {logEvent.UrlTo}, Response: {logEvent.Response}");
            }
        }
    }
}
