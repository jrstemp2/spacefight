using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceFight
{
    public partial class Form1 : Form
    {
        bool canFire;
        PictureBox[] stars;
        int backgroundSpeed;
        int playerSpeed;
        Random rand;

        //lasers
        PictureBox[] lasers;
        int laserSpeed;

        //enemies
        PictureBox[] enemies;
        int enemySpeed;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundSpeed = 4;
            playerSpeed = 4;
            enemySpeed = 4;
            stars = new PictureBox[30];
            rand = new Random();

            laserSpeed = 4;
            lasers = new PictureBox[1];
            
            canFire = true;

            

            Image laser = Image.FromFile(@"assets\laser.png");
            Image enemy1 = Image.FromFile(@"assets\en1.png");
            Image enemy2 = Image.FromFile(@"assets\en2.png");
            Image enemy3 = Image.FromFile(@"assets\en3.png");

            enemies = new PictureBox[10];

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new PictureBox();
                enemies[i].Size = new Size(40, 40);
                enemies[i].SizeMode = PictureBoxSizeMode.Zoom;
                enemies[i].BorderStyle = BorderStyle.None;
                enemies[i].Visible = false;
                this.Controls.Add(enemies[i]);
                enemies[i].Location = new Point((i + 1) * 50, -50);

            }

            enemies[0].Image = enemy1;
            enemies[1].Image = enemy1;
            enemies[2].Image = enemy2;
            enemies[3].Image = enemy3;
            enemies[4].Image = enemy1;
            enemies[5].Image = enemy3;
            enemies[6].Image = enemy2;
            enemies[7].Image = enemy1;
            enemies[8].Image = enemy2;
            enemies[9].Image = enemy3;



            //Create Laser
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i] = new PictureBox();
                lasers[i].Size = new Size(5, 10);
                lasers[i].Image = laser;
                lasers[i].SizeMode = PictureBoxSizeMode.Zoom;
                lasers[i].BorderStyle = BorderStyle.None;
                this.Controls.Add(lasers[i]);
            }


                for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new PictureBox();
                stars[i].BorderStyle = BorderStyle.None;
                stars[i].Location = new Point(rand.Next(20, 580), rand.Next(-10, 400));
                if (i % 2 == 1)
                {
                    stars[i].Size = new Size(2, 2);
                    stars[i].BackColor = Color.Red;
                }
                else if (i % 2 == 0)
                {
                    stars[i].Size = new Size(3, 3);
                    stars[i].BackColor = Color.Blue;
                }
                this.Controls.Add(stars[i]);
            }
        }

        private void MoveStars_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].Top += backgroundSpeed;

                if(stars[i].Top >= this.Height)
                {
                    stars[i].Top = -stars[i].Height;
                }
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void DownMove_Tick(object sender, EventArgs e)
        {
            if (player.Top < 400)
            {
                player.Top += playerSpeed;
            }
        }

        private void UpMove_Tick(object sender, EventArgs e)
        {
            if (player.Top > 10)
            {
                player.Top -= playerSpeed;
            }
        }

        private void RightMove_Tick(object sender, EventArgs e)
        {
            if (player.Right < 580)
            {
                player.Left += playerSpeed;
            }
        }

        private void LeftMove_Tick(object sender, EventArgs e)
        {
            if (player.Left > 10)
            {
                player.Left -= playerSpeed;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                RightMove.Start();
            }
            if (e.KeyCode == Keys.Left)
            {
                LeftMove.Start();
            }
            if (e.KeyCode == Keys.Up)
            {
                UpMove.Start();
            }
            if (e.KeyCode == Keys.Down)
            {
                DownMove.Start();
            }
            if (e.KeyCode == Keys.Space)
            {
                
                
                if (canFire == true)
                {
                    lasers[0].Location = new Point(player.Location.X + 20, player.Location.Y - 0);
                    MoveLaser.Start();
                }
                
                
                
            }
        }
         
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            RightMove.Stop();
            LeftMove.Stop();
            UpMove.Stop();
            DownMove.Stop();
           // canFire = false;
        }

        private void MoveLaser_Tick(object sender, EventArgs e)
        {
            

            

            for (int i = 0; i < lasers.Length; i++)
            {
                if (lasers[i].Top > 0)
                {
                    canFire = false;
                    lasers[i].Visible = true;
                    lasers[i].Top -= laserSpeed;
         
                }
                else
                {
                    canFire = true;
                    lasers[i].Visible = false;
                    MoveLaser.Stop();
                }
                
            }
            

            
        }

        private void EnemyMove_Tick(object sender, EventArgs e)
        {
            MoveEnemies(enemies, enemySpeed);
        }
        private void MoveEnemies(PictureBox[] array, int speed)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i].Visible = true;
                array[i].Top += speed;

                if (array[i].Top > this.Height)
                {
                    array[i].Location = new Point((i + 1) * 50, -200);
                }
            }
        }
    }
}
