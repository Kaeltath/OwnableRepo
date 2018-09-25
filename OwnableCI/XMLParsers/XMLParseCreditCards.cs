using OwnableCI.TestDataObjs;
using System;
using System.Collections.Generic;
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
            List<CreditCard> parsedCards = new List<CreditCard>();
            XDocument usersXml = XDocument.Load(@"XMLTestAsserts\CreditCards.xml");
            var xmlUsers = usersXml.Root.Elements("card");
            foreach (XElement elem in xmlUsers)
            {
                CreditCard cardToAdd = new CreditCard();
                
                cardToAdd.Number = elem.Element("number").Value;
                cardToAdd.ExpRes = elem.Element("ExpResult").Value;

                parsedCards.Add(cardToAdd);
            }
            return parsedCards;

        }
    }
}
