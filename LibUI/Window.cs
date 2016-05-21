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
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern string uiWindowTitle(ref IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiWindowSetTitle(ref IntPtr control, string title);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiWindowSetChild(ref IntPtr window, ref IntPtr child);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int uiWindowMargined(ref IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int uiWindowSetMargined(ref IntPtr control, int margin);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewWindow(string title, int width, int height, bool hasMenubar);
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
        }

        /// <summary>
        /// Gets or sets the margins of the control.
        /// </summary>
        public int Margins
        {
            get
            {
                return uiWindowMargined(ref Substrate);
            }
            set
            {
                uiWindowSetMargined(ref Substrate, value);
            }
        }

        /// <summary>
        /// Gets or sets the window's title.
        /// </summary>
        public string Title
        {
            get
            {
                return uiWindowTitle(ref Substrate);
            }
            set
            {
                uiWindowSetTitle(ref Substrate, value);
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
                uiWindowSetChild(ref Substrate, ref value.Substrate);
                child = value;
            }
        }
    }
}
