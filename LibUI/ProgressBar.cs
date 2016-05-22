using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    /// <summary>
    /// A bar that fills as progress is achieved.
    /// </summary>
    public class ProgressBar : Control
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiProgressBarSetValue(IntPtr control, long value);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiProgressBar();
        #endregion

        /// <summary>
        /// Creates a progress bar.
        /// </summary>
        public ProgressBar()
        {
            Substrate = uiProgressBar();
        }

        public long Value
        {
            set
            {
                uiProgressBarSetValue(Substrate, value);
            }
        }
    }
}
