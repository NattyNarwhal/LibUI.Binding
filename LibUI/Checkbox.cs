using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    /// <summary>
    /// A control that contains a toggleable boolean value.
    /// </summary>
    public class Checkbox : Control
    {
        #region Interop
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern string uiCheckboxText(IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiCheckboxSetText(IntPtr control, string text);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern bool uiCheckboxChecked(IntPtr control);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiCheckboxSetChecked(IntPtr control, bool @checked);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewCheckbox(string text);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiCheckboxOnToggled(IntPtr c, uiCheckboxOnToggledDelegate f, IntPtr data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void uiCheckboxOnToggledDelegate(IntPtr c, IntPtr data);
        #endregion

        /// <summary>
        /// Creates a new button.
        /// </summary>
        /// <param name="text">The text on the button.</param>
        public Checkbox(string text)
        {
            Substrate = uiNewCheckbox(text);
            uiCheckboxOnToggled(Substrate, (b, f) =>
                { OnToggled(new EventArgs()); }, IntPtr.Zero);
        }

        /// <summary>
        /// This event is fired whenever the checkbox is toggled.
        /// </summary>
        public event EventHandler<EventArgs> Toggled;

        protected virtual void OnToggled(EventArgs e)
        {
            Toggled?.Invoke(this, e);
        }

        /// <summary>
        /// Gets or sets the checkbox's text.
        /// </summary>
        public string Text
        {
            get
            {
                return uiCheckboxText(Substrate);
            }
            set
            {
                uiCheckboxSetText(Substrate, value);
            }
        }

        /// <summary>
        /// Gets or sets if the box is checked.
        /// </summary>
        public bool Checked
        {
            get
            {
                return uiCheckboxChecked(Substrate);
            }
            set
            {
                uiCheckboxSetChecked(Substrate, value);
            }
        }
    }
}
