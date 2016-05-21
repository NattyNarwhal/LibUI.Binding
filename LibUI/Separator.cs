using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    /// <summary>
    /// A horizontal separator.
    /// </summary>
    public class HSeparator : Control
    {
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiSeparator();

        public HSeparator()
        {
            Substrate = uiSeparator();
        }
    }
}
