using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Test_Assignment.Commands;
using Test_Assignment.Model;
using Test_Assignment.Model.Coin_From_Coingecko;

namespace Test_Assignment.View_Model
{
    public enum ParameterSearch
    {
        Name,
        Id,
        Symbol
    }
    public class MoreInformationViewModel : INotifyPropertyChanged
    {
        private const string COIN_GECKO_MAIN_LINC = $"https://api.coingecko.com/api/v3/coins/";
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public IAsyncCommand RequestlCoins { get; private set; }
        public IAsyncCommand RequestlTickers { get; set; }

        public ICommand PagePlus { get; set; }
        public ICommand PageMinus { get; set; }

        private CoinLarge coin;

        public CoinLarge Coin
        {
            get { return coin; }
            set
            {
                coin = value;
                OnPropertyChanged();
                Loading = false;
            }
        }
        private string searchValue = "Bitcoin";

        public string SearchValue
        {
            get { return searchValue; }
            set
            {
                searchValue = value;
                OnPropertyChanged();
            }
        }
        private ParameterSearch parametrSearch = ParameterSearch.Name;
        public ParameterSearch ParametrSearch
        {
            get { return parametrSearch; }
            set
            {
                parametrSearch = value;
                OnPropertyChanged();
            }
        }
        private bool loading = false;
        public bool Loading
        {
            get { return loading; }
            set
            {
                loading = value;
                OnPropertyChanged();
            }
        }
        private CoinTickers tickers;

        public CoinTickers Tickers
        {
            get { return tickers; }
            set
            {
                tickers = value;
                OnPropertyChanged();
                Loading = false;
            }
        }
        private string pageTickers = "0";

        public string PageTickers
        {
            get { return pageTickers; }
            set
            {
                pageTickers = value;
                OnPropertyChanged();
                RequestlTickers.ExecuteAsync();
            }
        }
        private bool visibilitiPage = false;

        public bool VisibilitiPage
        {
            get { return visibilitiPage; }
            set
            {
                visibilitiPage = value;
                OnPropertyChanged();
            }
        }


        public MoreInformationViewModel()
        {
            RequestlCoins = new MainAsyncCommand(async () =>
            {

                Coin = await GetCoins();
                if (Coin != null)
                {
                    VisibilitiPage = true;

                }
                else
                {
                    VisibilitiPage = false;
                }

                PageTickers = "0";

            }, () => !string.IsNullOrEmpty(SearchValue));

            RequestlTickers = new MainAsyncCommand(async () =>
            {
                Tickers = await GetCoinTrickers();

            });
            PagePlus = new MainCommands(PlusPage);
            PageMinus = new MainCommands(MinusPage);
        }

        private async Task<CoinLarge> GetCoins()
        {
            Loading = true;
            using (HttpClient client = new HttpClient())
            {
                List<CoinSmall> respSmall;
                HttpResponseMessage response;
                switch (parametrSearch)
                {
                    case ParameterSearch.Id:
                        try
                        {
                            response = await client.GetAsync(COIN_GECKO_MAIN_LINC +
                                $"{SearchValue.Trim()}?tickers=false&community_data=false&developer_data=false");


                            if (response.IsSuccessStatusCode)
                            {
                                var s = await response.Content.ReadAsStringAsync();
                                return await response.Content.ReadFromJsonAsync<CoinLarge>();
                            }
                            else
                            {
                                MessageBox.Show(response.StatusCode.ToString());
                                return null;
                            }
                        }
                        catch (HttpRequestException e)
                        {
                            MessageBox.Show($"Message:{e.Message}");
                            return null;
                        }
                    case ParameterSearch.Name:
                        try
                        {
                            respSmall = await client.GetFromJsonAsync<List<CoinSmall>>(COIN_GECKO_MAIN_LINC + "list");
                            respSmall = respSmall.Where((e) =>
                            {
                                return e.Name == SearchValue.Trim();
                            }).ToList();


                            if (respSmall?.Count != 0)
                            {
                                response = await client.GetAsync(COIN_GECKO_MAIN_LINC +
                                    $"{respSmall[0].Id}?tickers=false&community_data=false&developer_data=false");
                                if (response.IsSuccessStatusCode)
                                {
                                    return await response.Content.ReadFromJsonAsync<CoinLarge>();
                                }
                                else
                                {
                                    MessageBox.Show(response.StatusCode.ToString());
                                    return null;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Not found coin!!");
                                return null;
                            }
                        }
                        catch (HttpRequestException e)
                        {
                            MessageBox.Show($"Message:{e.Message}");
                            return null;
                        }
                    case ParameterSearch.Symbol:
                        try
                        {
                            respSmall = await client.GetFromJsonAsync<List<CoinSmall>>(COIN_GECKO_MAIN_LINC + "list");
                            respSmall = respSmall.Where((e) =>
                            {
                                return e.Symbol == SearchValue.Trim();
                            }).ToList();


                            if (respSmall?.Count != 0)
                            {
                                response = await client.GetAsync(COIN_GECKO_MAIN_LINC +
                                    $"{respSmall[0].Id}?tickers=false&community_data=false&developer_data=false");
                                if (response.IsSuccessStatusCode)
                                {
                                    return await response.Content.ReadFromJsonAsync<CoinLarge>();
                                }
                                else
                                {
                                    MessageBox.Show(response.StatusCode.ToString());
                                    return null;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Not found coin!!");
                                return null;
                            }
                        }
                        catch (HttpRequestException e)
                        {
                            MessageBox.Show($"Message:{e.Message}");
                            return null;
                        }
                    default:
                        return null;

                }
            }
        }

        private async Task<CoinTickers> GetCoinTrickers()
        {

            Loading = true;
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    if (Coin != null)
                    {
                        return await client.GetFromJsonAsync<CoinTickers>(COIN_GECKO_MAIN_LINC
                            + $"{Coin.Id}/tickers?page={PageTickers}");
                    }
                    else return null;
                }

            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Message:{e.Message}");
                return null;
            }
        }

        private void PlusPage(object parameter)
        {
            try
            {
                int local = int.Parse(PageTickers);
                local++;
                PageTickers = local.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void MinusPage(object parameter)
        {
            try
            {
                int local = int.Parse(PageTickers);
                local--;
                PageTickers = local.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
