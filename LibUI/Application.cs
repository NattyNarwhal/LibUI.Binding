using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    /// <summary>
    /// Functions to initialize the library and control the event flow of the application.
    /// </summary>
    public static class Application
    {
        #region Interop
        [StructLayout(LayoutKind.Sequential)]
        struct uiInitOptions
        {
            public UIntPtr Size;
        }

        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern string uiInit(ref uiInitOptions options);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void uiUninit();
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void uiFreeInitError(string err);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void uiMain();
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern bool uiMainStep(bool wait);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void uiQuit();
        #endregion

        /// <summary>
        /// Initializes LibUI.
        /// </summary>
        /// <exception cref="ExternalException">
        /// Thrown if LibUI encounters an error.
        /// </exception>
        public static void Init()
        {
            uiInitOptions o = new uiInitOptions();
            o.Size = UIntPtr.Zero;
            Init(o);
        }

        static void Init(uiInitOptions o)
        {
            var err = uiInit(ref o);
            if (!String.IsNullOrEmpty(err))
            {
                uiFreeInitError(err);
                throw new ExternalException(err);
            }
        }

        /// <summary>
        /// Perform cleanup tasks after the main event loop has finished.
        /// </summary>
        public static void Finish()
        {
            uiUninit();
        }

        /// <summary>
        /// Starts the LibUI main event loop.
        /// </summary>
        public static void Main()
        {
            uiMain();
        }
        
        /// <summary>
        /// Manually steps through an event loop to process UI messages.
        /// </summary>
        /// <param name="wait">
        /// If true, block until a message is recieved. If false and a
        /// message is available If false, and a message is available,
        /// process it immediately (returning either true or false
        /// depending on the message), otherwise return true.
        /// </param>
        /// <returns>
        /// Returns true if a message was processed, false if the main
        /// loop has quit.
        /// </returns>
        public static bool Step(bool wait)
        {
            return uiMainStep(wait);
        }

        /// <summary>
        /// Quits the application and thus, the event loop.
        /// </summary>
        public static void Quit()
        {
            uiQuit();
        }
    }
}
