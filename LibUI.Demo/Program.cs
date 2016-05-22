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
        static Slider s;
        static Spinbox n;
        static ProgressBar p;

        static long val = 0;

        static void ApplyValues(long v)
        {
            val = v;
            p.Value = val;
            s.Value = val;
            n.Value = val;
        }

        [STAThread]
        static void Main(string[] args)
        {
            Application.Init();

            Window w = new Window("Hello world", 300, 300, false);
            Button b = new Button("Increment me");
            s = new Slider(0, 100);
            n = new Spinbox(0, 100);
            p = new ProgressBar();
            Checkbox c = new Checkbox("Error");
            VBox x = new VBox();

            b.Clicked += (o, e) =>
            {
                val++;
                ApplyValues(val);
            };
            EventHandler<EventArgs> handle = (o, e) =>
            {
                var v = ((INumInput)o).Value;
                ApplyValues(v);
            };
            n.Changed += handle;
            s.Changed += handle;
            w.Closing += (o, e) =>
            {
                w.MessageBox("Bye!", "See you later", c.Checked);
                w.Destroy();
                Application.Quit();
            };

            w.Margins = 5;
            w.Child = x;

            x.Padding = 5;
            x.Append(b);
            x.Append(s);
            x.Append(n);
            x.Append(p);
            x.Append(c);
            w.Visible = true;

            Application.Main();
            Application.Finish();
        }
    }
}
