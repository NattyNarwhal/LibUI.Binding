using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    /// <summary>
    /// A top-level window, for putting controls inside of.
    /// </summary>
    public class Window : Control
    {
        #region Interop
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern string uiWindowTitle(IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiWindowSetTitle(IntPtr control, string title);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiWindowSetChild(IntPtr window, IntPtr child);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int uiWindowMargined(IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int uiWindowSetMargined(IntPtr control, int margin);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewWindow(string title, int width, int height, bool hasMenubar);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiWindowOnClosing(IntPtr b, uiWindowOnClosingDelegate f, IntPtr data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void uiWindowOnClosingDelegate(IntPtr b, IntPtr data);
        #endregion

        private Control child;

        /// <summary>
        /// Creates a new window.
        /// </summary>
        /// <param name="title">The window's title.</param>
        /// <param name="width">How wide the window is, in pixels.</param>
        /// <param name="height">How tall the window is, in pixels.</param>
        /// <param name="hasMenubar">If the window should have a menu bar.</param>
        public Window(string title, int width, int height, bool hasMenubar)
        {
            Substrate = uiNewWindow(title, width, height, hasMenubar);

            uiWindowOnClosing(Substrate, (b, f) =>
                { OnClosing(new EventArgs()); }, IntPtr.Zero);
        }

        public event EventHandler<EventArgs> Closing;

        protected virtual void OnClosing(EventArgs e)
        {
            Closing?.Invoke(this, e);
        }

        /// <summary>
        /// Gets or sets the margins of the control.
        /// </summary>
        public int Margins
        {
            get
            {
                return uiWindowMargined(Substrate);
            }
            set
            {
                uiWindowSetMargined(Substrate, value);
            }
        }

        /// <summary>
        /// Gets or sets the window's title.
        /// </summary>
        public string Title
        {
            get
            {
                return uiWindowTitle(Substrate);
            }
            set
            {
                uiWindowSetTitle(Substrate, value);
            }
        }
        
        /// <summary>
        /// Gets or sets the child control.
        /// </summary>
        public Control Child
        {
            get
            {
                return child;
            }
            set
            {
                uiWindowSetChild(Substrate, value.Substrate);
                child = value;
            }
        }
    }
}
