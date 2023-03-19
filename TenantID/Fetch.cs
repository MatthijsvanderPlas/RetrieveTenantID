
namespace TenantID;

public class Fetch : IFetch
{
    public string Url { get; set; }
    public Guid TenantID { get; set; }
    public HttpClient HttpClient { get; set; }

    public Fetch(string url)
    {
        this.Url = url;
        this.HttpClient = new HttpClient();
        this.TenantID = new Guid();
    }
    
    public Guid? FetchTenantID()
    {
        try
        {
            HttpResponseMessage response = HttpClient.GetAsync(Url).Result;
            if (response.IsSuccessStatusCode)
            {
                var xml = response.Content.ReadAsStringAsync().Result;
                var start = xml.IndexOf("entityID", StringComparison.Ordinal);
                TenantID = new Guid(xml.Substring(start + 34, 36));
                
                
            }
            else
            {
                throw new Exception("Domain not found");
            }
   
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return TenantID;
    }
}