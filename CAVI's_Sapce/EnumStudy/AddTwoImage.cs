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
    public partial class AddTwoImage : Form
    {
        string filePath;
        string savePath;

        public AddTwoImage()
        {
            InitializeComponent();
        }

        private void btnOpenFilePath_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                savePath = this.folderBrowserDialog2.SelectedPath;
            }
        }

        int offsetRight = 0;

        private void btnRun_Click(object sender, EventArgs e)
        {
            int ChangeOffsetRight;
            if (Int32.TryParse(tBoxRightOffset.Text,out ChangeOffsetRight))
            {
                offsetRight = ChangeOffsetRight;
            }
            OpenImage(filePath, 0);
            OpenImage(filePath, 1);
            
            Bitmap addPic = new Bitmap(5040, 4096);
            Graphics addGraphics = Graphics.FromImage(addPic);
            addGraphics.DrawImage(pBox1.Image, 0, 0);
            addGraphics.DrawImage(pBox2.Image, 2520, 0+ offsetRight);
            pBox3.Image = addPic;
            addPic.Save("C:\\Users\\SJ\\Desktop\\test_0818\\result_Image\\AddImage.bmp", ImageFormat.Bmp);
        }


        public void OpenImage(string path, int index)
        {
            if (index == 0)
            {
                path += "\\0_0.bmp";
                pBox1.Image = Bitmap.FromFile(path);
            }
            else if (index == 1)
            {
                path += "\\1_0.bmp";
                pBox2.Image = Bitmap.FromFile(path);
            }
        }

       
    }
}
