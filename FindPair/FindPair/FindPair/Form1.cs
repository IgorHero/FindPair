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
        Bitmap PictureA = new Bitmap(Properties.Resources.А);
        Bitmap[] image1 = new Bitmap[11];
        int step = 0;
        int[,] Mas = new int[4, 5];
        int razm = 0, CurX = -1, CurY = -1, PrevX = -1, PrevY = -1, Score, turn, TimeScore = 100;
        bool flag;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics Graph = pictureBox1.CreateGraphics();
            turn++;
            if (turn == 5)
            {
                for (int i = 0; i < razm; i++)
                    for (int j = 0; j < 4; j++)
                        Graph.DrawImage(image1[10], j * 90, i * 90);
                pictureBox1.Enabled = true;
                timer1.Enabled = false;
                ScoreTimer.Enabled = true;
                turn = 0;
            }
        }

        public void SwitchBlockControl()
        {
            groupBox1.Enabled = !groupBox1.Enabled;
            NewGameButton.Enabled = !NewGameButton.Enabled;
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {

            timer1.Enabled = true;
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
            TimeScore = 50 * razm;
            Draw();
        }

        public void CheckWin()
        {
            if (Score == razm * 2)
            {
                ScoreTimer.Enabled = false;
                MessageBox.Show("win " + TimeScore);
                pictureBox1.Enabled = false;
                NewGameButton.Enabled = true;
                groupBox1.Enabled = true;
                Score = 0;
                CurX = -1;
                CurY = -1;
                PrevX = -1;
                PrevY = -1;
            }
        }

        public void Draw()
        {
            int x = 0, y = 0;
            Random rnd1 = new Random();
            for (int i = 0; i < razm * 4; i++)
            {
                do
                {
                    x = rnd1.Next(0, 4);
                    y = rnd1.Next(0, razm);
                } while (Mas[x, y] != -1);
                Mas[x, y] = i / 2;

            }
            Graphics Graph = pictureBox1.CreateGraphics();
            for (int i = 0; i < razm; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Graph.DrawImage(image1[Mas[j, i]], j * 90, i * 90);
                }
            }
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
            image1[10] = Properties.Resources.Clear;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics Graph = pictureBox1.CreateGraphics();
            int x = e.X / 90;
            int y = e.Y / 90;
            if ((Mas[x, y] != -1) && ((CurX != x) || (CurY != y)))
            {
                if ((step == 0) && ((PrevX != x) || (PrevY != y)))
                {
                    if (!flag)
                    {
                        Graph.DrawImage(image1[10], CurX * 90, CurY * 90);
                        Graph.DrawImage(image1[10], PrevX * 90, PrevY * 90);
                    }
                    CurX = x;
                    CurY = y;
                    Graph.DrawImage(image1[Mas[CurX, CurY]], CurX * 90, CurY * 90);
                    step = 1;
                }
                else if ((step == 1) && ((CurX != x) || (CurY != y)))
                {
                    PrevX = CurX;
                    PrevY = CurY;
                    CurX = x;
                    CurY = y;
                    Graph.DrawImage(image1[Mas[CurX, CurY]], CurX * 90, CurY * 90);
                    step = 0;
                    if (Mas[CurX, CurY] == Mas[PrevX, PrevY])
                    {
                        Score++;

                        flag = true;
                        Mas[CurX, CurY] = -1;
                        Mas[PrevX, PrevY] = -1;
                        CheckWin();
                    }
                    else
                        flag = false;
                }
            }
        }

        private void ScoreTimer_Tick(object sender, EventArgs e)
        {
            TimeScore--;
        }
    }
}
