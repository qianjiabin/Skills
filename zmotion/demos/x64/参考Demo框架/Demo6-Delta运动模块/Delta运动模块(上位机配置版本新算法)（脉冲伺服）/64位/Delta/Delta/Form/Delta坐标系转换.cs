using cszmcaux;
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
    public partial class Delta坐标系转换 : UIPage
    {

        MainForm mainForm;

        float[] posA = new float[3];  //标定点位abcd，需要切换手系
        float[] posB = new float[3];
        float[] posC = new float[3];

        public Delta坐标系转换(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void Teach(object sender, EventArgs e)
        {
            int Id = 0;
            for (int i = 0; i < 4; i++)
            {
                if (((UISymbolButton)sender).Name == ("Teach" + i))
                {
                    Id = i;

                    switch (Id)
                    {
                        case 0:

                            for (int j = 0; j < 3; j++)
                            {
                                zmcaux.ZAux_Direct_GetMpos(mainForm.Delta.IntPtr, mainForm.Delta.vAxisList[j], ref posA[j]);                              
                                (this.uiPanel2.Controls["TX_Z" + j.ToString()].Text) = posA[j].ToString();
                            }
                           
                            break;

                        case 1:

                            for (int j = 0; j < 3; j++)
                            {
                                zmcaux.ZAux_Direct_GetMpos(mainForm.Delta.IntPtr, mainForm.Delta.vAxisList[j], ref posB[j]);                           
                                (this.uiPanel2.Controls["TX_X" + j.ToString()].Text) = posB[j].ToString();
                            }
                            zmcaux.ZAux_Direct_SetTable(mainForm.Delta.IntPtr, 105, 3, posB);
                            break;

                        case 2:

                            for (int j = 0; j < 3; j++)
                            {
                                zmcaux.ZAux_Direct_GetMpos(mainForm.Delta.IntPtr, mainForm.Delta.vAxisList[j], ref posC[j]);               
                                (this.uiPanel2.Controls["TX_Y" + j.ToString()].Text) = posC[j].ToString();
                            }
                            zmcaux.ZAux_Direct_SetTable(mainForm.Delta.IntPtr, 110, 3, posC);
                            break;

                    }

                }
            }
        }

        private void Btn_Compute_Click(object sender, EventArgs e)
        {
            int _res;

            float[] ConversionValue = new float[6];  //Delta机械手坐标系转换输出后的参数

            _res = mainForm.Delta.Demarcate(posA, posB,posC, ref ConversionValue);
            if (_res != 0)
            {
                MessageBox.Show("计算生成参数失败");
            }

            try
            {
                this.Invoke(new Action(() =>
                {
                             
                    for (int i = 0; i < 6; i++)
                    {
                        (this.uiPanel3.Controls["TX_Offset" + i.ToString()].Text) = ConversionValue[i].ToString();
                    }

                }));
            }
            catch (Exception ex)
            {

            }

        }

        private void Btn_effect_Click(object sender, EventArgs e)
        {
            int _res;

            _res = mainForm.Delta.Conversion(1);
            if (_res != 0)
            {
                MessageBox.Show("坐标系切换失败");
            }
            else
            {
                MessageBox.Show("坐标系切换成功");
            }
        }

        private void Btn_recover_Click(object sender, EventArgs e)
        {
            int _res;

            _res = mainForm.Delta.Conversion(0);
            if (_res != 0)
            {
                MessageBox.Show("坐标系恢复失败");
            }
            else
            {
                MessageBox.Show("坐标系恢复成功");
            }
        }
    }
}
