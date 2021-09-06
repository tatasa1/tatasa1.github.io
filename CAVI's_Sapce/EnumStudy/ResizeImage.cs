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
    public partial class ResizeImage : Form
    {
        public ResizeImage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String openPath="D:";
            Bitmap sr = new Bitmap(openPath);

            int width=0, height = 0;
            Size resize = new Size(width, height);
            Bitmap resizeImage = new Bitmap(sr, resize);
        }
    }
}
