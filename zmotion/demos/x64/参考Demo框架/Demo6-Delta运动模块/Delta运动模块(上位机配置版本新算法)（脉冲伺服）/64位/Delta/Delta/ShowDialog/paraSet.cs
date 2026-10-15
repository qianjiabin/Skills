using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delta
{
    public partial class paraSet : UIForm
    {
        public paraSet()
        {
            InitializeComponent();

            this.TopLevel = true;
        }

        public float[] value = new float[3];

        public delegate void TextEventHandler(float[] para);

        public TextEventHandler TextHandler;

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (null != TextHandler)
            {
                if (RB_L.Checked == true)
                {
                    value[0] = 0;
                }
                else
                {
                    value[0] = 1;
                }

                value[1] = Convert.ToSingle(TX_Speed.Text);
                value[2] = Convert.ToSingle(TX_Accel.Text);
                TextHandler.Invoke(value);
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }


        private void RB_L_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_L.Checked == true)
            {
                RB_J.Checked = false;
            }
        }

        private void RB_J_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_J.Checked == false)
            {
                RB_L.Checked = true;
            }
        }
    }
}
