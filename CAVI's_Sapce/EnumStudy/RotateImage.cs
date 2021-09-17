using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnumStudy
{
    public partial class RotateImage : Form
    {
        string fileName;
        //string savePath;
        Bitmap bmap;

        public RotateImage()
        {
            InitializeComponent();
        }

        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = this.openFileDialog1.FileName;
                bmap = new Bitmap(fileName);
                pBoxImage.Image = bmap;
            }
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            float degree = float.Parse(textBox1.Text);
            Graphics g = Graphics.FromImage(bmap);
            g.TranslateTransform(bmap.Width/2, bmap.Height/2);
            g.RotateTransform(degree);
            g.TranslateTransform(-bmap.Width/2, -bmap.Height/2);
            g.DrawImage(bmap, 0, 0);
            pBoxImage.Image = bmap;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bmap.Save(@"C:\Users\SJ\Desktop\새 폴더\직사각형\RotateImage.bmp", ImageFormat.Bmp);
        }
    }
}
