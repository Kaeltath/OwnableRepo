using OwnableCI.TestDataObjs;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace OwnableCI.XMLParsers
{
    public class XMLParseCreditCards
    {

        public List<CreditCard> CardsForTests()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            if (!config.HasFile)
            { throw new FileNotFoundException("Missing configuration file for test dll"); }
            var statesandCodesPathSetting = config.AppSettings.Settings["TestStatesAndCodesXMLPath"].Value;
            List<CreditCard> parsedCards = new List<CreditCard>();
            XDocument cardsXML = XDocument.Load(statesandCodesPathSetting);
            var xmlUsers = cardsXML.Root.Elements("card");
            foreach (XElement elem in xmlUsers)
            {
                CreditCard cardToAdd = new CreditCard
                {
                    Number = elem.Element("number").Value,
                    ExpRes = elem.Element("ExpResult").Value
                };

                parsedCards.Add(cardToAdd);
            }
            return parsedCards;

        }
    }
}
