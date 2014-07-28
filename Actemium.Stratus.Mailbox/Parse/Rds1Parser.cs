using Actemium.Stratus.MailboxPlugin.Bootstrapper;
using Actemium.Stratus.MailboxPlugin.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Actemium.Stratus.MailboxPlugin.Parse
{
    public class Rds1Parser : ShopFloorResultsExtensionBase, IParser
    {
        #region IParser Interface

        private XDocument Parse(string data)
        {
            try
            {
                if (data == null)
                {
                    var message = "Data passed into RDS1 parser cannot be null";
                    LogEvent(this, new LogEventArgs { Level = LogLevel.Error, Message = message });
                    throw new ArgumentException(message);
                }

                LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "RDS1 Parser" });

                var sequences = GetSequences(data);

                LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = sequences.Count() + " sequences found" });

                var rds1Sequences = new List<Rds1Sequence>();

                foreach (var sequence in sequences)
                {
                    var header = ParseHeader(sequence);

                    LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = header.ProductId + ", " + header.Sequence + ", " + header.Station });

                    var results = ParseResults(sequence);

                    LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = results.Count() + " results found" });

                    rds1Sequences.Add(new Rds1Sequence { Header = header, Results = results });
                }

                return CreateXDocument(rds1Sequences);
            }
            catch (Exception e)
            {
                var message = "An exception has occured parsing the data. See inner exception for details";
                LogEvent(this, new LogEventArgs { Level = LogLevel.Error, Message = message + " : " + e.Message });
                throw new ApplicationException(message, e);
            }
        }

        #endregion

        #region Private Methods

        private XDocument CreateXDocument(List<Rds1Sequence> rds1Sequences)
        {
            XDocument ret = null;
            foreach (var rds1Sequence in rds1Sequences)
            {

                ret = new XDocument(
                    new XDeclaration("1.0", "ISO-8859-1", "yes"),

                    new XElement("VEHICLE", new XAttribute("rds", "3.0"), new XAttribute("vin", rds1Sequence.Header.ProductId), new XAttribute("model", ""),
                        new XElement("VISIT", new XAttribute("co", ""),
                            new XAttribute("pl", ""),
                            new XAttribute("tester", rds1Sequence.Header.Station),
                            new XAttribute("pf", rds1Sequence.Header.Station),
                            new XAttribute("zo", rds1Sequence.Header.Station),
                            new XAttribute("cell", rds1Sequence.Header.Station),
                            new XAttribute("lo", rds1Sequence.Header.Station),
                            new XAttribute("start", rds1Sequence.Header.StartTime),
                            new XAttribute("end", rds1Sequence.Header.StartTime.AddSeconds(rds1Sequence.Header.Duration)),
                            new XAttribute("dur", rds1Sequence.Header.Duration),
                            new XAttribute("flags", rds1Sequence.Header.Flags),
                            new XAttribute("shift", ""),
                            new XAttribute("out", ""),
                                new XElement("SEQUENCE",
                                    new XAttribute("flags", rds1Sequence.Header.Flags),
                                    new XAttribute("name", rds1Sequence.Header.Sequence),
                                    new XAttribute("start", rds1Sequence.Header.StartTime),
                                    new XAttribute("dur", rds1Sequence.Header.Duration),
                                        new XElement("RESULTS",
                                            from result in rds1Sequence.Results
                                            select new XElement(result.ResultType,
                                                new XAttribute("name", result.Name), result.Value)
                                                )
                                            )
                                        )
                                    )
                                );


            }
            return ret;
        }

        private List<Rds1Result> ParseResults(string sequence)
        {
            Regex resultsRegEx = new Regex("(?<Type>V[BSOGNFV]\\s)(?<Name>.*):(?<Value>.*)\\r?\\n?",
                RegexOptions.IgnoreCase
                | RegexOptions.Multiline
                | RegexOptions.CultureInvariant
                | RegexOptions.IgnorePatternWhitespace
                | RegexOptions.Compiled
                );

            List<Rds1Result> ret = resultsRegEx.Matches(sequence).OfType<Match>().Select((m) =>
                         new Rds1Result
                         {
                             ResultType = m.Groups["Type"].Value.Trim(' ', '\r', '\n'),
                             Name = m.Groups["Name"].Value.Trim(' ', '\r', '\n'),
                             Value = m.Groups["Value"].Value.Trim(' ', '\r', '\n')
                         }).ToList();
            return ret;

        }

        private Rds1Header ParseHeader(string sequence)
        {
            Regex headerRegEx = new Regex("(?:STATION:)(?<Station>.+)(?:\\r?\\n?)(?:VEHICLE:)(?<ProductId>.+)" +
                "(?:\\r?\\n?)(?:SEQUENCE:)(?<Sequence>.+)(?:\\r?\\n?)(?:TIME:)(?<StartTime" +
                ">.+),(?<Duration>.+)(?:\\r?\\n?)(?:FLAGS:)(?<Flags>.+)(?:\\r?\\n?)",
                RegexOptions.IgnoreCase
                | RegexOptions.Multiline
                | RegexOptions.CultureInvariant
                | RegexOptions.IgnorePatternWhitespace
                | RegexOptions.Compiled
                );

            var header = headerRegEx.Matches(sequence).OfType<Match>().Select((m) =>
                         new Rds1Header
                         {
                             Station = m.Groups["Station"].Value.Trim(' ', '\r', '\n'),
                             ProductId = m.Groups["ProductId"].Value.Trim(' ', '\r', '\n'),
                             Sequence = m.Groups["Sequence"].Value.Trim(' ', '\r', '\n'),
                             StartTime = DateTime.Parse(m.Groups["StartTime"].Value.Trim(' ', '\r', '\n')),
                             Duration = int.Parse(m.Groups["Duration"].Value.Trim(' ', '\r', '\n')),
                             Flags = m.Groups["Flags"].Value.Trim(' ', '\r', '\n') == "PASS" ? "P" : "F"
                         }
                 );

            return header.SingleOrDefault();
        }

        private IEnumerable<string> GetSequences(string data)
        {
            return data.TrimEnd('\r', '\n').Split(new[] { "END" }, StringSplitOptions.RemoveEmptyEntries);
        }

        #endregion

        public event EventHandler<LogEventArgs> LogEvent;

        public override string Describe()
        {
            return "RDS1 Parser";
        }

        public override void Start()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Started " + Describe() });
        }

        public override void Stop()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Stopped " + Describe() });
        }


        public void ParseStringToXml(object sender, ParseStringToXmlEventArgs e)
        {
            if (e.Data.Contains("STATION:"))
            {
                try
                {
                    e.Xml = Parse(e.Data);
                }
                catch (ApplicationException)
                {
                    e.Xml = null;
                }
            }
        }
    }
}
