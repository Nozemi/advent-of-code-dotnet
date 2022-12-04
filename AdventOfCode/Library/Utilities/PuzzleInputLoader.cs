using System.Net;
using System.Net.Http.Headers;
using Serilog;

namespace AdventOfCode.Library.Utilities;

public static class PuzzleInputLoader
{
    public static async Task<bool> DownloadInput(int year, int day, string token, string inputFile)
    {
        if (File.Exists(inputFile))
            return true;
        
        Log.Debug("Downloading input data for year {Year}, day {Day}. Using token: {Token}",
            year, day, token
        );
        
        Directory.CreateDirectory($@"input/{year}");

        var downloadUri = new Uri("https://adventofcode.com");
        var cookieContainer = new CookieContainer();
        using var handler = new HttpClientHandler { CookieContainer = cookieContainer };
        using var client = new HttpClient(handler) { BaseAddress = downloadUri };
        
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        
        cookieContainer.Add(downloadUri, new Cookie("session", token));

        var message = new HttpRequestMessage();
        message.Method = HttpMethod.Get;
        message.RequestUri = new Uri(downloadUri + $"{year}/day/{day}/input");

        var result = client.Send(message);
        if (result.StatusCode == HttpStatusCode.NotFound)
        {
            Log.Error("Resource not found: {Uri}", result.RequestMessage?.RequestUri);
            return false;
        }
        
        if (!result.IsSuccessStatusCode)
        {
            Log.Error("Nope, request failed: {StatusCode}", result.StatusCode);
            return false;
        }

        await using var fs = new FileStream(inputFile, FileMode.CreateNew);
        await result.Content.CopyToAsync(fs);
        
        return true;
    }
}