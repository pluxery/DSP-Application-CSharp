using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsApp
{
    public partial class Statistic : Form
    {
        public List<Chart> charts = new List<Chart>();
        private int countInterval = 100;
        private int[] Histogram;
        private Signal signal = Signal.GetInstance();
        public static List<int> ChannelIndexList = new List<int>();
        private List<Label> LabelList = new List<Label>();
        private List<Label> TextList = new List<Label>();
        private Chart chart;
        private int curChannelIdx;
        private int X1;
        private int Y1;
        private int X2;
        private int Y2;
        private int margin_label = 22;
        private int W = 400;
        private int H = 200;

        public Statistic(MainForm ParrentForm)
        {
            InitializeComponent();
        }

        public void Update()
        {   Clear();
            ComputeStatictic(curChannelIdx, signal.BeginRangeOsci, signal.EndRangeOsci);
        }

        private void Clear()
        {
            LabelList.Clear();
            TextList.Clear();
            Controls.Clear();
        }
        public void Init(int channelIndex)
        {
            if (!ChannelIndexList.Contains(channelIndex))
            {
                Text = signal.Names[channelIndex] + " Статистика";
                curChannelIdx = channelIndex;
                ComputeStatictic(channelIndex, signal.BeginRangeOsci, signal.EndRangeOsci);
                signal.StatisticList.Add(this);
            }
            else
            {   
                var err = new Label();
                err.Text = $"Окно статистики сигнала {signal.Names[channelIndex]} уже открыто!";
                err.AutoSize = true;
                err.Padding = new Padding(100, 100, 100, 100);
                err.Location = new Point(0, 0);
                Controls.Add(err);
            }
        }

        private void ComputeStatictic(int channelIndex, int startIndex, int endIndex)
        {
            int lenght = endIndex - startIndex;
            double min = signal.Min(channelIndex, startIndex, endIndex);
            double max = signal.Max(channelIndex, startIndex, endIndex);

            Histogram = new int[countInterval];
            double range = max - min;
            double bucket = range / (countInterval - 3);
            for (int i = 0; i < countInterval; i++) Histogram[i] = 0;

            double average = 0;
            var sortedValuesY = new List<double>();
            for (int i = startIndex; i < endIndex; i++)
            {
                average += signal.Points[channelIndex, i].Y;
                int interval = (int) ((signal.Points[channelIndex, i].Y - min) / bucket);
                Histogram[interval] += 1;

                sortedValuesY.Add(signal.Points[channelIndex, i].Y);
            }

            average /= lenght;

            sortedValuesY.Sort();
            double median = sortedValuesY[(int) (lenght / 2)];
            int index_95 = Convert.ToInt32(lenght * 95 / 100);
            double quantile_95 = sortedValuesY[index_95];
            int index_05 = Convert.ToInt32(lenght * 5 / 100);
            double quantile_05 = sortedValuesY[index_05];

            CreateChart(Histogram.Min(), Histogram.Max(), channelIndex);
            charts.Add(chart);

            double σ2 = 0;
            double σ3 = 0;
            double σ4 = 0;
            for (int i = startIndex; i < endIndex; i++)
            {
                σ2 += Math.Pow(signal.Points[channelIndex, i].Y - average, 2);
                σ3 += Math.Pow(signal.Points[channelIndex, i].Y - average, 3);
                σ4 += Math.Pow(signal.Points[channelIndex, i].Y - average, 4);
            }

            σ2 /= lenght;
            σ3 /= lenght;
            σ4 /= lenght;
            double σ = Math.Pow(σ2, 0.5);
            double r = σ / average;
            σ3 /= Math.Pow(σ, 3);
            σ4 /= Math.Pow(σ, 4);
            σ4 -= 3;

            for (int i = 0; i < countInterval; i++)
                chart.Series[0].Points.AddXY(i, Histogram[i]);

            ChannelIndexList.Add(channelIndex);

            CreateTextLabel("1) Среднее =", String.Format("{0:0.000}", average));
            CreateTextLabel("2) Дисперсия =", String.Format("{0:0.000}", σ2));
            CreateTextLabel("3) Среднеквадратичное отклонение =", String.Format("{0:0.000}", σ));
            CreateTextLabel("4) Коэффициент вариации =", String.Format("{0:0.000}", r));
            CreateTextLabel("5) Коэффициент асимметрии =", String.Format("{0:0.000}", σ3));
            CreateTextLabel("6) Коэффициент эксцесса =", String.Format("{0:0.000}", σ4));
            CreateTextLabel("7) Минимальное значение сигнала =", String.Format("{0:0.000}", min));
            CreateTextLabel("8) Максимальное значение сигнала =", String.Format("{0:0.000}", max));
            CreateTextLabel("9) Квантиль порядка 0.05 =", String.Format("{0:0.000}", quantile_05));
            CreateTextLabel("10) Квантиль порядка 0.95 =", String.Format("{0:0.000}", quantile_95));
            CreateTextLabel("11) Медиана (квантиль порядка 0.5) =", String.Format("{0:0.000}", median));

            chart.Bounds = new Rectangle(0, (LabelList.Count + 1) * margin_label, W, H);
        }

        private void CreateTextLabel(string channelName, string statisticValue)
        {
            LabelList.Add(new Label());
            int last = LabelList.Count - 1;
            LabelList[last].AutoSize = true;
            LabelList[last].Location = new Point(14, margin_label * last);
            LabelList[last].Name = channelName;
            LabelList[last].Text = channelName;
            LabelList[last].Margin = new Padding(2, 0, 2, 0);
            LabelList[last].TextAlign = ContentAlignment.MiddleLeft;
            Controls.Add(LabelList[last]);

            TextList.Add(new Label());
            TextList[last].Location = new Point(14 + 100, margin_label * last);
            last = TextList.Count - 1;
            TextList[last].Name = channelName;
            TextList[last].Width = 300;
            var fill = "                      ";
            TextList[last].Text = fill + fill + statisticValue;
            TextList[last].Margin = new Padding(2, 0, 2, 0);
            Controls.Add(TextList[last]);
        }

        private void CreateChart(double minY, double maxY, int channelIndex)
        {
            chart = new Chart();
            chart.Parent = this;
            chart.MinimumSize = new Size(200, 200);
            signal.MainForm.CheckItemSt(channelIndex);
            ChartArea area = new ChartArea();
            area.Name = "myGraph";
            area.BorderDashStyle = ChartDashStyle.Solid;
            area.BorderColor = Color.Black;
            area.BorderWidth = 1;
            area.AxisY.Minimum = minY;
            area.AxisY.Maximum = maxY;
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
            series1.LegendText = signal.Names[channelIndex];
            chart.Legends.Add(signal.Names[channelIndex]);
            chart.Legends[signal.Names[channelIndex]].Docking = Docking.Bottom;
            chart.Series.Add(series1);
        }

        private void position1(object sender, MouseEventArgs e)
        {
            X1 = e.X;
            Y1 = e.Y;
            if (e.Button == MouseButtons.Right)
            {
                int zq = e.Y / ((LabelList.Count + 1) * margin_label + H);
                chart = charts[zq];
            }
        }

        private void position2(object sender, MouseEventArgs e)
        {
            X2 = e.X;
            Y2 = e.Y;
        }
        

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int channelIndex;
            for (channelIndex = 0; !chart.Equals(charts[channelIndex]); channelIndex++)
            {
            }

            RemoveChart(channelIndex);
        }

        public void RemoveChart(int channelIndex)
        {
            if (charts.Count == 1)
                Close();
            else
            {
                charts[channelIndex].Visible = false;
                charts[channelIndex].Dispose();
                charts.RemoveAt(channelIndex);
                signal.MainForm.UnCheckItemStatistic(ChannelIndexList[channelIndex]);
                ChannelIndexList.RemoveAt(channelIndex);
            }
        }
        
        public void close(object sender, FormClosedEventArgs e)
        {
            signal.MainForm.UnCheckItemStatistic();
            ChannelIndexList.Remove(curChannelIdx);
            signal.StatisticList.Remove(this);
            //TODO Где-то остаются ссылки и не удаляется статистика из памяти

        }

        private void resize(object sender, EventArgs e)
        {
            if (charts.Count > 0)
            {
                W = Width - 16;
                if (Height > ((LabelList.Count + 1) * margin_label) * charts.Count + 50 * charts.Count + 40)
                    H = (Height - 40) / charts.Count - (LabelList.Count + 1) * margin_label;
                else
                    H = 50;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }
    }
}