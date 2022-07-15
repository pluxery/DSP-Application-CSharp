using System;
using System.Windows.Forms;

namespace WindowsApp
{
    public partial class IntervalFFT : Form
    {
        Signal signal = Signal.GetInstance();

        public IntervalFFT()
        {
            InitializeComponent();
            textBoxStart.Text = Convert.ToString(signal.BeginRangeFft);
            textBoxFinish.Text = Convert.ToString(signal.EndRangeFft);
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }

        private string ReplaceDot(string str)
        {
            return str.Replace(".", ",");
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            try
            {
                double startVal = Convert.ToDouble(ReplaceDot(textBoxStart.Text));
                double endVal = Convert.ToDouble(ReplaceDot(textBoxFinish.Text));
                if (startVal < 0 || endVal < 0)
                {
                    MessageBox.Show("Значенние диапозона должно быть > 0");
                    if (startVal < 0) textBoxStart.Text = "0";
                    if (endVal < 0) textBoxFinish.Text = Convert.ToString(signal.Frequency/2);
                }
                else
                {
                    signal.BeginRangeFft = startVal;
                    signal.SetEndRangeFFT(endVal);
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Введите корректные данные.\n");
                textBoxStart.Text = "0";
                textBoxFinish.Text = Convert.ToString(signal.Frequency/2);
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void all_btn_Click(object sender, EventArgs e)
        {
            textBoxStart.Text = "0";
            textBoxFinish.Text = Convert.ToString(signal.Frequency/2);
        }
    }
}