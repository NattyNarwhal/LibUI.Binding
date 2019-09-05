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
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiBoxAppend(IntPtr box, IntPtr control, bool stretchy);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiBoxRemove(IntPtr box,  IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int uiBoxPadded(IntPtr box);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiBoxSetPadded(IntPtr box, int padding);
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
            uiBoxAppend(Substrate, c.Substrate, stretchy);
        }

        /// <summary>
        /// Removes a control from the box.
        /// </summary>
        /// <param name="c">The control to remove.</param>
        public void Remove(Control c)
        {
            uiBoxRemove(Substrate, c.Substrate);
        }

        /// <summary>
        /// Gets or sets the padding around items in the box.
        /// </summary>
        public int Padding
        {
            get
            {
                return uiBoxPadded(Substrate);
            }
            set
            {
                uiBoxSetPadded(Substrate, value);
            }
        }
    }

    /// <summary>
    /// A box that arranges its items in a left-right fashion.
    /// </summary>
    public class HBox : Box
    {
        #region Interop
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewHorizontalBox();
        #endregion

        public HBox()
        {
            Substrate = uiNewHorizontalBox();
        }
    }

    /// <summary>
    /// A box that arranges its items in an up-down fashion.
    /// </summary>
    public class VBox : Box
    {
        #region Interop
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewVerticalBox();
        #endregion

        public VBox()
        {
            Substrate = uiNewVerticalBox();
        }
    }
}
