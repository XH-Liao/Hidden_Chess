using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassPrac5_7;

namespace FinalProject
{
    public partial class FormMain : Form
    {
        public static int teamNow;    //現在誰進攻
        struct Chess
        {
            public int level;       //棋子種類，如：帥 士 相 ... 兵
            public int team;    //0->黑；->紅
            public int status;  //0->未翻開；1->已翻開
        }

        Random rd;
        Chess[,] chs;       //棋盤上的棋子
        int black, red;     //雙方剩餘棋子數
        Chess nll;      //死棋 (無棋子)
        int justMove;   //只有移動 (沒翻牌，沒吃棋)

        //當前被選中的棋子
        PictureBox picNow;
        int iNow, jNow;

        public FormMain()
        {
            InitializeComponent();
            
            rd=new Random();
            chs=new Chess[4,8];    //棋盤上的棋子矩陣
            //初始棋子數
            black = 16;
            red = 16;

            //先放入棋子
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    chs[i, j].status = 0;
                    if (i <= 1)
                        chs[i, j].team = 0;     //黑
                    else
                        chs[i, j].team = 1;     //紅

                    if (j == 0)
                        chs[i, j].level = 1;    //馬
                    else if (j == 1)
                        chs[i, j].level = 2;     //車
                    else if (j == 2)
                        chs[i, j].level = 3;     //相
                    else if (j == 3)
                        chs[i, j].level = 4;     //士
                    else if (j == 4)
                        chs[i, j].level = 6;     //炮
                    else if (j == 5 || j == 6)
                        chs[i, j].level = 0;    //兵
                    else if (j == 7)
                    {
                        if (i % 2 != 0)
                            chs[i, j].level = 5;    //帥
                        else
                            chs[i, j].level = 0;    //兵
                    }
                }
            }

            //再洗牌
            for(int i = 0; i < 4; i++)
            {
                for(int j=0;j< 8; j++)
                {
                    int i2 = rd.Next(0, 4);
                    int j2 = rd.Next(0, 8);

                    //交換
                    Chess temp = chs[i, j];
                    chs[i, j] = chs[i2, j2];
                    chs[i2, j2] = temp;
                }
            }

            //正被選取的棋子
            picNow = pictureBox0;
            iNow = -1;
            jNow = -1;

            //死棋 (無棋子)
            nll = new Chess();
            nll.team = -1;
            nll.level = -1;
            nll.status = -1;

            //只移動 (一直只有移動50步和局)
            label2.Text = "";
            justMove = 0;

            //誰先攻
            teamNow = -1;
            while (teamNow!=0 && teamNow != 1)  //若未決定開局先攻的順序
            {
                //暗下MenuStrip的新遊戲、結束、返回 -> 關閉遊戲視窗
                if(FormIndex.newGame==true || FormIndex.end==true || FormIndex.back == true)
                {
                    this.DialogResult = DialogResult.No;
                    Close();
                    break;
                }

                //開啟決定順序(比大小)的視窗
                Form3 f=new Form3();
                f.ShowDialog();
            }
            
            if (teamNow==0)
            {
                label1.Text = "黑方\n進攻";
                label1.ForeColor = Color.DodgerBlue;
            }
            else if (teamNow == 1)
            {
                label1.Text = "紅方\n進攻";
                label1.ForeColor=Color.Crimson;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("黑方確定投降？", "投降確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                FormIndex.redWin++;
                this.DialogResult = MessageBox.Show("恭喜紅方勝利\n是否再玩一局？", "棋局結束", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("雙方確定和局？", "和局確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                this.DialogResult = MessageBox.Show("雙方和局\n是否再玩一局？", "棋局結束", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("紅方確定投降？", "投降確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                FormIndex.blackWin++;
                this.DialogResult = MessageBox.Show("恭喜黑方勝利\n是否再玩一局？", "棋局結束", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
        }

        void move(int i,int j,PictureBox pic2)      //修改版本
        {
            //寫法說明：若是非法操作or非移動/Attack -> return
            //排除非法操作(未return) -> 執行最下面的code (移動/Attack)

            Chess chs1 = chs[iNow, jNow];    //進攻棋
            Chess chs2 = chs[i, j];                   //被吃棋

            if (iNow == i && jNow == j)     //重複選取同一棋子
                return;

            if (chs1.team == chs2.team)     //兩顆棋子同陣營 (改選其他棋子)
            {
                picNow.Image = picNow.Image;    //去除選取框線
                picNow = pic2;
                pick1st(i, j);
                return;
            }

            if (chs2.Equals(nll))   // "移動"：chs2無棋子->表示欲移動至此
            {
                if ((iNow == i && (jNow + 1 == j || jNow - 1 == j)) || (jNow == j && (iNow + 1 == i || iNow - 1 == i))) //上下左右
                {
                    //不return -> 執行最下面的移動/Attack
                }
                else  //非法操作：跳格移動
                {
                    MessageBox.Show("每棋只能移動一格", "非法移動", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    picNow.Image = picNow.Image;    //去除選取框線
                    iNow = -1;
                    jNow = -1;
                    return;
                }
            }
            else  //吃棋
            {
                if (chs1.level == 6)    //用炮吃棋
                {
                    if(iNow!=i && jNow != j)    //非法操作：兩顆棋子連線非水平，垂直
                    {
                        MessageBox.Show("炮只能水平或垂直攻擊！", "非法操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        picNow.Image = picNow.Image;    //去除選取框線
                        iNow = -1;
                        jNow = -1;
                        return;
                    }

                    int num = 0;    //隔num顆
                    if (iNow == i)  //垂直炮
                    {
                        int k1 = (jNow < j ? jNow : j);     //smaller
                        int k2 = (jNow > j ? jNow : j);     //bigger
                        for (int k = k1 + 1; k < k2; k++)   //計算隔num顆
                        {
                            if (!chs[i, k].Equals(nll))
                            {
                                num++;
                                if (num > 1)
                                    break;
                            }
                        }
                    }
                    else  //水平炮
                    {
                        int k1 = (iNow < i ? iNow : i);     //smaller
                        int k2 = (iNow > i ? iNow : i);     //bigger
                        for (int k = k1 + 1; k < k2; k++)       //計算隔num顆
                        {
                            if (!chs[k, j].Equals(nll))
                            {
                                num++;
                                if (num > 1)
                                    break;
                            }
                        }
                    }

                    //計算num後
                    if (num > 1 || num == 0)       //Error：炮必須只隔一棋攻擊
                    {
                        MessageBox.Show("炮必須只隔一棋攻擊！", "非法操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        picNow.Image = picNow.Image;    //去除選取框線
                        iNow = -1;
                        jNow = -1;
                        return;
                    }
                    // "排除所有非法操作(非法就return)" -> 進行Attack  (執行move最下面的code)
                    // Attack...
                }
                else if ((iNow == i && (jNow + 1 == j || jNow - 1 == j)) || (jNow == j && (iNow + 1 == i || iNow - 1 == i))) 
                    //一般棋子只能上下左右吃棋
                {
                    if ((chs1.level > 0 && chs2.level == 6) || chs1.level >= chs2.level || (chs1.level == 0 && chs2.level == 5))
                    //<除了兵/卒 都可去吃 炮> || <大吃小、吃平輩> || <兵 吃 將>
                    {
                        if (chs1.level == 5 && chs2.level == 0)       //Error：帥/將 不可吃 卒/兵
                        {
                            MessageBox.Show("帥/將 不可吃 卒/兵！", "非法吃棋", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            picNow.Image = picNow.Image;    //去除選取框線
                            iNow = -1;
                            jNow = -1;
                            return;
                        }
                        // "排除以上非法操作(非法就return)" -> 進行Attack  (執行move最下面的code)
                        // Attack...
                    }
                    else  //非法吃棋：以小吃大 (不在以上if內的條件皆是)
                    {
                        MessageBox.Show("不可以小吃大 (除了 卒吃帥 / 兵吃將)", "非法吃棋", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        picNow.Image = picNow.Image;    //去除選取框線
                        iNow = -1;
                        jNow = -1;
                        return;
                    }
                }
                else  //未在if、else if內的條件皆為非法操作 -> return (不可Attack)
                {
                    MessageBox.Show("除了<炮>吃棋，所有棋子只能吃相鄰棋子", "非法吃棋", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    picNow.Image = picNow.Image;    //去除選取框線
                    iNow = -1;
                    jNow = -1;
                    return;
                }
            }

            //到此尚未return -> 移動/Attack
            //移動/Attack
            pic2.Image = picNow.Image;
            picNow.Image = null;
            chs[i, j] = chs1;
            chs[iNow, jNow] = nll;

            if (chs2.Equals(nll))
            {
                justMove++;
                if (justMove >= 35)
                {
                    label2.Text = "和局步數\n" + justMove + "/50";
                    if (justMove >= 50)
                    {
                        this.DialogResult = MessageBox.Show("平局 (無翻棋/吃棋僅有移動達50步)\n是否再比一局？", "棋局結束",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    }
                }
            }
            else
            {
                label2.Text = "";
                justMove = 0;   //重新計算

                //計算剩餘棋子數
                if (chs2.team == 0)
                    black--;
                else if (chs2.team == 1)
                    red--;

                //判斷棋局結束
                if (black <= 0)
                {
                    FormIndex.redWin++;
                    this.DialogResult = MessageBox.Show("紅方獲勝，是否再玩一局？", "棋局結束", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else if (red <= 0)
                {
                    FormIndex.blackWin++;
                    this.DialogResult = MessageBox.Show("黑方獲勝，是否再玩一局？", "棋局結束", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
            }

            //已成功吃棋/移動 ->攻守交換
            if (teamNow == 0)
            {
                teamNow = 1;
                label1.Text = "紅方\n進攻";
                label1.ForeColor = Color.Crimson;
            }
            else
            {
                teamNow = 0;
                label1.Text = "黑方\n進攻";
                label1.ForeColor = Color.DodgerBlue;
            }
            iNow = -1;
            jNow = -1;
        }

        //move原版
        /*
        void move2(int i, int j, PictureBox pic2)    //原始版本
        {
            Chess chs1 = chs[iNow, jNow];    //進攻棋
            Chess chs2 = chs[i, j];                   //被吃棋
            if (iNow == i && jNow == j)
                return;
            if (chs1.level == 6)     //炮 可隔一棋攻擊任一棋
            {
                if ((iNow == i && (jNow + 1 == j || jNow - 1 == j)) || (jNow == j && (iNow + 1 == i || iNow - 1 == i))) //上下左右
                {
                    if (chs2.Equals(nll))    //移動
                    {
                        pic2.Image = picNow.Image;
                        picNow.Image = null;
                        chs[i, j] = chs1;
                        chs[iNow, jNow] = nll;
                    }
                    else
                    {
                        MessageBox.Show("炮必須格一棋吃棋", "非法操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        picNow.Image = picNow.Image;    //去除選取框線
                        iNow = -1;
                        jNow = -1;
                        return;
                    }
                }
                else
                {
                    int num = 0;    //隔num顆
                    if (iNow == i)  //垂直炮
                    {
                        if (chs1.team == chs2.team)     //不可炮己方棋子
                        {
                            MessageBox.Show("不可炮己方棋子", "非法操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            picNow.Image = picNow.Image;    //去除選取框線
                            iNow = -1;
                            jNow = -1;
                            return;
                        }
                        else    //炮對手棋子
                        {
                            int k1 = (jNow < j ? jNow : j);
                            int k2 = (jNow > j ? jNow : j);
                            for (int k = k1 + 1; k < k2; k++)
                            {
                                if (!chs[i, k].Equals(nll))
                                {
                                    num++;
                                    if (num > 1)
                                        break;
                                }
                            }
                            if (num > 1 || num == 0)       //Error：炮必須只隔一棋攻擊
                            {
                                MessageBox.Show("炮必須只隔一棋攻擊！", "非法操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                picNow.Image = picNow.Image;    //去除選取框線
                                iNow = -1;
                                jNow = -1;
                                return;
                            }
                            else   //Attack
                            {
                                if (chs2.team == 0)
                                    black--;
                                else
                                    red--;

                                pic2.Image = picNow.Image;
                                picNow.Image = null;
                                chs[i, j] = chs1;
                                chs[iNow, jNow] = nll;
                            }
                        }
                    }
                    else if (jNow == j)     //水平炮
                    {
                        if (chs1.team == chs2.team)     //不可炮己方棋子
                        {
                            MessageBox.Show("不可炮己方棋子", "非法操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            picNow.Image = picNow.Image;    //去除選取框線
                            iNow = -1;
                            jNow = -1;
                            return;
                        }
                        else    //炮對手棋子
                        {
                            int k1 = (iNow < i ? iNow : i);
                            int k2 = (iNow > i ? iNow : i);
                            for (int k = k1 + 1; k < k2; k++)
                            {
                                if (!chs[k, j].Equals(nll))
                                {
                                    num++;
                                    if (num > 1)
                                        break;
                                }
                            }
                            if (num > 1 || num == 0)    //Error：炮必須只隔一棋攻擊
                            {
                                MessageBox.Show("炮必須只隔一棋攻擊！", "非法操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                picNow.Image = picNow.Image;    //去除選取框線
                                iNow = -1;
                                jNow = -1;
                                return;
                            }
                            else   //Attack
                            {
                                if (chs2.team == 0)
                                    black--;
                                else
                                    red--;

                                pic2.Image = picNow.Image;
                                picNow.Image = null;
                                chs[i, j] = chs1;
                                chs[iNow, jNow] = nll;
                            }
                        }
                    }
                    else    //chs1, chs2未在水平or垂直線上
                    {
                        if (chs1.team == chs2.team)     //chs1, chs2同一陣營 (改選其他棋子)
                        {
                            picNow.Image = picNow.Image;    //去除選取框線
                            picNow = pic2;
                            pick1st(i, j);
                        }
                        else    //chs1, chs2不同陣營 (欲非法吃棋)
                        {
                            MessageBox.Show("炮只能水平或垂直攻擊！", "非法操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            picNow.Image = picNow.Image;    //去除選取框線
                            iNow = -1;
                            jNow = -1;
                        }
                        return;
                    }
                }
            }
            else if ((iNow == i && (jNow + 1 == j || jNow - 1 == j)) || (jNow == j && (iNow + 1 == i || iNow - 1 == i)))
            //非炮，一般棋子只能上下左右
            {
                if (chs1.team == chs2.team)
                {
                    MessageBox.Show("不可吃己方棋子", "非法操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    picNow.Image = picNow.Image;    //去除選取框線
                    iNow = -1;
                    jNow = -1;
                    return;
                }
                else if (chs2.Equals(nll))    //移動
                {
                    pic2.Image = picNow.Image;
                    picNow.Image = null;
                    chs[i, j] = chs1;
                    chs[iNow, jNow] = nll;
                }
                else if ((chs1.level > 0 && chs2.level == 6) || chs1.level >= chs2.level || (chs1.level == 0 && chs2.level == 5))
                //<除了兵/卒 去吃 炮> || <大吃小> || <兵 吃 將>
                {
                    if (chs1.level == 5 && chs2.level == 0)       //Error：帥/將 不可吃 卒/兵
                    {
                        MessageBox.Show("帥/將 不可吃 卒/兵！", "非法操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        picNow.Image = picNow.Image;    //去除選取框線
                        iNow = -1;
                        jNow = -1;
                        return;
                    }
                    //Attack
                    if (chs2.team == 0)
                        black--;
                    else
                        red--;

                    pic2.Image = picNow.Image;
                    picNow.Image = null;
                    chs[i, j] = chs1;
                    chs[iNow, jNow] = nll;
                }
                else
                {
                    MessageBox.Show("不可以小吃大 (除了 卒吃帥 / 兵吃將)", "非法操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    picNow.Image = picNow.Image;    //去除選取框線
                    iNow = -1;
                    jNow = -1;
                    return;
                }
            }
            else    //點選的棋子不是炮、兩顆棋子非位於上下左右 (非攻擊or移動)
            {
                if (chs1.team == chs2.team)     //兩顆棋子同陣營 (改選其他棋子)
                {
                    picNow.Image = picNow.Image;    //去除選取框線
                    picNow = pic2;
                    pick1st(i, j);
                }
                else    //兩顆棋子不同陣營 (欲非法吃棋)
                {
                    MessageBox.Show("除了<炮>以外，所有棋子只能移動一格", "非法操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    picNow.Image = picNow.Image;    //去除選取框線
                    iNow = -1;
                    jNow = -1;
                }
                return;
            }

            //判斷棋局結束
            if (black <= 0)
            {
                this.DialogResult = MessageBox.Show("紅方獲勝，是否再玩一局？", "棋局結束", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
            else if (red <= 0)
            {
                this.DialogResult = MessageBox.Show("黑方獲勝，是否再玩一局？", "棋局結束", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }

            //到此尚未return -> 已成功吃棋/移動 ->攻守交換
            if (teamNow == 0)
            {
                teamNow = 1;
                label1.Text = "紅方進攻";
                label1.ForeColor = Color.Red;
            }
            else
            {
                teamNow = 0;
                label1.Text = "黑方進攻";
                label1.ForeColor = Color.Black;
            }
            iNow = -1;
            jNow = -1;
        }
        */

        private void open(int i,int j,PictureBox pic)
        {
            label2.Text = "";
            justMove = 0;   //重新計算

            picNow.Image = picNow.Image;    //去除選取框線 (可能先選明棋，再開暗棋)
            if (chs[i, j].team == 0)
                pic.Image = imageList1.Images[chs[i, j].level];
            else
                pic.Image = imageList2.Images[chs[i, j].level];
            chs[i, j].status = 1;

            if (teamNow == 0)
            {
                teamNow = 1;
                label1.Text = "紅方\n進攻";
                label1.ForeColor = Color.Crimson;
            }
            else
            {
                teamNow = 0;
                label1.Text = "黑方\n進攻";
                label1.ForeColor = Color.DodgerBlue;
            }

            iNow = -1;
            jNow = -1;
        }
        private void pick1st(int i,int j)
        {
            if (teamNow == chs[i, j].team)
            {
                iNow = i;
                jNow = j;

                Graphics g = picNow.CreateGraphics();
                Pen pen = new Pen(Color.Blue, 3);
                g.DrawRectangle(pen, 1, 1, 78, 85);
            }
            else if (chs[i, j].Equals(nll))
                MessageBox.Show("若要移動至此，請先選擇一顆己方棋子","非法操作",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            else
                MessageBox.Show("現在是<" + label1.Text.Remove(2,1)+">", "非法操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int i = 0, j = 0;
            if(chs[i,j].status==0)      //還沒翻開
            {
                open(i,j,pictureBox1);
            }
            else if(iNow==-1  && jNow==-1)  //選取第一顆棋子
            {
                picNow = pictureBox1;
                pick1st(i,j);
            }
            else  //move
            {
                move(i,j,pictureBox1);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            int i = 0, j = 1;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox2);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox2;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox2);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            int i = 0, j = 2;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox3);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox3;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox3);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            int i = 0, j = 3;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox4);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox4;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox4);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            int i = 0, j = 4;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox5);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox5;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox5);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            int i = 0, j = 5;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox6);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox6;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox6);
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            int i = 0, j = 6;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox7);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox7;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox7);
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            int i = 0, j = 7;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox8);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox8;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox8);
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            int i = 1, j = 0;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox9);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox9;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox9);
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            int i = 1, j = 1;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox10);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox10;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox10);
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            int i = 1, j = 2;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox11);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox11;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox11);
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            int i = 1, j = 3;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox12);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox12;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox12);
            }
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            int i = 1, j = 4;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox13);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox13;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox13);
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            int i = 1, j = 5;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox14);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox14;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox14);
            }
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            int i = 1, j = 6;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox15);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox15;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox15);
            }
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            int i = 1, j = 7;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox16);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox16;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox16);
            }
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            int i = 2, j = 0;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox17);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox17;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox17);
            }
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            int i = 2, j = 1;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox18);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox18;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox18);
            }
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            int i = 2, j = 2;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox19);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox19;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox19);
            }
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            int i = 2, j = 3;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox20);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox20;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox20);
            }
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            int i = 2, j = 4;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox21);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox21;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox21);
            }
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            int i = 2, j = 5;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox22);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox22;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox22);
            }
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            int i = 2, j = 6;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox23);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox23;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox23);
            }
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            int i = 2, j = 7;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox24);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox24;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox24);
            }
        }
        private void pictureBox25_Click(object sender, EventArgs e)
        {
            int i = 3, j = 0;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox25);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox25;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox25);
            }
        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {
            int i = 3, j = 1;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox26);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox26;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox26);
            }
        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            int i = 3, j = 2;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox27);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox27;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox27);
            }
        }

        private void pictureBox28_Click(object sender, EventArgs e)
        {
            int i = 3, j = 3;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox28);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox28;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox28);
            }
        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {
            int i = 3, j = 4;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox29);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox29;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox29);
            }
        }

        private void pictureBox30_Click(object sender, EventArgs e)
        {
            int i = 3, j = 5;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox30);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox30;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox30);
            }
        }

        private void 新遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void pictureBox31_Click(object sender, EventArgs e)
        {
            int i = 3, j = 6;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox31);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox31;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox31);
            }
        }

        private void pictureBox32_Click(object sender, EventArgs e)
        {
            int i = 3, j = 7;
            if (chs[i, j].status == 0)      //還沒翻開
            {
                open(i, j, pictureBox32);
            }
            else if (iNow == -1 && jNow == -1)  //選取第一顆棋子
            {
                picNow = pictureBox32;
                pick1st(i, j);
            }
            else  //move
            {
                move(i, j, pictureBox32);
            }
        }

        

    }
}
