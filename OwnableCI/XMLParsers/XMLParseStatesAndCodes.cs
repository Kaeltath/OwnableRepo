using OwnableCI.TestDataObjs;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Linq;

namespace OwnableCI.XMLParsers
{
    class XMLParseStatesAndCodes
    {
        public List<CodeAndState> CardsForTests()
        {
            var appSettings = ConfigurationManager.AppSettings;
            List<CodeAndState> parseCodes = new List<CodeAndState>();
            XDocument cardsXML = XDocument.Load(appSettings["TestStatesAndCodesXMLPath"]);
            var xmlUsers = cardsXML.Root.Elements("state");
            foreach (XElement elem in xmlUsers)
            {
                CodeAndState codeAndState = new CodeAndState
                {
                    Fullname = elem.Element("fullname").Value,
                    Name = elem.Element("name").Value,
                    Code = elem.Element("code").Value,
                    ExpResult = elem.Element("ExpResult").Value
                };

                parseCodes.Add(codeAndState);
            }
            return parseCodes;

        }
    }
}
