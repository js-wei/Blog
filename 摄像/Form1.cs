using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tool;

namespace 摄像
{
    public partial class Form1 : Form
    {
        Pick pick = null;
        public Form1()
        {
            InitializeComponent();

            int left = 0;
            int top = 0;
            int width = 352;
            int height = 288;
            if (pick == null)
            {
                pick = new Pick(panelPreview.Handle, left, top, width, height);
            }
        }
        
        protected override void OnClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("退出本系统?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                Application.Exit();
            else
                e.Cancel = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
           

            if (this.b_pic.Text == "拍摄&A") //用于摄像的  
            {
                b_pic.Text = "重照&C";
                string path;
                sdf1.Filter = "图片*.bmp;*.jpg;.jpeg;*.gif|*.bmp|所有文件(*.*)|*.*";
                if (sdf1.ShowDialog() == DialogResult.OK)
                {
                    path = sdf1.FileName;
                    pick.GrabImage(path);
                    pic_show.ImageLocation = path; //并显示在方框中  
                    pic_show.Location = new Point(110, 12);
                    pic_show.Visible = true;
                    panelPreview.Visible = false;
                    pick.Stop();
                }
            }
            else
            {
                b_pic.Text = "拍摄&A";
                pic_show.Visible = false;
                panelPreview.Location = new Point(110, 12);
                panelPreview.Visible = true;
                pick.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pick.Stop();
        }
    }
}
