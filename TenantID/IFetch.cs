namespace TenantID;

public interface IFetch
{
    public HttpClient HttpClient { get; set; }
    string Url { get; set; }
    public Guid TenantID { get; set; }

    public Guid? FetchTenantID();


}