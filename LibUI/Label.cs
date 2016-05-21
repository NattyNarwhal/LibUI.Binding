using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    /// <summary>
    /// A decorative text label.
    /// </summary>
    public class Label : Control
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern string uiLabelText(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiLabelSetText(IntPtr control, string text);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewLabel(string text);
        #endregion

        /// <summary>
        /// Creates a new button.
        /// </summary>
        /// <param name="text">The text on the button.</param>
        public Label(string text)
        {
            Substrate = uiNewLabel(text);
        }

        /// <summary>
        /// Gets of sets the label's text.
        /// </summary>
        public string Text
        {
            get
            {
                return uiLabelText(Substrate);
            }
            set
            {
                uiLabelSetText(Substrate, value);
            }
        }
    }
}
