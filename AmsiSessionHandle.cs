using Microsoft.Win32.SafeHandles;

namespace AMSI;

internal sealed class AmsiSessionHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal AmsiContextHandle Context { get; set; }

    public override bool IsInvalid => base.IsInvalid || Context.IsInvalid;

    internal AmsiSessionHandle()
        : base(true)
    {
    }

    protected override bool ReleaseHandle()
    {
        AMSI.AmsiCloseSession(Context, handle);
        return true;
    }
}