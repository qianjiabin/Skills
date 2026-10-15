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
    public partial class Show : UIForm
    {
        public Show()
        {
            InitializeComponent();
            this.TopLevel=true;
        }

        public delegate void TextEventHandler(string strText);

        public TextEventHandler TextHandler;

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (null != TextHandler)
            {  
                if(txtString.Text ==string.Empty)
                {
                    MessageBox.Show("请填写正确的名称");
                    return;
                }
                TextHandler.Invoke(txtString.Text);
                DialogResult = DialogResult.OK;
            }          
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtString_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Keys.Enter == (Keys)e.KeyChar)
            {
                if (null != TextHandler)
                {
                    TextHandler.Invoke(txtString.Text);
                    DialogResult = DialogResult.OK;
                }                  
            }
              
        }
    }
}
