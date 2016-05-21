using LibUI.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    public class Button : Control
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern string uiButtonText(ref uiControl control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiButtonSetText(ref uiControl control, string title);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewButton(string text);
        #endregion

        public Button(string text)
        {
            Substrate = (uiControl)Marshal.PtrToStructure(uiNewButton(text), Substrate.GetType());
        }

        /// <summary>
        /// Gets of sets the button's text.
        /// </summary>
        public string Text
        {
            get
            {
                return uiButtonText(ref Substrate);
            }
            set
            {
                uiButtonSetText(ref Substrate, value);
            }
        }
    }
}
