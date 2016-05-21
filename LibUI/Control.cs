using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace LibUI
{
    /// <summary>
    /// The core methods shared by all controls.
    /// </summary>
    public abstract class Control : IDisposable
    {
        #region Interop
        /// <summary>
        /// The underlying native structure representing the control.
        /// </summary>
        public IntPtr Substrate;

        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlDestroy(ref IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern UIntPtr uiControlHandle(ref IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiControlParent(ref IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlSetParent(ref IntPtr control, ref IntPtr parent);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern bool uiControlToplevel(ref IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern bool uiControlVisible(ref IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlShow(ref IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlHide(ref IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern bool uiControlEnabled(ref IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlEnable(ref IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlDisable(ref IntPtr control);
        #endregion

        /// <summary>
        /// Gets or sets if the control should respond to input.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return uiControlEnabled(ref Substrate);
            }
            set
            {
                if (value)
                    uiControlEnable(ref Substrate);
                else
                    uiControlDisable(ref Substrate);
            }
        }

        /// <summary>
        /// Gets of sets if the control is visible to the user.
        /// </summary>
        public bool Visible
        {
            get
            {
                return uiControlVisible(ref Substrate);
            }
            set
            {
                if (value)
                    uiControlShow(ref Substrate);
                else
                    uiControlHide(ref Substrate);
            }
        }

        /// <summary>
        /// Gets if the control is top-level in the hierarchy.
        /// </summary>
        public bool Toplevel
        {
            get
            {
                return uiControlToplevel(ref Substrate);
            }
        }

        /// <summary>
        /// Gets the native handle for the control.
        /// </summary>
        public UIntPtr Handle
        {
            get
            {
                return uiControlHandle(ref Substrate);
            }
        }

        public void Destroy()
        {
            uiControlDestroy(ref Substrate);
        }

        public void Dispose()
        {
            Destroy();
            // should we free manually?
        }
    }
}
