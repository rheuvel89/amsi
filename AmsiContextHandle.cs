using Microsoft.Win32.SafeHandles;

namespace AMSI;

internal sealed class AmsiContextHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal AmsiContextHandle()
        : base(true)
    {
    }

    protected override bool ReleaseHandle()
    {
        AMSI.AmsiUninitialize(handle);
        return true;
    }
}