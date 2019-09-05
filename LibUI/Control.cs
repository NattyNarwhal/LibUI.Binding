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

        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlDestroy(IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern UIntPtr uiControlHandle(IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiControlParent(IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlSetParent(IntPtr control, IntPtr parent);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern bool uiControlToplevel(IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern bool uiControlVisible(IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlShow(IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlHide(IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern bool uiControlEnabled(IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlEnable(IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlDisable(IntPtr control);
        #endregion

        /// <summary>
        /// Gets or sets if the control should respond to input.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return uiControlEnabled(Substrate);
            }
            set
            {
                if (value)
                    uiControlEnable(Substrate);
                else
                    uiControlDisable(Substrate);
            }
        }

        /// <summary>
        /// Gets of sets if the control is visible to the user.
        /// </summary>
        public bool Visible
        {
            get
            {
                return uiControlVisible(Substrate);
            }
            set
            {
                if (value)
                    uiControlShow(Substrate);
                else
                    uiControlHide(Substrate);
            }
        }

        /// <summary>
        /// Gets if the control is top-level in the hierarchy.
        /// </summary>
        public bool Toplevel
        {
            get
            {
                return uiControlToplevel(Substrate);
            }
        }

        /// <summary>
        /// Gets the native handle for the control.
        /// </summary>
        public UIntPtr Handle
        {
            get
            {
                return uiControlHandle(Substrate);
            }
        }

        public void Destroy()
        {
            uiControlDestroy(Substrate);
        }

        public void Dispose()
        {
            Destroy();
            // should we free manually?
        }
    }
}
