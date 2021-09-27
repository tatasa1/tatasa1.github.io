using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.Dimensioning;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.Caliper;
using OpenCvSharp;

namespace Derrortect
{
    public partial class frmMain : Form
    {
        public static frmMain _frmGrabber;
        private Thread tGcProcess;
        private Thread tProcess;
        private VideoCapture capture_1;
        private Mat frame_1;
        private CogImage8Grey cogImageGrey;
        private bool _isLive;

        public CogToolBlock cogRecipe = (CogToolBlock)CogSerializer.LoadObjectFromFile(@"C:\Users\SJ\Desktop\Test\Derrortect.vpp");
        public ICogRecord cogRecipeRecord,cogOcrRecord;
        public CogBlobResults cogBlobResult = new CogBlobResults();
        

        public frmMain()
        {
            _frmGrabber = this;
            InitializeComponent();
        }

        private void btnLive_Click(object sender, EventArgs e)
        {
            _isLive = !_isLive;
            if (_isLive)
            {
                tProcess = new Thread(new ThreadStart(LiveCam));
                tGcProcess = new Thread(new ThreadStart(GarbageCollect));
                tProcess.Start();
                tGcProcess.Start();
            }
            else
            {
                tProcess.Abort();
                tGcProcess.Abort();

                _cogDisplay.Image = null;
            }
        }

        private void LiveCam()
        {
            try
            {
                // camera params
                frame_1 = new Mat();
                capture_1 = new VideoCapture(0);
                capture_1.FrameWidth = 3264;
                capture_1.FrameHeight = 1832;

                // Label에 필요한 params
                double principalWidth = 0, principalHeight = 0, blobLabelPointX = 0, blobLabelPointY = 0;
                double blobWidth = 0, blobHeight = 0, blobCenterX = 0, blobCenterY;
                double area = 0;
                string ocr;
                //Label 그리기 위한 Tool 생성
                CogCreateGraphicLabelTool CogCreateGraphicLabelTool = new CogCreateGraphicLabelTool();  

                while (true)
                {
                    capture_1.Read(frame_1);

                    if (!frame_1.Empty())
                    {
                        Bitmap image1 = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame_1);
                        cogImageGrey = new CogImage8Grey(image1);
                        _cogDisplay.Image = new CogImage8Grey(image1);
                       
                        cogRecipe.Inputs[0].Value = cogImageGrey;
                        cogRecipe.Run();
                       
                        cogRecipeRecord = cogRecipe.Tools[1].CreateLastRunRecord();
                        cogOcrRecord = cogRecipe.Tools[4].CreateLastRunRecord();
                        //Label을 그리기 위해 필요한 BlobResult 저장 
                        cogBlobResult = (CogBlobResults)cogRecipe.Outputs[0].Value;
                        _cogDisplayGrab.InteractiveGraphics.Clear();
                        _cogDisplayOCR.InteractiveGraphics.Clear();
                        // Blob이 잡힌 개수 만큼 반복해서 Label 만들기
                        foreach (int item in cogBlobResult.GetBlobIDs(true))
                        {
                            blobWidth = cogBlobResult.GetBlobMeasure(CogBlobMeasureConstants.BoundingBoxExtremaAngleWidth,item);
                            blobHeight = cogBlobResult.GetBlobMeasure(CogBlobMeasureConstants.BoundingBoxExtremaAngleHeight,item);
                            blobCenterX = cogBlobResult.GetBlobMeasure(CogBlobMeasureConstants.BoundingBoxExtremaAngleCenterX,item);
                            blobCenterY = cogBlobResult.GetBlobMeasure(CogBlobMeasureConstants.BoundingBoxExtremaAngleCenterY,item);
                            principalWidth = cogBlobResult.GetBlobMeasure(CogBlobMeasureConstants.BoundingBoxPrincipalAxisWidth,item);
                            principalHeight = cogBlobResult.GetBlobMeasure(CogBlobMeasureConstants.BoundingBoxPrincipalAxisHeight,item);
                            area = cogBlobResult.GetBlobMeasure(CogBlobMeasureConstants.Area, item);

                            blobLabelPointX = (blobCenterX - blobWidth / 2);
                            blobLabelPointY = (blobCenterY + blobHeight / 2);

                            CogCreateGraphicLabelTool.InputImage = (CogImage8Grey)cogRecipe.Outputs[1].Value;
                            CogCreateGraphicLabelTool.InputGraphicLabel.X = blobLabelPointX;
                            CogCreateGraphicLabelTool.InputGraphicLabel.Y = blobLabelPointY;
                            CogCreateGraphicLabelTool.InputGraphicLabel.Text = $"Width = {principalWidth:F2} , Height = {principalHeight:F2} , Area = {area:F2}";
                            CogCreateGraphicLabelTool.OutputColor = CogColorConstants.Cyan;
                            CogCreateGraphicLabelTool.Run();
                            
                            CogHelper.SetGraphics(_cogDisplayGrab, CogCreateGraphicLabelTool.CreateLastRunRecord());
                        }
                        _cogDisplayGrab.Image = cogImageGrey;
                        _cogDisplayOCR.Image = cogImageGrey;
                        CogHelper.SetGraphics(_cogDisplayGrab, cogRecipeRecord);
                        CogHelper.SetGraphics(_cogDisplayOCR, cogOcrRecord);
                    }
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                frame_1.Dispose();
                capture_1.Release();
                GC.Collect();
            }
        }

        private void GarbageCollect()
        {
            while (true)
            {
                Process ps = Process.GetCurrentProcess();
                long memory = ps.WorkingSet64 / 1024 / 1024;
                Thread.Sleep(500);
                if (memory > 1000)
                {
                    GC.Collect(0);
                }
            }
        }

        private void btnGrab_Click(object sender, EventArgs e)
        {
            capture_1 = new VideoCapture(0);
            
            frame_1 = new Mat();
            capture_1.FrameWidth = 3264;
            capture_1.FrameHeight = 1832;

            capture_1.Read(frame_1);
            if (!frame_1.Empty())
            {
                Bitmap image1 = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame_1);
                cogImageGrey = new CogImage8Grey(image1);
                _cogDisplayGrab.Image = cogImageGrey;

                Bitmap imageGrey = _cogDisplayGrab.Image.ToBitmap();
                imageGrey.Save(@"C:\Users\SJ\Desktop\Test\Image.bmp",System.Drawing.Imaging.ImageFormat.Bmp);
            }
            frame_1.Dispose();
            capture_1.Release();
        }

        private void btnRecipeTest_Click(object sender, EventArgs e)
        {
            cogRecipe.Inputs[0].Value = cogImageGrey;
            cogRecipe.Run();
            _cogDisplayGrab.Image = cogImageGrey;
            //_cogDisplay.Image = new CogImage8Grey(image1);
            cogRecipeRecord = cogRecipe.Tools[1].CreateLastRunRecord();
            CogHelper.SetGraphics(_cogDisplayGrab, cogRecipeRecord);
        }
    }
}
