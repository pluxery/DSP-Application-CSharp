﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsApp
{
    public partial class oscillogram : Form
    {
        //TODO align charts
        //TODO Axis X samples to seconds
        bool localScaleMode = true;
        bool GridMode = true; 
        bool markerMode = true;
        Signal signal = Signal.GetInstance();
        public List<Chart> charts = new List<Chart>(); 
        Chart chart; 
        public List<int> channelsList = new List<int>();
        private int X1;
        private int Y1;
        private int X2;
        private int Y2;
        int prob = 30;
        IntervalOsc interval;
        int W = 900;
        int H = 200;
        private int curChannelIndex;

        private void resize(object sender, EventArgs e)
        {
            if (charts.Count > 0)
            {
                W = Width;
                H = (Height - 40 - prob) / charts.Count;
                SetScale();
                if (signal.Spectrogram != null)
                {
                    signal.Spectrogram.Width = W;
                }
            }
        }

        public oscillogram(MainForm ParrentForm)
        {
            InitializeComponent();
        }


        public void Init(int n, double mini, double maxi)
        {
            if (!channelsList.Contains(n))
            {
                curChannelIndex = n;
                CreateChart(mini, maxi, n);
                chart.Tag = n.ToString();
                charts.Add(chart);
                for (int i = 0; i < signal.CountOfSamples; i++)
                {
                    chart.Series[0].Points.AddXY(i, signal.Points[n, i].Y);
                }

                chart.ChartAreas["myGraph"].AxisY.LabelStyle.Format = GetLabelFormat(
                    chart.ChartAreas["myGraph"].AxisY.Minimum,
                    chart.ChartAreas["myGraph"].AxisY.Maximum);

                chart.MouseDown += position1;
                chart.MouseUp += position2;

                Width = W;
                Height = prob + H * charts.Count + 40;

                chart.AxisScrollBarClicked += scroller;
                chart.AxisViewChanged += viewchanged;
            }
        }

        private void CreateChart(double min, double max, int n)
        {
            channelsList.Add(n);
            signal.MainForm.CheckItem(n);
            chart = new Chart();
            chart.Parent = this;
            chart.SetBounds(0, prob + H * charts.Count, W, H);
            ChartArea area = new ChartArea();
            area.Name = "myGraph";
            area.AxisY.Minimum = min;
            area.AxisY.Maximum = max;
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
            area.AxisX.MajorGrid.Enabled = GridMode;
            area.AxisY.MajorGrid.Enabled = GridMode;

            chart.ChartAreas.Add(area);

            Series series1 = new Series();

            series1.ChartArea = "myGraph";
            if (markerMode)
                series1.MarkerStyle = MarkerStyle.None;
            else
                series1.MarkerStyle = MarkerStyle.Circle;

            series1.ChartType = SeriesChartType.Line;
            series1.Color = Color.Black;
            series1.LegendText = signal.Names[n];
            chart.Legends.Add(signal.Names[n]);
            chart.Legends[signal.Names[n]].Docking = Docking.Top;
            chart.Legends[signal.Names[n]].Alignment = StringAlignment.Center;
            chart.Series.Add(series1);
            area.AxisX.ScaleView.Zoom(signal.BeginRangeOsci, signal.EndRangeOsci);
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

        public void ZoomCharts(double x1, double x2) 
        {
            foreach (Chart ch in charts)
            {
                ch.ChartAreas["myGraph"].AxisX.ScaleView.Zoom(x1, x2);
            }

            if (signal.Spectrogram != null)
            {
                signal.Spectrogram.ZoomX(x1, x2);
            }

            SetScale();
        }

        private double Min(in DataPointCollection points, int beginIndex, int endIndex)
        {
            double min = Double.MaxValue;
            for (int i = beginIndex; i < endIndex; i++)
            {
                if (min > points[i].YValues[0])
                    min = points[i].YValues[0];
            }

            return min;
        }

        private double Max(in DataPointCollection points, int beginIndex, int endIndex)
        {
            double max = Double.MinValue;
            for (int i = beginIndex; i < endIndex; i++)
            {
                if (max < points[i].YValues[0])
                    max = points[i].YValues[0];
            }

            return max;
        }

        private void SetScale()
        {
            for (int i = 0; i < charts.Count; i++)
            {
                charts[i].Bounds = new Rectangle(0, prob + H * i, W, H);
                if (localScaleMode)
                {
                    var beginX = (int) signal.BeginRangeOsci;
                    var endX = (int) signal.EndRangeOsci;
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
            Height = H * charts.Count + 40 + prob;
        }

        private void scroller(object sender, ScrollBarEventArgs e)
        {
        }

        private void viewchanged(object sender, ViewEventArgs e)
        {
            signal.SetBeginRangeOsci(e.ChartArea.AxisX.ScaleView.Position);
            signal.SetEndRangeOsci(e.ChartArea.AxisX.ScaleView.Position + e.ChartArea.AxisX.ScaleView.Size);
            if (signal.Spectrogram != null)
            {
                signal.Spectrogram.Update();
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int level;
            for (level = 0; !chart.Equals(charts[level]); level++)
            {
            }

            remove(level);
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
                signal.MainForm.UnCheckItem(channelsList[k]);
                channelsList.RemoveAt(k);
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
    }
}