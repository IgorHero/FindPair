using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FindPair
{
    public partial class Form1 : Form
    {
        Image[] imag = new Image[10];
        Bitmap PictureA = new Bitmap(Properties.Resources.А);
        Bitmap[] image1 = new Bitmap[10];
        int step = 0;
        int[,] Mas = new int[4, 5];
        int razm = 0, CurI, PrevI;
        public Form1()
        {
            InitializeComponent();
        }

        public void SwitchBlockControl()
        {
            groupBox1.Enabled = !groupBox1.Enabled;
            NewGameButton.Enabled = !NewGameButton.Enabled;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 5; j++)
                    Mas[i, j] = -1;
            SwitchBlockControl();
            if (radioButton1.Checked)
                razm = 3;
            else if (radioButton2.Checked)
                razm = 4;
            else
                razm = 5;
            Draw();
        }

        public void Draw()
        {
            int x = 0, y = 0;
            Random rnd1 = new Random();
            label1.Text += "x y\n";
            for (int i = 0; i < razm * 2; i++)
            {
                do
                {
                    x = rnd1.Next(0, 4);
                    y = rnd1.Next(0, razm);
                } while (Mas[x, y] != -1);
                Mas[x, y] = i;
                // Graph.DrawImage(image1[i], x * 90, y * 90);
                do
                {
                    x = rnd1.Next(0, 4);
                    y = rnd1.Next(0, razm);
                } while (Mas[x, y] != -1);
                Mas[x, y] = i;
                // Graph.DrawImage(image1[i], x * 90, y * 90);


            }
            for (int i = 0; i < razm; i++)
            {
                for (int j = 0; j < 4; j++)
                    label2.Text += Mas[j, i] + " ";
                label2.Text += "\n";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            image1[0] = Properties.Resources.А;
            image1[1] = Properties.Resources.Б;
            image1[2] = Properties.Resources.В;
            image1[3] = Properties.Resources.Г;
            image1[4] = Properties.Resources.Д;
            image1[5] = Properties.Resources.Е;
            image1[6] = Properties.Resources.Ж;
            image1[7] = Properties.Resources.З;
            image1[8] = Properties.Resources.И;
            image1[9] = Properties.Resources.К;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / 90;
            int y = e.Y / 90;
            CurI = Mas[x, y];
            Graphics Graph = pictureBox1.CreateGraphics();

            if (step == 0)
            {
                Graph.DrawImage(image1[CurI], x * 90, y * 90);
                step++;
                PrevI = CurI;
            }
            else if (step == 1)
            {
                Graph.DrawImage(image1[CurI], x * 90, y * 90);
            }
            else if (step == 2)
            {
                Graph.DrawImage(image1[CurI], x * 90, y * 90);
                if (CurI != PrevI)
                {
                    Graph.DrawImage(image1[CurI], x * 90, y * 90);
                    Graph.DrawImage(image1[PrevI], x * 90, y * 90);
                }
            }
            else


                label1.Text = e.X.ToString() + " " + e.Y.ToString() + "\n" + x + "  " + y + "\n" + Mas[x, y];
        }
    }
}
