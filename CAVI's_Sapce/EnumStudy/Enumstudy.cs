using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnumStudy
{
    public partial class Enumstudy : Form
    {
        public enum eMenuChanged : int {_Open =1, _Save, _Delete, _Exit};

        public Enumstudy()
        {
            InitializeComponent();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            var _btn = sender as Button;
            int _tag = Convert.ToInt32(_btn.Tag);
            this.Cursor = Cursors.WaitCursor;
            switch ((eMenuChanged)_tag)
            {
                case eMenuChanged._Open:
                    Thread.Sleep(500);
                    labelResult.Text = "Open이 눌렸습니다!";
                    break;
                case eMenuChanged._Save:
                    Thread.Sleep(500);
                    labelResult.Text = "Save가 눌렸습니다!";
                    break;
                case eMenuChanged._Delete:
                    Thread.Sleep(500);
                    labelResult.Text = "Delete가 눌렸습니다!";
                    break;
                case eMenuChanged._Exit:
                    Thread.Sleep(500);
                    labelResult.Text = "Exit가 눌렸습니다! 안녕히 계세요 여러분";
                    break;
            }
            this.Cursor = Cursors.Default;
        }

    }
}
