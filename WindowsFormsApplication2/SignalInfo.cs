using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsApp
{
    public partial class SignalInfo : Form
    {
        Signal signal = Signal.GetInstance();

        public SignalInfo()
        {
            InitializeComponent();
        }

        private void Print_information(object sender, PaintEventArgs e)
        {
            Kan.Text = Convert.ToString(signal.CountOfChannels);
            Count.Text = Convert.ToString(signal.CountOfSamples);
            frdis.Text = Convert.ToString(signal.Frequency) + " Гц";
            begin.Text = signal.DateBegin.ToString("dd-MM-yyyy HH:mm:ss");
            signal.SetDateStartOfSignal();
            end.Text = signal.DateStartOfSignal.ToString("dd-MM-yyyy HH:mm:ss");
            Len.Text = Convert.ToString(signal.DateStartOfSignal.Subtract(signal.DateBegin).Days) + " суток, " +
                       Convert.ToString(signal.DateStartOfSignal.Subtract(signal.DateBegin).Hours) + " часов, " + "\n" +
                       Convert.ToString(signal.DateStartOfSignal.Subtract(signal.DateBegin).Minutes) + " минут, " +
                       Convert.ToString(signal.DateStartOfSignal.Subtract(signal.DateBegin).Seconds) + " секунд";
            listView1.Items.Clear();
            for (int i = 0; i < signal.CountOfChannels; i++)
            {
                listView1.Items.Add(Convert.ToString(i + 1));
                listView1.Items[i].SubItems.Add(Convert.ToString(signal.Names[i]));
                listView1.Items[i].SubItems.Add("Файл: " + Convert.ToString(Path.GetFileName(signal.Path)));


                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                this.TopMost = true;
            }
        }
    }
}