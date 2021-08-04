using MarketView.Commons;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketView.Data
{
    public class MongoConfigurations
    {
        IConfiguration _iconfiguration;

        public MongoConfigurations(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }


        public string ConnectionString
        {
            get
            {
                return _iconfiguration.GetSection("MongoConfigurations").GetSection("ConnectionString").Value;
            }

        }

        public string DatabaseName
        {
            get
            {
                return _iconfiguration.GetSection("MongoConfigurations").GetSection("Database").Value;
            }

        }


        public string UserName
        {
            get
            {
                return _iconfiguration.GetSection("MongoConfigurations").GetSection("UserName").Value;
            }

        }

        public string Password
        {
            get
            {
                return _iconfiguration.GetSection("MongoConfigurations").GetSection("Password").Value; ;
            }

            

        }

        public string Host
        {
            get
            {
                return _iconfiguration.GetSection("MongoConfigurations").GetSection("Host").Value;
            }

        }

        public int Port
        {
            get
            {
                var port = Constants.MongoDBDefaultPort;
                try
                {
                    port = int.Parse(_iconfiguration.GetSection("MongoConfigurations").GetSection("Host").Value);
                }
                catch (Exception ex)
                {
                    Logging.LogException(typeof(MongoConfigurations).FullName, "Failed to parse Port from configuration. Using default port.", ex);
                }
                return port;

            }

        }

        public static string GetCollectionName<T>()
        {
            return typeof(T).Name;
        }

    }
}
