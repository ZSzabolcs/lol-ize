using ConsoleApp1.Models;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static string version = "1.0";
        static List<Champion> champion = new List<Champion>();
        static async Task Main(string[] args)
        {
            await LoadVersion();
            Console.WriteLine(version);
            await LoadChampions();
        }

        static async Task LoadChampions()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    string url = $"https://ddragon.leagueoflegends.com/cdn/{version}/data/en_US/champion.json";
                    var responseAPI = await client.GetStringAsync(url);
                    var response = JsonSerializer.Deserialize<ChampionData>(responseAPI);
                    champion = response.Data.Values.ToList();
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Kapcsolódási hiba: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Átalakítási hiba: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba: {ex.Message}");
            }
        }

        static async Task LoadVersion()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    string url = $"https://ddragon.leagueoflegends.com/api/versions.json";
                    var responseAPI = await client.GetStringAsync(url);
                    string[] response = JsonSerializer.Deserialize<string[]>(responseAPI);
                    version = response[0];
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Kapcsolódási hiba: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Átalakítási hiba: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba: {ex.Message}");
            }
        }
    }
}
