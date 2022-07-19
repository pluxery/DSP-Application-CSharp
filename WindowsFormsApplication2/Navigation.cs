using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace WindowsApp
{
    public partial class Navigation : Form
    
    {
        protected static Signal signal = Signal.GetInstance();
        protected Chart curChart;
        public List<Chart> charts = new List<Chart>();
        bool draw = false;
        private int X1;
        private int Y1;
        private int X2;
        private int Y2;
        Graphics formGraphics;
        int W = 200;
        int H = 95;
        MainForm Parrent;
        bool isExistRedLineOnChart = false;

        public Navigation(MainForm ParrentForm)
        {
            if (signal.Navigation != null)
            {
                signal.Navigation.Close();
            }

            InitializeComponent();
            Init(signal.CountOfChannels);
            Parrent = ParrentForm;
            signal.SetNavigation(this);
            signal.MainForm.CallItem();
        }

        //отрисовка графиков

        public  void Init(int countOfChannels)
        {
            Text = signal.Path.Split('\\')[signal.Path.Split('\\').Length - 1];
            for (int channel = 0; channel < countOfChannels; channel++)
            {
                curChart=CreateChart(channel);
                charts.Add(curChart);
                for (int i = 0; i < signal.CountOfSamples; i++)
                {
                    curChart.Series[0].Points.AddY(signal.Points[channel, i].Y);
                }

                charts[channel].MouseDown += position1;
                charts[channel].MouseMove += position;
                charts[channel].MouseUp += position2;
            }

            draw = true;

            Height = signal.CountOfChannels * H + 50;
            Width = W;
        }

        private  Chart CreateChart(int channelIndex)
        {
            var chart = new Chart();
            chart.Parent = this;
            chart.SetBounds(0, H * channelIndex, W - 19, H);
            ChartArea area = new ChartArea();
            area.Name = "myGraph";
            area.BorderDashStyle = ChartDashStyle.Solid;
            area.BorderColor = Color.Black;
            area.BorderWidth = 1;
            area.AxisY.Minimum = signal.Min(channelIndex, 0, signal.CountOfSamples);
            area.AxisY.Maximum = signal.Max(channelIndex, 0, signal.CountOfSamples);
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = signal.CountOfSamples;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisX.Enabled = AxisEnabled.False;
            area.AxisY.Enabled = AxisEnabled.False;
            chart.ChartAreas.Add(area);
            Series series1 = new Series();
            series1.ChartArea = "myGraph";
            series1.ChartType = SeriesChartType.Line;
            series1.ChartType = SeriesChartType.Line;
            series1.Color = Color.Black;
            series1.LegendText = signal.Names[channelIndex];
            chart.Legends.Add(signal.Names[channelIndex]);
            chart.Legends[signal.Names[channelIndex]].Docking = Docking.Bottom;
            chart.Series.Add(series1);
            return chart;
        }

        private void осцилограммаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int channelIndex;
            for (channelIndex = 0; !curChart.Equals(charts[channelIndex]); channelIndex++)
            {
            }
            
            signal.Oscillogram = (Oscillogram)new FactoryOscillogram().Create(channelIndex);
        }

        public void DrawRangeLines(int j)
        {
            switch (j)
            {
                case 0: //стартовая вертикальная линия
                    for (int i = 0; i < charts.Count; i++)
                    {
                        if (signal.Oscillogram != null)
                        {
                            if (signal.Oscillogram.channelIndexes.Contains(i) || curChart == charts[i])
                            {
                                charts[i].Annotations.Clear();
                                charts[i].Annotations.Add(VAlines(charts[i], "line1", signal.BeginRangeOsci));
                            }
                            else
                            {
                                charts[i].Annotations.Clear();
                            }
                        }
                        else
                        {
                            charts[i].Annotations.Clear();
                        }
                    }

                    break;
                case 1: //финишная вертикальная линия
                    if (curChart.Annotations.Count > 1)
                        curChart.Annotations.RemoveAt(curChart.Annotations.Count - 1);
                    for (int i = 0; i < charts.Count; i++)
                        if (charts[i].Annotations.Count > 0)
                        {
                            try
                            {
                                charts[i].Annotations.Add(VAlines(charts[i], "line2", signal.EndRangeOsci));
                            }
                            catch (Exception ex)
                            {
                            }
                        }

                    break;
            }
        }

        private VerticalLineAnnotation VAlines(Chart ch, string name, double x)
        {
            VerticalLineAnnotation VA = new VerticalLineAnnotation();
            VA.AxisX = ch.ChartAreas[0].AxisX;
            VA.AllowMoving = true;
            VA.IsInfinitive = true;
            VA.ClipToChartArea = ch.ChartAreas[0].Name;
            VA.Name = name;
            VA.LineColor = Color.Red;
            VA.LineWidth = 1;
            try
            {
                VA.X = x;
            }
            catch (Exception ex)
            {
            }

            return VA;
        }

        //позиция для первой выделительной линии
        private void position1(object sender, MouseEventArgs e)
        {
            if (draw) //если можно рисовать лиииинии :3
            {
                X1 = e.X;
                Y1 = e.Y;
                curChart = (Chart) sender;
                if (e.Button == MouseButtons.Left)
                {
                    isExistRedLineOnChart = true; //началась отрисовка вертикальных линий 
                    //добавление нач позиции в диспетчер
                    signal.SetBeginRangeOsci((int) curChart.ChartAreas["myGraph"].AxisX.PixelPositionToValue(e.X));
                }
            }
        }

        private void position2(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                X2 = e.X;
                Y2 = e.Y;
                if (e.Button == MouseButtons.Left && isExistRedLineOnChart)
                {
                    isExistRedLineOnChart = false; //закончилась отрисовка верт. линий
                    try
                    {
                        signal.SetEndRangeOsci((int) curChart.ChartAreas["myGraph"].AxisX.PixelPositionToValue(e.X));
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        //при отрисовке в этом окне в выделенном канале будет рисоваться зажатая верт. линия
        private void position(object sender, MouseEventArgs e)
        {
            if (draw && isExistRedLineOnChart)
            {
                int X = e.X;
                int Y = e.Y;
                if (e.Button == MouseButtons.Left)
                {
                    if (curChart.Annotations.Count > 1)
                        curChart.Annotations.RemoveAt(curChart.Annotations.Count - 1);
                    try
                    {
                        curChart.Annotations.Add(VAlines(curChart, "line",
                            curChart.ChartAreas["myGraph"].AxisX.PixelPositionToValue(e.X)));
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        private void Exercise_Paint(object sender, PaintEventArgs e)
        {
            formGraphics = e.Graphics;
        }
        
        public void close(object sender, FormClosedEventArgs e)
        {
            signal.SetNavigation(null);
            signal.SetDelNav(true);
            signal.MainForm.SaveFlag(false);
            signal.MainForm.CallItem();
            if (signal.Statistic != null) signal.Statistic.Close();
            if (signal.Spectrogram != null) signal.Spectrogram.Close();
            if (signal.Oscillogram != null) signal.Oscillogram.Close();
        }

        private void dPFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int channelIndex;
            for (channelIndex = 0; !curChart.Equals(charts[channelIndex]); channelIndex++)
            {
            }

            signal.Fft = (Fft)new FactoryFft().Create(channelIndex);
        }

        private void корреляционныйАнализToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int channelIndex;
            for (channelIndex = 0; !curChart.Equals(charts[channelIndex]); channelIndex++)
            {
            }

            signal.CreateCorrelation(channelIndex);
        }

        private void спектральныйАнализToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int channelIndex;
            for (channelIndex = 0; !curChart.Equals(charts[channelIndex]); channelIndex++)
            {
            }
            signal.Fft = (Fft)new FactoryFft().Create(channelIndex);
        }

        private void спектограммаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int channelIndex;
            for (channelIndex = 0; !curChart.Equals(charts[channelIndex]); channelIndex++)
            {
            }

            signal.CreateSpectogram(channelIndex);
        }

        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int channelIndex;
            for (channelIndex = 0; !curChart.Equals(charts[channelIndex]); channelIndex++)
            {
            }
            signal.Statistic = (Statistic)new FactoryStatistic().Create(channelIndex);
            
        }
    }
}