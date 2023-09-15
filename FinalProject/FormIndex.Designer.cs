namespace FinalProject
{
    partial class FormIndex
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIndex));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.遊戲ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.建立新遊戲ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.結束遊戲ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.返回主選單ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.競賽排行榜ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看排行榜ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.紀錄棋局結果ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文彩云", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(30, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 34);
            this.label1.TabIndex = 3;
            this.label1.Text = "00";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("华文彩云", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(559, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 34);
            this.label2.TabIndex = 4;
            this.label2.Text = "00";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.遊戲ToolStripMenuItem,
            this.返回主選單ToolStripMenuItem,
            this.競賽排行榜ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(649, 27);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 遊戲ToolStripMenuItem
            // 
            this.遊戲ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.建立新遊戲ToolStripMenuItem,
            this.結束遊戲ToolStripMenuItem});
            this.遊戲ToolStripMenuItem.Name = "遊戲ToolStripMenuItem";
            this.遊戲ToolStripMenuItem.Size = new System.Drawing.Size(53, 23);
            this.遊戲ToolStripMenuItem.Text = "遊戲";
            // 
            // 建立新遊戲ToolStripMenuItem
            // 
            this.建立新遊戲ToolStripMenuItem.Name = "建立新遊戲ToolStripMenuItem";
            this.建立新遊戲ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.建立新遊戲ToolStripMenuItem.Text = "建立新遊戲";
            this.建立新遊戲ToolStripMenuItem.Click += new System.EventHandler(this.建立新遊戲ToolStripMenuItem_Click);
            // 
            // 結束遊戲ToolStripMenuItem
            // 
            this.結束遊戲ToolStripMenuItem.Name = "結束遊戲ToolStripMenuItem";
            this.結束遊戲ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.結束遊戲ToolStripMenuItem.Text = "結束遊戲";
            this.結束遊戲ToolStripMenuItem.Click += new System.EventHandler(this.結束遊戲ToolStripMenuItem_Click);
            // 
            // 返回主選單ToolStripMenuItem
            // 
            this.返回主選單ToolStripMenuItem.Enabled = false;
            this.返回主選單ToolStripMenuItem.Name = "返回主選單ToolStripMenuItem";
            this.返回主選單ToolStripMenuItem.Size = new System.Drawing.Size(98, 23);
            this.返回主選單ToolStripMenuItem.Text = "返回主選單";
            // 
            // 競賽排行榜ToolStripMenuItem
            // 
            this.競賽排行榜ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看排行榜ToolStripMenuItem,
            this.紀錄棋局結果ToolStripMenuItem});
            this.競賽排行榜ToolStripMenuItem.Name = "競賽排行榜ToolStripMenuItem";
            this.競賽排行榜ToolStripMenuItem.Size = new System.Drawing.Size(98, 23);
            this.競賽排行榜ToolStripMenuItem.Text = "競賽排行榜";
            // 
            // 查看排行榜ToolStripMenuItem
            // 
            this.查看排行榜ToolStripMenuItem.Name = "查看排行榜ToolStripMenuItem";
            this.查看排行榜ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.查看排行榜ToolStripMenuItem.Text = "查看排行榜";
            this.查看排行榜ToolStripMenuItem.Click += new System.EventHandler(this.查看排行榜ToolStripMenuItem_Click);
            // 
            // 紀錄棋局結果ToolStripMenuItem
            // 
            this.紀錄棋局結果ToolStripMenuItem.Name = "紀錄棋局結果ToolStripMenuItem";
            this.紀錄棋局結果ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.紀錄棋局結果ToolStripMenuItem.Text = "紀錄棋局結果";
            this.紀錄棋局結果ToolStripMenuItem.Click += new System.EventHandler(this.紀錄棋局結果ToolStripMenuItem_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(331, 273);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(63, 60);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(249, 273);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(63, 60);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FinalProject.Properties.Resources.棋盤__封面;
            this.pictureBox1.Location = new System.Drawing.Point(12, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(622, 316);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FormIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(649, 356);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormIndex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "暗棋";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 遊戲ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 建立新遊戲ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 結束遊戲ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 返回主選單ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 競賽排行榜ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看排行榜ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 紀錄棋局結果ToolStripMenuItem;
    }
}