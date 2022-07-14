using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsApp
{
    public partial class Correlation : Form
    {
        bool localScaleMode = true;
        bool GridMode = true;
        bool markerMode = true;
        Signal signal = Signal.GetInstance();
        public List<Chart> order = new List<Chart>();
        Chart chart;
        public List<int> channelsList = new List<int>();
        private int X1;
        private int Y1;
        private int X2;
        private int Y2;
        int prob = 30;
        IntervalOsc intervalOsc;
        int W = 700;
        int H = 200;

        private bool logX = false, logY = false;

        private void resize(object sender, EventArgs e)
        {
            if (order.Count > 0)
            {
                W = Width;
                H = (Height - 40 - prob) / order.Count;
                Scale();
            }
        }

        public Correlation(MainForm ParrentForm)
        {
            InitializeComponent();
        }

        //создание и добавление нового чарта на форму
        public void Init(int n)
        {
            if (!channelsList.Contains(n))
            {
                double srx = SRX(n);
                chart = CreateChart(n);
                order.Add(chart); //добавление чарта в общий список
                //double k = 1 / 60 / signal.FD();
                double min = double.MaxValue, max = double.MinValue;
                for (int i = -signal.CountOfSamples + 1; i < signal.CountOfSamples; i++) //инициализация массива
                {
                    int l = Math.Abs(i);
                    double z = ComputeCorrelation(n, l, srx);
                    min = min > z ? z : min;
                    max = max < z ? z : max;
                    chart.Series[0].Points.AddXY((double) i, z); //то что показывается на графике
                    chart.Tag = n.ToString();
                }

                chart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.position1);
                chart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.position2);
                //изменение размеров окна
                this.Width = this.W;
                this.Height = prob + this.H * order.Count + 40;
                this.Controls.Add(chart);
                this.chart.AxisScrollBarClicked +=
                    new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ScrollBarEventArgs>(
                        this.scroller);
                this.chart.AxisViewChanged += new System.EventHandler<ViewEventArgs>(this.viewchanged);
            }
        }

        public double SRX(int k_channel)
        {
            double x = 0;
            for (int n = 0; n < signal.CountOfSamples; n++)
                x += signal.Points[k_channel, n].Y;


            return x / signal.CountOfSamples;
        }

        public double ComputeCorrelation(int N, int m, double srx)
        {
            double K = 0;
            for (int n = 0; n < signal.CountOfSamples - m; n++)
                K += (signal.Points[N, n].Y - srx) * (signal.Points[N, n + m].Y - srx);

            return K / signal.CountOfSamples;
        }

        private Chart CreateChart(int n)
        {
            Chart chart = new Chart();
            channelsList.Add(n);
            signal.MainForm.CheckItemFFT(n);
            chart.Parent = this;
            chart.Size = new Size(W, H);
            chart.Location = new Point(0, this.H * order.Count);
            ChartArea area = new ChartArea();
            area.Name = "myGraph";
            area.AxisY.LabelStyle.Format = "N2";
            area.AxisX.LabelStyle.Format = "N2";
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
            area.AxisY.MajorGrid.LineColor = Color.Black;
            area.AxisX.MajorGrid.LineColor = Color.Black;
            area.AxisX.IsLogarithmic = logX;
            area.AxisY.IsLogarithmic = logY;
            chart.ChartAreas.Add(area);
            Series series = new Series();
            series.ChartType = SeriesChartType.Line;

            if (markerMode)
                series.MarkerStyle = MarkerStyle.Circle;
            else
                series.MarkerStyle = MarkerStyle.None;
            series.ChartType = SeriesChartType.Line;

            chart.Series.Clear();
            chart.Series.Add(series);
            chart.Series[0].ChartArea = "myGraph";
            series.LegendText = signal.Names[n];
            chart.Legends.Add(signal.Names[n]);
            chart.MouseDown += position1;
            chart.MouseUp += position2;
            chart.AxisScrollBarClicked += scroller;
            chart.AxisViewChanged += viewchanged;
            return chart;
        }

        private void position1(object sender, MouseEventArgs e)
        {
            X1 = e.X;
            Y1 = e.Y;
            if (e.Button == MouseButtons.Right)
                chart = (Chart) sender;
        }

        private void position2(object sender, MouseEventArgs e)
        {
            X2 = e.X;
            Y2 = e.Y;
        }

        public void area(int x1, int x2) //изменяет интервал отображения
        {
            foreach (Chart ch in order)
                ch.ChartAreas["myGraph"].AxisX.ScaleView.Zoom(x1, x2);
            Scale();
        }

        private void Scale()
        {
            for (int i = 0; i < order.Count; i++)
            {
                order[i].Bounds = new Rectangle(0, prob + H * i, W, H);
                if (localScaleMode)
                {
                }
                else
                {
                }
            }

            Width = W;
            Height = H * order.Count + 40 + prob;
        }

        private void scroller(object sender, ScrollBarEventArgs e)
        {
            double round = signal.EndRangeOsci - signal.BeginRangeOsci;
            signal.SetBeginRangeOsci((int)e.ChartArea.AxisX.ScaleView.Position);
            signal.SetEndRangeOsci((int)(e.ChartArea.AxisX.ScaleView.Position + e.ChartArea.AxisX.ScaleView.Size));
        }

        private void viewchanged(object sender, ViewEventArgs e)
        {
            signal.SetBeginRangeOsci((int)e.ChartArea.AxisX.ScaleView.Position);
            signal.SetEndRangeOsci((int)(e.ChartArea.AxisX.ScaleView.Position + e.ChartArea.AxisX.ScaleView.Size));
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int channelIndex;
            for (channelIndex = 0; !chart.Equals(order[channelIndex]); channelIndex++)
            {
            }

            remove(channelIndex);
        }

        public void remove(int k)
        {
            if (order.Count == 1)
                Close();
            else
            {
                order[k].Visible = false;
                order[k].Dispose();
                order.RemoveAt(k);
                Scale();
                signal.MainForm.UnCheckItemCor(channelsList[k]);
                channelsList.RemoveAt(k);
            }
        }


        public void close(object sender, FormClosedEventArgs e)
        {
            signal.MainForm.UnCheckItemCor();
            signal.Correlation = null;
        }

        private void локальныйМасштабToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!localScaleMode)
            {
                toolStripButton3.CheckState = CheckState.Checked;
                toolStripButton3.CheckState = CheckState.Unchecked;
                localScaleMode = true;
                Scale();
            }
        }

        private void глобальныйМасштабToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (localScaleMode)
            {
                toolStripButton4.CheckState = CheckState.Unchecked;
                toolStripButton4.CheckState = CheckState.Checked;
                localScaleMode = false;
                Scale();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (order.Count > 0)
            {
                GridMode = !GridMode;
                this.toolStripButton1.Checked = GridMode;
                for (int i = 0; i < order.Count; i++)
                {
                    order[i].ChartAreas["myGraph"].AxisX.MajorGrid.Enabled = GridMode;
                    order[i].ChartAreas["myGraph"].AxisY.MajorGrid.Enabled = GridMode;
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (order.Count > 0)
            {
                markerMode = !markerMode;
                toolStripButton2.Checked = markerMode;
                for (int i = 0; i < order.Count; i++)
                {
                    if (markerMode)
                        order[i].Series[0].MarkerStyle = MarkerStyle.None;
                    else
                        order[i].Series[0].MarkerStyle = MarkerStyle.Circle;
                }
            }
        }

        private void Correlation_Load(object sender, EventArgs e)
        {
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            intervalOsc = new IntervalOsc();
            intervalOsc.Hide();
            intervalOsc.Show();
        }
    }
}