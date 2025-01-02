using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_Service_Integration.Pages
{
    public class IntegrationModel : PageModel
    {
        public Dictionary<string, decimal> WeeklyData { get; private set; } = new Dictionary<string, decimal>();
        public Dictionary<string, decimal> DailyData { get; private set; } = new Dictionary<string, decimal>();
        public Dictionary<string, decimal> GrowthData { get; private set; } = new Dictionary<string, decimal>();
        public string BestPerformer { get; private set; }
        public decimal BestGrowth { get; private set; }

        public Dictionary<string, dynamic> StockData { get; private set; }

        public void OnGet()
        {
            string apiKey = "GZ356QRI2FT68FFK";
            //string symbol = "IBM";
            //string interval = "5min";
            //string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={symbol}&interval={interval}&apikey={apiKey}";
            //string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={apiKey}";
            //string QUERY_URL1 = $"https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&symbol={symbol}&apikey={apiKey}";
            //Uri queryUri = new Uri(QUERY_URL);
            //Time Series(Daily)

            //using (WebClient client = new WebClient())
            //{
            //    try
            //    {
            //        string jsonResponse = client.DownloadString(queryUri);

            //        StockData = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(jsonResponse);
            //    }
            //    catch (WebException webEx)
            //    {
            //        Console.WriteLine($"WebException: {webEx.Message}");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"Exception: {ex.Message}");
            //    }
            //}
            string[] symbols = { "IBM", "AAPL", "GOOGL", "MSFT", "AMZN" };
            //string[] symbols = { "IBM"};

            foreach (var symbol in symbols)
            {
                FetchWeeklyData(apiKey, symbol);
                FetchDailyData(apiKey, symbol);
            }

            CalculateGrowth();
            DetermineBestPerformer();

        }
        private void FetchWeeklyData(string apiKey, string symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&symbol={symbol}&apikey={apiKey}";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                try
                {
                    string jsonResponse = client.DownloadString(queryUri);
                    var jsonData = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(jsonResponse);


                    if (jsonData.TryGetValue("Weekly Time Series", out var weeklyData))
                    {
                        
                        foreach (var entry in weeklyData.EnumerateObject())
                        {
                            Decimal.TryParse(entry.Value.GetProperty("4. close").GetString(), out decimal closingPrice);
                            WeeklyData[symbol] = closingPrice;
                            Console.WriteLine(closingPrice + "RERERERE");
                            break;
                        }
                    }
                }
                catch (WebException webEx)
                {
                    Console.WriteLine($"WebException for {symbol}: {webEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception for {symbol}: {ex.Message}");
                }
            }
        }

        private void FetchDailyData(string apiKey, string symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={apiKey}";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                try
                {
                    string jsonResponse = client.DownloadString(queryUri);
                    var jsonData = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(jsonResponse);

                    if (jsonData.TryGetValue("Time Series (Daily)", out var dailyData))
                    {
                        foreach (var entry in dailyData.EnumerateObject())
                        {
                            Decimal.TryParse(entry.Value.GetProperty("4. close").GetString(), out decimal closingPrice);
                            DailyData[symbol] = closingPrice;
                            break;
                        }

                    }
                }
                catch (WebException webEx)
                {
                    Console.WriteLine($"WebException for {symbol}: {webEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception for {symbol}: {ex.Message}");
                }
            }
        }

        private void CalculateGrowth()
        {
            GrowthData.Clear();

            foreach (var symbol in DailyData.Keys)
            {
                if (DailyData.TryGetValue(symbol, out decimal latestDailyClose) &&
                    WeeklyData.TryGetValue(symbol, out decimal latestWeeklyClose))
                {
                    decimal growth = latestDailyClose - latestWeeklyClose;
                    GrowthData[symbol] = growth;
                }
            }
        }

        private void DetermineBestPerformer()
        {
            if (GrowthData.Any())
            {
                var bestGrowthEntry = GrowthData.Aggregate((l, r) => l.Value > r.Value ? l : r);
                BestPerformer = bestGrowthEntry.Key;
                BestGrowth = bestGrowthEntry.Value;


            }
            else
            {
                BestPerformer = "N/A";
                BestGrowth = 0;
            }
        }
    }
}
