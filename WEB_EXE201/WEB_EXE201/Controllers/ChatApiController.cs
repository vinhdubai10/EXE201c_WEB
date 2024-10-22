using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class ChatApiController : Controller
{
    private readonly HttpClient _httpClient;

    public ChatApiController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_API_KEY");
    }

    public async Task<string> SendMessage(string message)
    {
        var content = new StringContent("{\"message\":\"" + message + "\"}", Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("YOUR_API_ENDPOINT", content);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        return "Error sending message.";
    }
}