using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static string version = "1.0";
        static async Task Main(string[] args)
        {
            await LoadVersion();
            Console.WriteLine(version);
        }

        static async Task LoadVersion()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    string url = "https://ddragon.leagueoflegends.com/api/versions.json";
                    var responseAPI = await client.GetStringAsync(url);
                    string[] response = JsonSerializer.Deserialize<string[]>(responseAPI);
                    version = response[0];
                }
            }
            catch
            {

            }
        }
    }
}
