namespace TenantID;

public interface IFetch
{
    public HttpClient HttpClient { get; set; }
    string Url { get; set; }
    public string TenantID { get; set; }

    public string FetchTenantID();
}