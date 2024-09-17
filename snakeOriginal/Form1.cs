using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snakeOriginal
{
    public partial class Form1 : Form
    {
        private int fieldSize = 50;
        private int rTop, rLeft;
        private PictureBox[] snake1 = new PictureBox [200];
        private int score = 0;
        private Keys _lastMove = Keys.None;
      
        private void Create_Boxes()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    PictureBox field = new PictureBox();
                    field.Size = new Size(fieldSize, fieldSize);
                    field.BorderStyle = BorderStyle.FixedSingle;
                    field.Location = new Point(j * fieldSize, fieldSize * i); 
                    this.Controls.Add(field);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            Create_Boxes();
            this.KeyDown += new KeyEventHandler(Move);
            snake1[0] = new PictureBox();
            snake1[0].Size = new Size(fieldSize, fieldSize);
            snake1[0].Location = new Point(300, 300);
            snake1[0].BackColor = Color.DodgerBlue;
            snake1[0].BorderStyle= BorderStyle.FixedSingle;
            this.Controls.Add(snake1[0]);
            snake1[0].BringToFront();
            
        }
        
        private void Move(object sender, KeyEventArgs e)
        {
            for (int i = score; i >= 0; i--)
            {
                if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && timer1.Enabled == true)
                {
                    if (i == 0)
                    {
                        snake1[i].Location = new Point(snake1[i].Location.X + fieldSize, snake1[i].Location.Y);
                    }
                    else
                    {
                        PictureBox[] temp = new PictureBox[1];
                        temp[0] = snake1[i - 1];
                        snake1[i].Location = new Point(temp[0].Location.X, temp[0].Location.Y);
                    }
                    

                }
                if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && timer1.Enabled == true)
                {
                    if (i == 0)
                    {
                        snake1[i].Location = new Point(snake1[i].Location.X - fieldSize, snake1[i].Location.Y);
                    }
                    else
                    {
                        PictureBox[] temp = new PictureBox[1];
                        temp[0] = snake1[i - 1];
                        snake1[i].Location = new Point(temp[0].Location.X, temp[0].Location.Y);
                    }
                    

                }
                if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && timer1.Enabled == true)
                {
                    if (i == 0)
                    {
                        snake1[i].Location = new Point(snake1[i].Location.X, snake1[i].Location.Y - fieldSize);
                    }
                    else
                    {
                        PictureBox[] temp = new PictureBox[1];
                        temp[0] = snake1[i - 1];
                        snake1[i].Location = new Point(temp[0].Location.X, temp[0].Location.Y);
                    }
                    

                }
                if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.S) && timer1.Enabled == true)
                {
                    if (i == 0)
                    {
                        snake1[i].Location = new Point(snake1[i].Location.X, snake1[i].Location.Y + fieldSize);
                    }
                    else
                    {
                        PictureBox[] temp = new PictureBox[1];
                        temp[0] = snake1[i - 1];
                        snake1[i].Location = new Point(temp[0].Location.X, temp[0].Location.Y );
                    }

                }
                
            }
            
        }
        private void AddSnake()
        {
            snake1[score] = new PictureBox();
            snake1[score].BackColor= Color.Blue;
            snake1[score].Size = new Size(fieldSize, fieldSize);
            PictureBox[] temp = new PictureBox[1];
            temp[0] = snake1[score - 1];
            snake1[score].Location = new Point(temp[0].Location.X, temp[0].Location.Y);
            snake1[score].BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(snake1[score]);
            snake1[score].BringToFront();
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void CoinEat()
        {
            Random random = new Random();
            rLeft = random.Next(0, 750);
            int diff1 = rLeft % fieldSize;
            rLeft = rLeft - diff1;
            rTop = random.Next(0, 600);
            int diff2 = rTop % fieldSize;
            rTop = rTop - diff2;
            coin.Location = new Point(rLeft, rTop);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (snake1[0].Top < 0 || snake1[0].Top > 550)
            {
                timer1.Enabled = false;
            }
            if (snake1[0].Left < 0 || snake1[0].Left > 700)
            {
                timer1.Enabled = false;
            }

            if (snake1[0].Bounds.IntersectsWith(coin.Bounds))
            {

                CoinEat();
                for (int i = score; i >= 0; i--)
                {


                    
                    if (snake1[i].Bounds.IntersectsWith(coin.Bounds))
                    {
                        CoinEat();
                        i = score;
                    }
                    
                }
                score++;
                AddSnake();
                scoreLabel.Text = score.ToString();
            }
            if (score == 180)
            {
                timer1.Enabled = false;
            }

            for (int i = score; i >= 2 ; i--)
            {
                if (snake1[0].Bounds.IntersectsWith(snake1[i].Bounds))
                {
                    timer1.Enabled = false;
                }
            }


        }
    }
}