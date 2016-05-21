using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI.Native
{
    public delegate void uiControl_Destroy(ref uiControl param0);
    public delegate UIntPtr uiControl_Handle(ref uiControl param0);
    public delegate IntPtr uiControl_Parent(ref uiControl param0);
    public delegate void uiControl_SetParent(ref uiControl param0, ref uiControl param1);
    public delegate bool uiControl_Toplevel(ref uiControl param0);
    public delegate bool uiControl_Visible(ref uiControl param0);
    public delegate void uiControl_Show(ref uiControl param0);
    public delegate void uiControl_Hide(ref uiControl param0);
    public delegate bool uiControl_Enabled(ref uiControl param0);
    public delegate void uiControl_Enable(ref uiControl param0);
    public delegate void uiControl_Disable(ref uiControl param0);

    [StructLayout(LayoutKind.Sequential)]
    public struct uiControl
    {
        public uint Signature;
        public uint OSSignature;
        public uint TypeSignature;
        public uiControl_Destroy Destroy;
        public uiControl_Handle Handle;
        public uiControl_Parent Parent;
        public uiControl_SetParent SetParent;
        public uiControl_Toplevel Toplevel;
        public uiControl_Visible Visible;
        public uiControl_Show Show;
        public uiControl_Hide Hide;
        public uiControl_Enabled Enabled;
        public uiControl_Enable Enable;
        public uiControl_Disable Disable;
    }
}
