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

            int counter = 0;

            Window w = new Window("Hello world", 300, 300, false);
            Button b = new Button("Increment me");
            VBox x = new VBox();

            b.Clicked += (o, e) =>
            {
                counter++;
                w.Title = counter.ToString();
            };

            w.Child = x;
            x.Append(b);
            w.Visible = true;

            Application.Main();
            Application.Finish();
        }
    }
}
