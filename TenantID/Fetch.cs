using System.Net;
using OneOf;

namespace TenantID;

public class Fetch : IFetch
{
    public Fetch(string url)
    {
        Url = url;
        HttpClient = new HttpClient();
    }

    public string Url { get; set; }
    public Guid TenantID { get; set; }
    public HttpClient HttpClient { get; set; }

    public OneOf<Guid, ErrorMessage> FetchTenantID()
    {
        var response = HttpClient.GetAsync(Url).Result;

        if (response.StatusCode == HttpStatusCode.NotFound)
            return new ErrorMessage("Domain not found!");
        
        var xml = response.Content.ReadAsStringAsync().Result;
        var start = xml.IndexOf("entityID", StringComparison.Ordinal);
        TenantID = new Guid(xml.Substring(start + 34, 36));
        return TenantID;
    }
}