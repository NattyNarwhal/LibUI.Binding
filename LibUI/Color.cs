using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    /// <summary>
    /// Represents an RGB color.
    /// </summary>
    public struct Color
    {
        /// <summary>
        /// The red channel, from 0.0-1.0
        /// </summary>
        public double R;
        /// <summary>
        /// The green channel, from 0.0-1.0
        /// </summary>
        public double G;
        /// <summary>
        /// The blue channel, from 0.0-1.0
        /// </summary>
        public double B;
        /// <summary>
        /// The alpha channel, from 0.0-1.0
        /// </summary>
        public double A;

        public Color(double r, double g, double b, double a = 1.0d)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }

    /// <summary>
    /// A button that contains a color and shows a dialog to change it.
    /// </summary>
    public class ColorButton : Control
    {
        #region Interop
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiColorButtonColor(IntPtr control,
            ref double r, ref double g, ref double b, ref double a);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiColorButtonSetColor(IntPtr control,
            ref double r, ref double g, ref double b, ref double a);
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewColorButton();
        [DllImport("libui", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiColorButtonOnChanged(IntPtr b, uiColorButtonOnChangedDelegate f, IntPtr data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void uiColorButtonOnChangedDelegate(IntPtr b, IntPtr data);
        #endregion

        /// <summary>
        /// Creates a new color button.
        /// </summary>
        public ColorButton()
        {
            Substrate = uiNewColorButton();
            uiColorButtonOnChanged(Substrate, (b, f) =>
                { OnChanged(new EventArgs()); }, IntPtr.Zero);
        }

        /// <summary>
        /// This event is fired whenever the user changes the color.
        /// </summary>
        public event EventHandler<EventArgs> Changed;

        protected virtual void OnChanged(EventArgs e)
        {
            Changed?.Invoke(this, e);
        }

        public Color Color
        {
            get
            {
                double r = 0;
                double g = 0;
                double b = 0;
                double a = 0;
                uiColorButtonColor(Substrate, ref r, ref g, ref b, ref a);
                return new Color(r, g, b, a);
            }
            set
            {
                uiColorButtonSetColor(Substrate, ref value.R, ref value.G, ref value.B, ref value.A);
            }
        }
    }
}
