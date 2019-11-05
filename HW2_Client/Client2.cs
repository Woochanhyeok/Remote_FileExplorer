using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW2_Client
{
    public partial class ClientForm2 : Form
    {
        public ClientForm2(ListViewItem listviewitem)
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            textBox1.Text = listviewitem.SubItems[0].Text;

            if (listviewitem.SubItems[0].Text.Contains(".avi"))
            {
                label2.Text = "avi";
                pictureBox1.Image = HW2_Client.Properties.Resources.avi;
            }
            else if (listviewitem.SubItems[0].Text.Contains(".png"))
            {
                label2.Text = "png";
                pictureBox1.Image = HW2_Client.Properties.Resources.image;
            }
            else if (listviewitem.SubItems[0].Text.Contains(".mp3"))
            {
                label2.Text = "mp3";
                pictureBox1.Image = HW2_Client.Properties.Resources.music;
            }
            else if (listviewitem.SubItems[0].Text.Contains(".txt"))
            {
                label2.Text = "txt";
                pictureBox1.Image = HW2_Client.Properties.Resources.text;
            }
            else
            {
                label2.Text = "temp";
                pictureBox1.Image = HW2_Client.Properties.Resources.temp;
            }
            label4.Text = listviewitem.SubItems[5].Text;
            label6.Text = listviewitem.SubItems[1].Text+" 바이트";
            label8.Text = listviewitem.SubItems[4].Text;
            label10.Text = listviewitem.SubItems[2].Text;
            label12.Text = listviewitem.SubItems[3].Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
