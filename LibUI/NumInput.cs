using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    /// <summary>
    /// Represents numeric inputs like spinboxes and sliders.
    /// </summary>
    /// <remarks>
    /// Values out of range will be clamped to the minimum or maximium, as
    /// specified by the constructor.
    /// </remarks>
    public interface INumInput
    {
        long Value { get; set; }
        // ifaces can't define ctors
        //INumInput(long min, long max);
    }

    /// <summary>
    /// A draggable slider that corresponds to a number.
    /// </summary>
    public class Slider : Control, INumInput
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern long uiSliderValue(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiSliderSetValue(IntPtr control, long value);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewSlider(long min, long max);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiSliderOnChanged(IntPtr c, uiEntryOnChangedDelegate f, IntPtr data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void uiEntryOnChangedDelegate(IntPtr c, IntPtr data);
        #endregion

        /// <summary>
        /// Creates a slider.
        /// </summary>
        /// <param name="min">The minimum value that can be entered.</param>
        /// <param name="min">The maximium value that can be entered.</param>
        public Slider(long min, long max)
        {
            Substrate = uiNewSlider(min, max);

            uiSliderOnChanged(Substrate, (b, f) =>
            { OnChanged(new EventArgs()); }, IntPtr.Zero);
        }

        /// <summary>
        /// Fired when the number in the entry is updated.
        /// </summary>
        public event EventHandler<EventArgs> Changed;

        protected virtual void OnChanged(EventArgs e)
        {
            Changed?.Invoke(this, e);
        }

        public long Value
        {
            get
            {
                return uiSliderValue(Substrate);
            }
            set
            {
                uiSliderSetValue(Substrate, value);
            }
        }
    }
    
    /// <summary>
    /// A box where numbers can be entered, incremented, and decremented.
    /// </summary>
    public class Spinbox : Control, INumInput
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern long uiSpinboxValue(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiSpinboxSetValue(IntPtr control, long value);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewSpinbox(long min, long max);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiSpinboxOnChanged(IntPtr c, uiEntryOnChangedDelegate f, IntPtr data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void uiEntryOnChangedDelegate(IntPtr c, IntPtr data);
        #endregion

        /// <summary>
        /// Creates a spinbox entry.
        /// </summary>
        /// <param name="min">The minimum value that can be entered.</param>
        /// <param name="min">The maximium value that can be entered.</param>
        public Spinbox(long min, long max)
        {
            Substrate = uiNewSpinbox(min, max);

            uiSpinboxOnChanged(Substrate, (b, f) =>
                { OnChanged(new EventArgs()); }, IntPtr.Zero);
        }

        /// <summary>
        /// Fired when the number in the entry is updated.
        /// </summary>
        public event EventHandler<EventArgs> Changed;

        protected virtual void OnChanged(EventArgs e)
        {
            Changed?.Invoke(this, e);
        }

        public long Value
        {
            get
            {
                return uiSpinboxValue(Substrate);
            }
            set
            {
                uiSpinboxSetValue(Substrate, value);
            }
        }
    }
}
