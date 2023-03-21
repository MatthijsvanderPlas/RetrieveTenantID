using OneOf;

namespace TenantID;

public interface IFetch
{
    public HttpClient HttpClient { get; set; }
    string Url { get; set; }
    public Guid TenantID { get; set; }

    public OneOf<Guid, ErrorMessage> FetchTenantID();
}