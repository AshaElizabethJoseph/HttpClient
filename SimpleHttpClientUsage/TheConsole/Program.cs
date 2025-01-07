using Common;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace TheConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region scenario1 : using HttpClient to get data from API

            /* var url = "http://localhost:5183/api/People";
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
             }*/

            #endregion

            #region scenario2 : using HttpClient to post data to API

            var url = "http://localhost:5183/api/People";
            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            using (var httpClient = new HttpClient())
            {
                #region Example 1 : Post json data to API
                /* 
                 var person = new Person() { Name = "Joseph" };
                 var response = await httpClient.PostAsJsonAsync(url, person);

                 if (response.IsSuccessStatusCode)
                 {
                     var responseContent = await response.Content.ReadAsStringAsync();
                     var id = JsonSerializer.Deserialize<int>(responseContent, jsonSerializerOptions);
                     Console.WriteLine($"Person Id: {id}");
                 }
                */
                #endregion

                #region Example 2 :post any content type to API
                /*
                var person = new Person() { Name = "Asha" };
                var stringContent = new StringContent(JsonSerializer.Serialize(person), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, stringContent);
                */
                #endregion

                #region Example 3 : Validations
                var person = new Person() { Age = -1,Email = "abc" };
                var response = await httpClient.PostAsJsonAsync(url, person);
                if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(body);
                    var errorsFromWebApi = Utils.ExtractErrorsFromWebApiResponse(body);
                    foreach (var fieldWithErrors in errorsFromWebApi)
                    {
                        Console.WriteLine($"Field: {fieldWithErrors.Key}");
                        foreach (var error in fieldWithErrors.Value)
                        {
                            Console.WriteLine($"- {error}");
                        }
                    }
                }
                #endregion

                var people = JsonSerializer.Deserialize<List<Person>>(await httpClient.GetStringAsync(url), jsonSerializerOptions);
                
            }

            #endregion
        }
    }
}
