using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    /// <summary>
    /// Shared attributes between each type of combobox.
    /// </summary>
    public interface ICombobox
    {
        void Append(string item);
        // TODO: select when this is finalized in libui
    }

    /// <summary>
    /// A box where items can be selected.
    /// </summary>
    public class Combobox : Control, ICombobox
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewCombobox();

        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiComboboxAppend(IntPtr control, string item);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern long uiComboboxSelected(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiComboboxSetSelected(IntPtr control, long index);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiComboboxOnSelected(IntPtr b, uiComboboxOnSelectedDelegate f, IntPtr data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void uiComboboxOnSelectedDelegate(IntPtr b, IntPtr data);
        #endregion

        /// <summary>
        /// Creates a new combobox.
        /// </summary>
        public Combobox()
        {
            Substrate = uiNewCombobox();
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

        /// <summary>
        /// Adds an item to the combobox.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Append(string item)
        {
            uiComboboxAppend(Substrate, item);
        }
    }

    /// <summary>
    /// A box where items can be selected and edited.
    /// </summary>
    public class EditableCombobox : Control, ICombobox, IEntry
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern string uiEditableComboboxText(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiEditableComboboxSetText(IntPtr control, string text);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewEditableCombobox();
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiEditableComboboxAppend(IntPtr control, string item);
        //[DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        //protected static extern long uiComboboxSelected(IntPtr control);
        //[DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        //protected static extern void uiComboboxSetSelected(IntPtr control, long index);
        //[DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        //protected static extern void uiComboboxOnSelected(IntPtr b, uiComboboxOnSelectedDelegate f, IntPtr data);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiEditableComboboxOnChanged(IntPtr b, uiEditableComboboxOnChangedDelegate f, IntPtr data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void uiEditableComboboxOnChangedDelegate(IntPtr b, IntPtr data);
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //protected delegate void uiComboboxOnSelectedDelegate(IntPtr b, IntPtr data);
        #endregion

        /// <summary>
        /// Creates a new combobox.
        /// </summary>
        public EditableCombobox()
        {
            Substrate = uiNewEditableCombobox();
            //uiComboboxOnSelected(Substrate, (b, f) =>
            //    { OnSelected(new EventArgs()); }, IntPtr.Zero);

            uiEditableComboboxOnChanged(Substrate, (b, f) =>
                { OnChanged(new EventArgs()); }, IntPtr.Zero);
        }

        /// <summary>
        /// This event is fired whenever the user changes the text.
        /// </summary>
        public event EventHandler<EventArgs> Changed;

        protected virtual void OnChanged(EventArgs e)
        {
            Changed?.Invoke(this, e);
        }

        public string Text
        {
            get
            {
                return uiEditableComboboxText(Substrate);
            }
            set
            {
                uiEditableComboboxSetText(Substrate, value);
            }
        }

        public void Append(string item)
        {
            uiEditableComboboxAppend(Substrate, item);
        }
    }
}
