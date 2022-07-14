using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace WindowsApp
{
    public partial class MainForm : Form
    {
        About about;
        Navigation navigation;
        Signal signal = Signal.GetInstance();
        IntervalOsc intervalOsc;
        DateTime dateStartOfSiganl;
        TimeSpan timaStartOfSignal;
        PointF[,] points;
        ToolStripMenuItem[] oscItems;
        ToolStripMenuItem[] statItems;
        ToolStripMenuItem[] fftItems;
        ToolStripMenuItem[] correlItem;
        ToolStripMenuItem[] spectralItem;
        ModelInputParams modellng;
        Super sup;

        public MainForm()
        {
            InitializeComponent();
            signal.MainForm = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IsMdiContainer = true;
        }

        private string ReplaceDot(string str)
        {
            return str.Replace(".", ",");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "help.chm");
        }

        private void оПрограммеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            about = new About();
            about.Show();
        }

        private void информацияОСигналеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            signal.SetSignalInfo();
            signal.SignalInformation.Show();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            signal.SetDelNav(false);
            Stream myStream;
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Открыть файл";
            theDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            theDialog.InitialDirectory = @"C:\Примеры"; //put your path to fold
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = theDialog.OpenFile()) != null)
                    {
                        signal.Path = theDialog.FileName;
                        using (myStream)
                        {
                            StreamReader reader = new StreamReader(theDialog.FileName, Encoding.GetEncoding(1251));
                            string line;
                            int row = 0;
                            while (row != 6)
                            {
                                line = reader.ReadLine().Trim();
                                if (line[0] != '#' && line.Length != 0)
                                {
                                    switch (row)
                                    {
                                        case 0:
                                            signal.CountOfChannels = Convert.ToInt32(line);
                                            break;
                                        case 1:
                                            signal.CountOfSamples = Convert.ToInt32(line);
                                            break;
                                        case 2:
                                            signal.Frequency = Convert.ToDouble(ReplaceDot(line));
                                            break;
                                        case 3:
                                            dateStartOfSiganl = Convert.ToDateTime(line);
                                            break;
                                        case 4:
                                            timaStartOfSignal = TimeSpan.Parse(line);
                                            dateStartOfSiganl += timaStartOfSignal;
                                            signal.DateBegin = dateStartOfSiganl;
                                            break;
                                        case 5:
                                            signal.Names = line.Split(new Char[] {';'});
                                            break;
                                    }

                                    row += 1;
                                }
                            }

                            signal.SetDateStartOfSignal();
                            points = new PointF[signal.CountOfChannels, signal.CountOfSamples];
                            row = 0;
                            while (!reader.EndOfStream)
                            {
                                line = reader.ReadLine().Trim();
                                String[] str = line.Split(new Char[] {' '});
                                int x = 0;
                                while (x != str.Length)
                                {
                                    points[x, row] = new PointF(row, Convert.ToSingle(ReplaceDot(str[x])));
                                    x += 1;
                                }

                                row += 1;
                            }

                            signal.WasNavigation = true;
                            signal.SetPoints(points);
                            reader.Close();
                            navigation = signal.Navigation;
                            if (navigation != null)
                            {
                                navigation.Close();
                                SigInfo.Enabled = false;
                            }

                            navigation = new Navigation(this);
                            navigation.MdiParent = this;
                            navigation.Show();
                        }

                        signal.CloseOscillogram();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        public void SignalInformation(bool flag)
        {
            SigInfo.Enabled = flag;
            save.Enabled = flag;
            арифметическаяСуперпозицияToolStripMenuItem.Enabled = flag;
            мультипликативнаяСуперпозицияToolStripMenuItem.Enabled = flag;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void CheckItem(int channelIndex)
        {
            oscItems[channelIndex].CheckState = CheckState.Checked;
        }

        public void UnCheckItem(int channelIndex)
        {
            oscItems[channelIndex].CheckState = CheckState.Unchecked;
        }

        public void UnCheckItem()
        {
            foreach (ToolStripMenuItem item in oscItems)
                item.CheckState = CheckState.Unchecked;
        }

        public void CheckItemCor(int channelIndex)
        {
            correlItem[channelIndex].CheckState = CheckState.Checked;
        }

        public void UnCheckItemCor(int channelIndex)
        {
            correlItem[channelIndex].CheckState = CheckState.Unchecked;
        }

        public void UnCheckItemCor()
        {
            foreach (ToolStripMenuItem item in correlItem)
                item.CheckState = CheckState.Unchecked;
        }

        public void CheckItemSp(int channelIndex)
        {
            spectralItem[channelIndex].CheckState = CheckState.Checked;
        }

        public void UnCheckItemSp(int channelIndex)
        {
            spectralItem[channelIndex].CheckState = CheckState.Unchecked;
        }

        public void UnCheckItemSp()
        {
            foreach (ToolStripMenuItem item in spectralItem)
                item.CheckState = CheckState.Unchecked;
        }

        public void CheckItemFFT(int channelIndex)
        {
            fftItems[channelIndex].CheckState = CheckState.Checked;
        }

        public void UnCheckItemDPF(int channelIndex)
        {
            fftItems[channelIndex].CheckState = CheckState.Unchecked;
        }

        public void UnCheckItemDPF()
        {
            foreach (ToolStripMenuItem item in fftItems)
                item.CheckState = CheckState.Unchecked;
        }

        public void CheckItemSt(int channelIndex)
        {
            statItems[channelIndex].CheckState = CheckState.Checked;
        }

        public void UnCheckItemStatistic(int channelIndex)
        {
            statItems[channelIndex].CheckState = CheckState.Unchecked;
        }

        public void UnCheckItemStatistic()
        {
            foreach (ToolStripMenuItem item in statItems)
                item.CheckState = CheckState.Unchecked;
        }

        public void CallItem()
        {
            осцилограммаToolStripMenuItem.DropDownItems.Clear();
            статистикаToolStripMenuItem.DropDownItems.Clear();
            дПToolStripMenuItem.DropDownItems.Clear();

            корреляционныйАнализToolStripMenuItem.DropDownItems.Clear();
            if (signal.Navigation != null)
            {
                oscItems = new ToolStripMenuItem[signal.Names.Length];
                statItems = new ToolStripMenuItem[signal.Names.Length];
                fftItems = new ToolStripMenuItem[signal.Names.Length];
                correlItem = new ToolStripMenuItem[signal.Names.Length];
                spectralItem = new ToolStripMenuItem[signal.Names.Length];
                for (int i = 0; i < signal.Names.Length; i++)
                {
                    oscItems[i] = new ToolStripMenuItem();
                    statItems[i] = new ToolStripMenuItem();
                    fftItems[i] = new ToolStripMenuItem();
                    correlItem[i] = new ToolStripMenuItem();
                    spectralItem[i] = new ToolStripMenuItem();
                    oscItems[i].Name = "dynamicItem" + signal.Names[i];
                    statItems[i].Name = "dynamicItem" + signal.Names[i];
                    fftItems[i].Name = "dynamicItem" + signal.Names[i];
                    correlItem[i].Name = "dynamicItem" + signal.Names[i];
                    spectralItem[i].Name = "dynamicItem" + signal.Names[i];
                    oscItems[i].Tag = i;
                    statItems[i].Tag = i;
                    fftItems[i].Tag = i;
                    correlItem[i].Tag = i;
                    spectralItem[i].Tag = i;
                    oscItems[i].Text = signal.Names[i];
                    statItems[i].Text = signal.Names[i];
                    fftItems[i].Text = signal.Names[i];
                    correlItem[i].Text = signal.Names[i];
                    spectralItem[i].Text = signal.Names[i];
                    oscItems[i].Click += MenuItemClickHandler;
                    statItems[i].Click += StatMenuItemClickHandler;
                    fftItems[i].Click += ДПТItemClickHandler;
                    correlItem[i].Click += CorrelItemClickHandler;
                }

                осцилограммаToolStripMenuItem.DropDownItems.AddRange(oscItems);
                статистикаToolStripMenuItem.DropDownItems.AddRange(statItems);
                дПToolStripMenuItem.DropDownItems.AddRange(fftItems);
                корреляционныйАнализToolStripMenuItem.DropDownItems.AddRange(correlItem);
            }
        }

        private void осцилограммаToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ДПТToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem) sender;
            if (clickedItem.CheckState.Equals(CheckState.Checked))
            {
                if (signal.Oscillogram.charts.Count() > 1)
                    for (int j = 0; j < signal.Oscillogram.charts.Count(); j++)
                    {
                        if (signal.Oscillogram.charts[j].Series[0].LegendText == clickedItem.Text)
                            signal.Oscillogram.remove(j); //передаём номер графика на удаление
                    }
                else
                    signal.Oscillogram.remove(0);
            }
            else
                signal.CreateOscillogram((int) clickedItem.Tag);
        }

        private void StatMenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem) sender;
            if (clickedItem.CheckState.Equals(CheckState.Checked))
            {
                if (signal.Statistic.charts.Count() > 1)
                    for (int j = 0; j < signal.Oscillogram.charts.Count(); j++)
                    {
                        if (signal.Oscillogram.charts[j].Series[0].LegendText == clickedItem.Text)
                            signal.Oscillogram.remove(j);
                    }
                else
                    signal.Oscillogram.remove(0);
            }
            else
                signal.CreateStatAsField((int) clickedItem.Tag);
            return;
            //TODO
            //ToolStripMenuItem clickedItem = (ToolStripMenuItem) sender;

            Statistic st = (Statistic) signal.GetHash("stat");
            if (clickedItem.CheckState.Equals(CheckState.Checked))
            {
                if (st != null)
                    if (st.charts.Count() > 1)
                        for (int j = 0; j < st.charts.Count(); j++)
                        {
                            if (st.charts[j].Series[0].LegendText == clickedItem.Text)
                                st.RemoveChart(j);
                        }
                    else
                        st.RemoveChart(0);
            }
            else
            {
                // signal.Statistic = new Statistic(this);
                // signal.Statistic.Init(0);
                // signal.Statistic.Show();
                signal.CreateStatistic((int) clickedItem.Tag);
            }
        }

        private void ДПТItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem) sender;

            if (clickedItem.CheckState.Equals(CheckState.Checked))
            {
                if (signal.Fft.charts.Count() > 1)
                    for (int i = 0; i < signal.Fft.charts.Count(); i++)
                    {
                        if (signal.Fft.charts[i][0].Series[0].LegendText == clickedItem.Text)
                            signal.Fft.remove(i);
                    }
                else
                    signal.Fft.remove(0);
            }
            else
                signal.CreateFFT((int) clickedItem.Tag);
        }

        private void CorrelItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem) sender;

            if (clickedItem.CheckState.Equals(CheckState.Checked))
            {
                if (signal.Correlation.order.Count() > 1)
                    for (int j = 0; j < signal.Correlation.order.Count(); j++)
                    {
                        if (signal.Correlation.order[j].Series[0].LegendText == clickedItem.Text)
                            signal.Correlation.remove(j);
                    }
                else
                    signal.Correlation.remove(0);
            }

            else
                signal.CreateCorrelation((int) clickedItem.Tag);
        }


        public void osc(bool k)
        {
            задатьДиапазонToolStripMenuItem.Enabled = k;
        }


        public void cor(bool k)
        {
            корреляционныйАнализToolStripMenuItem.Enabled = k;
        }

        public void SaveFlag(bool k)
        {
            save.Enabled = k;
        }

        private void задатьДиапазонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intervalOsc = new IntervalOsc();
            intervalOsc.Hide();
            intervalOsc.Show();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (signal.Navigation != null)
            {
                SaveOption saveOption = new SaveOption();
                saveOption.Show();
            }
        }

        private void modelling(object sender, EventArgs e)
        {
            delete_models();
            signal.Textmod = sender.ToString();

            modellng = new ModelInputParams();
            modellng.Show();
            signal.SetModelling(modellng);
        }

        private void арифметическаяСуперпозицияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete_models();
            signal.Textmod = sender.ToString();
            if (signal.CheckHash("super"))
            {
                sup = (Super) signal.GetHash("super");
                sup.Close();
            }

            sup = new Super();
            sup.Show();
            signal.SetHash("super", sup);
        }

        private void мультипликативнаяСуперпозицияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete_models();
            signal.Textmod = sender.ToString();
            sup = new Super();
            sup.Show();
            signal.SetHash("super", sup);
        }

        private void delete_models()
        {
            if (signal.CheckHash("super"))
            {
                sup = (Super) signal.GetHash("super");
                if (sup !=null)
                    sup.Close();
                signal.SetHash("super", null);
            }

            if (signal.ModelInputParams != null)
            {
                signal.ModelInputParams.Close();
                signal.SetModelling(null);
            }

            if (signal.Model != null)
            {
                signal.Model.Close();
                signal.SetModel(null);
            }
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void моделированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}