using TextCopy;

namespace TenantID;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter domain name:");
        var domain = Console.ReadLine();
        var url = "https://login.microsoftonline.com/" + domain + "/federationmetadata/2007-06/federationmetadata.xml";

        var query = new Fetch(url);
        var result = query.FetchTenantID();

        if (result.IsT1)
        {
            Console.WriteLine(result.AsT1.Message);
            Console.WriteLine("Hit any key to close...");
            Console.ReadKey();
        }

        if (!result.IsT0) return;
        Console.WriteLine($"TenantID: {result.Value} (copied to clipboard)");
        ClipboardService.SetText(result.AsT0.ToString());
        Console.WriteLine("Hit any key to close...");
        Console.ReadKey();
    }
}