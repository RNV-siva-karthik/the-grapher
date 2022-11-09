using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
namespace the_grapher
{
    public partial class Form1 : Form
    {
        double x1, x2;
        int a, b, c;
        float Xdiv, Ydiv;
        float x;
        float y;
        Pen p = new Pen(color: Color.Black, 3);
        PointF[] poi = new PointF[1000];
        PointF[] poi1 = new PointF[1000];
        bool tirger = false;
        float scale1, scale2;
        int[] poiX = new int[1000];
        float[] poiY = new float[1000];
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            Xdiv = panel1.Width / 20;
            Ydiv = panel1.Height / 20;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tirger = true;
            panel1.Invalidate();
        }

        private void DrawLinePoints(object sender, PaintEventArgs e)
        {
            scale1 = panel1.Height / 2;
            scale2 = panel1.Width / 2;
            Pen p = new Pen(color: Color.Black, 3);
            Pen p1 = new Pen(color: Color.Black, 2);
            Pen p2 = new Pen(color: Color.Red, 1);
            PointF[] poi0 = new PointF[1000];
            int i = 0;
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                poi0 = sinecurve();
            }
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                poi0 = coscurve();
            }
            if(checkBox3.Checked)
            {
                poi0 = quadratic();
            }
            e.Graphics.Clear(Color.White);
            e.Graphics.DrawLine(p1, 0, scale1, 1000, scale1);
            e.Graphics.DrawLine(p1, scale2, 0, scale2, 1000);
            int q = 0;
            //if(x1<-5||x2>5)
            {
                //Xdiv = Xdiv / 2;
               // Ydiv=Ydiv / 2;
                //panel1.Size = new Size(560, 419);
            }
            for (int b = 0; b <= panel1.Width; b = b + (int)Xdiv)
                {
                    if (b == panel1.Width / 2)
                    {
                        continue;
                    }
                    else
                    {
                        e.Graphics.DrawLine(p2, b, 0, b, 1000);
                        q++;
                    }
                }
                q = 0;
                for (int b = 0; b <= panel1.Height; b = b + (int)Ydiv)
                {
                    if (b == panel1.Height / 2)
                    {
                        continue;
                    }
                    else
                    {
                    e.Graphics.DrawLine(p2, 0, b, 1000, b);
                        q++;
                    }
                }
            if (tirger == true)
            {
                e.Graphics.DrawCurve(p, poi0);
            }
        }
        public PointF[] sinecurve()
        {
            label5.Location = new Point(0, panel1.Height / 2);
            int i = 0;
            for (x = 0; x < 1000; x = x + 1)
            {
                y = (float)Math.Sin(Math.PI * x / 180);
                poi1[i] = new PointF(x, (-y * scale1) + scale1);
                i++;
            }
            return poi1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public PointF[] coscurve()
        {
            label5.Location = new Point(0, panel1.Height / 2);
            int i = 0;
            for (x = 0; x < 1000; x = x + 1)
            {
                y = (float)Math.Cos(Math.PI * x / 180);
                poi1[i] = new PointF(x, (-y * scale1) + scale1);
                i++;
            }
            return poi1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox3.Checked = false;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Start();
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox3.Checked = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox2.Visible = false;
                checkBox1.Visible = false;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                checkBox2.Checked = false;
                checkBox1.Checked = false;
                label4.Visible = true;
            }
            if(checkBox3.Checked==false)
            {
                checkBox2.Visible = true;
                checkBox1.Visible = true;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                label4.Visible = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Location = Cursor.Position;
        }

        public PointF[] quadratic()
        {
            double deno, disc;
            int i = 0;
            bool img = false;
            try
            { 
                if(textBox1.Text!=String.Empty&& textBox2.Text != String.Empty&& textBox3.Text != String.Empty)
                {
                    a = Convert.ToInt32(textBox1.Text);
                    b = Convert.ToInt32(textBox2.Text);
                    c = Convert.ToInt32(textBox3.Text);
                }
                if (textBox1.Text == String.Empty && textBox2.Text == String.Empty && textBox3.Text == String.Empty)
                {
                    MessageBox.Show("please enter a value!!", "No values given");
                }
            }
           catch
            {
                MessageBox.Show("please do not use tab here", "tab error");
            }
            if (a == 0&&b!=0)
            {
                x1 = x2 =-c / b;
            }
            else if(a==0&&b==0)
            {
                x1 = x2 = c;
            }
            else
            {
                disc = (b * b) - (4 * a * c);
                deno = 2 * a;
                if (disc > 0)
                {
                    x1 = (-b / deno) - (Math.Sqrt(disc) / deno);
                    x2 = (-b / deno) + (Math.Sqrt(disc) / deno);
                }
                else if (disc == 0)
                {
                    x1= x2 = -b / deno;
                }
                else
                {
                    img = true;
                    x1= -b / deno;
                    x2 = ((Math.Sqrt((4 * a * c) - (b * b))) / deno);
                }
            }
            i = 0;
            x = 0;
            int h = 0;
            PointF[] poi2 = new PointF[Convert.ToInt32(x2-x1+20)];
            for (int u = (int)(x1 - 5); u < x2+5; u++)
            {
                y = a *u *u + b * u + c;
                poi2[i]=new PointF((u*Xdiv)+scale2, (-y*Ydiv)+scale1);
                if(y==0)
                {
                    poiY[h] = poi2[i].X;
                   h++;
                }
                i++;
            }
            label7.Text = "x1 = " + x1;
            label8.Text = "x2 = " + x2;
            if (img != true)
            {
                label7.Text = "x1 = " + x1;
                label8.Text = "x2 = " + x2;
                label9.Location = new Point((int)(poiY[0]) - 45, (int)scale1);
                label9.Text = "(" + x1 + ",0)";
                label10.Location = new Point((int)(poiY[1]), (int)scale1);
                label10.Text = "(" + x2 + ",0)";
            }
            else
            {
                label7.Text = "x1 = (" + x1 + "+" + x2 + "i" + ",0)";
                label8.Text = "x2 = (" + x1 + "-" + x2 + "i" + ",0)";
                label9.Location = new Point((int)(poiY[0]) - 45, (int)scale1);
                label9.Text = "(" +x1+"+"+ x2+"i" + ",0)";
                label10.Location = new Point((int)(poiY[1]), (int)scale1);
                label10.Text = "(" + x1 + "-" + x2 + "i" + ",0)";
            }
            return poi2;
        }
    }
}
