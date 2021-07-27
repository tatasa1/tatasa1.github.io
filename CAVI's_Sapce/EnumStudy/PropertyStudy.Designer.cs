namespace EnumStudy
{
    partial class PropertyStudy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tBoxInputNum = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.timerRunMethod = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tBoxInputNum
            // 
            this.tBoxInputNum.Location = new System.Drawing.Point(36, 48);
            this.tBoxInputNum.Name = "tBoxInputNum";
            this.tBoxInputNum.Size = new System.Drawing.Size(100, 21);
            this.tBoxInputNum.TabIndex = 0;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(34, 93);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(85, 12);
            this.lblResult.TabIndex = 1;
            this.lblResult.Text = "결과 표시 라벨";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(34, 19);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(309, 12);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "속성 테스트     아래 텍스트박스에 숫자를 입력해 보세요";
            // 
            // timerRunMethod
            // 
            this.timerRunMethod.Tick += new System.EventHandler(this.timerRunMethod_Tick);
            // 
            // PropoertyStudy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 128);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.tBoxInputNum);
            this.Name = "PropoertyStudy";
            this.Text = "PropoertyStudy";
            this.Load += new System.EventHandler(this.PropoertyStudy_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tBoxInputNum;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Timer timerRunMethod;
    }
}