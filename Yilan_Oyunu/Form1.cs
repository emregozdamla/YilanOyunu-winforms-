using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Yilan_Oyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        List<Label> snakeParts = new List<Label>();
        public static Label food;
        public static Label label1;
        public static Label label4;
        public string direction = "";
        public int snakeHeight = 1;
        public int point = 0;
        public int unitSize = 20;
        public bool gameOver=false;
        public void AddPauseText()
        {
            label4 = new Label();
            label4.Font = new Font("Verdana", 22);
            label4.BackColor = Color.Empty;
            label4.Visible = false;
            label4.ForeColor = Color.Blue;
            label4.Text = "Oyun Donduruldu";
            label4.Location = new Point(170, 270);
            label4.Size = new Size(300, 200);
            pictureBox1.Controls.Add(label4);

            
        }
        public void RemovePauseText()
        {
            pictureBox1.Controls.Remove(label4);
        }
        public void CheckCollision()
        {
            int i = 3;
            while(i<snakeParts.Count)
            { 
                if (snakeParts[0].Location.X == snakeParts[i].Location.X
                    && snakeParts[0].Location.Y == snakeParts[i].Location.Y )
                {
                    gameOver = true;
                }
                i++;
            }
        }
        public void EatFoodControl()
        {
            int i = 0;
            while(i<snakeParts.Count)
            {
                if (snakeParts[i].Location.X<food.Location.X+unitSize
                    && snakeParts[i].Location.X > food.Location.X - unitSize
                    && snakeParts[i].Location.Y > food.Location.Y - unitSize
                    && snakeParts[i].Location.Y < food.Location.Y + unitSize)
                {
                    pictureBox1.Controls.Remove(food);
                    snakeHeight++;
                    point++;
                    PutFood();
                    if(timer1.Interval>=20)
                    {
                        timer1.Interval -= 10;
                    }
                }
                i++;
            }

        }
        public void FormSnakeParts()
        {
            
            int i = snakeParts.Count;
            while(i<=snakeHeight)
            {
                Label part = new Label();
                part.Size = new Size(unitSize, unitSize);
                part.BackColor = Color.Black;
                snakeParts.Add(part);
                i++;
            }
            i = snakeParts.Count - 1;
            while(i>0)
            {
                snakeParts[i].Location = snakeParts[i - 1].Location;
                i--;
            }
            i = 1;
            while (i <= snakeHeight)
            {

                pictureBox1.Controls.Add(snakeParts[i]);
                i++;
            }

        }
        public void PutFood()
        {
            food = new Label();
            food.Size = new Size(unitSize, unitSize);
            food.BackColor = Color.Red;
            Random random = new Random();
            int foodX = random.Next(0, 29) * unitSize;
            int foodY= random.Next(0, 29) * unitSize;
            int i = 0;
            while(i<snakeParts.Count)
            {
                if (snakeParts[i].Location.X==foodX && snakeParts[i].Location.Y==foodY)
                {
                    PutFood();
                }
                i++;
            }
            food.Location = new Point(foodX, foodY);
            pictureBox1.Controls.Add(food);
        }
        public void FormHead()
        {
            label1 = new Label();
            label1.BackColor = Color.Black;
            label1.Location = new Point(280, 280);
            label1.Size = new Size(unitSize, unitSize);
            pictureBox1.Controls.Add(label1);
            snakeParts.Add(label1);
        }
        public void PassWalls()
        {
            if (label1.Location.Y<0) label1.Location = new Point(label1.Location.X,600-unitSize);
            if (label1.Location.Y > 600) label1.Location = new Point(label1.Location.X, 0);
            if (label1.Location.X < 0) label1.Location = new Point(600-unitSize, label1.Location.Y);
            if (label1.Location.X > 600) label1.Location = new Point(0, label1.Location.Y);
        }
        public void DetermineHeadDirection(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W && direction != "Down")
            {
                direction = "Up";
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.S && direction != "Up")
            {
                direction = "Down";
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.A && direction != "Right")
            {
                direction = "Left";
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.D && direction != "Left")
            {
                direction = "Right";
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.W )
            {
                
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.S )
            {
                
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.A )
            {
                
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.D )
            {
                
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        public void MoveHead()
        {
            //if (direction == "Up") label1.Top -= unitSize;
            //if (direction == "Down") label1.Top += unitSize;
            //if (direction == "Right") label1.Left += unitSize;
            //if (direction == "Left") label1.Left -= unitSize;
            if (direction == "Up") label1.Location = new Point(label1.Location.X, label1.Location.Y - unitSize);
            if (direction == "Down") label1.Location = new Point(label1.Location.X, label1.Location.Y + unitSize);
            if (direction == "Right") label1.Location = new Point(label1.Location.X + unitSize, label1.Location.Y);
            if (direction == "Left") label1.Location = new Point(label1.Location.X - unitSize, label1.Location.Y);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveHead();
            PassWalls();
            EatFoodControl();
            FormSnakeParts();
            CheckCollision();
            //AddPauseText();
            label2.Text = "Puanınız: " + point;
            if(gameOver==true)
            {
                timer1.Stop();
                timer1.Interval = 200;
                
                snakeHeight = 1;
                snakeParts.Clear();
                pictureBox1.Controls.Clear();
                pictureBox1.Visible = false;
                label2.Visible = false;
                button3.Visible = true;
                button3.Enabled = true;
                button4.Visible = true;
                button4.Enabled = true;
                label3.Visible = true;
                label3.Text = "Puanınız: " + point;
                label3.TextAlign = ContentAlignment.MiddleCenter;
                
                direction = "";
                

            }

        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            DetermineHeadDirection(e);
            if (e.KeyCode == Keys.Space)
            {
                if (timer1.Enabled && gameOver == false)
                {
                    timer1.Stop();
                    label4.Visible = true;
                    e.Handled = true;
                    e.SuppressKeyPress = true;

                }
                else if (!timer1.Enabled && gameOver == false)
                {
                    timer1.Start();
                    label4.Visible = false;
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            label2.Visible = true;

            button1.Visible = false;
            button2.Visible = false;
            button1.Enabled = false;
            button2.Enabled = false;
            direction = "";
            FormHead();
            PutFood();
            AddPauseText();
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            label2.Visible = true;
            gameOver = false;
            label3.Visible = false;
            button3.Visible = false;
            button3.Enabled = false;
            button4.Visible = false;
            button4.Enabled = false;
            point = 0;
            direction = "";
            FormHead();
            PutFood();
            AddPauseText();
            timer1.Start();
        }
    }
}
