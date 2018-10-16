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
        static bool Succided = true;

        static int Main(string[] args)
        {
            int result;
            RecurlyClient client = new RecurlyClient(Logger);
            client.DeleteAcounts(out Succided);
            Console.ReadLine();
            result = Succided ? 0 : 1;
            return result;
        }
    }
}
