/*
 *Zachary Job
 *gui.cs
 *2/1/17
 *
 *A simple gui handler for the application

N A I V E

 */

using System;
using System.Windows.Forms;

namespace fiyield
{
	internal class draw : Form
	{
        private WindowsFormsApplication1.Form1
            draw_form;

		internal draw()
		{
            draw_form = new WindowsFormsApplication1.Form1();
            draw_form.ShowDialog();

            Console.WriteLine("\n\nEXIT\n");
		}
	}
}
