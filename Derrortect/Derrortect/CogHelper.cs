using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.Dimensioning;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.PMAlign;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Derrortect
{
    public class CogHelper
    {
        public struct Coordinate
        {
            public int X;
            public int Y;
            public int Color;
            // 생성자
            public Coordinate(int _x, int _y, int _color)
            {
                // 모든 필드 초기화 필수
                this.X = _x;
                this.Y = _y;
                this.Color = _color;
            }
            // 출력
            public override string ToString()
            {
                return "X: " + X + ", Y : " + Y + " PixelColor : " + Color;
            }
        }
       
        // 두점의 거리
        public static double GetDistance(ICogImage _value, System.Windows.Point[] _point)
        {
            
            using (CogDistancePointPointTool _mCogDistancePointPointTool = new CogDistancePointPointTool())
            {
                _mCogDistancePointPointTool.StartX = _point[0].X;
                _mCogDistancePointPointTool.StartY = _point[0].Y;

                _mCogDistancePointPointTool.EndX = _point[1].X;
                _mCogDistancePointPointTool.EndY = _point[1].Y;
                _mCogDistancePointPointTool.InputImage = _value;

                _mCogDistancePointPointTool.Run();
                try
                {
                    return _mCogDistancePointPointTool.Distance;
                }
                catch
                {
                    return 0;
                }

            }
        }

        // Point To Point Line
        public void CreateXYPoint(
            CogDistancePointPointTool _CogDistancePointPointTool,
            CogDisplay _CogDisplay,
            double[] _startXY,
            double[] _endXY
            )
        {
            _CogDistancePointPointTool.StartX = _startXY[0];
            _CogDistancePointPointTool.StartY = _startXY[1];

            _CogDistancePointPointTool.EndX = _endXY[0];
            _CogDistancePointPointTool.EndY = _endXY[1];

            _CogDistancePointPointTool.Run();

            CogHelper.SetGraphics(_CogDisplay, _CogDistancePointPointTool.CreateLastRunRecord());
        }

        /// <summary>
        /// ICogRegion 설정
        /// </summary>
        /// <param name="_region">Current Region</param>
        /// <param name="_type">region 문자열</param>
        /// <param name="_imageWidth">이미지넓이</param>
        /// <param name="_imageHeight">이미지높이</param>
        /// <returns></returns>
        //public static ICogRegion GetRegion(ICogRegion _region, string _type , int _imageWidth, int _imageHeight)
        //{

        //    if (ClsHelper.RegionShortName(_region.GetType().ToString()) == _type)
        //    {
        //        #region "Interactive false 되는 버그 처리 (Cognex bug)"
        //        if (_region.GetType() == typeof(CogRectangle))
        //        {
        //            CogRectangle _CogRectangle = new CogRectangle((CogRectangle)_region);
        //            _CogRectangle.Interactive = true;
        //            return (ICogRegion)_CogRectangle;
        //        }
        //        else if (_region.GetType() == typeof(CogRectangleAffine))
        //        {
        //            CogRectangleAffine _CogRectangleAffine = new CogRectangleAffine((CogRectangleAffine)_region);
        //            _CogRectangleAffine.Interactive = true;
        //            return (ICogRegion)_CogRectangleAffine;
        //        }
        //        else if (_region.GetType() == typeof(CogCircle))
        //        {
        //            CogCircle _CogCircle = new CogCircle((CogCircle)_region);
        //            _CogCircle.Interactive = true;
        //            return (ICogRegion)_CogCircle;
        //        }
        //        else if (_region.GetType() == typeof(CogPolygon))
        //        {
        //            CogPolygon _CogPolygon = new CogPolygon((CogPolygon)_region);
        //            _CogPolygon.Interactive = true;
        //            return (ICogRegion)_CogPolygon;
        //        }
        //        #endregion

        //    }
        //    else
        //    {
        //        #region "기본 값 ROI"
        //        double _halfwidth = _imageWidth / 2;
        //        double _halfheight = _imageHeight / 2;

        //        if (_type == ClsHelper.RegionShortName(typeof(CogRectangle).ToString()))
        //        {
        //            CogRectangle _CogRectangle = new CogRectangle();
        //            _CogRectangle.X = _halfwidth / 2;
        //            _CogRectangle.Y = _halfheight / 2;
        //            _CogRectangle.Width = _halfwidth;
        //            _CogRectangle.Height = _halfheight;
        //            _CogRectangle.Interactive = true;
        //            _CogRectangle.GraphicDOFEnable = CogRectangleDOFConstants.Position | CogRectangleDOFConstants.Size;
        //            return (ICogRegion)_CogRectangle;
        //        }
        //        else if (_type == ClsHelper.RegionShortName(typeof(CogRectangleAffine).ToString()))
        //        {
        //            CogRectangleAffine _CogRectangleAffine = new CogRectangleAffine();
        //            _CogRectangleAffine.CenterX = _halfwidth;
        //            _CogRectangleAffine.CenterY = _halfheight;
        //            _CogRectangleAffine.SideXLength = _halfwidth;
        //            _CogRectangleAffine.SideYLength = _halfheight;

        //            _CogRectangleAffine.Interactive = true;
        //            _CogRectangleAffine.GraphicDOFEnable = CogRectangleAffineDOFConstants.Position | CogRectangleAffineDOFConstants.Size;
        //            return (ICogRegion)_CogRectangleAffine;
        //        }
        //        else if (_type == ClsHelper.RegionShortName(typeof(CogCircle).ToString()))
        //        {
        //            CogCircle _CogCircle = new CogCircle();
        //            _CogCircle.Interactive = true;
        //            _CogCircle.CenterX = _halfwidth;
        //            _CogCircle.CenterY = _halfheight;
        //            _CogCircle.Radius = (_halfwidth + _halfheight) / 4;
        //            _CogCircle.GraphicDOFEnable = CogCircleDOFConstants.Position | CogCircleDOFConstants.Radius;
        //            return (ICogRegion)_CogCircle;
        //        }
        //        else if (_type == ClsHelper.RegionShortName(typeof(CogPolygon).ToString()))
        //        {
        //            CogPolygon _CogPolygon = new CogPolygon();

        //            _CogPolygon.AddVertex(_halfwidth + (_halfwidth / 2), _halfheight + (_halfheight / 2), 0);
        //            _CogPolygon.AddVertex(_halfwidth - (_halfwidth / 2), _halfheight + (_halfheight / 2), 1);
        //            _CogPolygon.AddVertex(_halfwidth - (_halfwidth / 2), _halfheight - (_halfheight / 2), 2);
        //            _CogPolygon.AddVertex(_halfwidth + (_halfwidth / 2), _halfheight - (_halfheight / 2), 3);

        //            _CogPolygon.Interactive = true;
        //            _CogPolygon.GraphicDOFEnable = CogPolygonDOFConstants.Position | CogPolygonDOFConstants.VertexPositions;
        //            return (ICogRegion)_CogPolygon;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //        #endregion
        //    }

        //    return null;
        //}

        // CogTool ROI 셋팅
        public static void SetToolROI(ICogTool _ICogTool, ICogRegion _ICogRegion)
        {
            if (_ICogTool.GetType() == typeof(Cognex.VisionPro.PMAlign.CogPMAlignTool))
            {
                ((CogPMAlignTool)_ICogTool).SearchRegion = _ICogRegion;
            }
            else if (_ICogTool.GetType() == typeof(Cognex.VisionPro.Caliper.CogCaliperTool))
            {
                if (_ICogRegion.GetType() == typeof(Cognex.VisionPro.CogRectangleAffine))
                {
                    ((CogCaliperTool)_ICogTool).Region = (CogRectangleAffine)_ICogRegion;
                }
                else
                {
                    MessageBox.Show("ROI can not be specified", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (_ICogTool.GetType() == typeof(Cognex.VisionPro.Blob.CogBlobTool))
            {
                ((CogBlobTool)_ICogTool).Region = _ICogRegion;
            }
        }

        // Display에 Interactive CogRectangle 설정
        public static void SetCogRegion(CogDisplay _CogDisplay, CogRectangle _CogRectangle, string _tag = "region")
        {
            if (_CogDisplay.Image != null)
            {
                _CogDisplay.InteractiveGraphics.Add(_CogRectangle, _tag, false);
            }

        }
        // Display에 Interactive CogRectangle 제거
        public static void SetCogRegionRemove(CogDisplay _CogDisplay, string _tag = "region")
        {
            if (_CogDisplay.Image != null)
            {
                if (_CogDisplay.InteractiveGraphics.Count > 0)
                {
                    try { _CogDisplay.InteractiveGraphics.Remove(_tag); }
                    catch { }
                }
            }

        }
        // Display에 StaticGraphics CogRectangle 설정
        public static void SetStaticCogRegion(CogDisplay _CogDisplay, CogRectangle _CogRectangle, string _tag = "region")
        {
            if (_CogDisplay.Image != null)
            {
                _CogDisplay.StaticGraphics.Add(_CogRectangle, _tag);
            }

        }

        // Display에 StaticGraphics CogRectangle 제거
        public static void SetStaticCogRegionRemove(CogDisplay _CogDisplay, string _tag = "region")
        {
            if (_CogDisplay.Image != null)
            {
                try { _CogDisplay.StaticGraphics.Remove(_tag); }
                catch { }
            }
        }

        // 해당 이미지의 ROI의 Sharpness 검출함 (0.9이상 이면 매우좋음)
        public static double GetImageSharpness(CogRectangle _CogRectangle, CogImage8Grey _CogImage8Grey)
        {
            using (CogImageSharpnessTool _cCogImageSharpnessTool = new CogImageSharpnessTool())
            {
                _cCogImageSharpnessTool.InputImage = _CogImage8Grey;
                _cCogImageSharpnessTool.RunParams.Mode = CogImageSharpnessModeConstants.GradientEnergy;
                _cCogImageSharpnessTool.RunParams.GradientEnergyLowPassSmoothing = 0;
                _cCogImageSharpnessTool.Region = _CogRectangle;
                _cCogImageSharpnessTool.Run();
                return _cCogImageSharpnessTool.Score;
            }
        }

        // GetPixelCoordinate 마우스 오버시 좌표 및 픽셀 Grayscale값 반환
        public static Coordinate GetPixelCoordinate(CogDisplay _CogDisplay, MouseEventArgs e)
        {
            Coordinate _mCoordinate = new Coordinate();

            if (_CogDisplay.Image != null)
            {
                CogTransform2DLinear _mCogTransform2DLinear;
                double _mPixelX, _mPixelY;
                try
                {
                    Point position = new Point(e.X, e.Y);
                    _mCogTransform2DLinear = _CogDisplay.GetTransform("*\\#", "*") as CogTransform2DLinear;
                    _mCogTransform2DLinear.MapPoint(position.X, position.Y, out _mPixelX, out _mPixelY);
                    int _mPixelPoint = -1;
                    unsafe
                    {

                        CogImage8Grey _mImage = (CogImage8Grey)_CogDisplay.Image;
                        ICogImage8PixelMemory _mICogImage8PixelMemory = _mImage.Get8GreyPixelMemory(CogImageDataModeConstants.Read, 0, 0, _mImage.Width, _mImage.Height);
                        byte* _pPixel = (byte*)_mICogImage8PixelMemory.Scan0.ToPointer();

                        if (_mPixelX >= 0 && _mPixelX < _mImage.Width
                            && _mPixelY >= 0 && _mPixelY < _mImage.Height)
                        {
                            _mPixelPoint = *(_pPixel + (int)_mPixelY * _mICogImage8PixelMemory.Stride + (int)_mPixelX);
                        }
                    }

                    _mCoordinate.X = (int)Math.Truncate(_mPixelX);
                    _mCoordinate.Y = (int)Math.Truncate(_mPixelY);
                    _mCoordinate.Color = (int)_mPixelPoint;
                }
                catch
                {
                    _mCoordinate.X = -1;
                    _mCoordinate.Y = -1;
                    _mCoordinate.Color = -1;
                }
            }
            return _mCoordinate;
        }

        // Tool Graphic
        public static void SetGraphics(CogDisplay _CogDisplay, ICogRecord _ICogRecord)
        {
            if (_CogDisplay != null)
            {
                SetGraphics(_CogDisplay, _ICogRecord, "Result");
            }

        }
        // 비권장
        public static void SetGraphics(CogDisplay _CogDisplay, ICogRecord _ICogRecord, bool _bGraphicIndex)
        {
            if (_CogDisplay.Image != null)
            {
                SetGraphics(_CogDisplay, _ICogRecord, _bGraphicIndex ? "Result0" : "Result1");
            }
        }
        // Tool Graphic
        public static void SetGraphics(CogDisplay _CogDisplay, ICogRecord _ICogRecord, string _tag)
        {
            foreach (Cognex.VisionPro.Implementation.CogRecord _record in _ICogRecord.SubRecords)
            {
                if (typeof(ICogGraphic).IsAssignableFrom(_record.ContentType))
                {
                    if (_record.Content != null)
                        _CogDisplay.InteractiveGraphics.Add(_record.Content as ICogGraphicInteractive, _tag, false);
                }

                else if (typeof(CogGraphicCollection).IsAssignableFrom(_record.ContentType))
                {
                    if (_record.Content != null)
                    {
                        CogGraphicCollection graphics = _record.Content as CogGraphicCollection;
                        foreach (ICogGraphic graphic in graphics)
                        {
                            _CogDisplay.InteractiveGraphics.Add(graphic as ICogGraphicInteractive, _tag, false);
                        }
                    }
                }
                else if (typeof(CogGraphicInteractiveCollection).IsAssignableFrom(_record.ContentType))
                {
                    if (_record.Content != null)
                    {
                        _CogDisplay.InteractiveGraphics.AddList(_record.Content as CogGraphicInteractiveCollection, _tag, false);
                    }
                }

                SetGraphics(_CogDisplay, _record, _tag);
            }
        }

        // Tool Graphic Tag명 기준으로 제거
        public static void SetGraphicsRemove(CogDisplay _CogDisplay, string _tag)
        {
            if (_CogDisplay.Image != null)
            {
                if (_CogDisplay.InteractiveGraphics.Count > 0)
                {
                    try { _CogDisplay.InteractiveGraphics.Remove(_tag); }
                    catch { }
                }
            }

        }
        // Image Save
        public static void ImageSave(string _path, ICogImage _Image, string _name = "")
        {
            try
            {
                System.IO.DirectoryInfo _DirectoryInfo = new System.IO.DirectoryInfo(_path);

                if (!_DirectoryInfo.Exists)
                {
                    _DirectoryInfo.Create();
                }
                using (CogImageFile _ImageFile = new CogImageFile())
                {
                    _ImageFile.Open(_path + string.Format("{0}", _name), CogImageFileModeConstants.Write);
                    _ImageFile.Append(_Image);
                    _ImageFile.Close();
                }
            }
            catch { }
        }

        // Display Image Save
        public static void DisplayImageSave(string _path, CogDisplay _CogDisplay, string _name = "", CogDisplayContentBitmapConstants _CogDisplayContentBitmapConstants = CogDisplayContentBitmapConstants.Display)
        {
            Image _Image;
            _Image = _CogDisplay.CreateContentBitmap(_CogDisplayContentBitmapConstants);
            _Image.Save(_path + string.Format("{0}", _name));
        }

        // Thumbnail Image Save
        public static void ImageThumbnailSave(string _path, ICogImage _Image, int _size, string _name = "")
        {
            try
            {
                System.IO.DirectoryInfo _DirectoryInfo = new System.IO.DirectoryInfo(_path);

                if (!_DirectoryInfo.Exists)
                {
                    _DirectoryInfo.Create();
                }
                using (CogImageFile _ImageFile = new CogImageFile())
                {
                    _ImageFile.Open(_path + string.Format("{0}", _name), CogImageFileModeConstants.Write);
                    _ImageFile.Append(CogMisc.GetThumbnailImage((ICogImage)_Image, _size));
                    _ImageFile.Append(_Image);
                    _ImageFile.Close();
                }
            }
            catch { }
        }

        // Path기준으로 CogImage 가져오기
        public static ICogImage ImageLoad(string _path)
        {
            ICogImage _Image = null;
            try
            {
                using (CogImageFile _ImageFile = new CogImageFile())
                {
                    _ImageFile.Open(_path, CogImageFileModeConstants.Read);
                    if (_ImageFile.Count > 0)
                        _Image = _ImageFile[0] as ICogImage;
                    _ImageFile.Close();
                }

                return _Image;
            }
            catch
            {
                return _Image;
            }
        }

        // bitmap to ICogImage
        public static ICogImage BitmapToCogImage(Bitmap _Bitmap)
        {
            return new CogImage8Grey(_Bitmap);
        }

        // 빈 이미지 만들기
        public static CogImage8Grey BlankImage(System.Drawing.Size _size)
        {
            using (Bitmap _Image = new Bitmap(_size.Width, _size.Height))
            {
                CogImage8Grey _CogImage8Grey = new CogImage8Grey(_Image);
                return _CogImage8Grey;
            }

        }

        // StaticGraphics Marker , tag = Marker
        public static void SetStaticPointMark(System.Windows.Point _point, CogDisplay _CogDisplay, double _rotate, CogColorConstants _CogColorConstants = CogColorConstants.White, int _pixelsize = 10, bool _line = false)
        {
            if (_CogDisplay.Image != null)
            {
                SetStaticPointMark(_point, _CogDisplay, "Marker", _rotate, _CogColorConstants, _pixelsize, _line);
            }

        }

        // StaticGraphics Marker
        public static void SetStaticPointMark(System.Windows.Point _point, CogDisplay _CogDisplay, string _tag, double _rotate, CogColorConstants _CogColorConstants = CogColorConstants.White, int _pixelsize = 10, bool _line = false)
        {
            using (CogPointMarker _CogPointMarker = new CogPointMarker())
            {
                SetStaticPointMark(_CogPointMarker, _point, _CogDisplay, _tag, _rotate, _CogColorConstants, _pixelsize, _line);
            }
        }

        public static void SetStaticPointMark(CogPointMarker _CogPointMarker , System.Windows.Point _point, CogDisplay _CogDisplay, string _tag, double _rotate, CogColorConstants _CogColorConstants = CogColorConstants.White, int _pixelsize = 10, bool _line = false)
        {
            if (_CogDisplay.Image != null)
            {
                _CogPointMarker.Color = _CogColorConstants;
                _CogPointMarker.SizeInScreenPixels = _pixelsize;
                _CogPointMarker.LineWidthInScreenPixels = 1;
                _CogPointMarker.LineStyle = _line ? _CogPointMarker.LineStyle = CogGraphicLineStyleConstants.Solid : _CogPointMarker.LineStyle = CogGraphicLineStyleConstants.Dot;
                _CogPointMarker.X = _point.X;
                _CogPointMarker.Y = _point.Y;
                _CogDisplay.StaticGraphics.Add(_CogPointMarker, _tag);
            }
        }

        // Tag명 기준으로 StaticGraphics 제거
        public static void SetStaticPointMarkRemove(CogDisplay _CogDisplay, string _tag)
        {
            if (_CogDisplay.Image != null)
            {
                if (_CogDisplay.InteractiveGraphics.Count > 0)
                {
                    _CogDisplay.StaticGraphics.Remove(_tag);
                }
            }
        }

        // InteractiveGraphics Marker , tag = Marker
        public static void SetPointMark(System.Windows.Point _point, CogDisplay _CogDisplay, double _rotate, CogColorConstants _CogColorConstants = CogColorConstants.White, int _pixelsize = 10, bool _line = false)
        {
            if (_CogDisplay.Image != null)
            {
                SetPointMark(_point, _CogDisplay, "Marker", _rotate, _CogColorConstants, _pixelsize, _line);
            }

        }

        // InteractiveGraphics Marker
        public static void SetPointMark(System.Windows.Point _point, CogDisplay _CogDisplay, string _tag, double _rotate, CogColorConstants _CogColorConstants = CogColorConstants.White, int _pixelsize = 10, bool _line = false)
        {
            if (_CogDisplay.Image != null)
            {
                using (CogPointMarker _CogPointMarker = new CogPointMarker())
                {
                    _CogPointMarker.Interactive = true;
                    _CogPointMarker.Color = _CogColorConstants;
                    _CogPointMarker.SelectedColor = _CogColorConstants;
                    _CogPointMarker.SizeInScreenPixels = _pixelsize;
                    _CogPointMarker.LineWidthInScreenPixels = 1;
                    if (!_line)
                    {
                        _CogPointMarker.LineStyle = CogGraphicLineStyleConstants.Solid;
                    }
                    else
                    {
                        _CogPointMarker.LineStyle = CogGraphicLineStyleConstants.Dot;
                    }
                    _CogPointMarker.X = _point.X;
                    _CogPointMarker.Y = _point.Y;

                    _CogDisplay.InteractiveGraphics.Add(_CogPointMarker, _tag, false);
                }
            }
        }
        // CogPointMarker 객체를 넘겨줘야 함
        public static void SetPointMark(CogPointMarker _CogPointMarker, System.Windows.Point _point, CogDisplay _CogDisplay, string _tag, double _rotate, CogColorConstants _CogColorConstants = CogColorConstants.White, int _pixelsize = 10, bool _line = true)
        {
            if (_CogDisplay.Image != null)
            {
                _CogPointMarker.Interactive = true;
                _CogPointMarker.Color = _CogColorConstants;
                _CogPointMarker.SizeInScreenPixels = _pixelsize;
                _CogPointMarker.LineWidthInScreenPixels = 1;
                _CogPointMarker.LineStyle = _line ? _CogPointMarker.LineStyle = CogGraphicLineStyleConstants.Solid : _CogPointMarker.LineStyle = CogGraphicLineStyleConstants.Dot;
                _CogPointMarker.X = _point.X;
                _CogPointMarker.Y = _point.Y;

                _CogDisplay.InteractiveGraphics.Add(_CogPointMarker, _tag, false);
            }
        }

        // InteractiveGraphics Remove
        public static void SetPointMarkRemove(CogDisplay _CogDisplay, string _tag)
        {
            if (_CogDisplay.Image != null)
            {
                if (_CogDisplay.InteractiveGraphics.Count > 0)
                {
                    try
                    {
                        _CogDisplay.InteractiveGraphics.Remove(_tag);
                    }
                    catch (Exception _e)
                    {
                        MessageBox.Show(_e.Message.ToString());
                    }
                }
            }
        }

          // StaticGraphics Cross Line , tag = Cross
        public static void SetStaticCrossLine(System.Windows.Point _point, CogDisplay _CogDisplay, CogColorConstants _CogColorConstants = CogColorConstants.White, bool _line = false)
        {
            if (_CogDisplay.Image != null)
            {
                SetStaticCrossLine(_point, _CogDisplay, "Cross", _CogColorConstants, _line);
            }
        }

        // StaticGraphics Cross Line
        public static void SetStaticCrossLine(System.Windows.Point _point, CogDisplay _CogDisplay, string _tag, CogColorConstants _CogColorConstants = CogColorConstants.White, bool _line = false)
        {
            CogLine[] _CogLine = new CogLine[2];
            _CogLine[0] = new CogLine(); //Vertical
            _CogLine[1] = new CogLine(); //Horizon

            SetStaticCrossLine(_CogLine, _point, _CogDisplay, _tag, _CogColorConstants, _line);
        }

        public static void SetStaticCrossLine(CogLine[] _CogLine , System.Windows.Point _point, CogDisplay _CogDisplay, string _tag, CogColorConstants _CogColorConstants = CogColorConstants.White, bool _line = false)
        {
            if (_CogDisplay.Image != null)
            {
                if (_CogLine.Length != 2) { return; }
      
                _CogLine[0].Color = _CogColorConstants;
                _CogLine[0].LineWidthInScreenPixels = 1;
                _CogLine[0].LineStyle = _line ? CogGraphicLineStyleConstants.Dot : CogGraphicLineStyleConstants.Solid;
                _CogLine[0].SetFromStartXYEndXY(_point.X, 0, _point.X, _point.Y * 2);

                _CogLine[1].Color = _CogColorConstants;
                _CogLine[1].LineWidthInScreenPixels = 1;
                _CogLine[1].LineStyle = _line ? CogGraphicLineStyleConstants.Dot : CogGraphicLineStyleConstants.Solid;
                _CogLine[1].SetFromStartXYEndXY(0, _point.Y, _point.X * 2, _point.Y);

                _CogDisplay.StaticGraphics.Add(_CogLine[0], _tag);
                _CogDisplay.StaticGraphics.Add(_CogLine[1], _tag);
            }
        }
        // Static CrossLine Remove
        public static void SetStaticCrossLineRemove(CogDisplay _CogDisplay, string _tag)
        {
            if (_CogDisplay.Image != null)
            {
                if (_CogDisplay.InteractiveGraphics.Count > 0)
                {
                    _CogDisplay.StaticGraphics.Remove(_tag);
                }
            }
        }

        // InteractiveGraphics  Cross Line , tag = Cross
        public static void SetCrossLine(System.Windows.Point _point, CogDisplay _CogDisplay, CogColorConstants _CogColorConstants = CogColorConstants.White, bool _line = false)
        {
            if (_CogDisplay.Image != null)
            {
                SetCrossLine(_point, _CogDisplay, "Cross", _CogColorConstants, _line);
            }
        }

        // InteractiveGraphics  Cross Line
        public static void SetCrossLine(System.Windows.Point _point, CogDisplay _CogDisplay, string _tag, CogColorConstants _CogColorConstants = CogColorConstants.White, bool _line = false)
        {
            CogLine[] _CogLine = new CogLine[2];
            _CogLine[0] = new CogLine(); //Vertical
            _CogLine[1] = new CogLine(); //Horizon
            SetCrossLine(_CogLine, _point, _CogDisplay, _tag, _CogColorConstants,_line);
        }

        public static void SetCrossLine(CogLine[] _CogLine , System.Windows.Point _point, CogDisplay _CogDisplay, string _tag, CogColorConstants _CogColorConstants = CogColorConstants.White, bool _line = false)
        {
            if (_CogDisplay.Image != null)
            {
                if (_CogLine.Length != 2) { return; }

                _CogLine[0].Color = _CogColorConstants;
                _CogLine[0].LineWidthInScreenPixels = 1;
                _CogLine[0].LineStyle = _line ? CogGraphicLineStyleConstants.Dot : CogGraphicLineStyleConstants.Solid;               
                _CogLine[0].SetFromStartXYEndXY(_point.X, 0, _point.X, _point.Y * 2);

                _CogLine[1].Color = _CogColorConstants;
                _CogLine[1].LineWidthInScreenPixels = 1;
                _CogLine[1].LineStyle = _line ? CogGraphicLineStyleConstants.Dot : CogGraphicLineStyleConstants.Solid;
                _CogLine[1].SetFromStartXYEndXY(0, _point.Y, _point.X * 2, _point.Y);

                _CogDisplay.InteractiveGraphics.Add(_CogLine[0], _tag, false);
                _CogDisplay.InteractiveGraphics.Add(_CogLine[1], _tag, false);
            }
        }

        // InteractiveGraphics  Cross Line Remove
        public static void SetCrossLineRemove(CogDisplay _CogDisplay, string _tag)
        {
            if (_CogDisplay.Image != null)
            {
                if (_CogDisplay.InteractiveGraphics.Count > 0)
                {
                    _CogDisplay.InteractiveGraphics.Remove(_tag);
                }
            }
        }

        // GraphicStatic Label , tag = Label
        public static void SetGraphicStaticLabel(System.Windows.Point _point, CogDisplay _CogDisplay, string _str, float _fontsize = 12F)
        {
            if (_CogDisplay.Image != null)
            {
                SetGraphicStaticLabel(_point, _CogDisplay, _str, "Label", _fontsize);
            }
        }

        // GraphicStatic Label
        public static void SetGraphicStaticLabel(System.Windows.Point _point, CogDisplay _CogDisplay, string _str, string _tag, float _fontsize = 12F)
        {
            if (_CogDisplay.Image != null)
            {
                CogGraphicLabel _CogGraphicLabel = new CogGraphicLabel();
                SetGraphicStaticLabel(_CogGraphicLabel, _point, _CogDisplay, _str, _tag, _fontsize);
            }
        }

        public static void SetGraphicStaticLabel(CogGraphicLabel _CogGraphicLabel , System.Windows.Point _point, CogDisplay _CogDisplay, string _str, string _tag, float _fontsize = 12F)
        {
            if (_CogDisplay.Image != null)
            {
                _CogGraphicLabel.Color = CogColorConstants.Red;
                _CogGraphicLabel.Font = new Font(_CogGraphicLabel.Font.Name, _fontsize, FontStyle.Bold, GraphicsUnit.Pixel);
                _CogGraphicLabel.SetXYText(_point.X, _point.Y, _str);
                _CogDisplay.StaticGraphics.Add(_CogGraphicLabel, _tag);   
            }
        }

        // GraphicStatic Label Remove
        public static void SetGraphicStaticLabelRemove(CogDisplay _CogDisplay, string _tag)
        {
            if (_CogDisplay.Image != null)
            {
                if (_CogDisplay.InteractiveGraphics.Count > 0)
                {
                    _CogDisplay.StaticGraphics.Remove(_tag);
                }
            }
        }

        // InteractiveGraphics Label , tag = Label
        public static void SetGraphicLabel(System.Windows.Point _point, CogDisplay _CogDisplay, string _str, float _fontsize = 12F)
        {
            if (_CogDisplay.Image != null)
            {
                SetGraphicLabel(_point, _CogDisplay, _str, "Label", _fontsize);
            }
        }

        // InteractiveGraphics Label
        public static void SetGraphicLabel(System.Windows.Point _point, CogDisplay _CogDisplay, string _str, string _tag, float _fontsize = 12F)
        {
            if (_CogDisplay.Image != null)
            {
                CogGraphicLabel _CogGraphicLabel = new CogGraphicLabel();
                SetGraphicLabel(_CogGraphicLabel, _point, _CogDisplay, _str, _tag, _fontsize);
            }
        }

        // CogGraphLabel 객체에 그래픽 Label 그림
        public static void SetGraphicLabel(CogGraphicLabel _CogGraphicLabel, System.Windows.Point _point, CogDisplay _CogDisplay, string _str, string _tag, float _fontsize = 12F)
        {
            if (_CogDisplay.Image != null)
            {
                _CogGraphicLabel.Interactive = true;
                _CogGraphicLabel.Color = CogColorConstants.Red;
                _CogGraphicLabel.Font = new Font(_CogGraphicLabel.Font.Name, _fontsize, FontStyle.Bold, GraphicsUnit.Pixel);
                _CogGraphicLabel.SetXYText(_point.X, _point.Y, _str);
                _CogDisplay.InteractiveGraphics.Add(_CogGraphicLabel, _tag , false);
            }
        }
        public static void SetGraphicLabel(System.Windows.Point _point, CogDisplay _CogDisplay, string _str, string _tag, CogColorConstants color, float _fontsize = 12F)
        {
            if (_CogDisplay.Image != null)
            {
                CogGraphicLabel _CogGraphicLabel = new CogGraphicLabel();
                _CogGraphicLabel.Interactive = true;
                _CogGraphicLabel.Color = color;
                _CogGraphicLabel.Font = new Font(_CogGraphicLabel.Font.Name, _fontsize, FontStyle.Bold, GraphicsUnit.Pixel);
                _CogGraphicLabel.SetXYText(_point.X, _point.Y, _str);
                _CogDisplay.InteractiveGraphics.Add(_CogGraphicLabel, _tag, false);
            }
        }

        // InteractiveGraphics Label
        public static void SetGraphicLabelRemove(CogDisplay _CogDisplay, string _tag)
        {
            if (_CogDisplay.Image != null)
            {
                if (_CogDisplay.InteractiveGraphics.Count > 0)
                {
                    _CogDisplay.InteractiveGraphics.Remove(_tag);
                }
            }
        }

        // CogDisplay Invoke
        public static void CogDisplayInvoke(Cognex.VisionPro.Display.CogDisplay _CogDisplay, ICogImage _ICogImage)
        {
            if (_CogDisplay.InvokeRequired)
            {
                _CogDisplay.Invoke(new MethodInvoker(
                    delegate ()
                    {
                        _CogDisplay.Image = _ICogImage;
                    }
                    ));
            }
            else
            {
                _CogDisplay.Image = _ICogImage;
            }
        }


        // Rotate Method(90 , 180 , 270만 지원)
        public static ICogImage SetRotate(Bitmap _Bmp, int _angle)
        {
            if (_Bmp.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            {
                return SetRotate((ICogImage)new CogImage24PlanarColor(_Bmp), _angle);
            }
            else
            {
                return SetRotate((ICogImage)new CogImage8Grey(_Bmp), _angle);
            }
        }

        // Rotate Method(90 , 180 , 270만 지원)
        public static ICogImage SetRotate(ICogImage _ICogImage, int _angle)
        {

            CogIPOneImageTool _mCogIPOneImageTool = new CogIPOneImageTool();
            CogIPOneImageFlipRotate _mCogIPOneImageFlipRotate = new CogIPOneImageFlipRotate();
            switch (_angle)
            {
                case 0:
                    _mCogIPOneImageFlipRotate.OperationInPixelSpace = CogIPOneImageFlipRotateOperationConstants.None;
                    break;
                case 90:
                    _mCogIPOneImageFlipRotate.OperationInPixelSpace = CogIPOneImageFlipRotateOperationConstants.Rotate90Deg;
                    break;
                case 180:
                    _mCogIPOneImageFlipRotate.OperationInPixelSpace = CogIPOneImageFlipRotateOperationConstants.Rotate180Deg;
                    break;
                case 270:
                    _mCogIPOneImageFlipRotate.OperationInPixelSpace = CogIPOneImageFlipRotateOperationConstants.Rotate270Deg;
                    break;
            }

            _mCogIPOneImageTool.Operators.Add(_mCogIPOneImageFlipRotate);
            _mCogIPOneImageTool.InputImage = _ICogImage;
            _mCogIPOneImageTool.Run();

            if (null != _mCogIPOneImageTool.OutputImage)
            {
                _mCogIPOneImageTool.OutputImage.SelectedSpaceName = "#";
                return _mCogIPOneImageTool.OutputImage;
            }
            else
            {
                return null;
            }
        }
    }
}
