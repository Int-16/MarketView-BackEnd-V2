using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Castle.MicroKernel.SubSystems.Conversion;
using log4net;
using log4net.Config;
using Paket;



namespace MarketView.Commons
{
    public class Logging
    {
        private static log4net.ILog log;
        const string ErrorInLoggingObject = "Error in Getting Object Xml for Logging - {0}. Ignored. Message - {1}";
        const string TypeName = "MarketView.Commons.Logging";

        public Logging()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        public static void LogInfo(string type, string message)
        {
            GetLogger(type);

            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }

        }

        public static void LogException(string type, string message, Exception exception)
        {
            GetLogger(type);
            
            if (log.IsErrorEnabled)
            {
                log.Error(message, exception);
            }
        }

        public static void LogException(string type, string message, Exception exception, object input)
        {
            GetLogger(type);
            
            if (log.IsErrorEnabled)
            {
                string inputObjectXml = string.Empty;
                try
                {
                    inputObjectXml = GetObjectXml(input);
                }
                catch (Exception)
                {
                    inputObjectXml = string.Empty;
                }
                message = string.Format("Error Message: {0} \r\n Input Object: {1}", message, inputObjectXml);
                log.Error(message, exception);
            }
        }

        public static void LogException(string type, string message, Exception exception, params string[] inputs)
        {
            GetLogger(type);
            if (log.IsErrorEnabled)
            {
                log.Error(
                    string.Format("{0} \r\n- Additional Information: {1}", message, GetCommaSeparatedString(inputs)),
                    exception);
            }
        }

        public static string GetCommaSeparatedString(IEnumerable<string> inputList)
        {
            if (inputList == null || inputList.Count() == 0)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();

            foreach (string value in inputList)
            {
                if (!string.IsNullOrEmpty(value.Trim()))
                {
                    builder.Append(value);
                    builder.Append(",");
                }
            }
            if (builder.Length > 0)
            {
                builder.Remove(builder.Length - 1, 1);
            }


            return builder.ToString();
        }


        public static string GetObjectXml(object input)
        {
            try
            {
                XmlSerializer writer = new XmlSerializer(input.GetType());
                Stream stream = new MemoryStream();
                writer.Serialize(stream, input);
                stream.Position = 0;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);
                string outputXml = xmlDoc.OuterXml;

                writer = null;
                xmlDoc = null;

                return outputXml;
            }
            catch (Exception exception)
            {
               LogInfo(TypeName, string.Format(ErrorInLoggingObject, input.GetType().Name, exception.Message));
               return ErrorInLoggingObject; 
            }
        }
        public static void LogInfo(string type, string messageFormat, params object[] parameters)
        {
            string message = string.Format(messageFormat, parameters);
            GetLogger(type);
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }

        public static void LogDebug(string type, string message, Exception exception)
        {
            GetLogger(type);
            if (log.IsDebugEnabled)
            {
                log.Debug(message, exception);
            }
        }

        public static void LogError(string type, string message)
        {
            GetLogger(type);
            if (log.IsDebugEnabled)
            {
                log.Error(message);
            }
        }

        private static ILog GetLogger(string type)
        {
            if (log == null)
            {
                log = log4net.LogManager.GetLogger(type);
                return log;
            }
            else
            {
                if (log.Logger.Name.Equals(type, StringComparison.InvariantCultureIgnoreCase))
                {
                    return log;
                }
                else
                {
                    log = log4net.LogManager.GetLogger(type);
                    return log;
                }
            }
        }



    }
}
