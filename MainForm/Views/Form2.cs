using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm.Views
{
    public partial class Form2 : Form
    {
        double x;
        double y;
        string toret;
        public Form2(string s)
        {
            InitializeComponent();
            string[] arr = s.Split(' ');
            string[] arr1 = arr[0].Split('.');
            y = Convert.ToDouble(arr1[0]);
            y+= Convert.ToDouble(arr1[1]) * 0.0166;
            y = (y / 18) * 43;
            if (arr[1][0] == 'с') y = -y;
            arr1 = arr[2].Split('.');

            x = Convert.ToDouble(arr1[0]);
            x+=Convert.ToDouble(arr1[1]) * 0.0166;
            x= (x / 180) * 395;
            if (arr[3][0] == 'з') x = -x;
            x += 388;
            y += 212;
            ShowPoint();
        }
        private void ShowPoint()
        {
            label1.Visible = true;
            label1.Location = new System.Drawing.Point((int)x,(int)y);
        }
        public Form2()
        {
            InitializeComponent();
            
        }

        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            string[] arr = new string[4];
            x = e.X;
            y = e.Y;
            x -= 388;
            y -= 212;
            if (y < 0) arr[1] = " с.ш. ";
            else arr[1] = " ю.ш. ";
            if (x < 0) arr[3] = " з.д.";
            else arr[3] = " в.д.";
            x = Math.Abs(x);
            y = Math.Abs(y);
            y = (y / 43) * 18;
            x = (x / 395) * 180;
            double rpx = x % 1;
            double rpy = y % 1;
            y -= rpy;
            x -= rpx;
            rpx = Math.Round(rpx / 0.0166);
            rpy = Math.Round(rpy / 0.0166);
            //y = y + rpy;
            //x = x + rpx;
            y-= (arr[1] == " с.ш. ") ? -4 : 4;
            x-=(arr[3]==" з.д.") ? -4:4;
            toret = y+"."+rpy + arr[1] + x+"."+rpx + arr[3];
        }
        public string getpos()
        {
            return toret;
        }
    }
}
