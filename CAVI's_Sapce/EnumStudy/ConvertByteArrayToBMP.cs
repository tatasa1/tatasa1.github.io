using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnumStudy
{
    public partial class ConvertByteArrayToBMP : Form
    {
        public ConvertByteArrayToBMP()
        {
            InitializeComponent();
        }
        byte[] sample;
        private void button1_Click(object sender, EventArgs e)
        {
            sample = new Byte[16] {
                55 ,0  ,0  ,255,
                12 ,255,75 ,255,
                255,150,19 ,255,
                42 ,255,78 ,255 };
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SaveByteArryToBmp("test.bmp", sample, 4, 4);
        }

        public static void SaveByteArryToBmp(string imagePath, Byte[] data, int width, int height)
        {
            if (width * height != data.Length)
                throw new FormatException("Size does not match");

            Bitmap bmp = new Bitmap(width, height);

            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    Byte value = data[r * width + c];
                    bmp.SetPixel(c, r, Color.FromArgb(value, value, value));
                }
            }

            bmp.Save(imagePath);
        }
    }
}
