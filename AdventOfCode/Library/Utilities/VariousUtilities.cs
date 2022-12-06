using System.Net;

namespace AdventOfCode.Library.Utilities;

public static class VariousUtilities
{
    public static List<int> IntListFromRange(int start, int end)
    {
        var numbers = new List<int>();

        for (var i = start; i <= end; i++)
            numbers.Add(i);

        return numbers;
    }

    public static HttpResponseMessage? FetchHtmlContent(HttpRequestMessage message, 
        Action<HttpClient, CookieContainer>? callback = null)
    {
        var cookieContainer = new CookieContainer();
        using var handler = new HttpClientHandler { CookieContainer = cookieContainer };
        using var client = new HttpClient(handler);

        callback?.Invoke(client, cookieContainer);

        var result = client.Send(message);
        return result.IsSuccessStatusCode ? result : null;
    }
}