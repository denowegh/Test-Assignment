using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Assignment.Model.Coin_From_Coingecko
{
    public class Links
    {
        public List<Uri>? Homepage { get; set; }
        public List<Uri>? Blockchain_site { get; set; }
        public List<Uri>? Official_forum_url { get; set; }
        public string? Twitter_screen_name { get; set; }
        public string? Facebook_username { get; set; }
        public string? Subreddit_url { get; set; }
        public override string ToString()
        {
            string RetValue = "";
            string HomepageSTR = "HomepageSTR: ";
            string Blockchain_siteSTR = "Blockchain_siteSTR: ";
            string Official_forum_urlSTR = "Official_forum_urlSTR: ";
            foreach (Uri uri in Homepage)
            {
                HomepageSTR += $"{uri.ToString()} \n";
            }
            foreach (Uri uri in Blockchain_site)
            {
                Blockchain_siteSTR += $"{uri.ToString()} \n";
            }
            foreach (Uri uri in Official_forum_url)
            {
                Official_forum_urlSTR += $"{uri.ToString()} \n";
            }
            RetValue += HomepageSTR + "\n";
            RetValue += Blockchain_siteSTR + "\n";
            RetValue += Official_forum_urlSTR + "\n";
            return RetValue + Twitter_screen_name + Facebook_username + Subreddit_url;
        }
    }
}
