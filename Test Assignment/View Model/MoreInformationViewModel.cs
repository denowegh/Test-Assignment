using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using Microsoft.Windows.Themes;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public enum Currency
    {
        USD, UAH, EUR
    }
    public enum  DaysCount
    {
        One = 1,
        Seven =7,
        Fourteen = 14,
            Thirty=30,
            Ninety=90,
        One_hundred_and_eighty =180,
        Three_hundred_and_sixty_five=365
    }
    public class MoreInformationViewModel : INotifyPropertyChanged
    {
        private const string COIN_GECKO_MAIN_LINC = $"https://api.coingecko.com/api/v3/coins/";
        private static readonly IEnumerable<FinancialPoint> CHART_STANDART_VALUE = new ObservableCollection<FinancialPoint>
            {
                
                //                      date, high, open, close, low
                new(new DateTime(2021, 1, 1), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 2), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 3), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 4), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 5), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 6), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 7), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 8), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 9), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 10), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 11), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 12), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 13), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 14), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 15), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 16), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 17), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 18), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 19), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 20), 1, 1, 1, 1),
                new(new DateTime(2021, 1, 21), 1, 1, 1, 1),
            };
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public IAsyncCommand RequestlCoins { get; private set; }
        public IAsyncCommand RequestlTickers { get; set; }
        public IAsyncCommand RequestOHLC { get; set; }
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
                RequestOHLC.ExecuteAsync();
                Loading = false;
            }
        }
        private string searchValue;

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
        private List<OHLC> dataGrafic;

        public List<OHLC> DataGrafic
        {
            get { return dataGrafic; }
            set
            {
                
                dataGrafic = value;
                OnPropertyChanged();
                if(dataGrafic!= null)
                {
                    var financialPoints = new ObservableCollection<FinancialPoint>();

                    foreach (var a in DataGrafic)
                    {
                        financialPoints.Add(a.ToFinancialPoint());
                    }

                    
                    Series = new ISeries[]{
                    new CandlesticksSeries<FinancialPoint>
                        {
                            UpFill = new SolidColorPaint(SKColors.Green),
                            UpStroke = new SolidColorPaint(SKColors.Green) { StrokeThickness = 2},
                            DownFill = new SolidColorPaint(SKColors.Red),
                            DownStroke = new SolidColorPaint(SKColors.Red) { StrokeThickness = 2 },
                            MiniatureShapeSize = 3,
                            Values =financialPoints
                        }
                    };
                }
                else
                {
                    Series = new ISeries[]{
                    new CandlesticksSeries<FinancialPoint>
                        {
                            UpFill = new SolidColorPaint(SKColors.Green),
                            UpStroke = new SolidColorPaint(SKColors.Green) { StrokeThickness = 2},
                            DownFill = new SolidColorPaint(SKColors.Red),
                            DownStroke = new SolidColorPaint(SKColors.Red) { StrokeThickness = 2 },
                            MiniatureShapeSize = 3,
                            Values =CHART_STANDART_VALUE
                        }
                    };
                }

            }
        }
        

        private Currency currencyGrafic = Currency.USD;

        public Currency CurrencyGrafic
        {
            get { return currencyGrafic; }
            set
            {
                currencyGrafic = value;
                OnPropertyChanged();
                RequestOHLC.ExecuteAsync();
            }
        }


        private DaysCount daysOHLC;
        public DaysCount DaysOHLC
        {
            get { return daysOHLC; }
            set
            {
                daysOHLC = value;
                switch (daysOHLC)
                {
                    case DaysCount.One:
                        XAxis = new[]
                        {
                            new Axis
                            {
                                LabelsRotation = 75,
                                TextSize= 12,
                                Labeler = value => new DateTime((long) value).ToString("0:MM/dd/yy H:mm:ss"),
                                
                                UnitWidth = TimeSpan.FromHours(0.1).Ticks
                            }
                        };
                        RequestOHLC.ExecuteAsync();
                        break;
                    case DaysCount.Seven:
                        XAxis = new[]
                        {
                            new Axis
                            {
                                LabelsRotation = 75,
                                TextSize= 12,
                                
                                Labeler = value => new DateTime((long) value).ToString("0:MM/dd/yy H:mm:ss"),
                                
                                UnitWidth = TimeSpan.FromHours(2).Ticks
                            }
                        };
                        RequestOHLC.ExecuteAsync();
                        break;
                    case DaysCount.Fourteen:
                        XAxis = new[]
                        {
                            new Axis
                            {
                                LabelsRotation = 75,
                                TextSize= 12,
                                Labeler = value => new DateTime((long) value).ToString("0:MM/dd/yy H:mm:ss"),

                                UnitWidth = TimeSpan.FromHours(2).Ticks
                            }
                        };
                        RequestOHLC.ExecuteAsync();
                        break;
                    case DaysCount.Thirty:
                        XAxis = new[]
                        {
                            new Axis
                            {
                                LabelsRotation = 75,
                                TextSize= 12,
                                Labeler = value => new DateTime((long) value).ToString("0:MM/dd/yy H:mm:ss"),

                                UnitWidth = TimeSpan.FromHours(1).Ticks
                            }
                        };
                        RequestOHLC.ExecuteAsync();
                        break;
                    case DaysCount.Ninety:
                        XAxis = new[]
                        {
                            new Axis
                            {
                                LabelsRotation = 75,
                                TextSize= 12,
                                Labeler = value => new DateTime((long) value).ToString("0:MM/dd/yy H:mm:ss"),

                                UnitWidth = TimeSpan.FromDays(2).Ticks
                            }
                        };
                        RequestOHLC.ExecuteAsync();
                        break;
                    case DaysCount.One_hundred_and_eighty:
                        XAxis = new[]
                        {
                            new Axis
                            {
                                LabelsRotation = 75,
                                TextSize= 12,
                                Labeler = value => new DateTime((long) value).ToString("0:MM/dd/yy H:mm:ss"),

                                UnitWidth = TimeSpan.FromDays(2).Ticks
                            }
                        };
                        RequestOHLC.ExecuteAsync();
                        break;
                    case DaysCount.Three_hundred_and_sixty_five:
                        XAxis = new[]
                        {
                            new Axis
                            {
                                LabelsRotation = 75,
                                TextSize= 12,
                                Labeler = value => new DateTime((long) value).ToString("0:MM/dd/yy H:mm:ss"),

                                UnitWidth = TimeSpan.FromDays(2).Ticks
                            }
                        };
                        RequestOHLC.ExecuteAsync();
                        break;

                }
                OnPropertyChanged();

                
            }
        }
        
        private Axis[] xAxis = new[]
        {
            new Axis
            {
                LabelsRotation = 65,
                TextSize= 14,
                
                Labeler = value => new DateTime((long) value).ToString("0:MM/dd/yy H:mm:ss"),
                
                UnitWidth = TimeSpan.FromDays(1).Ticks
            }
        };

        public Axis[] XAxis
        {
            get { return xAxis; }
            set 
            { 
                xAxis = value;
                OnPropertyChanged();
            }
        }


        private ISeries[] _series =
        {
        new CandlesticksSeries<FinancialPoint>
        {
            UpFill = new SolidColorPaint(SKColors.Green),
            UpStroke = new SolidColorPaint(SKColors.Green) { StrokeThickness = 1 },
            DownFill = new SolidColorPaint(SKColors.Red),
            
            DownStroke = new SolidColorPaint(SKColors.Red) { StrokeThickness = 1 },
            
            Values = CHART_STANDART_VALUE
        }};

        public ISeries[] Series
        {
            get => _series;
            set
            {
                if (_series == value) return;
                _series = value;
                OnPropertyChanged(nameof(Series));
            }
        }
        public LabelVisual Title { get; set; } =
        new LabelVisual
        {
            Text = "Chart OHLC",
            TextSize = 20,
            Padding = new LiveChartsCore.Drawing.Padding(15),
            Paint = new SolidColorPaint(SKColors.DarkSlateGray)
        };


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
            RequestOHLC = new MainAsyncCommand(async () =>
            {
                DataGrafic = await GetCoinOHLCCur();
                
                Loading = false;

            });

        }

        private async Task<CoinLarge> GetCoins()
        {
            DaysOHLC = DaysCount.One;
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
                        string req = COIN_GECKO_MAIN_LINC
                            + $"{Coin.Id}/tickers?page={PageTickers}";
                        return await client.GetFromJsonAsync<CoinTickers>(req);
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
        private async Task<List<OHLC>> GetOHLC(string cur)
        {
            Loading = true;
            try
            {
                List<List<double>> data;
                List<OHLC> dataObject = new List<OHLC>();
                HttpResponseMessage responseMessage;
                using (HttpClient client = new HttpClient())
                {
                    if (Coin != null)
                    {
                        string req = COIN_GECKO_MAIN_LINC
                            + $"{Coin.Id}/ohlc?vs_currency={cur}&days={((int)DaysOHLC)}";
                        
                        responseMessage = await client.GetAsync(req);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            data = await client.GetFromJsonAsync<List<List<double>>>(req);
                            if (data != null)
                            {
                                foreach (var a in data)
                                {
                                    var newOHLC = new OHLC();
                                    String propString;
                                    for (int i = 0; i <= 4; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                propString = a[i].ToString();
                                                newOHLC.Time = UnixTimeToDateTime(long.Parse(propString));
                                                break;

                                            case 1:
                                                newOHLC.Open = a[i];
                                                break;

                                            case 2:
                                                newOHLC.High = a[i];
                                                break;

                                            case 3:
                                                newOHLC.Low = a[i];
                                                break;
                                            case 4:
                                                newOHLC.Close = a[i];
                                                break;
                                        }
                                    }
                                    dataObject.Add(newOHLC);
                                }

                            }

                        }
                        else
                        {
                            MessageBox.Show(responseMessage.StatusCode.ToString());
                            return null;
                        }
                        
                        return dataObject;
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
        private async Task<List<OHLC>> GetCoinOHLCCur()
        {
            switch (CurrencyGrafic)
            {
                case Currency.USD:
                    return await GetOHLC("usd");
                case Currency.UAH:
                    return await GetOHLC("uah");
                case Currency.EUR:
                    return await GetOHLC("eur");
            }
            return null;
        }
        private DateTime UnixTimeToDateTime(long unixtime)
        {

            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixtime).ToLocalTime();
            return dtDateTime;
        }

    }
}
