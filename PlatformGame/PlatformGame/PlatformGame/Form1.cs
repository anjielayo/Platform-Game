using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlatformGame
{
    public partial class Form1 : Form
    {
        
        bool goleft = false;
        bool goright = false;
        bool jumping = false;

        int jumpSpeed = 10;
        int force = 8;
        int score = 0;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            DrawingControl.SuspendDrawing(this);
            
            scorelabel.Text = score.ToString() + "pts";
            if (e.KeyCode == Keys.Left)
            {
                goleft = true; //When the left button is pressed we can change go left to true.
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true; //When the left button is pressed we can change go right to true.
            }
            if (e.KeyCode == Keys.Space && !jumping)
            {
                jumping = true; //When the space bar is pressed AND the character is not jumping we change the jumping to true.
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            DrawingControl.ResumeDrawing(this);
            scorelabel.Text = score.ToString() + "pts";
            if (e.KeyCode == Keys.Left)
            {
                goleft = false; //When the left button is released we can change go left to false.
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false; //When the left button is released we can change go right to false.
            }
            if (jumping)
            {
                jumping = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawingControl.SuspendDrawing(this);
            player.Top += jumpSpeed; //Continuously drops the player towards the floor. Gives gravity effect.
            if (jumping && force < 0)
            {
                jumping = false; //Checks if if the player is jumping and if the force of the jump is less than 0 if so then we can change jump back to false.
            }
            if (goleft)
            {
                player.Left -= 5; //If go left is true then we can push the character towards left of the screen.
            }
            if (goright)
            {
                player.Left += 5;//If go right is true then we can push the character towards right of the screen.
            }
            if (jumping)
            {
                jumpSpeed = -12;
                force -= 1;//If jumping is true then we change the jump speed integer to minus 12 which means it will thrust the player towards the top and we decrease the force by 1. If we don’t do this then the character can fly over everything we need to give the jump a limit.
            }
            else
            {
                jumpSpeed = 12;//ELSE if the character is not jumping then we can keep the jump speed on 12.
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "platform")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && !jumping)
                    {
                        force = 8;
                        player.Top = x.Top - player.Height;
                    }
                }
                /*In this piece of code we will scan the whole form to find the picture boxes with this once our player interacts it will have to land on top of it.

                For each control x in this.Controls means for each of the windows component we create a variable called X and give it a type of controls.

               IF play bounds intersect with the bounds of the platform and out character is not jumping then we change the force integer back to 8 and player will be above the platform.*/
                if (x is PictureBox && x.Tag == "coin")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && !jumping)
                    {
                        this.Controls.Remove(x);
                        score++;
                    }
                }
            }
            if (player.Bounds.IntersectsWith(door.Bounds))
            {
                timer1.Stop();
                MessageBox.Show("Congratulations! You Win");
                MessageBox.Show(score.ToString() + "pts.");
            }
            DrawingControl.ResumeDrawing(this);
        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {
            scorelabel.Text = score.ToString() + "pts";
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            scorelabel.Text = score.ToString() + "pts";
        }

        private void player_Click(object sender, EventArgs e)
        {
            scorelabel.Text = score.ToString() + "pts";
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            scorelabel.Text = score.ToString() + "pts";
        }

        private void scorelabel_Click(object sender, EventArgs e)
        {
            scorelabel.Text = score.ToString() + "pts";
        }
      
        class DrawingControl //prevents flickering
        {
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

            private const int WM_SETREDRAW = 11;

            public static void SuspendDrawing(Control parent)
            {
                SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
            }

            public static void ResumeDrawing(Control parent)
            {
                SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
                parent.Refresh();
            }
        }

    }

}
    
