using LibUI.Native;
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
        public uiControl Substrate;

        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlDestroy(ref uiControl control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern UIntPtr uiControlHandle(ref uiControl control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern uiControl uiControlParent(ref uiControl control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlSetParent(ref uiControl control, ref uiControl parent);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern bool uiControlToplevel(ref uiControl control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern bool uiControlVisible(ref uiControl control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlShow(ref uiControl control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlHide(ref uiControl control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern bool uiControlEnabled(ref uiControl control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlEnable(ref uiControl control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiControlDisable(ref uiControl control);
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
