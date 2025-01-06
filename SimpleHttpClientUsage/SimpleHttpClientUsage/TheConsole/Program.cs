using Common;
using System.Text.Json;

namespace TheConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var url = "http://localhost:5183/api/People";
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url); // await httpClient.GetStringAsync(url); get  body  alone as string
                response.EnsureSuccessStatusCode(); //throws exception 
                if (response.IsSuccessStatusCode) //(response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var people = JsonSerializer.Deserialize<List<Person>>(responseContent,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
        }
    }
}
