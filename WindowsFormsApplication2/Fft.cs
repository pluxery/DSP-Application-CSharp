using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics.IntegralTransforms;


namespace WindowsApp
{
    public partial class Fft : Form
    {
        private bool localScaleMode;
        private bool sharpMode;
        private bool markerMode;

        Signal signal = Signal.GetInstance();
        private int halfSize = Signal.GetInstance().CountOfSamples / 2;

        private string[] freqValues = new string[Signal.GetInstance().CountOfSamples / 2];

        public List<Chart[]> fftCharts = new List<Chart[]>();
        private Chart curChart;
        private List<int> channelsIndexes = new List<int>();

        private int X1;
        private int Y1;
        private int X2;
        private int Y2;

        private bool isLogMode;
        private bool isSmooth;

        private double[] re;
        private double[] im;
        private double reX0;
        private double imX0;

        const int margin = 30;
        private IntervalFFT intervalFft;
        private int width = 700;
        private int height = 200;

        private TabControl tabControl1;
        private TabPage tabPage1, tabPage2;
        System.ComponentModel.Container components = new System.ComponentModel.Container();
        private ContextMenuStrip contextMenuStrip1;


        private void resize(object sender, EventArgs e)
        {
            if (fftCharts.Count > 0)
            {
                width = Width;
                height = (Height - margin * 3) / fftCharts.Count;
                SetScale();
            }
        }

        public Fft(MainForm ParrentForm)
        {
            InitializeComponent();
            for (int i = 0; i < halfSize; i++)
            {
                freqValues[i] = (Math.Round(i * (signal.Frequency / halfSize) / 2, 7)).ToString();
            }
        }

        public void Redraw()
        {
            Zoom(signal.BeginRangeOsci, signal.EndRangeOsci);
        }

        public void Zoom(double x1, double x2)
        {
            foreach (Chart[] ch in fftCharts)
            {
                for (int i = 0; i <= 1; i++)
                {
                    ch[i].ChartAreas["myGraph"].AxisX.ScaleView.Zoom(x1, x2);
                }
            }

            SetScale();
        }

        private void ChangeFftMode(int fftMode)
        {
            if (fftMode == 0) //X(0) = |X(1)|
            {
                re[0] = re[1];
                im[0] = im[1];
            }
            else if (fftMode == 1) // X(0) = 0
            {
                re[0] = 0;
                im[0] = 0;
            }
            else //X(0) = X(0)
            {
                re[0] = reX0;
                im[0] = imX0;
            }
        }

        private void ComputeFft(int channelIndex)
        {
            int N = signal.CountOfSamples;
            re = new double[N];
            im = new double[N];
            for (int i = 0; i < N; i++)
            {
                re[i] = signal.Points[channelIndex, i].Y;
                im[i] = 0.0F;
            }

            reX0 = re[0];
            imX0 = im[0];

            var fourier = new Complex[N];
            for (int i = 0; i < N; i++)
            {
                fourier[i] = new Complex(re[i], 0);
            }

            Fourier.Forward(fourier, FourierOptions.NoScaling);
            for (int i = 0; i < N; i++)
            {
                re[i] = fourier[i].Real;
                im[i] = fourier[i].Imaginary;
            }

            ChangeFftMode(signal.FFTMode);
        }


        private void ContextStrip()
        {
            contextMenuStrip1 = new ContextMenuStrip(components);
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
            {
                this.удалитьToolStripMenuItem,
                this.локальныйМасштабToolStripMenuItem,
                this.глобальныйМасштабToolStripMenuItem
            });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(236, 82);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // локальныйМасштабToolStripMenuItem
            // 
            // this.локальныйМасштабToolStripMenuItem.Checked = true;
            // this.локальныйМасштабToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.локальныйМасштабToolStripMenuItem.Name = "локальныйМасштабToolStripMenuItem";
            this.локальныйМасштабToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            this.локальныйМасштабToolStripMenuItem.Text = "Локальный масштаб";
            this.локальныйМасштабToolStripMenuItem.Click +=
                new System.EventHandler(this.локальныйМасштабToolStripMenuItem_Click);
            // 
            // глобальныйМасштабToolStripMenuItem
            // 

            this.глобальныйМасштабToolStripMenuItem.Checked = true;
            this.глобальныйМасштабToolStripMenuItem.CheckState = CheckState.Checked;
            this.глобальныйМасштабToolStripMenuItem.Name = "глобальныйМасштабToolStripMenuItem";
            this.глобальныйМасштабToolStripMenuItem.Size = new Size(235, 26);
            this.глобальныйМасштабToolStripMenuItem.Text = "Глобальный масштаб";
            this.глобальныйМасштабToolStripMenuItem.Click += глобальныйМасштабToolStripMenuItem_Click;


            this.contextMenuStrip1.ResumeLayout(false);
        }

        public void Init(int channelIndex)
        {
            signal.EndRangeFft = signal.CountOfSamples / 2;
            var fd = signal.Frequency;
            if (contextMenuStrip1 == null)
                ContextStrip();
            if (!channelsIndexes.Contains(channelIndex))
            {
                channelsIndexes.Add(channelIndex);
                var chartsTab = new Chart[4];
                ComputeFft(channelIndex);

                if (tabControl1 == null)
                {
                    tabControl1 = new TabControl();
                    tabControl1.Location = new Point(0, 27);
                    tabControl1.Name = "tabControl1";
                    tabControl1.SelectedIndex = 0;
                    tabControl1.TabIndex = 1;
                }

                if (tabPage1 == null)
                {
                    tabPage1 = new TabPage();
                    tabPage1.Location = new Point(0, 0);
                    tabPage1.Name = "tabPage1";
                    tabPage1.TabIndex = 0;
                    tabPage1.Text = "Амплитудный спектр";
                    tabPage1.UseVisualStyleBackColor = true;
                    tabPage1.ContextMenuStrip = contextMenuStrip1;
                }

                curChart = CreateChart(channelIndex);
                chartsTab[0] = curChart;
                var yValues = new double[halfSize];
                if (signal.FFTMode == 2)
                {
                    var y0 = Math.Abs(Math.Sqrt(reX0 * reX0 + imX0 * imX0));
                    curChart.Series[0].Points.AddXY(0, y0);
                    yValues[0] = y0;
                }
                else
                {
                    var y = Math.Abs(Math.Sqrt(re[0] * re[0] + im[0] * im[0]));
                    curChart.Series[0].Points.AddXY(0, y);
                    yValues[0] = y;
                }

                for (int i = 1; i < halfSize; i++)
                {
                    var y = Math.Abs(Math.Sqrt(re[i] * re[i] + im[i] * im[i]));
                    curChart.Series[0].Points.AddXY(i, y);
                    yValues[i] = y;
                }

                curChart.Series[0].Points.DataBindXY(freqValues, yValues);
                tabPage1.Controls.Add(curChart);

                if (tabPage2 == null)
                {
                    tabPage2 = new TabPage();
                    tabPage2.Location = new Point(0, 0);
                    tabPage2.Name = "tabPage1";
                    tabPage2.TabIndex = 0;
                    tabPage2.Text = "СПМ";
                    tabPage2.UseVisualStyleBackColor = true;
                    tabPage2.ContextMenuStrip = contextMenuStrip1;
                }

                curChart = CreateChart(channelIndex);
                chartsTab[1] = curChart;
                if (signal.FFTMode == 2)
                {
                    var y = Math.Abs(reX0 * reX0 + imX0 * imX0);
                    curChart.Series[0].Points.AddXY(0, y);
                    yValues[0] = y;
                }
                else
                {
                    var y = Math.Abs((re[0] * re[0] + im[0] * im[0]));
                    curChart.Series[0].Points.AddXY(0, y);
                    yValues[0] = y;
                }

                for (int i = 1; i < halfSize; i++)
                {
                    var y = Math.Abs((re[i] * re[i] + im[i] * im[i]));
                    curChart.Series[0].Points.AddXY(i, y);
                    yValues[i] = y;
                }

                curChart.Series[0].Points.DataBindXY(freqValues, yValues);
                tabPage2.Controls.Add(curChart);
                fftCharts.Add(chartsTab);

                if (!tabControl1.Controls.Contains(tabPage1))
                    tabControl1.Controls.Add(tabPage1);
                if (!tabControl1.Controls.Contains(tabPage2))
                    tabControl1.Controls.Add(tabPage2);


                Width = width;
                tabControl1.Size = new Size(width, height * fftCharts.Count + margin);
                tabPage1.Size = new Size(width, height * fftCharts.Count + margin);
                tabPage2.Size = new Size(width, height * fftCharts.Count + margin);
                Height = margin * 3 + height * fftCharts.Count;
                if (!Controls.Contains(tabControl1))
                    Controls.Add(tabControl1);
            }
        }

        private Chart CreateChart(int channelIdx)
        {
            var chart = new Chart();
            signal.MainForm.CheckItemFFT(channelIdx);
            chart = new Chart();
            chart.Name = signal.Names[channelIdx];
            chart.Tag = channelIdx;
            chart.Parent = this;
            chart.Size = new Size(width, height);
            chart.Location = new Point(0, this.height * fftCharts.Count);
            var area = new ChartArea();
            area.Name = "myGraph";
            area.AxisX.ScrollBar.Enabled = true;
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = halfSize;
            area.CursorX.IsUserEnabled = true;
            area.CursorX.AutoScroll = true;
            area.CursorX.IsUserSelectionEnabled = true;
            area.AxisX.ScaleView.Zoomable = true;
            area.AxisX.ScrollBar.IsPositionedInside = true;
            area.BorderDashStyle = ChartDashStyle.Solid;
            area.BorderColor = Color.Black;
            area.BorderWidth = 1;
            area.AxisX.MajorGrid.Enabled = sharpMode;
            area.AxisY.MajorGrid.Enabled = sharpMode;
            area.AxisY.MajorGrid.LineColor = Color.Gray;
            area.AxisX.MajorGrid.LineColor = Color.Gray;
            area.AxisY.IsLogarithmic = isLogMode;
            area.AxisX.IsLogarithmic = isLogMode;
            area.AxisY.LabelStyle.Format = "N0";
            chart.ChartAreas.Add(area);
            var series = new Series();
            series.ChartType = SeriesChartType.Line;
            if (markerMode)
                series.MarkerStyle = MarkerStyle.Circle;
            else
                series.MarkerStyle = MarkerStyle.None;
            series.ChartType = SeriesChartType.Line;
            series.Color = Color.Black;
            chart.Series.Clear();
            chart.Series.Add(series);
            chart.Series[0].ChartArea = "myGraph";
            series.LegendText = signal.Names[channelIdx];
            chart.Legends.Add(signal.Names[channelIdx]);
            chart.Legends[signal.Names[channelIdx]].Docking = Docking.Top;
            chart.Legends[signal.Names[channelIdx]].Alignment = StringAlignment.Center;
            chart.ChartAreas["myGraph"].AxisX.ScaleView.Zoom(0, halfSize);
            chart.MouseDown += Position1;
            chart.MouseUp += Position2;
            chart.AxisScrollBarClicked += Scroller;
            chart.AxisViewChanged += Viewchanged;
            chart.GetToolTipText += Tooltip;
            return chart;
        }

        private void Tooltip(object sender, ToolTipEventArgs e) //при наведении на точку показывать значения X, Y
        {
            switch (e.HitTestResult.ChartElementType)
            {
                case ChartElementType.DataPoint:
                    var point = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                    var x = curChart.Series[0].Points.IndexOf(point);
                    var seconds = 2 / Convert.ToDouble(freqValues[e.HitTestResult.PointIndex]);
                    var time = TimeSpan.FromSeconds(seconds);
                    var period = time.Days + "д:" + time.Hours + "ч:" + time.Minutes + "м:" + time.Seconds + "с";
                    e.Text = string.Format("Y = {1}\nX = {0}\nP = {2}", freqValues[e.HitTestResult.PointIndex],
                        point.YValues[0], period);
                    break;
            }
        }


        private void Position1(object sender, MouseEventArgs e)
        {
            X1 = e.X;
            Y1 = e.Y;
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    curChart = (Chart) sender;
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void Position2(object sender, MouseEventArgs e)
        {
            X2 = e.X;
            Y2 = e.Y;
        }


        private void SetScale()
        {
            for (int i = 0; i < fftCharts.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    fftCharts[i][j].Bounds = new Rectangle(0, height * i, width, height);
                    if (localScaleMode)
                    {
                        var beginX = (int) signal.BeginRangeFft;
                        var endX = (int) signal.EndRangeFft;
                        var minY = Min(fftCharts[i][j].Series[0].Points, beginX, endX);
                        var maxY = Max(fftCharts[i][j].Series[0].Points, beginX, endX);
                        fftCharts[i][j].ChartAreas["myGraph"].AxisY.Minimum = minY;
                        fftCharts[i][j].ChartAreas["myGraph"].AxisY.Maximum = maxY;
                    }

                    else
                    {
                        var minY = Min(fftCharts[i][j].Series[0].Points, 0, fftCharts[i][j].Series[0].Points.Count);
                        var maxY = Max(fftCharts[i][j].Series[0].Points, 0, fftCharts[i][j].Series[0].Points.Count);
                        fftCharts[i][j].ChartAreas["myGraph"].AxisY.Minimum = minY;
                        fftCharts[i][j].ChartAreas["myGraph"].AxisY.Maximum = maxY;
                    }
                }

                tabControl1.Size = new Size(width, height * fftCharts.Count + margin);
                tabPage1.Size = new Size(width, height * fftCharts.Count + margin);
                tabPage2.Size = new Size(width, height * fftCharts.Count + margin);
                Width = width;
                Height = margin * 3 + height * fftCharts.Count;
            }
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

        private void Scroller(object sender, ScrollBarEventArgs e)
        {
        }

        private void Viewchanged(object sender, ViewEventArgs e)
        {
            signal.BeginRangeFft = (int) e.ChartArea.AxisX.ScaleView.Position;
            signal.SetEndRangeFFT((int) (e.ChartArea.AxisX.ScaleView.Position + e.ChartArea.AxisX.ScaleView.Size));
            SetScale();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int channelIndex = 0;
            for (int i = 0; i < fftCharts.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (curChart.Equals(fftCharts[i][j]))
                        channelIndex = i;
                }
            }

            Remove(channelIndex);
        }

        public void Remove(int k)
        {
            if (fftCharts.Count == 1)
                Close();
            else
            {
                for (int j = 0; j < 2; j++)
                {
                    fftCharts[k][j].Visible = false;
                    fftCharts[k][j].Dispose();
                }

                fftCharts.RemoveAt(k);
                SetScale();
                signal.MainForm.UnCheckItemDPF(channelsIndexes[k]);
                channelsIndexes.RemoveAt(k);
            }
        }


        public void Close(object sender, FormClosedEventArgs e)
        {
            signal.MainForm.UnCheckItemDPF();
            signal.Fft = null;
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

        private void DFT_Load(object sender, EventArgs e)
        {
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (fftCharts.Count > 0)
            {
                sharpMode = !sharpMode;
                grid.Checked = sharpMode;
                for (int i = 0; i < fftCharts.Count; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        fftCharts[i][j].ChartAreas["myGraph"].AxisX.MajorGrid.Enabled = sharpMode;
                        fftCharts[i][j].ChartAreas["myGraph"].AxisY.MajorGrid.Enabled = sharpMode;
                    }
                }
            }
        }

        private void marks_Click(object sender, EventArgs e)
        {
            if (fftCharts.Count > 0)
            {
                markerMode = !markerMode;
                this.marks.Checked = markerMode;
                for (int i = 0; i < fftCharts.Count; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (markerMode)
                            fftCharts[i][j].Series[0].MarkerStyle = MarkerStyle.Circle;
                        else
                            fftCharts[i][j].Series[0].MarkerStyle = MarkerStyle.None;
                    }
                }
            }
        }

        private void interv_Click(object sender, EventArgs e)
        {
            intervalFft = new IntervalFFT();
            intervalFft.Hide();
            intervalFft.Show();
        }

        private void log_Click(object sender, EventArgs e)
        {
            if (signal.FFTMode != 1)
            {
                localScaleMode = true;

                глобальныйМасштабToolStripMenuItem.Checked = false;
                локальныйМасштабToolStripMenuItem.Checked = true;


                SetScale();
                if (fftCharts.Count > 0)
                {
                    isLogMode = !isLogMode;

                    if (isLogMode) глобальныйМасштабToolStripMenuItem.Enabled = false;
                    else глобальныйМасштабToolStripMenuItem.Enabled = true;

                    logBtn.Checked = isLogMode;
                    for (int i = 0; i < fftCharts.Count; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            fftCharts[i][j].ChartAreas["myGraph"].AxisY.IsLogarithmic = isLogMode;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("X(0) = 0, Измените режим БПФ\nДля отображения логарифммического режима");
            }
        }

        private List<double>
            GetFftArray(int numTabPage,
                int channelIndex) // для какой страницы создаем точки Y (Амплитудный или СПМ)
        {
            var YValues = new List<double>();
            ComputeFft(channelIndex);
            if (numTabPage == 0)
            {
                for (int i = 0; i < halfSize; i++)
                    YValues.Add(Math.Abs(Math.Sqrt(re[i] * re[i] + im[i] * im[i])));
            }
            else if (numTabPage == 1)
            {
                for (int i = 0; i < halfSize; i++)
                    YValues.Add(Math.Abs(re[i] * re[i] + im[i] * im[i]));
            }
            else
            {
                throw new Exception($"страница = {numTabPage} (не равно 0 или 1!)");
            }

            return YValues;
        }


        private void smoothBtn_Click(object sender, EventArgs e)
        {
            SmoothDialog smoothDialog = new SmoothDialog();
            smoothDialog.ShowDialog();
            if (DialogResult.OK == smoothDialog.DialogResult)
            {
                isSmooth = !isSmooth;
                smoothBtn.Checked = isSmooth;
                int smoothVal = Signal.GetInstance().Smooth;
                var fft = new List<double>();
                int idx = 0;
                if (smoothVal == 0) //если нет сглаживания просто рисуем изначальный Амплитудный спектр и СПМ
                {
                    foreach (var chart in fftCharts)
                    {
                        for (int page = 0; page < 2; page++)
                        {
                            chart[page].Series[0].Points.Clear();
                            fft = GetFftArray(page, channelsIndexes[idx]);
                            for (int x = 0; x < halfSize; x++)
                            {
                                chart[page].Series[0].Points.AddXY(x, fft[x]);
                            }

                            chart[page].Series[0].Points.DataBindXY(freqValues, fft);
                            SetScale();
                        }

                        idx++;
                    }
                }
                else
                {
                    var smoothYValues = new List<double>();
                    foreach (var chart in fftCharts)
                    {
                        for (int page = 0; page < 2; page++)
                        {
                            smoothYValues.Clear();
                            fft = GetFftArray(page, channelsIndexes[idx]);
                            for (int i = smoothVal; i < halfSize - smoothVal; i++)
                            {
                                double avgYVal = 0;
                                for (int j = i - smoothVal; j < i + smoothVal; j++)
                                {
                                    avgYVal += fft[j];
                                }

                                smoothYValues.Add(avgYVal / 2 * smoothVal + 1);
                            }

                            chart[page].Series[0].Points.Clear();
                            int x = 0;
                            var freqLabels = new List<string>();
                            foreach (var pointY in smoothYValues)
                            {
                                chart[page].Series[0].Points.AddXY(x, pointY);
                                freqLabels.Add(freqValues[x]);
                                x++;
                            }

                            chart[page].Series[0].Points.DataBindXY(freqLabels, smoothYValues);
                            SetScale();
                        }

                        idx++;
                    }
                }
            }
        }

        private void X0_btn_Click(object sender, EventArgs e)
        {
            var firstValueMode = new FFTMode();
            firstValueMode.ShowDialog();
            if (DialogResult.OK == firstValueMode.DialogResult)
            {
                var yValues = new List<double>();
                int channelIndex = 0;
                foreach (var chart in fftCharts)
                {
                    for (int page = 0; page < 2; page++)
                    {
                        yValues.Clear();
                        chart[page].Series[0].Points.Clear();
                        yValues = GetFftArray(page, channelsIndexes[channelIndex]); 
                        for (int x = 0; x < halfSize; x++)
                        {
                            chart[page].Series[0].Points.AddXY(x, yValues[x]);
                            
                        }
                        chart[page].Series[0].Points.DataBindXY(freqValues, yValues);


                        SetScale();
                    }

                    channelIndex++;
                }
            }
        }
    }
}