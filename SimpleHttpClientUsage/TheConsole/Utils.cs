using System.Text.Json;

namespace TheConsole
{
    public class Utils
    {
        public static Dictionary<string, List<string>>ExtractErrorsFromWebApiResponse(string body)
        {
           var response = new Dictionary<string, List<string>>();
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(body);
            var errorsJsonElement = jsonElement.GetProperty("errors");
            foreach (var fieldwithErrors in errorsJsonElement.EnumerateObject())
            {
                var field = fieldwithErrors.Name;
                var errors = new List<string>();
                foreach (var error in fieldwithErrors.Value.EnumerateArray())
                {
                    errors.Add(error.GetString());
                }
                response.Add(field, errors);
            }
            return response;
        }
    }
}
