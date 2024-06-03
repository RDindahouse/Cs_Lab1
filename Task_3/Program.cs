using System;
using System.Collections.Generic;
using System.Xml;

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

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            XmlNodeList eventNodes = xmlDoc.SelectNodes("/log/event");
            foreach (XmlNode eventNode in eventNodes)
            {
                Event logEvent = new Event();
                logEvent.Date = DateTime.ParseExact(eventNode.Attributes["date"].Value.Trim(), "dd/MMM/yyyy:HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                logEvent.Result = eventNode.Attributes["result"].Value.Trim();
                logEvent.IpFrom = eventNode.SelectSingleNode("ip-from")?.InnerText.Trim() ?? string.Empty;
                logEvent.Method = eventNode.SelectSingleNode("method")?.InnerText.Trim() ?? string.Empty;
                logEvent.UrlTo = eventNode.SelectSingleNode("url-to")?.InnerText.Trim() ?? string.Empty;
                logEvent.Response = int.Parse(eventNode.SelectSingleNode("response")?.InnerText.Trim() ?? "0");

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
