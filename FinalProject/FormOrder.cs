using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinalProject;

namespace ClassPrac5_7
{
    public partial class Form3 : Form
    {
        int num1,num2;
        public Form3()
        {
            InitializeComponent();

            //圖片自動調整和圖片控制像一樣大小
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox1.Image = imageList1.Images["Pokerbk.jpg"];
            pictureBox2.Image = imageList1.Images["Pokerbk.jpg"];

            label2.Text = "";
        }

        private void picFunc()
        {
            FormMain.teamNow = -1;

            Random random = new Random();
            num1 = random.Next(2, 15);
            num2 = random.Next(2, 15);
            pictureBox1.Image = imageList1.Images["Poker" + Convert.ToString(num1) + ".jpg"];
            pictureBox2.Image = imageList1.Images["Poker" + Convert.ToString(num2) + ".jpg"];

            if (num1 > num2)
            {
                FormMain.teamNow = 0;
                label2.Text = "黑方贏了！黑方先攻...";
                label2.BackColor = Color.DodgerBlue;
                label2.ForeColor = Color.White;
                Refresh();
                System.Threading.Thread.Sleep(3000);
                //MessageBox.Show("黑方贏了！黑方先攻", "開局順序", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else if (num2 > num1)
            {
                FormMain.teamNow = 1;
                label2.Text = "紅方贏了！紅方先攻...";
                label2.BackColor = Color.Crimson;
                label2.ForeColor = Color.White;
                Refresh();
                System.Threading.Thread.Sleep(3000);
                //MessageBox.Show("紅方贏了！紅方先攻", "開局順序", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                label2.Text = "平手！再比一次...";
                label2.BackColor = Color.Goldenrod;
                label2.ForeColor = Color.White;
                Refresh();
                System.Threading.Thread.Sleep(3000);
                //MessageBox.Show("平手！再比一次...", "開局順序", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pictureBox1.Image = imageList1.Images["Pokerbk.jpg"];
                pictureBox2.Image = imageList1.Images["Pokerbk.jpg"];
                Refresh();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            picFunc();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            picFunc();
        }

        private void 建立新遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定建立新遊戲？", "警告",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                FormIndex.newGame = true;
                Close();
            }
        }
        private void 結束遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定結束遊戲？", "警告",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                FormIndex.end = true;
                Close();
            }
        }
        private void 返回主選單ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定返回主選單？", "警告",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                FormIndex.back = true;
                Close();
            }
        }

    }
}
