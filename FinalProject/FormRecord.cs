using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;        //引用OleDb命名空間

namespace FinalProject
{
    public partial class FormRecord : Form
    {
        OleDbConnection cn;     //建立Connection物件cn
        public FormRecord()
        {
            InitializeComponent();

            //成績紀錄
            string cnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db.mdb";
            cn = new OleDbConnection(cnStr);
            cn.Open();  //連接db.mdb資料庫
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string red_name = textBox1.Text;
            string black_name = textBox2.Text;
            if (red_name.Equals("") && black_name.Equals(""))
            {
                MessageBox.Show("請至少輸入一方使用者名稱！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int times = 0;
            string name;
            int score;

            if (black_name.Equals(""))
            {
                times = 1;
                name = red_name;
                score = FormIndex.redWin;
            }else if (red_name.Equals(""))
            {
                times = 1;
                name = black_name;
                score = FormIndex.blackWin;
            }
            else
            {
                times = 2;
                name = red_name;
                score = FormIndex.redWin;
            }

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cn;
            for (int i = 0; i < times; i++)
            {
                cmd.CommandText = "SELECT * FROM score WHERE 使用者名稱='" + name + "'";
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    int sum = Convert.ToInt32(dr[1]);
                    sum += score;

                    int heighest=Convert.ToInt32(dr[2]);
                    if(score>heighest)
                        heighest = score;

                    cmd.CommandText = "UPDATE score SET 累計分數='" + sum + "', 單局最高分數='" + heighest + "'" +
                        "WHERE 使用者名稱='" + name + "'";
                }
                else
                {
                    cmd.CommandText = "INSERT INTO score VALUES('" + name + "', '" + score + "', '" + score + "')";
                }
                dr.Close();
                cmd.ExecuteReader().Close();

                //換黑方
                name = black_name;
                score = FormIndex.blackWin;
            }

            cn.Close();
            MessageBox.Show("登記成功", "結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
