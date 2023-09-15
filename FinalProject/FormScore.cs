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
    public partial class FormScore : Form
    {
        OleDbConnection cn;     //建立Connection物件cn
        public FormScore()
        {
            InitializeComponent();

            //成績排行榜
            string cnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db.mdb";
            cn = new OleDbConnection(cnStr);
            cn.Open();  //連接db.mdb資料庫

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cn;
            cmd.CommandText = "SELECT * FROM score ORDER BY 累計分數 DESC";
            OleDbDataReader dr=cmd.ExecuteReader();

            //欄位名稱
            for (int i = 0; i < dr.FieldCount; i++)
            {
                listView1.Columns.Add(dr.GetName(i), 115);
            }

            int row = 0;
            while (dr.Read())
            {
                listView1.Items.Add(dr[0].ToString());
                for (int i = 1; i < dr.FieldCount; i++)
                {
                    listView1.Items[row].SubItems.Add(dr[i].ToString());
                }
                row++;   
            }
            
        }
    }
}
