namespace Derrortect
{
    partial class frmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnGrab = new System.Windows.Forms.Button();
            this._cogDisplayGrab = new Cognex.VisionPro.Display.CogDisplay();
            this.btnRecipeTest = new System.Windows.Forms.Button();
            this.btnLive = new System.Windows.Forms.Button();
            this._cogDisplay = new Cognex.VisionPro.Display.CogDisplay();
            this.panel1 = new System.Windows.Forms.Panel();
            this._cogDisplayOCR = new Cognex.VisionPro.Display.CogDisplay();
            ((System.ComponentModel.ISupportInitialize)(this._cogDisplayGrab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._cogDisplay)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._cogDisplayOCR)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGrab
            // 
            this.btnGrab.Location = new System.Drawing.Point(94, 12);
            this.btnGrab.Name = "btnGrab";
            this.btnGrab.Size = new System.Drawing.Size(76, 41);
            this.btnGrab.TabIndex = 2;
            this.btnGrab.Text = "Grab";
            this.btnGrab.UseVisualStyleBackColor = true;
            this.btnGrab.Click += new System.EventHandler(this.btnGrab_Click);
            // 
            // _cogDisplayGrab
            // 
            this._cogDisplayGrab.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this._cogDisplayGrab.ColorMapLowerRoiLimit = 0D;
            this._cogDisplayGrab.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this._cogDisplayGrab.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this._cogDisplayGrab.ColorMapUpperRoiLimit = 1D;
            this._cogDisplayGrab.DoubleTapZoomCycleLength = 2;
            this._cogDisplayGrab.DoubleTapZoomSensitivity = 2.5D;
            this._cogDisplayGrab.Location = new System.Drawing.Point(0, 59);
            this._cogDisplayGrab.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this._cogDisplayGrab.MouseWheelSensitivity = 1D;
            this._cogDisplayGrab.Name = "_cogDisplayGrab";
            this._cogDisplayGrab.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("_cogDisplayGrab.OcxState")));
            this._cogDisplayGrab.Size = new System.Drawing.Size(736, 650);
            this._cogDisplayGrab.TabIndex = 3;
            // 
            // btnRecipeTest
            // 
            this.btnRecipeTest.Location = new System.Drawing.Point(176, 12);
            this.btnRecipeTest.Name = "btnRecipeTest";
            this.btnRecipeTest.Size = new System.Drawing.Size(72, 40);
            this.btnRecipeTest.TabIndex = 4;
            this.btnRecipeTest.Text = "test";
            this.btnRecipeTest.UseVisualStyleBackColor = true;
            this.btnRecipeTest.Visible = false;
            // 
            // btnLive
            // 
            this.btnLive.Location = new System.Drawing.Point(12, 11);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(76, 41);
            this.btnLive.TabIndex = 1;
            this.btnLive.Text = "Live";
            this.btnLive.UseVisualStyleBackColor = true;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // _cogDisplay
            // 
            this._cogDisplay.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this._cogDisplay.ColorMapLowerRoiLimit = 0D;
            this._cogDisplay.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this._cogDisplay.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this._cogDisplay.ColorMapUpperRoiLimit = 1D;
            this._cogDisplay.DoubleTapZoomCycleLength = 2;
            this._cogDisplay.DoubleTapZoomSensitivity = 2.5D;
            this._cogDisplay.Location = new System.Drawing.Point(27, 59);
            this._cogDisplay.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this._cogDisplay.MouseWheelSensitivity = 1D;
            this._cogDisplay.Name = "_cogDisplay";
            this._cogDisplay.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("_cogDisplay.OcxState")));
            this._cogDisplay.Size = new System.Drawing.Size(76, 57);
            this._cogDisplay.TabIndex = 0;
            this._cogDisplay.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRecipeTest);
            this.panel1.Controls.Add(this.btnLive);
            this.panel1.Controls.Add(this.btnGrab);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1499, 62);
            this.panel1.TabIndex = 5;
            // 
            // _cogDisplayOCR
            // 
            this._cogDisplayOCR.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this._cogDisplayOCR.ColorMapLowerRoiLimit = 0D;
            this._cogDisplayOCR.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this._cogDisplayOCR.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this._cogDisplayOCR.ColorMapUpperRoiLimit = 1D;
            this._cogDisplayOCR.DoubleTapZoomCycleLength = 2;
            this._cogDisplayOCR.DoubleTapZoomSensitivity = 2.5D;
            this._cogDisplayOCR.Location = new System.Drawing.Point(742, 59);
            this._cogDisplayOCR.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this._cogDisplayOCR.MouseWheelSensitivity = 1D;
            this._cogDisplayOCR.Name = "_cogDisplayOCR";
            this._cogDisplayOCR.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("_cogDisplayOCR.OcxState")));
            this._cogDisplayOCR.Size = new System.Drawing.Size(736, 650);
            this._cogDisplayOCR.TabIndex = 6;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1499, 716);
            this.Controls.Add(this._cogDisplayOCR);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._cogDisplay);
            this.Controls.Add(this._cogDisplayGrab);
            this.Name = "frmMain";
            this.Text = "Derrortect";
            ((System.ComponentModel.ISupportInitialize)(this._cogDisplayGrab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._cogDisplay)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._cogDisplayOCR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGrab;
        private Cognex.VisionPro.Display.CogDisplay _cogDisplayGrab;
        private System.Windows.Forms.Button btnRecipeTest;
        private System.Windows.Forms.Button btnLive;
        private Cognex.VisionPro.Display.CogDisplay _cogDisplay;
        private System.Windows.Forms.Panel panel1;
        private Cognex.VisionPro.Display.CogDisplay _cogDisplayOCR;
    }
}

