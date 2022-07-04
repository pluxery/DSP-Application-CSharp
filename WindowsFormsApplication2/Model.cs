using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace WindowsApp 
{
    public partial class Model : Form
    {
        bool localScaleMode = false;
        bool GridMode = true;
        bool markerMode = true;

        public PointF[] data;
        Random rand = new Random();

        List<TextBox> form; //принимает информацию из текстбоксов при моделировании

        Signal signal = Signal.GetInstance();

        List<Chart> charts = new List<Chart>();
        Chart chart;
        List<int> channelsList = new List<int>();
        private int X1;
        private int Y1;
        private int X2;
        private int Y2;
        int prob = 30;
        int W = 700;
        int H = 150;
        IntervalOsc inter;


        public Model(MainForm ParrentForm)
        {
            InitializeComponent();
        }

        private void resize(object sender, EventArgs e)
        {
            if (charts.Count > 0)
            {
                W = Width;
                H = (Height - 40 - prob) / charts.Count;
                SetScale();
            }
        }

        public void Init(int n)
        {
            PointF[] whiteData = new PointF[signal.CountOfSamples];
            data = new PointF[signal.CountOfSamples];

            form = (List<TextBox>) signal.GetHash("model");
            CreateChart(n);

            switch ((int) signal.GetHash("model_k"))
            {
                case 12:
                    for (int i = 0; i < Convert.ToInt64(signal.CountOfSamples); i++)
                    {
                        double sumrand = 0;
                        for (int j = 1; j <= 12; j++)
                            sumrand += rand.NextDouble();
                        sumrand = sumrand / 12 - 0.5;
                        whiteData[i] = new PointF(i, Convert.ToSingle(sumrand));
                    }

                    break;
            }

            charts.Add(chart);
            float y;
            for (int i = 0; i < signal.CountOfSamples; i++)
            {
                double t = i / signal.Frequency;
                switch ((int) signal.GetHash("model_k"))
                {
                    case 1:
                        data[i] = new PointF(i, i == Convert.ToDouble(form[0].Text) ? 1 : 0);
                        break;
                    case 2:
                        data[i] = new PointF(i, i < Convert.ToDouble(form[0].Text) ? 0 : 1);
                        break;
                    case 3:
                        data[i] = new PointF(i, Convert.ToSingle(Math.Pow(Convert.ToDouble(form[0].Text), i)));
                        break;
                    case 4:
                        y = Convert.ToSingle(Convert.ToDouble(form[0].Text) *
                                             Math.Sin(i * (Convert.ToDouble(form[1].Text) * Math.PI / 180) +
                                                      Convert.ToDouble(form[2].Text) * Math.PI / 360));
                        data[i] = new PointF(i, y);
                        break;
                    case 5:
                        data[i] = new PointF(i,
                            i % Convert.ToDouble(form[0].Text) < Convert.ToDouble(form[0].Text) / 2 ? 1 : -1);
                        break;
                    case 6:
                        data[i] = new PointF(i,
                            Convert.ToSingle((i % Convert.ToDouble(form[0].Text)) /
                                             Convert.ToDouble(form[0].Text)));
                        break;
                    case 7:
                        t = i / signal.Frequency;
                        y = Convert.ToSingle(Convert.ToDouble(form[0].Text) *
                                             Math.Exp(-t / Convert.ToDouble(form[1].Text)) *
                                             Math.Cos(2 * Math.PI * Convert.ToDouble(form[2].Text) * t +
                                                      Convert.ToDouble(form[3].Text)));
                        data[i] = new PointF(i, y);
                        break;
                    case 8:
                        t = i / signal.Frequency;
                        y = Convert.ToSingle((Convert.ToDouble(form[0].Text) *
                                              Math.Cos(2 * Math.PI * Convert.ToDouble(form[1].Text) * t)
                                              * Math.Cos(2 * Math.PI * Convert.ToDouble(form[2].Text) * t +
                                                         Convert.ToDouble(form[3].Text))));
                        data[i] = new PointF(i, y);
                        break;
                    case 9:
                        t = i / signal.Frequency;
                        y = Convert.ToSingle(Convert.ToDouble(form[0].Text) *
                                             (1 + Convert.ToDouble(form[1].Text) *
                                                 Math.Cos(2 * Math.PI * Convert.ToDouble(form[2].Text) * t)) *
                                             Math.Cos(2 * Math.PI * Convert.ToDouble(form[3].Text) * t +
                                                      Convert.ToDouble(form[4].Text)));
                        data[i] = new PointF(i, y);
                        break;


                    case 15: // 10 МОДЕЛЬ НОВАЯ
                        t = i / signal.Frequency;
                        var _a = Convert.ToDouble(form[0].Text);
                        var _f0 = Convert.ToDouble(form[1].Text);
                        var _fk = Convert.ToDouble(form[2].Text);
                        var _TN = 1 / signal.Frequency * Convert.ToDouble(signal.CountOfSamples);
                        var _fi = Convert.ToDouble(form[4].Text);
                        y = Convert.ToSingle(_a * Math.Cos(2 * Math.PI * (_f0 + ((_fk - _f0) / _TN) * t) * t + _fi));
                        data[i] = new PointF(i, y);
                        break;

                    case 10: // пошли случайные модели
                        y = Convert.ToSingle(Convert.ToDouble(form[0].Text) +
                                             (Convert.ToDouble(form[1].Text) - Convert.ToDouble(form[0].Text)) *
                                             rand.NextDouble());
                        data[i] = new PointF(i, y);
                        break;
                    case 11:
                        double sum_rand = 0;
                        for (int j = 1; j <= 12; j++)
                            sum_rand += rand.NextDouble();
                        sum_rand = sum_rand - 6;
                        data[i] = new PointF(i,
                            Convert.ToSingle(Convert.ToDouble(form[0].Text) +
                                             Math.Pow(Convert.ToDouble(form[1].Text), 0.5) * sum_rand));
                        break;
                    case 12:
                        List<TextBox>
                            form_t = (List<TextBox>) signal.GetHash("modell_t"); //form_t - это просто поля ввода
                        float dy = whiteData[i].Y; // обычный белый шум
                        int a = Convert.ToInt32(form_t[0].Text);
                        for (int j = 1; j <= Convert.ToDouble(form_t[1].Text); j++)
                            if (i - j >= 0)
                                dy += Convert.ToSingle(form[a - 1 + j].Text) * whiteData[i - j].Y;

                        for (int j = 1; j <= Convert.ToDouble(form_t[0].Text); j++)
                            if (i - j >= 0)
                                dy -= Convert.ToSingle(form[j - 1].Text) * data[i - j].Y;
                        data[i] = new PointF(i, dy); //полученнвя точка;
                        break;

                    case 13:
                        CheckedListBox clb = (CheckedListBox) signal.GetHash("super_ch");
                        int k = 0;
                        float beg = Convert.ToSingle(form[0].Text);
                        for (int j = 0; j < signal.CountOfChannels; j++)
                        {
                            if (clb.GetItemChecked(j) == true)
                            {
                                k += 1;
                                beg += Convert.ToSingle(form[k].Text) * signal.Points[j, i].Y;
                            }
                        }

                        data[i] = new PointF(i, beg);
                        break;
                    case 14:
                        CheckedListBox sub = (CheckedListBox) signal.GetHash("super_ch");
                        float na = Convert.ToSingle(form[0].Text);
                        for (int j = 0; j < signal.CountOfChannels; j++)
                            if (sub.GetItemChecked(j) == true)
                                na *= signal.Points[j, i].Y;

                        data[i] = new PointF(i, na);
                        break;
                }

                chart.Series[0].Points.AddXY(i, data[i].Y);
            }

            // Случайный сигнал АРСС (p,q) - сбрасываем значения p и q, иначе попытается открыть сразу второе окно с a и b и упадет
            signal.RemoveHash("modell_t");

            chart.MouseDown += position1;
            chart.MouseUp += position2;
            Width = W;
            Height = prob + H * charts.Count + 40;
            chart.AxisScrollBarClicked += scroller;
            chart.AxisViewChanged += viewchanged;
            chart.Tag = signal.GetHash("model_k");
        }

        private void CreateChart(int n)
        {
            channelsList.Add(n);
            chart = new Chart();
            chart.Parent = this;
            chart.SetBounds(0, prob + H * charts.Count, W, H);

            ChartArea area = new ChartArea();
            area.Name = "myGraph";

            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = signal.CountOfSamples;

            area.AxisY.LabelStyle.Format = "N2";
            area.AxisX.LabelStyle.Format = "N0";

            area.AxisX.ScrollBar.Enabled = true;

            area.CursorX.IsUserEnabled = true;
            area.CursorX.IsUserSelectionEnabled = true;

            area.AxisX.ScaleView.Zoomable = true;
            area.AxisX.ScrollBar.IsPositionedInside = true;

            area.BorderDashStyle = ChartDashStyle.Solid;
            area.BorderColor = Color.Black;
            area.BorderWidth = 1;

            area.AxisX.MajorGrid.Enabled = GridMode;
            area.AxisY.MajorGrid.Enabled = GridMode;
            area.AxisY.MajorGrid.LineColor = Color.Gray;
            area.AxisX.MajorGrid.LineColor = Color.Gray;

            chart.ChartAreas.Add(area);

            Series series1 = new Series();
            series1.ChartArea = "myGraph";
            series1.ChartType = SeriesChartType.Line;
            series1.Color = Color.Black;

            if (markerMode)
                series1.MarkerStyle = MarkerStyle.None;
            else
                series1.MarkerStyle = MarkerStyle.Circle;

            series1.LegendText = signal.Textmod.Trim();
            chart.Legends.Add(signal.Textmod.Trim());
            chart.Legends[0].Docking = Docking.Bottom;

            chart.Series.Add(series1);

            area.AxisX.ScaleView.Zoom(0, signal.CountOfSamples);
        }


        private void position1(object sender, MouseEventArgs e)
        {
            X1 = e.X;
            Y1 = e.Y;
            if (e.Button == MouseButtons.Right)
            {
                chart = (Chart) sender;
            }
        }

        private void position2(object sender, MouseEventArgs e)
        {
            X2 = e.X;
            Y2 = e.Y;
        }

        public void ZoomCharts(double x1, double x2)
        {
            foreach (Chart ch in charts)
                ch.ChartAreas["myGraph"].AxisX.ScaleView.Zoom(x1, x2);
            SetScale();
        }

        public double Max(in PointF[] samples, int startXRange, int endXRange)
        {
            double maxValue = samples[startXRange].Y;
            for (int i = startXRange; i < endXRange; i++)
            {
                maxValue = maxValue < samples[i].Y ? samples[i].Y : maxValue;
            }

            return maxValue;
        }

        public double Min(in PointF[] samples, int startXRange, int endXRange)
        {
            double minValue = samples[startXRange].Y;
            for (int i = startXRange; i < endXRange; i++)
            {
                minValue = minValue > samples[i].Y ? samples[i].Y : minValue;
            }

            return minValue;
        }

        private void SetScale()
        {
            for (int i = 0; i < charts.Count; i++)
            {
                charts[i].Bounds = new Rectangle(0, prob + H * i, W, H);
                if (localScaleMode)
                {
                    int start = (int) signal.BeginRangeOsci;
                    int end = signal.EndRangeOsci == 0 ? data.Length : (int) signal.EndRangeOsci;
                    charts[i].ChartAreas["myGraph"].AxisY.Minimum = Min(data, start, end);
                    charts[i].ChartAreas["myGraph"].AxisY.Maximum = Max(data, start, end);
                }
                else
                {
                    charts[i].ChartAreas["myGraph"].AxisY.Minimum = Min(data, 0, data.Length);
                    charts[i].ChartAreas["myGraph"].AxisY.Maximum = Max(data, 0, data.Length);
                }
            }

            Width = W;
            Height = prob + H * charts.Count + 40;
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
                SetScale();
                channelsList.RemoveAt(k);
            }
        }


        public void close(object sender, FormClosedEventArgs e)
        {
            signal.SetModel(null);
        }

        private void добавитьВНавигациюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(signal.ChechHash("model_" + signal.GetHash("model_k"))))
            {
                signal.SetHash("model_" + signal.GetHash("model_k"), 1);
                signal.AddChannel(data, "Model_" + signal.GetHash("model_k") + "_" + "1");
            }
            else
            {
                signal.AddChannel(data,
                    "Model_" + signal.GetHash("model_k") + "_" +
                    Convert.ToString((int) signal.GetHash("model_" + signal.GetHash("model_k")) + 1));
                signal.SetHash("model_" + signal.GetHash("model_k"),
                    (int) signal.GetHash("model_" + signal.GetHash("model_k")) + 1);
            }
        }

        private void глобальныйМасштабToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (localScaleMode)
            {
                локальныйМасштабToolStripMenuItem.CheckState = CheckState.Unchecked;
                глобальныйМасштабToolStripMenuItem.CheckState = CheckState.Checked;
                toolStripLabel1.Text = "Локальный масштаб";
                localScaleMode = false;
                SetScale();
            }
        }

        private void локальныйМасштабToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!localScaleMode)
            {
                локальныйМасштабToolStripMenuItem.CheckState = CheckState.Checked;
                глобальныйМасштабToolStripMenuItem.CheckState = CheckState.Unchecked;
                toolStripLabel1.Text = "Глобальный масштаб";
                localScaleMode = true;
                SetScale();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (charts.Count > 0)
            {
                GridMode = !GridMode;
                toolStripButton1.Checked = GridMode;
                for (int i = 0; i < charts.Count; i++)
                {
                    charts[i].ChartAreas["myGraph"].AxisX.MajorGrid.Enabled = GridMode;
                    charts[i].ChartAreas["myGraph"].AxisY.MajorGrid.Enabled = GridMode;
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (charts.Count > 0)
            {
                markerMode = !markerMode;
                toolStripButton2.Checked = !markerMode;
                for (int i = 0; i < charts.Count; i++)
                {
                    if (markerMode)
                        charts[i].Series[0].MarkerStyle = MarkerStyle.None;
                    else
                        charts[i].Series[0].MarkerStyle = MarkerStyle.Circle;
                }
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            localScaleMode = !localScaleMode;
            локальныйМасштабToolStripMenuItem.Checked = localScaleMode;
            глобальныйМасштабToolStripMenuItem.Checked = !localScaleMode;
            if (localScaleMode)
                toolStripLabel1.Text = "Глобальный масштаб";
            else
                toolStripLabel1.Text = "Локальный масштаб";
            SetScale();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            inter = new IntervalOsc();
            inter.Hide();
            inter.Show();
        }

        private void Model_Load(object sender, EventArgs e)
        {
        }
    }
}