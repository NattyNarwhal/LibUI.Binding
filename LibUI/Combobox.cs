using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    /// <summary>
    /// A box where items can be selected, and optionally edited.
    /// </summary>
    public class Combobox : Control
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiComboboxAppend(IntPtr control, string item);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern long uiComboboxSelected(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiComboboxSetSelected(IntPtr control, long index);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewCombobox();
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewEditableCombobox();
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiComboboxOnSelected(IntPtr b, uiComboboxOnSelectedDelegate f, IntPtr data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void uiComboboxOnSelectedDelegate(IntPtr b, IntPtr data);
        #endregion

        /// <summary>
        /// Creates a new color button.
        /// </summary>
        public Combobox(bool editable)
        {
            Substrate = editable ? uiNewEditableCombobox() : uiNewCombobox();
            uiComboboxOnSelected(Substrate, (b, f) =>
                { OnSelected(new EventArgs()); }, IntPtr.Zero);
        }

        /// <summary>
        /// This event is fired whenever the user changes the selection.
        /// </summary>
        public event EventHandler<EventArgs> Selected;

        protected virtual void OnSelected(EventArgs e)
        {
            Selected?.Invoke(this, e);
        }

        /// <summary>
        /// Gets the index of the selected item.
        /// </summary>
        public long SelectedIndex
        {
            get
            {
                return uiComboboxSelected(Substrate);
            }
            set
            {
                uiComboboxSetSelected(Substrate, value);
            }
        }

        public void Append(string item)
        {
            uiComboboxAppend(Substrate, item);
        }
    }
}
