namespace AMSI;

public class AmsiSession : IDisposable
{
    private readonly AmsiSessionHandle _amsiSessionHandle;
    private readonly AmsiClient _amsiClient;
    
    internal AmsiSession(AmsiSessionHandle amsiSessionHandle, AmsiClient amsiClient)
    {
        _amsiSessionHandle = amsiSessionHandle;
        _amsiClient = amsiClient;
    }

    public AmsiResult ScanString(string content, string contentName)
    {
        AMSI.AmsiScanString(_amsiClient.AmsiContext, content, contentName, _amsiSessionHandle, out var result).CheckResult();
        return (AmsiResult)result;
    }
    
    public AmsiResult ScanBuffer(byte[] buffer, string contentName)
    {
        AMSI.AmsiScanBuffer(_amsiClient.AmsiContext, buffer, (ulong) buffer.Length, contentName, _amsiSessionHandle, out var result).CheckResult();
        return (AmsiResult)result;
    }
    
    public AmsiResult ScanFile(string filePath)
    {
        var contentName = Path.GetFileName(filePath);
        var buffer = File.ReadAllBytes(filePath);
        return ScanBuffer(buffer, contentName);
    }
    
    public void Dispose() => _amsiSessionHandle?.Dispose();
}