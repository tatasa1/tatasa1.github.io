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
    public partial class PropertyStudy : Form
    {
        ClsStudy clsStudy = new ClsStudy();

        public PropertyStudy()
        {
            InitializeComponent();
        }
        private void PropoertyStudy_Load(object sender, EventArgs e)
        {
            timerRunMethod.Interval = 2000;
            timerRunMethod.Start();
        }

        private void timerRunMethod_Tick(object sender, EventArgs e)
        {
            if(int.TryParse(tBoxInputNum.Text,out int Num))
            {
                clsStudy.studyMemberCount = Num;
                lblResult.Text = $"{clsStudy.studyMemberCount} 의 사람이 스터디에 가입되어 있습니다.";
            }
            else
            {
                lblResult.Text = "수를 입력해주세요";
            }
        }
    }

    public class ClsStudy
    {
        public int studyMemberCount
        {
            get;set;
        }

        public string studyMemberName   // get 만 사용하면 읽기전용 으로 사용하는 효과, set만 있으면 쓰기전용 (쓰기전용은 용도와 동작 결과를 명확하게 표기)
        {
            get;
        }

    }
}
