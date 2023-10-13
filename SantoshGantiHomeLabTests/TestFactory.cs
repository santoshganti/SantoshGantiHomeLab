using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using System.Net.Http.Headers;

namespace Functions.Tests;

public class TestFactory
{
    public static IEnumerable<object[]> Data()
    {
        return new List<object[]>
        {
            new object[] { "name", "Bill" },
            new object[] { "name", "Paul" },
            new object[] { "name", "Steve" }
        };
    }

    private static Dictionary<string, StringValues> CreateDictionary(string key, string value)
    {
        var qs = new Dictionary<string, StringValues>
        {
            { key, value }
        };
        return qs;
    }

    public static HttpRequestMessage CreateHttpRequest(HttpMethod method, string uri, string content)
    {
        var memory = new MemoryStream();
        var writer = new StreamWriter(memory);
        writer.Write(content);
        writer.Flush();
        var streamContent = new StreamContent(memory);
        streamContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
        var request = new HttpRequestMessage();
        request.Method = method;
        //request.RequestUri = new System.Uri(uri);
        request.Content = streamContent;
        request.Content.Headers.ContentLength = streamContent.Headers.ContentLength;
        return request;
    }

    public static ILogger CreateLogger(LoggerTypes type = LoggerTypes.Null)
    {
        ILogger logger;
        if (type == LoggerTypes.List)
            logger = new ListLogger();
        else
            logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");

        return logger;
    }
}