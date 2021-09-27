using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.Dimensioning;
using Cognex.VisionPro.Display;


namespace EnumStudy
{
    public partial class DisplayWidthHeight : Form
    {
        public DisplayWidthHeight()
        {
            InitializeComponent();
        }

        CogImageFileTool CogImageFile = new CogImageFileTool();
        CogImageConvertTool CogImageConvert = new CogImageConvertTool();
        CogBlobTool CgBlob,CgCopyBlob;
        CogCreateGraphicLabelTool CogCreateGraphicLabelTool = new CogCreateGraphicLabelTool();
        CogRectangleAffine cogRectangleAffine = new CogRectangleAffine();



        ICogImage cogImage;
        ICogRecord cogRecord,cogRecordLabel;
        ICogRegion Region;
        string openImagePath= @"C:/Users/SJ/Desktop/Test/test.jpg";
        CogHelper CogHelper = new CogHelper();

        private void btnSetRegion_Click(object sender, EventArgs e)
        {
            CogHelper.SetGraphics(cogDisplay1, CgBlob.CreateCurrentRecord());
        }

        private void btnRegionSave_Click(object sender, EventArgs e)
        {
            cogDisplay1.InteractiveGraphics.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            CgBlob = new CogBlobTool();
            // 파일불러오는 Tool
            CogImageFile.Operator.Open(openImagePath, CogImageFileModeConstants.Read);
            CogImageFile.Run();
            // 8bitGrey로 이미지 변환 Tool
            CogImageConvert.InputImage = CogImageFile.OutputImage;
            CogImageConvert.Run();
            cogImage = CogImageConvert.OutputImage;
            // 메인화면에 이미지 띄우기
            cogDisplay1.Image = cogImage;
            // BlobTool에 params 넣기
            CgBlob.InputImage = cogImage;
            CgBlob.Region = cogRectangleAffine; //ROI설정
            //CgCopyBlob = CgBlob;
            cogBlobEditV21.Subject = CgBlob;
        }

        private void DisplayWidthHeight_Load(object sender, EventArgs e)
        {
            cogRectangleAffine.Interactive = true;
            cogRectangleAffine.GraphicDOFEnable = CogRectangleAffineDOFConstants.Position | CogRectangleAffineDOFConstants.Size;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            cogDisplay1.InteractiveGraphics.Clear();

            double principalWidth = 0, principalHeight = 0, blobLabelPointX = 0, blobLabelPointY = 0;
            double blobWidth=0, blobHeight=0 , blobCenterX=0, blobCenterY;
           
            CogImageFile.Operator.Open(openImagePath, CogImageFileModeConstants.Read);
            CogImageFile.Run();
            CogImageConvert.InputImage = CogImageFile.OutputImage;
            CogImageConvert.Run();
            cogImage = CogImageConvert.OutputImage;
            cogDisplay1.Image = cogImage;
            CgBlob.InputImage = cogImage;

            CgBlob.Region = cogRectangleAffine;

            CgBlob.RunParams.SegmentationParams.Mode = CogBlobSegmentationModeConstants.HardFixedThreshold;
            CgBlob.RunParams.SegmentationParams.HardFixedThreshold = 200;
            CgBlob.RunParams.SegmentationParams.Polarity = CogBlobSegmentationPolarityConstants.LightBlobs;
            CgBlob.RunParams.ConnectivityMinPixels = 100;
            CgBlob.Run();
            
            if (CgBlob.Results.GetBlobs().Count > 0)
            {
                foreach (CogBlobResult blobResult in CgBlob.Results.GetBlobs())
                {
                    principalWidth = blobResult.GetMeasure(CogBlobMeasureConstants.BoundingBoxPrincipalAxisWidth);
                    principalHeight = blobResult.GetMeasure(CogBlobMeasureConstants.BoundingBoxPrincipalAxisHeight);
                    blobWidth = blobResult.GetMeasure(CogBlobMeasureConstants.BoundingBoxExtremaAngleWidth);
                    blobHeight = blobResult.GetMeasure(CogBlobMeasureConstants.BoundingBoxExtremaAngleHeight);
                    blobCenterX = blobResult.GetMeasure(CogBlobMeasureConstants.BoundingBoxExtremaAngleCenterX);
                    blobCenterY = blobResult.GetMeasure(CogBlobMeasureConstants.BoundingBoxExtremaAngleCenterY);

                    blobLabelPointX = (blobCenterX - blobWidth / 2);
                    blobLabelPointY = (blobCenterY + blobHeight / 2);

                    CogCreateGraphicLabelTool.InputImage = cogImage;
                    CogCreateGraphicLabelTool.InputGraphicLabel.X = blobLabelPointX;
                    CogCreateGraphicLabelTool.InputGraphicLabel.Y = blobLabelPointY;
                    CogCreateGraphicLabelTool.InputGraphicLabel.Text = $"Width = {principalWidth:F2} , Height = {principalHeight:F2}";
                    CogCreateGraphicLabelTool.OutputColor = CogColorConstants.DarkGrey;
                    CogCreateGraphicLabelTool.Run();

                    cogRecordLabel = CogCreateGraphicLabelTool.CreateLastRunRecord();
                    CogHelper.SetGraphics(cogDisplay1, cogRecordLabel);
                }
            }

            //CogBlob.LastRunRecordEnable = CogBlobLastRunRecordConstants.BlobImage;
            //CogBlob.LastRunRecordEnable = CogBlobLastRunRecordConstants.ResultsBoundary;
            //CogBlob.LastRunRecordEnable = CogBlobLastRunRecordConstants.ResultsBoundingBoxPixelAligned;
            CgBlob.LastRunRecordEnable = CogBlobLastRunRecordConstants.ResultsBoundingBoxPrincipalAxis;
            //CogBlob.LastRunRecordDiagEnable = CogBlobLastRunRecordDiagConstants.Histogram;
            cogRecord = CgBlob.CreateLastRunRecord();
            
            CogHelper.SetGraphics(cogDisplay1, CgBlob.CreateLastRunRecord());


        }


    }
}
