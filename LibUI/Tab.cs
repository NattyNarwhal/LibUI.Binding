using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    // TODO: similar to group, but without a unified child
    /// <summary>
    /// A tabbed notebook with multiple pages.
    /// </summary>
    public class Tab : Control
    {
        #region Interop
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiTabAppend(IntPtr control, string title, IntPtr child);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiTabInsertAt(IntPtr control, string title, long before, IntPtr child);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiTabDelete(IntPtr window, long index);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int uiTabMargined(IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiTabSetMargined(IntPtr control, int margin);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewTab();
        #endregion

        public Tab()
        {
            Substrate = uiNewTab();
        }

        /// <summary>
        /// Adds a control to the tabbed notebook.
        /// </summary>
        /// <param name="c">The control to add.</param>
        /// <param name="title">The title of the tab.</param>
        public void Append(string title, Control c)
        {
            uiTabAppend(Substrate, title, c.Substrate);
        }

        /// <summary>
        /// Adds a control to the tabbed notebook at the specified index..
        /// </summary>
        /// <param name="c">The control to add.</param>
        /// <param name="title">The title of the tab.</param>
        /// <param name="before">The index of the page to insert before.</param>
        public void Append(string title, Control c, long before)
        {
            uiTabInsertAt(Substrate, title, before, c.Substrate);
        }

        /// <summary>
        /// Removes a control from the tabbed notebook..
        /// </summary>
        /// <param name="c">The index of the control to remove.</param>
        public void Remove(long index)
        {
            uiTabDelete(Substrate, index);
        }

        /// <summary>
        /// Gets or sets the margins of the controls.
        /// </summary>
        public int Margins
        {
            get
            {
                return uiTabMargined(Substrate);
            }
            set
            {
                uiTabSetMargined(Substrate, value);
            }
        }
    }
}
