using System;
using System.Windows.Forms;

namespace WindowsApp
{
    public partial class FFTMode : Form
    {
        public int selectedBox;
        Signal signal = Signal.GetInstance();

        public FFTMode()
        {
            InitializeComponent();
            checkedListBox1.CheckOnClick = true;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            try
            {
                var checkedIndices = checkedListBox1.CheckedIndices;
                selectedBox = checkedIndices[0];
                signal.FFTMode = selectedBox;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                MessageBox.Show("Выберете режим.");
            }
        }
    }
}