using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Test_Assignment.Commands;
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

public class TableViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand FilterCommand { get; set; }
        public IAsyncCommand  Request { get; set; }
        public ICommand ResetCoinsFromCoinsForFilter { get; set; }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private IEnumerable<CoinFromCoincap> coinsForFilter;
        private IEnumerable<CoinFromCoincap> coins;
        
        public IEnumerable<CoinFromCoincap> Coins
        {
            get 
            {
                return coins; 
            }
            set
            {
                
                coins = value;
                OnPropertyChanged();
                Loading = false;
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
        public string FilterValue { get; set; }
        private bool _loading = false;

        public bool Loading
        {
            get { return _loading; }
            set { 
                _loading = value;
                OnPropertyChanged();
            }
        }



        public TableViewModel()
        {
            
            FilterCommand = new MainCommands(Filter,()=>!string.IsNullOrEmpty(FilterValue));
            ResetCoinsFromCoinsForFilter = new MainCommands(Reset);

            Request = new MainAsyncCommand(async () =>
            {
                coinsForFilter = Coins = (await GetCoins())?.Data;
            }, () => !string.IsNullOrEmpty(N));
            Request.ExecuteAsync();
        }
        
        

        private void Reset(object value)
        {
            Loading = true;
            if (coinsForFilter != null)
                Coins = coinsForFilter;
        }
        private void Filter(object value)
        {
            Loading = true;
            
           

            IEnumerable<CoinFromCoincap> newListCoins;
            switch (parameters)
            {
                case Parameters.Rank:
                    if (int.TryParse(FilterValue, out int num))
                    {
                        newListCoins = coins.Where((coin) => (coin.Rank == num));
                        Coins = newListCoins;
                    }
                    else
                    {
                        MessageBox.Show("Enter Number!!");
                    }
                    break;
                case Parameters.Name:
                    newListCoins = coins.Where((coin) => coin.Name.Contains(FilterValue));
                    Coins = newListCoins;
                    break;
                case Parameters.Id:
                    newListCoins = coins.Where((coin) => coin.Id.Contains(FilterValue));
                    Coins = newListCoins;
                    break;
                case Parameters.Symbol:
                    newListCoins = coins.Where((coin) => coin.Symbol.Contains(FilterValue));
                    Coins = newListCoins;
                    break;
            }
        }

        private async Task<RequestCoin<CoinFromCoincap>?> GetCoins()
        {
            Loading = true;
            
            if (int.Parse(N) > 2000)
            {
                MessageBox.Show($"Enter number less 2001");
                return null;
            }
            try
            { 
                using (HttpClient client = new HttpClient())
                    return (await client.GetFromJsonAsync<RequestCoin<CoinFromCoincap>>($"https://api.coincap.io/v2/assets?limit={int.Parse(N)}"));
                
                
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Message:{e.Message}");
                return null;
            }


        }
    }
}
