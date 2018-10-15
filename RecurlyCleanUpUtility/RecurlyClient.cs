using System;
using System.Configuration;
using Recurly;
using System.Net;
using RecurlyCleanUpUtility.Entities;
using System.Collections.Generic;

namespace RecurlyCleanUpUtility
{
    internal class RecurlyClient
    {
        log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Members
        private bool success = true;
        private string apiKey;
        private string publicKey;
        private string subdomain;       
        private int pageSize;
        //List of Acconts that should be added/removed/modified/etc... 
        private List<Account> operationalList;
        //List of User based on which accounts should be modified
        private List<User> inputList;
        #endregion

        #region Consrtuctors
        public RecurlyClient(List<User> InputList)
        {
            inputList = InputList;
            Initialyze();
        }
        #endregion

        #region Private Methods
        private void Initialyze()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings.Count > 0)
                {
                    apiKey = appSettings["ApiKey"];
                    publicKey = appSettings["PublicKey"];
                    subdomain = appSettings["SubDomain"];
                    pageSize = Int32.Parse(appSettings["PageSize"]);
                }
                else
                {
                    Console.WriteLine("No setting read from appconfig");
                    success = false;

                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to readd App setings: " + ex.Message);
                success = false;
            }

            Recurly.Configuration.SettingsManager.Initialize(apiKey, subdomain, "", pageSize);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;// |= (SecurityProtocolType)(SecurityProtocolTypeTls12 | SecurityProtocolTypeTls11); 
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            if (success)
            {
                Logger.Info("Client initialyzed successfully");
            }           
        }

        private void FillOperationalList()
        {
            operationalList = new List<Account>();
            try
            {
                var accounts = Accounts.List();
                while (accounts.HasAny<Account>())
                {
                    foreach (var account in accounts)
                    {
                        CheckifOperational(account);
                    }

                    // fetch the next "page" of accounts
                    accounts = accounts.Next;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response == null) throw;

                var response = (HttpWebResponse)ex.Response;
                var statusCode = response.StatusCode;

                Logger.Debug(String.Format("Recurly Client Received: {0} - {1}, Reponce message: {2}",
                    (int)statusCode, statusCode, response));
                success = false;
            }

        }

        //Check if Account corresponde to any of the user that we need to handle,
        //if yes - add to operationalList
        private void CheckifOperational(Account acc)
        {
            User realUser = new User()
            {
                FirstName = acc.FirstName,
                LastName = acc.LastName,
                Adress = acc.Address.Address1,
                City = acc.Address.City,
                State = acc.Address.State,
                Zip = acc.Address.Zip,
                Mobile = acc.Address.Phone,
                BirthDate = DateTime.Now.Date.ToString(),
                LastDigitsOFSocial = "1111"
            };

            foreach(User user in inputList)
            {
                if (User.IsEqual(realUser, user, "BirthDate", "LastDigitsOFSocial"))
                {
                    operationalList.Add(acc);
                    break;
                }
            }
        }
        #endregion

        #region Public Methods

        public void DeleteAcounts()
        {
            FillOperationalList();

            foreach (Account acc in operationalList)
            {
                if (acc.State == Account.AccountState.Closed)
                { continue; }

                try
                {
                    acc.Close();
                }

                catch (WebException ex)
                {
                    if (ex.Response == null) throw;

                    var response = (HttpWebResponse)ex.Response;
                    var statusCode = response.StatusCode;

                    Logger.Debug(String.Format("Recurly Client Received: {0} - {1}, Reponce message: {2}",
                        (int)statusCode, statusCode, response));
                    success = false;
                }
            }
        }

        #endregion
    }
}
