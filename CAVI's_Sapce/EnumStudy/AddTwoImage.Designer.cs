namespace EnumStudy
{
    partial class AddTwoImage
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
            this.pBox1 = new System.Windows.Forms.PictureBox();
            this.pBox2 = new System.Windows.Forms.PictureBox();
            this.pBox3 = new System.Windows.Forms.PictureBox();
            this.btnOpenFilePath = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.tBoxRightOffset = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pBox1
            // 
            this.pBox1.Location = new System.Drawing.Point(13, 13);
            this.pBox1.Name = "pBox1";
            this.pBox1.Size = new System.Drawing.Size(185, 164);
            this.pBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pBox1.TabIndex = 0;
            this.pBox1.TabStop = false;
            // 
            // pBox2
            // 
            this.pBox2.Location = new System.Drawing.Point(223, 13);
            this.pBox2.Name = "pBox2";
            this.pBox2.Size = new System.Drawing.Size(183, 164);
            this.pBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pBox2.TabIndex = 1;
            this.pBox2.TabStop = false;
            // 
            // pBox3
            // 
            this.pBox3.Location = new System.Drawing.Point(15, 208);
            this.pBox3.Name = "pBox3";
            this.pBox3.Size = new System.Drawing.Size(391, 203);
            this.pBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pBox3.TabIndex = 2;
            this.pBox3.TabStop = false;
            // 
            // btnOpenFilePath
            // 
            this.btnOpenFilePath.Location = new System.Drawing.Point(431, 17);
            this.btnOpenFilePath.Name = "btnOpenFilePath";
            this.btnOpenFilePath.Size = new System.Drawing.Size(175, 65);
            this.btnOpenFilePath.TabIndex = 3;
            this.btnOpenFilePath.Text = "Open Path";
            this.btnOpenFilePath.UseVisualStyleBackColor = true;
            this.btnOpenFilePath.Click += new System.EventHandler(this.btnOpenFilePath_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(432, 186);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(174, 64);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(431, 100);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(175, 65);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save Path";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tBoxRightOffset
            // 
            this.tBoxRightOffset.Location = new System.Drawing.Point(432, 316);
            this.tBoxRightOffset.Name = "tBoxRightOffset";
            this.tBoxRightOffset.Size = new System.Drawing.Size(100, 21);
            this.tBoxRightOffset.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(430, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Right Offset";
            // 
            // AddTwoImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 423);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBoxRightOffset);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnOpenFilePath);
            this.Controls.Add(this.pBox3);
            this.Controls.Add(this.pBox2);
            this.Controls.Add(this.pBox1);
            this.Name = "AddTwoImage";
            this.Text = "AddTwoImage";
            ((System.ComponentModel.ISupportInitialize)(this.pBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pBox1;
        private System.Windows.Forms.PictureBox pBox2;
        private System.Windows.Forms.PictureBox pBox3;
        private System.Windows.Forms.Button btnOpenFilePath;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.TextBox tBoxRightOffset;
        private System.Windows.Forms.Label label1;
    }
}