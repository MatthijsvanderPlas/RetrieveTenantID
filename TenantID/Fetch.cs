using System.Net;

namespace TenantID;

public class Fetch : IFetch
{
    public Fetch(string url)
    {
        Url = url;
        HttpClient = new HttpClient();
        TenantID = "";
    }

    public string Url { get; set; }
    public string TenantID { get; set; }
    public HttpClient HttpClient { get; set; }

    public string FetchTenantID()
    {
        var response = HttpClient.GetAsync(Url).Result;
        if (response.StatusCode == HttpStatusCode.NotFound)
            return "Domain not found";
        var xml = response.Content.ReadAsStringAsync().Result;
        var start = xml.IndexOf("entityID", StringComparison.Ordinal);
        TenantID = xml.Substring(start + 34, 36);
        return TenantID;
    }
}
