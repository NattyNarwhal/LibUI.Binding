using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI.Demo
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.Init();
            Window w = new Window("Hello world", 300, 300, false);

            Button b = new Button("I don't do anything");

            VBox x = new VBox();

#if DEBUG
            Console.WriteLine(w.Handle);
            Console.WriteLine(b.Handle);
            Console.WriteLine(x.Handle);
#endif

            w.Child = x;
            x.Append(b);
            w.Visible = true;
            Application.Main();
            Application.Finish();
        }
    }
}
