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
using System.IO;
using System.Threading;
using System.Diagnostics;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;

namespace EnumStudy
{
    public partial class AddTwoImage : Form
    {
        string filePath= @"C:\Users\SJ\Desktop\Merge Image\CutTest";    // 불러오는 이미지 경로
        //string savePath;
        bool flag1=false, flag2 = false, flag3 = false, flag4 = false;
        static int mergeImageWidth = 11000, mergeImageHeight = 3650, offsetRightX=0, offsetRightY=0;
        static Bitmap addPic = new Bitmap(mergeImageWidth, mergeImageHeight);  // 합친 이미지 넣을 공간
        static Bitmap addPic1 = new Bitmap(mergeImageWidth, mergeImageHeight);  // 합친 이미지 넣을 공간
        static Bitmap addPic2 = new Bitmap(mergeImageWidth, mergeImageHeight);  // 합친 이미지 넣을 공간
        static Bitmap addPic3 = new Bitmap(mergeImageWidth, mergeImageHeight);  // 합친 이미지 넣을 공간
        Graphics addGraphics = Graphics.FromImage(addPic);
        Graphics addGraphics1 = Graphics.FromImage(addPic1);
        Graphics addGraphics2 = Graphics.FromImage(addPic2);
        Graphics addGraphics3 = Graphics.FromImage(addPic3);
        Rectangle drL = new Rectangle(0, 0, mergeImageWidth / 2, mergeImageHeight); //왼쪽 이미지 박스
        Rectangle srL = new Rectangle(0, 0, mergeImageWidth / 2, mergeImageHeight); //왼쪽 이미지 소스
        Rectangle srR ;
        Rectangle drR ;

        Image surfi1L,surfi1R,surfi2L, surfi2R, coxialL, coxialR, backLightL, backLightR;

        Stopwatch stopWatch = new Stopwatch();
        Stopwatch stopWatch1 = new Stopwatch();
        Stopwatch stopWatch2 = new Stopwatch();
        Stopwatch stopWatch3 = new Stopwatch();
        Stopwatch StopwatchAll = new Stopwatch();
        CogToolBlock mainRecipe = (CogToolBlock)CogSerializer.LoadObjectFromFile(@"C:\Users\SJ\Desktop\MOBIS_MEA\MEA_Test.vpp");    // 레시피
        ICogImage cogImage; //코그넥스 툴에 들어가는 이미지

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
            NoMergeImage();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            StopwatchAll.Start();
            new Thread(() => MergeImage()).Start();
            new Thread(() => MergeImage1()).Start();
            new Thread(() => MergeImage2()).Start();
            new Thread(() => MergeImage3()).Start();
            new Thread(() => CheckEndProcess()).Start();
            
            //MergeImage();   //두개 이미지 합쳐서 진행
            //MergeImage();
            //MergeImage();
            //MergeImage();
        }
        public void CheckEndProcess()
        {
            bool flag = true;
            while (flag)
            {
                if (flag1 && flag2 && flag3 && flag4 == true)
                {
                    StopwatchAll.Stop();
                    using (StreamWriter outputFile = new StreamWriter(@"C:\Users\SJ\Desktop\test_0818\result_Image\Result3.txt", true))
                    {
                        outputFile.WriteLine((StopwatchAll.ElapsedMilliseconds).ToString());
                    }
                    StopwatchAll.Reset();
                    flag1 = false;
                    flag2 = false;
                    flag3 = false;
                    flag4 = false;

                    return;
                }
            }
        }

        public void OpenImage(string path, int index)
        {
            if (index == 0)
            {
                path += "\\0_0.bmp";
                surfi1L = Bitmap.FromFile(path);
                
            }
            else if (index == 1)
            {
                path += "\\1_0.bmp";
                surfi1R = Bitmap.FromFile(path);
            }
        }
        public void OpenImage1(string path, int index)
        {
            if (index == 0)
            {
                path += "\\0_01.bmp";
                surfi2L = Bitmap.FromFile(path);

            }
            else if (index == 1)
            {
                path += "\\1_01.bmp";
                surfi2R = Bitmap.FromFile(path);
            }
        }
        public void OpenImage2(string path, int index)
        {
            if (index == 0)
            {
                path += "\\0_0c.bmp";
                coxialL = Bitmap.FromFile(path);

            }
            else if (index == 1)
            {
                path += "\\1_0c.bmp";
                coxialR = Bitmap.FromFile(path);
            }
        }
        public void OpenImage3(string path, int index)
        {
            if (index == 0)
            {
                path += "\\0_0b.bmp";
                backLightL = Bitmap.FromFile(path);

            }
            else if (index == 1)
            {
                path += "\\1_0b.bmp";
                backLightR = Bitmap.FromFile(path);
            }
        }

        private void AddTwoImage_Load(object sender, EventArgs e)
        {
            RefreshRec(offsetRightX, offsetRightY);
        }
        public void NoMergeImage()
        {
            
            stopWatch.Start();
            OpenImage(filePath, 0);
            OpenImage(filePath, 1);
            stopWatch.Stop();
            lblOpenTime.Text = (stopWatch.ElapsedMilliseconds).ToString() + " ms";
            stopWatch.Reset();

            cogImage = new CogImage8Grey(new Bitmap(pBox1.Image));
            mainRecipe.Inputs[0].Value = cogImage;
            stopWatch.Start();
            mainRecipe.Run();
            stopWatch.Stop();
            lblVppTime.Text = (stopWatch.ElapsedMilliseconds).ToString() + " ms";
            stopWatch.Reset();

            cogImage = new CogImage8Grey(new Bitmap(pBox2.Image));
            mainRecipe.Inputs[0].Value = cogImage;
            stopWatch.Start();
            mainRecipe.Run();
            stopWatch.Stop();
            lblVppTime2.Text = (stopWatch.ElapsedMilliseconds).ToString() + " ms";
            stopWatch.Reset();
        }
        public void RefreshRec(int offsetRightX,int offsetRightY)
        {
            srR = new Rectangle(offsetRightX, 0, mergeImageWidth / 2, mergeImageHeight);
            drR = new Rectangle(mergeImageWidth / 2, 0 + offsetRightY, mergeImageWidth / 2, mergeImageHeight);
        }

        public void MergeImage()
        {
            int offsetRightY = 0, offsetRightX = 0;
            int parseOffset;
            bool offsetChangeX=false, offsetChangeY=false;
            string timeRecode = "";

            if (Int32.TryParse(tBoxRightOffsetX.Text, out parseOffset))
            {
                if(offsetRightX!= parseOffset)
                {
                    offsetRightX = parseOffset;
                    offsetChangeX = true;
                }
            }
            if (Int32.TryParse(tBoxRightOffsetY.Text, out parseOffset))
            {
                if (offsetRightX != parseOffset)
                {
                    offsetRightY = parseOffset;
                    offsetChangeY = true;
                }
            }
            if (offsetChangeX == true || offsetChangeY == true)
            {
                RefreshRec(offsetRightX, offsetRightY);
                offsetChangeX = false;
                offsetChangeY = false;
            }

            stopWatch.Start();
            OpenImage(filePath, 0);
            OpenImage(filePath, 1);
            stopWatch.Stop();
            //lblOpenTime.Text = (stopWatch.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += (stopWatch.ElapsedMilliseconds).ToString();
            stopWatch.Reset();

            stopWatch.Start();
            addGraphics.DrawImage(surfi1L, drL, srL, GraphicsUnit.Pixel);   // 왼쪽 이미지 그리기
            //addGraphics.DrawImage(pBox2.Image, mergeImageWidth/2+ offsetRightX, 0+ offsetRightY);
            addGraphics.DrawImage(surfi1R, drR, srR, GraphicsUnit.Pixel);
            //pBox3.Image = addPic;
            stopWatch.Stop();
            //lblMergeTime.Text = (stopWatch.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += ","+(stopWatch.ElapsedMilliseconds).ToString();
            stopWatch.Reset();

            stopWatch.Start();
            addPic.Save("D:\\MEA_Test\\AddImage.jpg", ImageFormat.Jpeg);
            stopWatch.Stop();
            //lblSavetime.Text = (stopWatch.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += "," + (stopWatch.ElapsedMilliseconds).ToString();
            stopWatch.Reset();

            cogImage = new CogImage8Grey(addPic);
            mainRecipe.Inputs[0].Value = cogImage;
            stopWatch.Start();
            mainRecipe.Run();
            stopWatch.Stop();
            //lblVppTime2.Text = (stopWatch.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += "," + (stopWatch.ElapsedMilliseconds).ToString();
            stopWatch.Reset();

            using (StreamWriter outputFile = new StreamWriter(@"C:\Users\SJ\Desktop\test_0818\result_Image\Result.txt", true))
            {
                outputFile.WriteLine(timeRecode);
            }
            flag1 = true;
        }
        public void MergeImage1()
        {
            int offsetRightY = 0, offsetRightX = 0;
            int parseOffset;
            bool offsetChangeX = false, offsetChangeY = false;
            string timeRecode = "";

            if (Int32.TryParse(tBoxRightOffsetX.Text, out parseOffset))
            {
                if (offsetRightX != parseOffset)
                {
                    offsetRightX = parseOffset;
                    offsetChangeX = true;
                }
            }
            if (Int32.TryParse(tBoxRightOffsetY.Text, out parseOffset))
            {
                if (offsetRightX != parseOffset)
                {
                    offsetRightY = parseOffset;
                    offsetChangeY = true;
                }
            }
            if (offsetChangeX == true || offsetChangeY == true)
            {
                RefreshRec(offsetRightX, offsetRightY);
                offsetChangeX = false;
                offsetChangeY = false;
            }

            stopWatch1.Start();
            OpenImage1(filePath, 0);
            OpenImage1(filePath, 1);
            stopWatch1.Stop();
            //lblOpenTime.Text = (stopWatch1.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += (stopWatch1.ElapsedMilliseconds).ToString();
            stopWatch1.Reset();

            stopWatch1.Start();
            addGraphics1.DrawImage(surfi2L, drL, srL, GraphicsUnit.Pixel);   // 왼쪽 이미지 그리기
            //addGraphics.DrawImage(pBox2.Image, mergeImageWidth/2+ offsetRightX, 0+ offsetRightY);
            addGraphics1.DrawImage(surfi2R, drR, srR, GraphicsUnit.Pixel);
            //pBox3.Image = addPic;
            stopWatch1.Stop();
            //lblMergeTime.Text = (stopWatch1.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += "," + (stopWatch1.ElapsedMilliseconds).ToString();
            stopWatch1.Reset();

            stopWatch1.Start();
            addPic1.Save("D:\\MEA_Test\\AddImage1.jpg", ImageFormat.Jpeg);
            stopWatch1.Stop();
            //lblSavetime.Text = (stopWatch1.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += "," + (stopWatch1.ElapsedMilliseconds).ToString();
            stopWatch1.Reset();

            cogImage = new CogImage8Grey(addPic1);
            mainRecipe.Inputs[0].Value = cogImage;
            stopWatch1.Start();
            mainRecipe.Run();
            stopWatch1.Stop();
            //lblVppTime2.Text = (stopWatch1.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += "," + (stopWatch1.ElapsedMilliseconds).ToString();
            stopWatch1.Reset();

            using (StreamWriter outputFile = new StreamWriter(@"C:\Users\SJ\Desktop\test_0818\result_Image\Result1.txt", true))
            {
                outputFile.WriteLine(timeRecode);
            }
            flag2 = true;
        }
        public void MergeImage2()
        {
            int offsetRightY = 0, offsetRightX = 0;
            int parseOffset;
            bool offsetChangeX = false, offsetChangeY = false;
            string timeRecode = "";

            if (Int32.TryParse(tBoxRightOffsetX.Text, out parseOffset))
            {
                if (offsetRightX != parseOffset)
                {
                    offsetRightX = parseOffset;
                    offsetChangeX = true;
                }
            }
            if (Int32.TryParse(tBoxRightOffsetY.Text, out parseOffset))
            {
                if (offsetRightX != parseOffset)
                {
                    offsetRightY = parseOffset;
                    offsetChangeY = true;
                }
            }
            if (offsetChangeX == true || offsetChangeY == true)
            {
                RefreshRec(offsetRightX, offsetRightY);
                offsetChangeX = false;
                offsetChangeY = false;
            }

            stopWatch2.Start();
            OpenImage2(filePath, 0);
            OpenImage2(filePath, 1);
            stopWatch2.Stop();
            //lblOpenTime.Text = (stopWatch2.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += (stopWatch2.ElapsedMilliseconds).ToString();
            stopWatch2.Reset();

            stopWatch2.Start();
            addGraphics2.DrawImage(coxialL, drL, srL, GraphicsUnit.Pixel);   // 왼쪽 이미지 그리기
            //addGraphics.DrawImage(pBox2.Image, mergeImageWidth/2+ offsetRightX, 0+ offsetRightY);
            addGraphics2.DrawImage(coxialR, drR, srR, GraphicsUnit.Pixel);
            //pBox3.Image = addPic;
            stopWatch2.Stop();
            //lblMergeTime.Text = (stopWatch2.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += "," + (stopWatch2.ElapsedMilliseconds).ToString();
            stopWatch2.Reset();

            stopWatch2.Start();
            addPic2.Save("D:\\MEA_Test\\AddImage2.jpg", ImageFormat.Jpeg);
            stopWatch2.Stop();
            //lblSavetime.Text = (stopWatch2.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += "," + (stopWatch2.ElapsedMilliseconds).ToString();
            stopWatch2.Reset();

            cogImage = new CogImage8Grey(addPic2);
            mainRecipe.Inputs[0].Value = cogImage;
            stopWatch2.Start();
            mainRecipe.Run();
            stopWatch2.Stop();
            //lblVppTime2.Text = (stopWatch2.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += "," + (stopWatch2.ElapsedMilliseconds).ToString();
            stopWatch2.Reset();

            using (StreamWriter outputFile = new StreamWriter(@"C:\Users\SJ\Desktop\test_0818\result_Image\Result2.txt", true))
            {
                outputFile.WriteLine(timeRecode);
            }
            flag3 = true;
        }
        public void MergeImage3()
        {
            int offsetRightY = 0, offsetRightX = 0;
            int parseOffset;
            bool offsetChangeX = false, offsetChangeY = false;
            string timeRecode = "";

            if (Int32.TryParse(tBoxRightOffsetX.Text, out parseOffset))
            {
                if (offsetRightX != parseOffset)
                {
                    offsetRightX = parseOffset;
                    offsetChangeX = true;
                }
            }
            if (Int32.TryParse(tBoxRightOffsetY.Text, out parseOffset))
            {
                if (offsetRightX != parseOffset)
                {
                    offsetRightY = parseOffset;
                    offsetChangeY = true;
                }
            }
            if (offsetChangeX == true || offsetChangeY == true)
            {
                RefreshRec(offsetRightX, offsetRightY);
                offsetChangeX = false;
                offsetChangeY = false;
            }

            stopWatch3.Start();
            OpenImage3(filePath, 0);
            OpenImage3(filePath, 1);
            stopWatch3.Stop();
            //lblOpenTime.Text = (stopWatch3.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += (stopWatch3.ElapsedMilliseconds).ToString();
            stopWatch3.Reset();

            stopWatch3.Start();
            addGraphics3.DrawImage(backLightL, drL, srL, GraphicsUnit.Pixel);   // 왼쪽 이미지 그리기
            //addGraphics.DrawImage(pBox2.Image, mergeImageWidth/2+ offsetRightX, 0+ offsetRightY);
            addGraphics3.DrawImage(backLightR, drR, srR, GraphicsUnit.Pixel);
            //pBox3.Image = addPic;
            stopWatch3.Stop();
            //lblMergeTime.Text = (stopWatch3.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += "," + (stopWatch3.ElapsedMilliseconds).ToString();
            stopWatch3.Reset();

            stopWatch3.Start();
            addPic3.Save("D:\\MEA_Test\\AddImage3.jpg", ImageFormat.Jpeg);
            stopWatch3.Stop();
            //lblSavetime.Text = (stopWatch3.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += "," + (stopWatch3.ElapsedMilliseconds).ToString();
            stopWatch3.Reset();

            cogImage = new CogImage8Grey(addPic3);
            mainRecipe.Inputs[0].Value = cogImage;
            stopWatch3.Start();
            mainRecipe.Run();
            stopWatch3.Stop();
            StopwatchAll.Stop();
            //lblVppTime2.Text = (stopWatch3.ElapsedMilliseconds).ToString() + " ms";
            timeRecode += "," + (stopWatch3.ElapsedMilliseconds).ToString();
            stopWatch3.Reset();
            StopwatchAll.Stop();
            using (StreamWriter outputFile = new StreamWriter(@"C:\Users\SJ\Desktop\test_0818\result_Image\Result3.txt", true))
            {
                outputFile.WriteLine(timeRecode);
            }
            flag4 = true;
        }
    }
}
