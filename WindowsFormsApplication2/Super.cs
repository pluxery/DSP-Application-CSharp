using System;
using System.Windows.Forms;


namespace WindowsApp
{
    public partial class Super : Form
    {
        Signal signal = Signal.GetInstance();
        ModelInputParams modellng;

        public Super()
        {
            InitializeComponent();
            checkedListBox1.Items.AddRange(signal.Names);
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }

            start.Text = "0";
            finish.Text = Convert.ToString(signal.CountOfSamples - 1);
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Не было выбрано ни одного канала");
            }
            else
            {
                signal.SetHash("super_c", checkedListBox1.CheckedItems.Count);
                signal.SetHash("super_ch", checkedListBox1);
                try
                {
                    signal.SetBeginRangeOsci(Convert.ToInt32(start.Text));
                    signal.SetEndRangeOsci(Convert.ToInt32(finish.Text));
                    if (signal.CheckHash("super_m"))
                    {
                        modellng = (ModelInputParams) signal.GetHash("super_m");
                        modellng.Close();
                    }

                    modellng = new ModelInputParams();
                    modellng.Show();
                    signal.SetHash("super_m", modellng);
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            start.Text = check(start.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            finish.Text = check(finish.Text);
        }

        //Проверка на граничные значения
        private String check(String s)
        {
            if (s == "")
                s = "0";
            else if (Convert.ToDouble(s) > (signal.CountOfSamples - 1))
                s = (signal.CountOfSamples - 1).ToString();
            return s;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            start.Text = "0";
            finish.Text = (signal.CountOfSamples).ToString();
        }
    }
}