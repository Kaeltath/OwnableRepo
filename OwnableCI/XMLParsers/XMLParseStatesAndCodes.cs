using OwnableCI.TestDataObjs;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace OwnableCI.XMLParsers
{
    public class XMLParseStatesAndCodes
    {
        public List<CodeAndState> CardsForTests()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            if (!config.HasFile)
            { throw new FileNotFoundException("Missing configuration file for test dll"); }
            var cardsPathSetting = config.AppSettings.Settings["TestUsersXMLPath"].Value;
            List<CodeAndState> parseCodes = new List<CodeAndState>();
            XDocument cardsXML = XDocument.Load(cardsPathSetting);
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
