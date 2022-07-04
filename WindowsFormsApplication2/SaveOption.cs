using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


// выбор отсчётов                                                   //done // need test
// изменение даты и времени начала записи сигнала                   //done // need test
// убирать окно после сохранения                                    //хайдится, затем закрывается
// сохранение поверх окна с чеклистом (прям по центру)
// сделать недоступным сохранение из меню, когда не открыт файл     //done //need test //мб нужна доработка при создании смоделированных сигналов
// добавить кнопки для чека/анчека всех пунктов
namespace WindowsApp
{
    public partial class SaveOption : Form
    {
        Signal signal = Signal.GetInstance();

        public SaveOption()
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
        
        private string ReplaceDot(string s) 
        {
            return s.Replace(",", ".");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Не было выбрано ни одного канала");
            }
            else
            {
                String str = "";
                SaveFileDialog theDialog = new SaveFileDialog();

                theDialog.Title = "Сохранить файл";
                theDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                theDialog.FilterIndex = 1;
                theDialog.InitialDirectory = signal.Path;
                theDialog.RestoreDirectory = true;
                signal.Path = theDialog.FileName;
                this.Hide();

                if (theDialog.ShowDialog() == DialogResult.OK) //если выбрано ОК, то
                {
                    try
                    {
                        //тут будет инциализация данный
                        StreamWriter sr = new StreamWriter(theDialog.FileName, false, Encoding.GetEncoding(1251));
                        sr.WriteLine("# channels number");
                        sr.WriteLine(checkedListBox1.CheckedItems.Count);
                        sr.WriteLine("# samples number");
                        sr.WriteLine(Convert.ToInt32(finish.Text) - Convert.ToInt32(start.Text) + 1);
                        sr.WriteLine("# sampling rate");
                        sr.WriteLine(ReplaceDot(signal.Frequency.ToString()));
                        sr.WriteLine("# start date");
                        sr.WriteLine(signal.DateBegin
                            .Add(TimeSpan.FromSeconds((1 / signal.Frequency) * Convert.ToInt32(start.Text)))
                            .ToString("dd-MM-yyyy"));
                        sr.WriteLine("# start time");
                        sr.WriteLine(signal.DateBegin
                            .Add(TimeSpan.FromSeconds((1 / signal.Frequency) * Convert.ToInt32(start.Text)))
                            .ToString("HH:mm:ss.fff"));
                        sr.WriteLine("# channels names");
                        sr.WriteLine(String.Join(";", checkedListBox1.CheckedItems.OfType<string>()));
                        // Code to write the stream goes here.(
                        for (int i = Convert.ToInt32(start.Text); i <= Convert.ToInt32(finish.Text); i++)
                        {
                            for (int j = 0; j < signal.CountOfChannels; j++)
                            {
                                if (checkedListBox1.GetItemChecked(j) == true)
                                {
                                    str = String.Format(str + " " + ReplaceDot(signal.Points[j, i].Y.ToString()));
                                }
                            }

                            sr.WriteLine(str.Trim());
                            str = "";
                        }

                        sr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Произошла ошибка");
                    }

                    this.Close();
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