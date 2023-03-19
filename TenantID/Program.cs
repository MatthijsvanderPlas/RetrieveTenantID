using System.Runtime.InteropServices;

namespace TenantID;

internal abstract class Program 
{
    [DllImport("user32.dll")]
    internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll")]
    internal static extern bool CloseClipboard();

    [DllImport("user32.dll")]
    internal static extern bool SetClipboardData(uint uFormat, IntPtr data);
    [STAThread]
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter domain name:");
        var domain = Console.ReadLine();
        var url = "https://login.microsoftonline.com/" + domain + "/federationmetadata/2007-06/federationmetadata.xml";

        var query = new Fetch(url);
        var result = query.FetchTenantID();
        
        Console.WriteLine($"TenantID: {result} (copied to clipboard)");

        OpenClipboard(IntPtr.Zero);
        var ptr = Marshal.StringToHGlobalUni(result.ToString());
        SetClipboardData(13, ptr);
        CloseClipboard();
        Marshal.FreeHGlobal(ptr);
        
        Console.WriteLine("Hit any key to close...");
        Console.ReadKey();
    }

}
