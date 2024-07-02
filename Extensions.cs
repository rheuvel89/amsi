using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AMSI;

public static class Extensions
{
    internal static void CheckResult(this int amsiMethodResult, [CallerMemberName]string methodName = "")
    {
        if (amsiMethodResult == -2147019873)
            throw AMSIException.AMSIInvalidState;
        if (amsiMethodResult != 0)
            throw AMSIException.AMSIFailedToExecute(methodName, new Win32Exception(amsiMethodResult));
    }
    
    
}

public class AMSIException : Exception
{
    public AMSIException()
    {
    }

    public AMSIException(string message)
        : base(message)
    {
    }

    public AMSIException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static AMSIException AMSINotFound
    {
        get => new AMSIException("AMSI (Amsi.dll) was not found on this machine");
    }

    public static AMSIException AMSIFailedToExecute(string methodName, Exception innerException)
    {
        return new AMSIException("AMSI method " + methodName + " failed to execute", innerException);
    }

    public static AMSIException AMSIInvalidState
    {
        get
        {
            return new AMSIException("AMSI is in invalid state to perform operation. Check configuration of available AMSI providers");
        }
    }

    public static AMSIException NoDetectionEngineFound
    {
        get => new AMSIException("No detection engine found. AMSI call cannot be executed");
    }

    public static AMSIException FailedToInitialize()
    {
        return new AMSIException("AMSI failed to initialize");
    }

    public static AMSIException FailedToInitializeSession()
    {
        return new AMSIException("AMSI failed to initialize session");
    }

    public static AMSIException UnsupportedOperation()
    {
        return new AMSIException("This operation is not supported");
    }
}