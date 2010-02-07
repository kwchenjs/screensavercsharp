using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace FormsAnimation
{
    public partial class FormsAnimation : Form
    {
        Color penColor;

        public FormsAnimation()
        {
            penColor = Color.White;

            InitializeComponent();
        }

        private GraphicsPath GetGraphic()
        {
            GraphicsPath graphic;
            Point[] pt;
            Random random = new Random();
            int intPoints;

            intPoints = random.Next(3,8);
            pt = new Point[intPoints];

            for (int i = 0; i < intPoints; i++)
            {
                pt[i] = new Point(random.Next(SystemInformation.VirtualScreen.Width),
                    random.Next(SystemInformation.VirtualScreen.Height));
            }

            graphic = new GraphicsPath();
            graphic.AddPolygon(pt);

            return graphic;
        }

        private void FormsAnimation_MouseDown(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void FormsAnimation_KeyDown(object sender, KeyEventArgs e)
        {
            Application.Exit();
        }

        private void FormsAnimation_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            FadeAnimation(100, 0, 15, 30);
        }

        private void FormsAnimation_Load(object sender, EventArgs e)
        {
            // Show form
            Cursor.Hide();
            this.Location = new Point(0, 0);
            this.Size = SystemInformation.VirtualScreen.Size;
            FadeAnimation(0, 100, 15, 30);

            // Enable timer to drawing animations
            timer1.Start();
        }

        private void FadeAnimation(int Initial, int Final, int Step, int MillisecondsTime)
        {
            if (Initial > Final)
            {
                for (int i = Initial; i > Final; i -= Step)
                {
                    this.Opacity = (double)i / 100;
                    System.Threading.Thread.Sleep(MillisecondsTime);
                    Application.DoEvents();
                }
            }
            else
            {
                for (int i = Initial; i < Final; i += Step)
                {
                    this.Opacity = (double)i / 100;
                    System.Threading.Thread.Sleep(MillisecondsTime);
                    Application.DoEvents();
                }
            }

            this.Opacity = Final;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random random;
            Graphics graphic;
            GraphicsPath graphicPath;

            // Get new colors
            random = new Random();
            penColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));

            // Draw a new form
            graphic = this.CreateGraphics();
            graphicPath = GetGraphic();

            graphic.DrawPath(new Pen(penColor), graphicPath);
        }
    }
}
