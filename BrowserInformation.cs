using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppDriverAndWebDriver
{
    class BrowserInformation
    {
        private string _browserName;
        
        public BrowserInformation(string browserName)
        {
            _browserName = browserName;
        }
        public string applicationIdentifier
        {
            get
            {
                string appId = string.Empty;
                switch (_browserName)
                {
                    case "edge":
                        appId = "Bing - Microsoft Edge";
                        break;
                    case "chrome":
                        appId = "Bing";
                        break;
                    default:
                        appId = "Bing - Microsoft Edge";
                        break;
                }
                return appId; 
            }
        }
        public string searchElementIdenifier
        {
            get
            {
                string elem = string.Empty;
                switch (_browserName)
                {
                    case "edge":
                        elem = "Search or enter web address";
                        break;
                    case "chrome":
                        elem = "Address and search bar";
                        break;
                    default:
                        elem = "Bing - Microsoft Edge";
                        break;
                }
                return elem;
            }
        }
        public string basicAuthDialogEditFields
        {
            get
            {
                string elem = string.Empty;
                switch (_browserName)
                {
                    case "edge":
                        elem = "//Window[@Name=\"Windows Security\"]//Edit";
                        break;
                    case "chrome":
                        elem = "//Window[@Name=\"the-internet.herokuapp.com/basic_auth\"]/Edit";
                        break;
                    default:
                        elem = "//Window[@Name=\"Windows Security\"]/Pane//Edit";
                        break;
                }
                return elem;
            }
        }
    }
}
