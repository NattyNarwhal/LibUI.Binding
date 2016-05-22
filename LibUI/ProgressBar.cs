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
        protected static extern IntPtr uiNewProgressBar();
        #endregion

        /// <summary>
        /// Creates a progress bar.
        /// </summary>
        public ProgressBar()
        {
            Substrate = uiNewProgressBar();
        }

        public long Value
        {
            set
            {
                if (value >= 0 && value <= 100)
                    uiProgressBarSetValue(Substrate, value);
                else
                    throw new ArgumentOutOfRangeException("ProgressBar only supports values 0-100.");
            }
        }
    }
}
