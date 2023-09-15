using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FinalProject
{
    public partial class FormIndex : Form
    {
        public static int redWin, blackWin;
        public static bool newGame, end, back;
        public FormIndex()
        {
            InitializeComponent();

            redWin = 0;
            blackWin = 0;

            newGame = false;
            end = false;
            back = false;

            label1.Text = blackWin.ToString("00");
            label2.Text = redWin.ToString("00");

            
        }

        private void 建立新遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定建立新遊戲？", "警告",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private void 結束遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定結束遊戲？", "警告",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void 查看排行榜ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormScore formScore = new FormScore();
            formScore.ShowDialog();
        }

        private void 紀錄棋局結果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes== MessageBox.Show("登記排行榜之後將會自動關閉本戰局，\n是否同意？","警告",MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                FormRecord formRecord = new FormRecord();
                if(DialogResult.OK== formRecord.ShowDialog())
                    Application.Restart();
            }
            
        }

        private void funcForm1()    //開始遊戲
        {
            FormMain f;
            do
            {
                if (redWin > 100 || blackWin > 100)     //棋局上限
                {
                    MessageBox.Show("該休戰囉~", "棋局結束", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    break;
                }
                this.Visible = false;         //隱藏首頁視窗
                f = new FormMain();     //開啟遊戲視窗
                try
                {
                    f.ShowDialog();
                    f.Close();
                }
                catch (Exception ex) { }

                //遊戲結束，更新記分板顯示
                label1.Text = blackWin.ToString("00");
                label2.Text = redWin.ToString("00");
            } while (f.DialogResult == DialogResult.Yes);
            this.Visible = true;    //恢復顯示首頁視窗

            //按下MenuStrip的結束or新遊戲
            if (end==true)
                Application.Exit();
            if (newGame == true)
            {
                Application.Restart();
            }
            back=false;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            funcForm1();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            funcForm1();
        }
    }
}
