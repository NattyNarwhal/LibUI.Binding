using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    // TODO: should this and Window share an iface due to Child and Margins?
    /// <summary>
    /// A labelled container.
    /// </summary>
    public class Group : Control
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern string uiGroupTitle(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiGroupSetTitle(IntPtr control, string title);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiGroupSetChild(IntPtr window, IntPtr child);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int uiGroupMargined(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int uiGroupSetMargined(IntPtr control, int margin);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewGroup(string title);
        #endregion

        private Control child;

        /// <summary>
        /// Creates a new window.
        /// </summary>
        /// <param name="title">The window's title.</param>
        /// <param name="width">How wide the window is, in pixels.</param>
        /// <param name="height">How tall the window is, in pixels.</param>
        /// <param name="hasMenubar">If the window should have a menu bar.</param>
        public Group(string title)
        {
            Substrate = uiNewGroup(title);
        }

        /// <summary>
        /// Gets or sets the margins of the control.
        /// </summary>
        public int Margins
        {
            get
            {
                return uiGroupMargined(Substrate);
            }
            set
            {
                uiGroupSetMargined(Substrate, value);
            }
        }

        /// <summary>
        /// Gets or sets the group's title.
        /// </summary>
        public string Title
        {
            get
            {
                return uiGroupTitle(Substrate);
            }
            set
            {
                uiGroupSetTitle(Substrate, value);
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
                uiGroupSetChild(Substrate, value.Substrate);
                child = value;
            }
        }
    }
}
