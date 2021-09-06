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
using System.Diagnostics;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;

namespace EnumStudy
{
    public partial class AddTwoImage : Form
    {
        string filePath;
        string savePath;
        static int mergeImageWidth = 11000, mergeImageHeight = 3650, offsetRightX=0, offsetRightY=0;
        static Bitmap addPic = new Bitmap(mergeImageWidth, mergeImageHeight);  // 합친 이미지 넣을 공간
        Graphics addGraphics = Graphics.FromImage(addPic);
        Rectangle drL = new Rectangle(0, 0, mergeImageWidth / 2, mergeImageHeight);
        Rectangle srL = new Rectangle(0, 0, mergeImageWidth / 2, mergeImageHeight);
        Rectangle srR = new Rectangle(offsetRightX, 0, mergeImageWidth / 2, mergeImageHeight);
        Rectangle drR = new Rectangle(mergeImageWidth / 2, 0 + offsetRightY, mergeImageWidth / 2, mergeImageHeight);
        Stopwatch stopWatch = new Stopwatch();
        CogToolBlock mainRecipe = (CogToolBlock)CogSerializer.LoadObjectFromFile(@"C:\Users\SJ\Desktop\MOBIS_MEA\MEA_Test.vpp");
        ICogImage cogImage;
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
            //if (this.folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            //{
            //    savePath = this.folderBrowserDialog2.SelectedPath;
            //}
            
            cogImage = new CogImage8Grey(addPic);
            mainRecipe.Inputs[0].Value = cogImage;
            stopWatch.Start();
            mainRecipe.Run();
            stopWatch.Stop();
            lblVppTime.Text = (stopWatch.ElapsedMilliseconds).ToString() + " ms";
            
        }


        private void btnRun_Click(object sender, EventArgs e)
        {
            
            int offsetRightY=0 , offsetRightX=0;
            int mergeImageWidth=11000, mergeImageHeight=3650;
            int parseOffset;

            if (Int32.TryParse(tBoxRightOffsetX.Text, out parseOffset))
            {
                offsetRightX = parseOffset;
            }
            if (Int32.TryParse(tBoxRightOffsetY.Text,out parseOffset))
            {
                offsetRightY = parseOffset;
            }

            stopWatch.Start();
            OpenImage(filePath, 0);
            OpenImage(filePath, 1);
            stopWatch.Stop();
            lblOpenTime.Text=(stopWatch.ElapsedMilliseconds).ToString()+" ms";

            stopWatch.Start();
            addGraphics.DrawImage(pBox1.Image, drL,srL,GraphicsUnit.Pixel);   // 왼쪽 이미지 그리기
            //addGraphics.DrawImage(pBox2.Image, mergeImageWidth/2+ offsetRightX, 0+ offsetRightY);
            addGraphics.DrawImage(pBox2.Image, drR, srR, GraphicsUnit.Pixel);
            pBox3.Image = addPic;
            stopWatch.Stop();
            lblMergeTime.Text= (stopWatch.ElapsedMilliseconds).ToString() + " ms";

            stopWatch.Start();
            addPic.Save("D:\\MEA_Test\\AddImage.jpg", ImageFormat.Jpeg);
            stopWatch.Stop();
            lblSavetime.Text = (stopWatch.ElapsedMilliseconds).ToString() + " ms";
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

        private void AddTwoImage_Load(object sender, EventArgs e)
        {

        }
    }
}
