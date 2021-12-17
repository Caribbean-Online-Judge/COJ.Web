using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace COJ.Web.API.Tests.Utils;

public static class HttpContent
{
    public static StringContent Get(object payload)
    {
        // Serialize our concrete class into a JSON String
        var stringPayload = JsonConvert.SerializeObject(payload);

        // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
        return new StringContent(stringPayload, Encoding.UTF8, "application/json");
    }
}