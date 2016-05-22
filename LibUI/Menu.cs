using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    /// <summary>
    /// A top-level menu item.
    /// </summary>
    public class Menu
    {
        #region Interop
        /// <summary>
        /// The underlying native structure representing the menu.
        /// </summary>
        protected IntPtr Substrate;
        string title;

        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiMenuAppendItem(IntPtr menu, string item);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiMenuAppendCheckItem(IntPtr menu, string item);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiMenuAppendQuitItem(IntPtr menu);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiMenuAppendPreferencesItem(IntPtr menu);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiMenuAppendAboutItem(IntPtr menu);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiMenuAppendSeparator(IntPtr menu);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewMenu(string menu);
        #endregion

        public Menu(string menu)
        {
            title = menu;
            Substrate = uiNewMenu(menu);
        }

        public string Title
        {
            get
            {
                return title;
            }
        }

        public MenuItem AppendItem(string item)
        {
            return new MenuItem(uiMenuAppendItem(Substrate, item), false);
        }

        public CheckableMenuItem AppendCheckableItem(string item, bool initState = false)
        {
            var i = new CheckableMenuItem(uiMenuAppendCheckItem(Substrate, item));
            i.Checked = initState;
            return i;
        }

        public MenuItem AppendQuitItem()
        {
            return new MenuItem(uiMenuAppendQuitItem(Substrate), true);
        }

        public MenuItem AppendPreferencesItem()
        {
            return new MenuItem(uiMenuAppendPreferencesItem(Substrate), true);
        }

        public MenuItem AppendAboutItem()
        {
            return new MenuItem(uiMenuAppendAboutItem(Substrate), true);
        }

        public void AppendSeparator()
        {
            uiMenuAppendSeparator(Substrate);
        }
    }

    /// <summary>
    /// A menu item.
    /// </summary>
    public class MenuItem
    {
        #region Interop
        /// <summary>
        /// The underlying native structure representing the menu.
        /// </summary>
        protected IntPtr Substrate;

        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiMenuItemEnable(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiMenuItemDisable(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiMenuItemOnClicked(IntPtr b, uiMenuItemOnClickedDelegate f, IntPtr data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void uiMenuItemOnClickedDelegate(IntPtr menu, IntPtr window, IntPtr data);
        #endregion

        protected internal MenuItem(IntPtr substrate, bool special)
        {
            Substrate = substrate;
            if (!special)
                uiMenuItemOnClicked(Substrate, (m, w, f) => 
                    { OnClicked(new EventArgs()); }, IntPtr.Zero);
        }

        public event EventHandler<EventArgs> Clicked;

        protected virtual void OnClicked(EventArgs e)
        {
            Clicked?.Invoke(this, e);
        }

        public bool Enabled
        {
            set
            {
                if (value)
                    uiMenuItemEnable(Substrate);
                else
                    uiMenuItemDisable(Substrate);
            }
        }
    }

    /// <summary>
    /// A menu item that can be toggled.
    /// </summary>
    public class CheckableMenuItem : MenuItem
    {
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiMenuItemSetChecked(IntPtr control, bool @checked);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern bool uiMenuItemChecked(IntPtr control);

        internal CheckableMenuItem(IntPtr substrate) : base(substrate, false)
        { }

        public bool Checked
        {
            get
            {
                return uiMenuItemChecked(Substrate);
            }
            set
            {
                uiMenuItemSetChecked(Substrate, value);
            }
        }
    }
}
