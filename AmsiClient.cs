using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace AMSI;

public class AmsiClient : IDisposable
{
    internal readonly AmsiContextHandle AmsiContext;

    public static AmsiClient Create(string application)
    {
        AMSI.AmsiInitialize(application, out var amsiContext).CheckResult();
        return new AmsiClient(amsiContext);
    }

    private AmsiClient(AmsiContextHandle amsiContext)
    {
        AmsiContext = amsiContext;
    }

    public AmsiSession OpenSession()
    {
        AMSI.AmsiOpenSession(AmsiContext, out var amsiSession).CheckResult();
        amsiSession.Context = AmsiContext;
        return new AmsiSession(amsiSession, this);
    }

    public void Dispose() => AmsiContext?.Dispose();
    
    public static bool IsAmsiAvailabe()
    {
        try
        {
            Marshal.PrelinkAll(typeof (AMSI));
            return true;
        }
        catch
        {
            return false;
        }
    }
    
}