using System.Net;

namespace AdventOfCode.Library;

public static class PuzzleInputLoader
{
    public static async Task<IEnumerable<string>> DownloadInput(int year, int day)
    {
        Directory.CreateDirectory($@"input/{year}");
        
        var downloadUri = new Uri("https://adventofcode.com");
        var cookieContainer = new CookieContainer();
        using var handler = new HttpClientHandler {CookieContainer = cookieContainer};
        using var client = new HttpClient(handler) {BaseAddress = downloadUri};
        
        cookieContainer.Add(downloadUri, new Cookie("session", "nope..."));
        
        var result = await client.GetStringAsync($"/{year}/day/{day}/input");

        await File.WriteAllTextAsync(Puzzle<object>.InputFile(year, day), result);

        return File.ReadLines(Puzzle<object>.InputFile(year, day));
    }
}