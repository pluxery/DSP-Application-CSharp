using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsApp
{
    public partial class ModelImage : Form
    {
        public ModelImage(int modelNumber)
        {
            InitializeComponent();
            string num = Convert.ToString(modelNumber);
            PictureBox pictureBox = new PictureBox();
            pictureBox.Location = new Point(0, 0);
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            //put your path to fold
            string path =
                "C:\\Users\\goul\\Desktop\\my dsp\\DSP-master\\WindowsFormsApplication2\\model_img\\Screenshot_" + num +
                ".png";
            pictureBox.Image = Image.FromFile(path);
            pictureBox.Visible = true;
            Controls.Add(pictureBox);
        }
    }
}