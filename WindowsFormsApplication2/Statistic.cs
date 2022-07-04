using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsApp
{
    public partial class Statistic : Form
    {
        public List<Chart> charts = new List<Chart>(); //порядок графиков
        int countInterval;
        int[] Histogram;
        Signal signal = Signal.GetInstance();
        public List<int> channels = new List<int>();
        Chart chart; 
        private int X1;
        private int Y1;
        private int X2;
        private int Y2;
        List<Label>[] labels = new List<Label>[Signal.GetInstance().CountOfChannels];
        List<Label>[] texts = new List<Label>[Signal.GetInstance().CountOfChannels];
        int margin_label = 22; 
        int W = 400; 
        int H = 200;

        public Statistic(MainForm ParrentForm)
        {
            InitializeComponent();
            BackColor = Color.White;
        }

        public void Init(int n)
        {
            if (!channels.Contains(n))
            {
                Object currentK = signal.GetHash("stat_k");
                if (currentK == null || !int.TryParse(currentK.ToString(), out countInterval))
                {
                    countInterval = 100;
                    signal.SetHash("stat_k", countInterval);
                }

                double min = signal.Min(n, 0, signal.CountOfSamples);
                double max = signal.Max(n, 0, signal.CountOfSamples);

                Histogram = new int[countInterval];
                double range = max - min;
                double p = range / (countInterval - 3);
                for (int i = 0; i < countInterval; i++) Histogram[i] = 0;

                double average = 0;
                var sortedchannelValuesY = new List<double>();
                for (int i = 0; i < signal.CountOfSamples; i++)
                {
                    average += signal.Points[n, i].Y;
                    int interval = (int) ((signal.Points[n, i].Y - min) / p);
                    Histogram[interval] += 1;

                    sortedchannelValuesY.Add(signal.Points[n, i].Y);
                }

                average /= signal.CountOfSamples;

                sortedchannelValuesY.Sort();
                double median = sortedchannelValuesY[signal.CountOfSamples / 2];
                int index_95 = Convert.ToInt32(signal.CountOfSamples * 95 / 100);
                double quantile_95 = sortedchannelValuesY[index_95];
                int index_05 = Convert.ToInt32(signal.CountOfSamples * 5 / 100);
                double quantile_05 = sortedchannelValuesY[index_05];

                CreateChart(Histogram.Min(), Histogram.Max(), n);
                charts.Add(chart);

                double σ2 = 0;
                double σ3 = 0;
                double σ4 = 0;
                for (int i = 0; i < signal.CountOfSamples; i++)
                {
                    σ2 += Math.Pow(signal.Points[n, i].Y - average, 2);
                    σ3 += Math.Pow(signal.Points[n, i].Y - average, 3);
                    σ4 += Math.Pow(signal.Points[n, i].Y - average, 4);
                }

                σ2 /= signal.CountOfSamples;
                σ3 /= signal.CountOfSamples;
                σ4 /= signal.CountOfSamples;
                double σ = Math.Pow(σ2, 0.5);
                double r = σ / average;
                σ3 /= Math.Pow(σ, 3);
                σ4 /= Math.Pow(σ, 4);
                σ4 -= 3;

                for (int i = 0; i < countInterval; i++)
                    chart.Series[0].Points.AddXY(i, Histogram[i]);

                channels.Add(n);
                labels[channels.Count - 1] = new List<Label>();
                texts[channels.Count - 1] = new List<Label>();
                CreateTextLabel("1) Среднее =", String.Format("{0:0.000}", average), channels.Count - 1);
                CreateTextLabel("2) Дисперсия =", String.Format("{0:0.000}", σ2), channels.Count - 1);
                CreateTextLabel("3) Среднеквадратичное отклонение =", String.Format("{0:0.000}", σ), channels.Count - 1);
                CreateTextLabel("4) Коэффициент вариации =", String.Format("{0:0.000}", r), channels.Count - 1);
                CreateTextLabel("5) Коэффициент асимметрии =", String.Format("{0:0.000}", σ3), channels.Count - 1);
                CreateTextLabel("6) Коэффициент эксцесса =", String.Format("{0:0.000}", σ4), channels.Count - 1);
                CreateTextLabel("7) Минимальное значение сигнала =", String.Format("{0:0.000}", min),
                    channels.Count - 1);
                CreateTextLabel("8) Максимальное значение сигнала =", String.Format("{0:0.000}", max),
                    channels.Count - 1);
                CreateTextLabel("9) Квантиль порядка 0.05 =", String.Format("{0:0.000}", quantile_05),
                    channels.Count - 1);
                CreateTextLabel("10) Квантиль порядка 0.95 =", String.Format("{0:0.000}", quantile_95),
                    channels.Count - 1);
                CreateTextLabel("11) Медиана (квантиль порядка 0.5) =", String.Format("{0:0.000}", median),
                    channels.Count - 1);

                chart.MouseDown += position1;
                chart.MouseUp += position2;
                SetLocation();
                chart.AxisScrollBarClicked += scroller;
                chart.AxisViewChanged += viewchanged;
            }
            else
            {
                MessageBox.Show("График уже добавлен");
            }
        }

        private void CreateTextLabel(string name, string text, int n)
        {
            labels[n].Add(new Label());
            int j = labels[n].Count - 1;
            labels[n][j].AutoSize = true;
            labels[n][j].Name = name;
            labels[n][j].Text = name;
            labels[n][j].Margin = new Padding(2, 0, 2, 0);
            labels[n][j].TextAlign = ContentAlignment.MiddleLeft;
            Controls.Add(labels[n][j]);

            texts[n].Add(new Label());
            int k = texts[n].Count - 1;
            texts[n][k].Name = name;
            texts[n][k].Width = 300;
            var fill = "                      ";
            texts[n][k].Text = fill + fill + text;
            texts[n][k].Margin = new Padding(2, 0, 2, 0);
            Controls.Add(texts[n][texts[n].Count - 1]);
        }
        
        private void CreateChart(double min, double max, int n)
        {
            chart = new Chart();
            chart.Parent = this;
            ChartArea area = new ChartArea();
            area.Name = "myGraph";
            area.BorderDashStyle = ChartDashStyle.Solid;
            area.BorderColor = Color.Black;
            area.BorderWidth = 1;
            area.AxisY.Minimum = min;
            area.AxisY.Maximum = max;
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = countInterval;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisX.Enabled = AxisEnabled.False;
            area.AxisY.Enabled = AxisEnabled.False;
            chart.ChartAreas.Add(area);
            Series series1 = new Series();
            series1.ChartArea = "myGraph";
            series1.ChartType = SeriesChartType.Column;
            series1.Color = Color.Green;
            series1.LegendText = signal.Names[n];
            chart.Legends.Add(signal.Names[n]);
            chart.Legends[signal.Names[n]].Docking = Docking.Bottom;
            chart.Series.Add(series1);
        }

        private void position1(object sender, MouseEventArgs e)
        {
            X1 = e.X;
            Y1 = e.Y;
            if (e.Button == MouseButtons.Right)
            {
                int zq = e.Y / ((labels[0].Count + 1) * margin_label + H);
                chart = charts[zq];
            }
        }

        private void position2(object sender, MouseEventArgs e)
        {
            X2 = e.X;
            Y2 = e.Y;
        }

        private void scroller(object sender, ScrollBarEventArgs e)
        {
            signal.SetBeginRangeOsci(e.ChartArea.AxisX.ScaleView.Position);
            signal.SetEndRangeOsci(e.ChartArea.AxisX.ScaleView.Position + e.ChartArea.AxisX.ScaleView.Size);
        }

        private void viewchanged(object sender, ViewEventArgs e)
        {
            signal.SetBeginRangeOsci(e.ChartArea.AxisX.ScaleView.Position);
            signal.SetEndRangeOsci(e.ChartArea.AxisX.ScaleView.Position + e.ChartArea.AxisX.ScaleView.Size);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int channelIndex;
            for (channelIndex = 0; !chart.Equals(charts[channelIndex]); channelIndex++)
            {
            }

            remove(channelIndex);
        }

        public void remove(int k)
        {
            if (charts.Count == 1)
                Close();
            else
            {
                charts[k].Visible = false;
                charts[k].Dispose();
                charts.RemoveAt(k);
                SetLocation();
                signal.MainForm.UnCheckItemSt(channels[k]);
                channels.RemoveAt(k);
            }
        }

        private void SetLocation()
        {
            for (int i = 0; i < charts.Count; i++)
            {
                for (int j = 0; j < labels[i].Count; j++)
                {
                    labels[i][j].Location =
                        new Point(14,
                            (labels[0].Count + 1) * margin_label * i + H * i + 11 + j * margin_label);
                    texts[i][j].Location =
                        new Point(14 + 100,
                            (texts[0].Count + 1) * margin_label * i + H * i + 11 + j * margin_label);
                }

                charts[i].Bounds = new Rectangle(0, (labels[i].Count + 1) * margin_label * (i + 1) + H * i, W, H);
            }

            Width = W + 16;
            Height = ((labels[0].Count + 1) * margin_label + H) * charts.Count + 40;
        }


        public void close(object sender, FormClosedEventArgs e)
        {
            signal.MainForm.UnCheckItemSt();
            signal.SetHash("stat", null);
        }

        private void resize(object sender, EventArgs e)
        {
            if (charts.Count > 0)
            {
                W = Width - 16;
                if (Height > ((labels[0].Count + 1) * margin_label) * charts.Count + 50 * charts.Count + 40)
                    H = (Height - 40) / charts.Count - (labels[0].Count + 1) * margin_label;
                else
                    H = 50;
                SetLocation();
            }
        }
    }
}