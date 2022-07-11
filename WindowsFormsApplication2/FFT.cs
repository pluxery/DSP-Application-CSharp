using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace WindowsApp
{
    public partial class FFT : Form
    {   
        bool localScaleMode = false;
        bool sharpMode = true;
        bool markerMode = false;

        Signal signal = Signal.GetInstance();
        private int N_part = Signal.GetInstance().CountOfSamples / 2;


        public List<Chart[]> charts = new List<Chart[]>();
        Chart chart;
        public List<int> channelsList = new List<int>();

        private int X1;
        private int Y1;
        private int X2;
        private int Y2;

        private bool isLogY = false;
        private bool isSmooth = false;

        double[] Re;
        double[] Im;
        double Re_x0;
        double Im_x0;

        int prob = 30;
        private IntervalFFT interval_fft;
        private int W = 700;
        private int H = 200;

        TabControl tabControl1;
        private TabPage tabPage1, tabPage2;
        System.ComponentModel.Container components = new System.ComponentModel.Container();
        private ContextMenuStrip contextMenuStrip1;


        private void resize(object sender, EventArgs e)
        {
            if (charts.Count > 0)
            {
                W = Width;
                H = (Height - prob * 3) / charts.Count;
                SetScale();
            }
        }

        public FFT(MainForm ParrentForm)
        {
            InitializeComponent();
    
        }

        private void ChangeFFTMode(int fftMode)
        {
            if (fftMode == 0) //X(0) = |X(1)|
            {
                Re[0] = Re[1];
                Im[0] = Im[1];
            }
            else if (fftMode == 1) // X(0) = 0
            {
                Re[0] = 0;
                Im[0] = 0;
            }
            else //X(0) = X(0)
            {
                Re[0] = Re_x0;
                Im[0] = Im_x0;
            }
        }

        public void ComputeFFT(int channelIndex)
        {
            Re = new double[signal.CountOfSamples];
            Im = new double[signal.CountOfSamples];
            for (int i = 0; i < signal.CountOfSamples; i++)
            {
                Re[i] = signal.Points[channelIndex, i].Y;
                Im[i] = 0.0F;
            }

            Re_x0 = Re[0];
            Im_x0 = Im[0];

            FastFurierTransform fft = new FastFurierTransform();
            fft.init((uint) Math.Log(signal.CountOfSamples, 2));
            fft.run(Re, Im);

            ChangeFFTMode(signal.FFTMode);
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
            signal.EndRangeFft = signal.Frequency;
            double fd = signal.Frequency;
            if (contextMenuStrip1 == null)
                ContextStrip();
            if (!channelsList.Contains(channelIndex))
            {
                Chart[] ch = new Chart[4];
                ComputeFFT(channelIndex);

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

                chart = CreateChart(channelIndex);
                ch[0] = chart;

                for (int i = 0; i < N_part; i++)
                {
                    if (signal.FFTMode == 2 && i == 0)
                        chart.Series[0].Points.AddXY(i * (fd / N_part),
                            Math.Abs(Math.Sqrt(Re_x0 * Re_x0 + Im_x0 * Im_x0)));
                    else
                        chart.Series[0].Points.AddXY(i * (fd / N_part),
                            Math.Abs(Math.Sqrt(Re[i] * Re[i] + Im[i] * Im[i])));
                }

                tabPage1.Controls.Add(chart);

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

                chart = CreateChart(channelIndex);
                ch[1] = chart;
                for (int i = 0; i < N_part; i++)
                {
                    if (signal.FFTMode == 2 && i == 0)
                        chart.Series[0].Points.AddXY(i * (fd / N_part), Math.Abs(Re_x0 * Re_x0 + Im_x0 * Im_x0));
                    else
                        chart.Series[0].Points.AddXY(i * (fd / N_part), Math.Abs(Re[i] * Re[i] + Im[i] * Im[i]));
                }

                tabPage2.Controls.Add(chart);


                charts.Add(ch);

                if (!tabControl1.Controls.Contains(tabPage1))
                    tabControl1.Controls.Add(tabPage1);
                if (!tabControl1.Controls.Contains(tabPage2))
                    tabControl1.Controls.Add(tabPage2);


                Width = W;
                tabControl1.Size = new Size(W, H * charts.Count + prob);
                tabPage1.Size = new Size(W, H * charts.Count + prob);
                tabPage2.Size = new Size(W, H * charts.Count + prob);
                Height = prob * 3 + H * charts.Count;
                if (!Controls.Contains(tabControl1))
                    Controls.Add(tabControl1);
            }
        }

        private Chart CreateChart(int n)
        {
            Chart chart = new Chart();
            if (!channelsList.Contains(n))
                channelsList.Add(n);
            signal.MainForm.CheckItemFFT(n);
            chart = new Chart();
            chart.Parent = this;
            chart.Size = new Size(W, H);
            chart.Location = new Point(0, this.H * charts.Count);
            ChartArea area = new ChartArea();
            area.Name = "myGraph";
            area.AxisX.ScrollBar.Enabled = false;//Todo сделать скроллбар
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = signal.Frequency;
            area.AxisX.LabelStyle.Format = GetLabelFormat(0, signal.Frequency);
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
            area.AxisY.IsLogarithmic = isLogY;
            area.AxisY.LabelStyle.Format = "N0";
            area.AxisX.ScaleView.Zoom(signal.BeginRangeFft, signal.EndRangeFft);
            chart.ChartAreas.Add(area);

            Series series = new Series();
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

            series.LegendText = signal.Names[n];
            chart.Legends.Add(signal.Names[n]);
            chart.Legends[signal.Names[n]].Docking = Docking.Top;
            chart.Legends[signal.Names[n]].Alignment = StringAlignment.Center;

            chart.MouseDown += position1;
            chart.MouseUp += position2;

            chart.AxisScrollBarClicked += scroller;
            chart.AxisViewChanged += viewchanged;
            chart.GetToolTipText += tooltip;
            return chart;
        }

        private void tooltip(object sender, ToolTipEventArgs e) //при наведении на точку показывать значения X, Y
        {
            switch (e.HitTestResult.ChartElementType)
            {
                case ChartElementType.DataPoint:
                    var dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                    e.Text = string.Format("X = {0}\nY = {1}", dataPoint.XValue, dataPoint.YValues[0]);
                    break;
            }
        }


        private void position1(object sender, MouseEventArgs e)
        {
            X1 = e.X;
            Y1 = e.Y;
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    chart = (Chart) sender;
                }
                catch (Exception ex)
                {
                }
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

            if (difference >= 1)
            {
                return "N1";
            }

            if (difference > 0.1)
            {
                return "N2";
            }
            if (difference < 0.001)
            {
                return "N5";
            }

            if (difference < 0.01)
            {
                return "N4";
            }

            return "N3";
        }

        public void ZoomCharts(double x1, double x2)
        {
            foreach (Chart[] ch in charts)
            {
                for (int i = 0; i <= 1; i++)
                {
                    ch[i].ChartAreas["myGraph"].AxisX.ScaleView.Zoom(x1, x2);
                    ch[i].ChartAreas["myGraph"].AxisX.LabelStyle.Format = GetLabelFormat(x1, x2);
                }
            }

            SetScale();
        }

        private void SetScale()
        {
            for (int i = 0; i < charts.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    charts[i][j].Bounds = new Rectangle(0, H * i, W, H);
                    if (localScaleMode)
                    {
                        double localMin = Double.MaxValue;
                        double localMax = Double.MinValue;

                        var pointsX = new List<double>();
                        foreach (var point in charts[i][j].Series[0].Points)
                            pointsX.Add(point.XValue);
                        int startIndex = pointsX.FindIndex(minX => minX >= signal.BeginRangeFft);

                        var pointsXreverse = new List<double>();
                        for (int n = charts[i][j].Series[0].Points.Count - 1; n >= 0; n--)
                            pointsXreverse.Add(charts[i][j].Series[0].Points[n].XValue);

                        double endValue = pointsXreverse.Find(x => x <= signal.EndRangeFft);
                        int endIndex = pointsX.FindIndex(x => x == endValue);

                        for (int k = startIndex; k < endIndex; k++)
                        {
                            localMin = localMin > charts[i][j].Series[0].Points[k].YValues[0]
                                ? charts[i][j].Series[0].Points[k].YValues[0]
                                : localMin;
                            localMax = localMax < charts[i][j].Series[0].Points[k].YValues[0]
                                ? charts[i][j].Series[0].Points[k].YValues[0]
                                : localMax;
                        }

                        charts[i][j].ChartAreas["myGraph"].AxisY.Minimum = localMin;
                        charts[i][j].ChartAreas["myGraph"].AxisY.Maximum = localMax;
                    }
                    else
                    {
                        double globalMax = Double.MinValue;
                        for (int k = 0; k < charts[i][j].Series[0].Points.Count; k++)
                        {
                            globalMax = globalMax < charts[i][j].Series[0].Points[k].YValues[0]
                                ? charts[i][j].Series[0].Points[k].YValues[0]
                                : globalMax;
                        }

                        charts[i][j].ChartAreas["myGraph"].AxisY.Minimum = 0;
                        charts[i][j].ChartAreas["myGraph"].AxisY.Maximum = globalMax;
                    }
                }
            }

            tabControl1.Size = new Size(W, H * charts.Count + prob);
            tabPage1.Size = new Size(W, H * charts.Count + prob);
            tabPage2.Size = new Size(W, H * charts.Count + prob);
            Width = W;
            Height = prob * 3 + H * charts.Count;
        }

        private void scroller(object sender, ScrollBarEventArgs e)
        {
        }

        private void viewchanged(object sender, ViewEventArgs e)
        {
            signal.BeginRangeFft = e.ChartArea.AxisX.ScaleView.Position;
            signal.SetEndRangeFFT(e.ChartArea.AxisX.ScaleView.Position + e.ChartArea.AxisX.ScaleView.Size);
            SetScale();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int channelIndex = 0;
            for (int i = 0; i < charts.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (chart.Equals(charts[i][j]))
                        channelIndex = i;
                }
            }

            remove(channelIndex);
        }

        public void remove(int k)
        {
            if (charts.Count == 1)
                Close();
            else
            {
                for (int j = 0; j < 2; j++)
                {
                    charts[k][j].Visible = false;
                    charts[k][j].Dispose();
                }

                charts.RemoveAt(k);
                SetScale();
                signal.MainForm.UnCheckItemDPF(channelsList[k]);
                channelsList.RemoveAt(k);
            }
        }


        public void close(object sender, FormClosedEventArgs e)
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
            if (charts.Count > 0)
            {
                sharpMode = !sharpMode;
                grid.Checked = sharpMode;
                for (int i = 0; i < charts.Count; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        charts[i][j].ChartAreas["myGraph"].AxisX.MajorGrid.Enabled = sharpMode;
                        charts[i][j].ChartAreas["myGraph"].AxisY.MajorGrid.Enabled = sharpMode;
                    }
                }
            }
        }

        private void marks_Click(object sender, EventArgs e)
        {
            if (charts.Count > 0)
            {
                markerMode = !markerMode;
                this.marks.Checked = markerMode;
                for (int i = 0; i < charts.Count; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (markerMode)
                            charts[i][j].Series[0].MarkerStyle = MarkerStyle.Circle;
                        else
                            charts[i][j].Series[0].MarkerStyle = MarkerStyle.None;
                    }
                }
            }
        }

        private void interv_Click(object sender, EventArgs e)
        {
            interval_fft = new IntervalFFT();
            interval_fft.Hide();
            interval_fft.Show();
        }

        private void lgy_Click(object sender, EventArgs e)
        {
            if (signal.FFTMode != 1)
            {
                localScaleMode = true;

                глобальныйМасштабToolStripMenuItem.Checked = false;
                локальныйМасштабToolStripMenuItem.Checked = true;


                SetScale();
                if (charts.Count > 0)
                {
                    isLogY = !isLogY;

                    if (isLogY) глобальныйМасштабToolStripMenuItem.Enabled = false;
                    else глобальныйМасштабToolStripMenuItem.Enabled = true;

                    logYBtn.Checked = isLogY;
                    for (int i = 0; i < charts.Count; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            charts[i][j].ChartAreas["myGraph"].AxisY.IsLogarithmic = isLogY;
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
            GetSpectralYPointsByPage(int numTabPage,
                int channelIndex) // для какой страницы создаем точки Y (Амплитудный или СПМ)
        {
            var pointsY = new List<double>();
            ComputeFFT(channelIndex);
            if (numTabPage == 0)
            {
                for (int i = 0; i < N_part; i++)
                    pointsY.Add(Math.Abs(Math.Sqrt(Re[i] * Re[i] + Im[i] * Im[i])));
            }
            else if (numTabPage == 1)
            {
                for (int i = 0; i < N_part; i++)
                    pointsY.Add(Math.Abs(Re[i] * Re[i] + Im[i] * Im[i]));
            }
            else
            {
                throw new Exception($"страница = {numTabPage} (не равно 0 или 1!)");
            }

            return pointsY;
        }


        private void smoothBtn_Click(object sender, EventArgs e)
        {
            SmoothDialog smoothDialog = new SmoothDialog();
            smoothDialog.ShowDialog();
            if (DialogResult.OK == smoothDialog.DialogResult)
            {
                isSmooth = !isSmooth;
                smoothBtn.Checked = isSmooth;
                int smoothValue = Signal.GetInstance().Smooth;
                var SpectralPointsY = new List<double>();
                int channelIndex = 0;
                if (smoothValue == 0) //если нет сглаживания просто рисуем изначальный Амплитудный спектр и СПМ
                {
                    foreach (var chart in charts)
                    {
                        for (int page = 0; page < 2; page++)
                        {
                            chart[page].Series[0].Points.Clear();
                            SpectralPointsY = GetSpectralYPointsByPage(page, channelsList[channelIndex]);
                            for (int x = 0; x < N_part; x++)
                                chart[page].Series[0].Points.AddXY(x * (signal.Frequency / N_part), SpectralPointsY[x]);
                            SetScale();
                        }

                        channelIndex++;
                    }
                }
                else
                {
                    var SmoothPointsY = new List<double>();
                    foreach (var chart in charts)
                    {
                        for (int page = 0; page < 2; page++)
                        {
                            SmoothPointsY.Clear();
                            SpectralPointsY = GetSpectralYPointsByPage(page, channelsList[channelIndex]);
                            for (int i = smoothValue; i < N_part - smoothValue; i++)
                            {
                                double avgYpoint = 0;
                                for (int j = i - smoothValue; j < i + smoothValue; j++)
                                {
                                    avgYpoint += SpectralPointsY[j];
                                }

                                SmoothPointsY.Add(avgYpoint / 2 * smoothValue + 1);
                            }

                            chart[page].Series[0].Points.Clear();
                            int x = 0;
                            foreach (var pointY in SmoothPointsY)
                            {
                                chart[page].Series[0].Points.AddXY(x * (signal.Frequency / N_part), pointY);
                                x++;
                            }

                            SetScale();
                        }

                        channelIndex++;
                    }
                }
            }
        }

        private void X0_btn_Click(object sender, EventArgs e)
        {
            var FirstValueMode = new FFTMode();
            FirstValueMode.ShowDialog();
            if (DialogResult.OK == FirstValueMode.DialogResult)
            {
                var pointsY = new List<double>();
                int channelIndex = 0;
                foreach (var chart in charts)
                {
                    for (int page = 0; page < 2; page++)
                    {
                        pointsY.Clear();
                        chart[page].Series[0].Points.Clear();
                        pointsY = GetSpectralYPointsByPage(page, channelsList[channelIndex]);
                        for (int x = 0; x < N_part; x++)
                            chart[page].Series[0].Points.AddXY(x * (signal.Frequency / N_part), pointsY[x]);
                        SetScale();
                    }

                    channelIndex++;
                }
            }
        }
    }
}