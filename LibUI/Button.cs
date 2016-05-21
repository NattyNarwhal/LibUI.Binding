using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    /// <summary>
    /// A button that raises events when clicked.
    /// </summary>
    public class Button : Control
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern string uiButtonText(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiButtonSetText(IntPtr control, string title);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewButton(string text);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiButtonOnClicked(IntPtr b, uiButtonOnClickedDelegate f, IntPtr data);

        protected delegate void uiButtonOnClickedDelegate(IntPtr b, IntPtr data);
        #endregion

        /// <summary>
        /// Creates a new button.
        /// </summary>
        /// <param name="text">The text on the button.</param>
        public Button(string text)
        {
            Substrate = uiNewButton(text);
            uiButtonOnClicked(Substrate, (b, f) =>
                { OnClicked(new EventArgs()); }, IntPtr.Zero);
        }

        public event EventHandler<EventArgs> Clicked;

        protected virtual void OnClicked(EventArgs e)
        {
            Clicked?.Invoke(this, e);
        }

        /// <summary>
        /// Gets of sets the button's text.
        /// </summary>
        public string Text
        {
            get
            {
                return uiButtonText(Substrate);
            }
            set
            {
                uiButtonSetText(Substrate, value);
            }
        }
    }
}
