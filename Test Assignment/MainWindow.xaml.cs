using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Reflection.PortableExecutable;
using System.IO;
using Newtonsoft.Json;
using Test_Assignment.Model;
using System.Data;

namespace Test_Assignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetCoins();
        }

        

        private void Delete(object sender, RoutedEventArgs e)
        {
            var target = (FrameworkElement)sender;
            while (target is TabItem == false)
                target = (FrameworkElement)target.Parent;
            
            Tabs.Items.Remove(target);


        }

        private void Find_Coin_Btn(object sender, RoutedEventArgs e)
        {




            TabItem newTabItem = new TabItem
            {
                Header = "Test",
                Name = "Test",

            };
            _ = Tabs.Items.Add(newTabItem);
        }

        private void SetCoins(int N = 100)
        {

            WebRequest request = WebRequest.Create($"https://api.coincap.io/v2/assets?limit={N}");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (var response = (HttpWebResponse)request.GetResponse())
            {


                using (Stream dataStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(dataStream))
                    {

                        string responseFromServer = reader.ReadToEnd();
                        
                        var resp = JsonConvert.DeserializeObject<RequestCoin<CoinFromCoincap>>(responseFromServer);
                        TirstTenCoins.ItemsSource = resp.Data;
                    }
                }
            }


        }




        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");

            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetCoins(int.Parse( Count_coin.Text));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
