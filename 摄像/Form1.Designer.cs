namespace 摄像
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.b_pic = new System.Windows.Forms.Label();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.pic_show = new System.Windows.Forms.PictureBox();
            this.sdf1 = new System.Windows.Forms.SaveFileDialog();
            this.panelPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_show)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(114, 308);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "开始";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(278, 308);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "结束";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // b_pic
            // 
            this.b_pic.AutoSize = true;
            this.b_pic.Location = new System.Drawing.Point(30, 40);
            this.b_pic.Name = "b_pic";
            this.b_pic.Size = new System.Drawing.Size(35, 12);
            this.b_pic.TabIndex = 2;
            this.b_pic.Text = "拍摄&A";
            // 
            // panelPreview
            // 
            this.panelPreview.Controls.Add(this.pic_show);
            this.panelPreview.Location = new System.Drawing.Point(32, 67);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(475, 212);
            this.panelPreview.TabIndex = 3;
            // 
            // pic_show
            // 
            this.pic_show.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_show.Location = new System.Drawing.Point(0, 0);
            this.pic_show.Name = "pic_show";
            this.pic_show.Size = new System.Drawing.Size(475, 212);
            this.pic_show.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_show.TabIndex = 0;
            this.pic_show.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 425);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.b_pic);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "摄像机";
            this.panelPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_show)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label b_pic;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.PictureBox pic_show;
        private System.Windows.Forms.SaveFileDialog sdf1;
    }
}

