using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Test_Assignment.Model;

namespace Test_Assignment.View_Model
{
    public enum Parameters
    {
        Rank,
        Name,
        Id,
        Symbol,
    }
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public ICommand RequestCoins { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand ResetCoinsFromCoinsForFilter { get; set; }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private IEnumerable<CoinFromCoincap> coinsForFilter;
        private IEnumerable<CoinFromCoincap> coins;
        
        public IEnumerable<CoinFromCoincap> Coins
        {
            get { return coins; }
            set
            {
                coins = value;
                OnPropertyChanged();
            }
        }
        private string n = "10";

        public string N
        {
            get { return n; }
            set
            {
                n = value;
                OnPropertyChanged();
            }
        }
        private Parameters parameters= Parameters.Name;

        public Parameters Parameters
        {
            get { return parameters; }
            set 
            { 
                parameters = value;
                OnPropertyChanged();
            }
        }
        private string filterValue;

        public string FilterValue
        {
            get { return filterValue; }
            set { filterValue = value; }
        }



        public MainViewModel()
        {
            GetCoins();

            RequestCoins = new MainCommands(RequestCoin);
            FilterCommand = new MainCommands(Filter);
            ResetCoinsFromCoinsForFilter = new MainCommands(Reset);
        }
        private void RequestCoin(object value)
        {
            GetCoins();
        }
        private void Reset(object value)
        {
            if(coinsForFilter != null)
                Coins = coinsForFilter;
        }
        private async void Filter(object value)
        {
            if (string.IsNullOrEmpty(FilterValue))
            {
                Coins = coinsForFilter;
                return;
            }
            else
            {
                if(coinsForFilter == null)
                    coinsForFilter = Coins;
            }

            List<CoinFromCoincap> newListCoins = new List<CoinFromCoincap>();
            switch (parameters)
            {
                case Parameters.Rank:
                    if (int.TryParse(filterValue, out int num))
                    {
                        foreach (var coin in this.coins)
                        {
                            if (coin.Rank == num)
                            {
                                newListCoins.Add(coin);
                                break;
                            }
                        }
                        Coins = newListCoins;
                    }
                    break;
                case Parameters.Name:
                    foreach (var coin in this.coins)
                    {
                        if (coin.Name.Contains(FilterValue))
                        {
                            newListCoins.Add(coin);
                            return;
                        }
                    }
                    Coins = newListCoins;
                    break;
                case Parameters.Id:
                    foreach (var coin in this.coins)
                    {
                        if (coin.Id.Contains(FilterValue))
                        {
                            newListCoins.Add(coin);
                            return;
                        }
                    }
                    Coins = newListCoins;
                    break;
                case Parameters.Symbol:
                    foreach (var coin in this.coins)
                    {
                        if ( coin.Symbol.Contains(FilterValue))
                        {
                            newListCoins.Add(coin);
                            return;
                        }
                    }
                    Coins = newListCoins;
                    break;
            }
        }

        private async void GetCoins()
        {
            N = (string.IsNullOrEmpty(n)) ? "0" : N;
            WebRequest request = WebRequest.Create($"https://api.coincap.io/v2/assets?limit={int.Parse(N)}");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (var response = await request.GetResponseAsync() as HttpWebResponse)
            {
                using (Stream dataStream =  response.GetResponseStream())
                {
                    using (var reader =  new StreamReader(dataStream))
                    {

                        string responseFromServer = reader.ReadToEnd();
                        
                        var resp = JsonConvert.DeserializeObject<RequestCoin<CoinFromCoincap>>(responseFromServer);
                        Coins =  resp.Data;
                    }
                }
            }
        }

    }
}
