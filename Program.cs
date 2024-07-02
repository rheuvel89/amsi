// Variety of test files at: https://github.com/fire1ce/eicar-standard-antivirus-test-files/blob/master/README.md

using AMSI;

var opt = args[0];
var url = args[1];

if (!AmsiClient.IsAmsiAvailabe()) {
    Console.WriteLine("AMSI is not available on this system.");
    return;
}

using var client = AmsiClient.Create("AMSI");
using var session = client.OpenSession();

Console.WriteLine($"Scanning {url}");

var buffer =  opt switch
{
    "-u" => await new HttpClient().GetByteArrayAsync(url),
    "-f" => File.ReadAllBytes(url),
    _ => throw new ArgumentException("Invalid option")
};

var result = session.ScanBuffer(buffer, url);
Console.WriteLine($"Result: {result}");