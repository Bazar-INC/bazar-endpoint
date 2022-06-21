
using Shared;
using System.Net.Http.Json;

namespace Application.Features.AuthFeatures.Services;

public static class SmsSenderService
{
    private static HttpClient _httpClient = new();

    public static async Task<Responce> SendMessageAsync(string code, string recipient)
    {
        var values = new Dictionary<string, string>
        {
            { "recipient", recipient },
            { "text", code },
            { "from", "" }
        };

        var content = new FormUrlEncodedContent(values);

        var response = await _httpClient.PostAsync($"{AppSettings.Sms.BaseUrl}{AppSettings.Sms.ApiKey}", content);

        var responseString = await response.Content.ReadFromJsonAsync<Responce>();

        return responseString!;
    }
}

public class Data
{
    public List<string>? From { get; set; }
    public string? Message { get; set; }
}
public class Responce
{
    public int Code { get; set; }
    public Data? Data { get; set; }
}
