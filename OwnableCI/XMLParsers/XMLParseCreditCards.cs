using OwnableCI.TestDataObjs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OwnableCI.XMLParsers
{
    class XMLParseCreditCards
    {

        public List<CreditCard> CardsForTests()
        {
            var appSettings = ConfigurationManager.AppSettings;
            List<CreditCard> parsedCards = new List<CreditCard>();
            XDocument cardsXML = XDocument.Load(appSettings["TestCardsXMLPath"]);
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
