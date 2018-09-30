using OwnableCI.TestDataObjs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OwnableCI.XMLParsers
{
    class XMLParseStatesAndCodes
    {
        public List<CodeAndState> CardsForTests()
        {
            List<CodeAndState> parseCodes = new List<CodeAndState>();
            XDocument cardsXML = XDocument.Load(@"OwnableCI\XMLTestAsserts\StatesAndCodes.xml");
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
