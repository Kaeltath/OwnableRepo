using System;
using System.Collections.Generic;
using System.Text;
using Recurly;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace RecurlyCleanUpUtility
{
    class Program
    {
        static log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            //TO DO: Deside exception handling
            RecurlyClient client = new RecurlyClient(XmlParser.GetUsers(Logger));
            client.DeleteAcounts();
            Console.ReadLine();
        }
    }
}
