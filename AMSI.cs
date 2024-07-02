using System.Runtime.InteropServices;

namespace AMSI;

internal class AMSI
{
    private const string AmsiDllPath = "C:\\Windows\\System32\\amsi.dll";
    
    public enum AMSI_RESULT
    {
        AMSI_RESULT_CLEAN = 0,
        AMSI_RESULT_NOT_DETECTED = 1,
        AMSI_RESULT_DETECTED = 32768
    }

    [DllImport(AmsiDllPath, EntryPoint = "AmsiInitialize", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern int AmsiInitialize([MarshalAs(UnmanagedType.LPWStr)] string appName, out AmsiContextHandle amsiContextHandle);

    [DllImport(AmsiDllPath, EntryPoint = "AmsiUninitialize", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern void AmsiUninitialize(IntPtr amsiContextHandle);

    [DllImport(AmsiDllPath, EntryPoint = "AmsiOpenSession", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern int AmsiOpenSession(AmsiContextHandle amsiContextHandle, out AmsiSessionHandle session);

    [DllImport(AmsiDllPath, EntryPoint = "AmsiCloseSession", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern void AmsiCloseSession(AmsiContextHandle amsiContextHandle, IntPtr session);

    [DllImport(AmsiDllPath, EntryPoint = "AmsiScanString", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern int AmsiScanString(AmsiContextHandle amsiContextHandle,
        [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPWStr)] string @string,
        [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPWStr)] string contentName, AmsiSessionHandle session,
        out AMSI_RESULT result);

    [DllImport(AmsiDllPath, EntryPoint = "AmsiScanBuffer", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern int AmsiScanBuffer(AmsiContextHandle amsiContextHandle, [In] [MarshalAs(UnmanagedType.LPArray)] byte[] buffer,
        ulong length, [In()] [MarshalAs(UnmanagedType.LPWStr)] string contentName, AmsiSessionHandle session,
        out AMSI_RESULT result);

    // //This method apparently exists on MSDN but not in AMSI.dll (version 4.9.10586.0)
    // [DllImport(AmsiDllPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
    // public static extern bool AmsiResultIsMalware(AMSI_RESULT result);
}