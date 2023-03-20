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

        if (result == "Domain not found")
        {
            Console.WriteLine(result);
            Console.WriteLine("Hit any key to close...");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine($"TenantID: {result} (copied to clipboard)");
            ClipboardService.SetText(result);
            Console.WriteLine("Hit any key to close...");
            Console.ReadKey();
        }
    }
}