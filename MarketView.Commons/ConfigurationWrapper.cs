using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarketView.Commons
{

    public interface IConfigurationWrapper
    {
        NameValueCollection AppSettings { get; }

        ConnectionStringSettingsCollection ConnectionStrings { get; }
    }


    public class ConfigurationWrapper: IConfigurationWrapper
    {
        public NameValueCollection AppSettings
        {
            get
            {
                return ConfigurationManager.AppSettings;
            }
        }

        public ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                return ConfigurationManager.ConnectionStrings;
            }
        }

    }
}
