using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delta
{
    internal class InputDialog
    {

        /// <summary>
        /// 返回用户填写的字符串名称
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static DialogResult show(out string strText)
        {
            string strTemp = string.Empty;
            Show inputDialog = new Show();
            inputDialog.StartPosition = FormStartPosition.CenterParent;
            inputDialog.TextHandler = (str) => { strTemp = str; };
            DialogResult result = inputDialog.ShowDialog();
            strText = strTemp;
            return result;
        }

        public static DialogResult paraSet(ref float[] para)
        {
            float[] temp = new float[3];
            paraSet ParaDialog = new paraSet();
            ParaDialog.StartPosition = FormStartPosition.CenterParent;
            ParaDialog.TextHandler = (str) => { temp = str; };
            DialogResult result = ParaDialog.ShowDialog();
            Array.Copy(temp,para, 3);
            return result;
        }

    }
}
