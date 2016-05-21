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
    /// Represents basic functionality for a box.
    /// </summary>
    public abstract class Box : Control
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiBoxAppend(ref uiControl box, ref uiControl control, bool stretchy);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiBoxRemove(ref uiControl box,  ref uiControl control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int uiBoxPadded(ref uiControl box);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiBoxSetPadded(ref uiControl box, int padding);
        #endregion

        // TODO: Should these be in a collection?

        /// <summary>
        /// Adds a control to the box.
        /// </summary>
        /// <param name="c">The control to add.</param>
        /// <param name="stretchy">
        /// If the control should stretch to fit the box optimally.
        /// </param>
        public void Append(Control c, bool stretchy = false)
        {
            uiBoxAppend(ref Substrate, ref c.Substrate, stretchy);
        }

        /// <summary>
        /// Removes a control from the box.
        /// </summary>
        /// <param name="c">The control to remove.</param>
        public void Remove(Control c)
        {
            uiBoxRemove(ref Substrate, ref c.Substrate);
        }

        /// <summary>
        /// Gets or sets the padding around items in the box.
        /// </summary>
        public int Padding
        {
            get
            {
                return uiBoxPadded(ref Substrate);
            }
            set
            {
                uiBoxSetPadded(ref Substrate, value);
            }
        }
    }

    /// <summary>
    /// A box that arranges its items in a left-right fashion.
    /// </summary>
    public class HBox : Box
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewHorizontalBox();
        #endregion

        public HBox()
        {
            Substrate = (uiControl)Marshal.PtrToStructure(uiNewHorizontalBox(), Substrate.GetType());
        }
    }

    /// <summary>
    /// A box that arranges its items in an up-down fashion.
    /// </summary>
    public class VBox : Box
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewVerticalBox();
        #endregion

        public VBox()
        {
            Substrate = (uiControl)Marshal.PtrToStructure(uiNewVerticalBox(), Substrate.GetType());
        }
    }
}
