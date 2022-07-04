using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsApp
{
    public partial class SmoothDialog : Form
    {
        public SmoothDialog()
        {
            InitializeComponent();
            textBox1.Text = 0.ToString();
        }

        private bool check_L_value(string s)
        {
            long number;
            if (Int64.TryParse(s, out number) && Convert.ToInt64(s) >= 0 &&
                Convert.ToInt64(s) <= Signal.GetInstance().CountOfSamples)
                return true;
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!check_L_value(textBox1.Text))
            {
                MessageBox.Show("Ошибка!");
            }
            else
            {
                Signal.GetInstance().Smooth = Convert.ToInt32(textBox1.Text);
            }
        }
    }
}