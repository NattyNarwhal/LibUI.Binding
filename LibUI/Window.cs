using LibUI.Native;
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
        protected static extern string uiWindowTitle(ref uiControl control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiWindowSetTitle(ref uiControl control, string title);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiWindowSetChild(ref uiControl window, ref uiControl child);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int uiWindowMargined(ref uiControl control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int uiWindowSetMargined(ref uiControl control, int margin);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewWindow(string title, int width, int height, bool hasMenubar);
        #endregion

        /// <summary>
        /// Creates a new window.
        /// </summary>
        /// <param name="title">The window's title.</param>
        /// <param name="width">How wide the window is, in pixels.</param>
        /// <param name="height">How tall the window is, in pixels.</param>
        /// <param name="hasMenubar">If the window should have a menu bar.</param>
        public Window(string title, int width, int height, bool hasMenubar)
        {
            Substrate = (uiControl)Marshal.PtrToStructure(uiNewWindow(title, width, height, false), Substrate.GetType());
            // Substrate = uiNewWindow(title, width, height, hasMenubar);
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
        /// Gets of sets the window's title.
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
        /// Adds a control to the window.
        /// </summary>
        public Control Child
        {
            set
            {
                uiWindowSetChild(ref Substrate, ref value.Substrate);
            }
        }
    }
}
