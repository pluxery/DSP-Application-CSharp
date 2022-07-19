using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsApp
{
    public partial class Oscillogram : AbstractGraphic
    {
        private Dictionary<string, double[]> yValuesByChannel = new Dictionary<string, double[]>();
        private List<int> samples = new List<int>();
        private List<string> timeList = new List<string>();
        IntervalOsc interval;
        int W = 900;
        int H = 200;
        private int curChannelIndex;
        private bool AxisXLabelMode;//false - samples; true - time
        private void resize(object sender, EventArgs e)
        {
            if (charts.Count > 0)
            {
                W = Width;
                H = (Height - 40 - margin) / charts.Count;
                SetScale();
                if (signal.Spectrogram != null)
                {
                    signal.Spectrogram.Width = W;
                }
            }
        }

        public Oscillogram(MainForm ParrentForm)
        {
            InitializeComponent();
            SetAxisXTimeLabel();
        }
        private void SetAxisXTimeLabel()//todo сделать лэйбл в зависимости от интервала времени
        {
            for (int i = 0; i < signal.CountOfSamples; i++)
                {
                    TimeSpan time = TimeSpan.FromSeconds(i * (1 / signal.Frequency));
                    timeList.Add(time.Days + "д:" + time.Hours + "ч:" + time.Minutes + "м:" + time.Seconds + "с");
                }
            
        }

        public override void Init(int channelIndex)
        {
            if (!channelIndexes.Contains(channelIndex))
            {
                curChannelIndex = channelIndex;
                curChart = CreateChart(channelIndex);
                
                charts.Add(curChart);
                var yPoints = new double[signal.CountOfSamples];
                for (int i = 0; i < signal.CountOfSamples; i++)
                {
                    yPoints[i] = signal.Points[channelIndex, i].Y;
                    samples.Add(i);
                    curChart.Series[0].Points.AddXY(i, signal.Points[channelIndex, i].Y);
                }
                yValuesByChannel[curChart.Name] = yPoints;

                curChart.ChartAreas["myGraph"].AxisY.LabelStyle.Format = GetLabelFormat(
                    curChart.ChartAreas["myGraph"].AxisY.Minimum,
                    curChart.ChartAreas["myGraph"].AxisY.Maximum);

                curChart.MouseDown += position1;
                curChart.MouseUp += position2;

                Width = W;
                Height = margin + H * charts.Count + 40;

                curChart.AxisScrollBarClicked += scroller;
                curChart.AxisViewChanged += viewchanged;
            }
        }

        protected override Chart CreateChart(int channelIndex)
        {
            channelIndexes.Add(channelIndex);
            signal.MainForm.CheckItem(channelIndex);
            var chart = new Chart();
            chart.Name = signal.Names[channelIndex];
            chart.Tag = channelIndex.ToString();
            chart.Parent = this;
            chart.SetBounds(0, margin + H * charts.Count, W, H);
            ChartArea area = new ChartArea();
            area.Name = "myGraph";
            area.AxisY.Minimum = signal.Min(channelIndex, 0, signal.CountOfSamples);
            area.AxisY.Maximum = signal.Max(channelIndex, 0, signal.CountOfSamples);
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = signal.CountOfSamples;
            area.AxisY.LabelStyle.Format = "N0";
            area.AxisX.LabelStyle.Format = "N0";
            area.AxisX.ScrollBar.Enabled = true;
            area.CursorX.IsUserEnabled = true;
            area.CursorX.IsUserSelectionEnabled = true;
            area.AxisX.ScaleView.Zoomable = true;
            area.AxisX.ScrollBar.IsPositionedInside = true;
            area.BorderDashStyle = ChartDashStyle.Solid;
            area.BorderColor = Color.Black;
            area.BorderWidth = 1;
            area.AxisX.MajorGrid.Enabled = gridMode;
            area.AxisY.MajorGrid.Enabled = gridMode;

            chart.ChartAreas.Add(area);

            Series series1 = new Series();

            series1.ChartArea = "myGraph";
            if (markerMode)
                series1.MarkerStyle = MarkerStyle.None;
            else
                series1.MarkerStyle = MarkerStyle.Circle;

            series1.ChartType = SeriesChartType.Line;
            series1.Color = Color.Black;
            series1.LegendText = signal.Names[channelIndex];
            chart.Legends.Add(signal.Names[channelIndex]);
            chart.Legends[signal.Names[channelIndex]].Docking = Docking.Top;
            chart.Legends[signal.Names[channelIndex]].Alignment = StringAlignment.Center;
            chart.Series.Add(series1);
            area.AxisX.ScaleView.Zoom(signal.BeginRangeOsci, signal.EndRangeOsci);
            return chart;
        }

        private void position1(object sender, MouseEventArgs e)
        {
            X1 = e.X;
            Y1 = e.Y;
            if (e.Button == MouseButtons.Right)
            {
                 curChart = (Chart) sender;
            }
        }

        private void position2(object sender, MouseEventArgs e)
        {
            X2 = e.X;
            Y2 = e.Y;
        }

        private string GetLabelFormat(double min, double max)
        {
            var difference = max - min;
            if (difference >= 10)
            {
                return "N0";
            }

            if (difference < 10 && difference >= 1)
            {
                return "N1";
            }

            if (difference < 1 && difference > 0.1)
            {
                return "N2";
            }

            return "N3";
        }

        public void Zoom(double x1, double x2) 
        {
            foreach (Chart ch in charts)
            {
                ch.ChartAreas["myGraph"].AxisX.ScaleView.Zoom(x1, x2);
            }
            

            if (signal.Spectrogram != null)
            {
                signal.Spectrogram.Zoom(x1, x2);
            }

            if (signal.Fft != null)
            {
                if (x2 < signal.CountOfSamples/2)
                    signal.Fft.Zoom(x1,x2);
            }

            SetScale();
        }
        
        protected override void SetScale()
        {
            for (int i = 0; i < charts.Count; i++)
            {
                charts[i].Bounds = new Rectangle(0, margin + H * i, W, H);
                if (localScaleMode)
                {
                    var beginX =  signal.BeginRangeOsci;
                    var endX =  signal.EndRangeOsci;
                    var minY = Min(charts[i].Series[0].Points, beginX, endX);
                    var maxY = Max(charts[i].Series[0].Points, beginX, endX);
                    charts[i].ChartAreas["myGraph"].AxisY.Minimum = minY;
                    charts[i].ChartAreas["myGraph"].AxisY.Maximum = maxY;
                }
                else
                {
                    charts[i].ChartAreas["myGraph"].AxisY.Minimum =
                        signal.Min(Convert.ToInt32(charts[i].Tag), 0, signal.CountOfSamples);
                    charts[i].ChartAreas["myGraph"].AxisY.Maximum =
                        signal.Max(Convert.ToInt32(charts[i].Tag), 0, signal.CountOfSamples);
                }
            }

            Width = W;
            Height = H * charts.Count + 40 + margin;
        }

        private void scroller(object sender, ScrollBarEventArgs e)
        {
        }

        private void viewchanged(object sender, ViewEventArgs e)
        {
            signal.SetBeginRangeOsci((int)e.ChartArea.AxisX.ScaleView.Position);
            signal.SetEndRangeOsci((int)(e.ChartArea.AxisX.ScaleView.Position + e.ChartArea.AxisX.ScaleView.Size));
            if (signal.Spectrogram != null)
            {
                signal.Spectrogram.Update();
            }
            if (signal.Statistic != null)
            {
                foreach (var statistic in signal.Statistics)
                    statistic.Update();
            }

            if (signal.Fft != null)
            {
                signal.Fft.UpdateFft();
            }

        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int level;
            for (level = 0; !curChart.Equals(charts[level]); level++)
            {
            }

            Remove(level);
        }
        public void Remove(int k)
        {
            if (charts.Count == 1)
                Close();
            else
            {
                charts[k].Visible = false;
                charts[k].Dispose();
                charts.RemoveAt(k);
                SetScale();
                channelIndexes.RemoveAt(k);
            }
        }
        public void close(object sender, FormClosedEventArgs e)
        {
            signal.MainForm.UnCheckItem();
            signal.SetOscillogram(null);
        }

        private void локальныйМасштабToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!localScaleMode)
            {
                локальныйМасштабToolStripMenuItem.CheckState = CheckState.Checked;
                глобальныйМасштабToolStripMenuItem.CheckState = CheckState.Unchecked;
                localScaleMode = true;
                SetScale();
            }
        }

        private void глобальныйМасштабToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (localScaleMode)
            {
                локальныйМасштабToolStripMenuItem.CheckState = CheckState.Unchecked;
                глобальныйМасштабToolStripMenuItem.CheckState = CheckState.Checked;
                localScaleMode = false;
                SetScale();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (charts.Count > 0)
            {
                gridMode = !gridMode;
                toolStripButton1.Checked = gridMode;
                for (int i = 0; i < charts.Count; i++)
                {
                    charts[i].ChartAreas["myGraph"].AxisX.MajorGrid.Enabled = gridMode;
                    charts[i].ChartAreas["myGraph"].AxisY.MajorGrid.Enabled = gridMode;
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (charts.Count > 0)
            {
                markerMode = !markerMode;
                toolStripButton2.Checked = markerMode;
                for (int i = 0; i < charts.Count; i++)
                {
                    if (markerMode)
                        charts[i].Series[0].MarkerStyle = MarkerStyle.None;
                    else
                        charts[i].Series[0].MarkerStyle = MarkerStyle.Circle;
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            interval = new IntervalOsc();
            interval.Hide();
            interval.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AxisXLabelMode = !AxisXLabelMode;
            if (AxisXLabelMode)
            {
                button1.Text = "Время";
                foreach (var chart in charts)
                {
                    chart.Series[0].Points.DataBindXY(timeList, yValuesByChannel[chart.Name]);
                    
                }
            }
            else
            {
                button1.Text = "Отсчеты";
                foreach (var chart in charts)
                {
                    chart.Series[0].Points.DataBindXY(samples, yValuesByChannel[chart.Name]);
                }
            }
        }
    }
}