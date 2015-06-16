using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInOut.Display.Helpers
{
    public class SettingsManager
    {
        public static string WebApiUrl
        {
            get
            {
                return ((ConfigurationManager.AppSettings["WebApiUrl"].EndsWith("/")
                    ? ConfigurationManager.AppSettings["WebApiUrl"]
                    : ConfigurationManager.AppSettings["WebApiUrl"] + "/") ?? "http://192.168.23.154/japi/");
            }
        }

        public static int DeviceId
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["DeviceId"]);
            }
        }
    }
}
