using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics.IntegralTransforms;

namespace WindowsApp
{
    public partial class Spectrogram : AbstractGraphic
    {
        private double Coeff { get; set; }
        private double maxA { get; set; }
        private char[] colorPlots { get; set; }
        private byte[] curCollorPallete { get; set; }
        private Bitmap bitmap { get; set; }

        private int width { get; set; } = 900;
        private int height { get; set; } = 200;
        private double coeff_n { get; set; } = 1.5;
        private double[] A { get; set; }

        private List<byte[][]> Palletes;
        private byte[][] grey;
        private byte[][] cool;
        private byte[][] hot;

        private int curChannelIndex;
        private static List<string> timeList = new List<string>();


        public Spectrogram(MainForm parrentForm)
        {
            InitializeComponent();
            InitColorPallete();
            SetAxisXTimeLabel();
            trackBar1.Maximum = 100;
            trackBar1.Value = trackBar1.Maximum / 3;
            trackBar1.TickStyle = TickStyle.None;
            trackBar2.Maximum = 8;
            trackBar2.Minimum = 0;
            trackBar2.Value = 3;
            trackBar1.Scroll += trackBar1_Scroll;
        }

        private void SetAxisXTimeLabel()
        {
            if (timeList.Count!=signal.CountOfSamples){
                timeList.Clear();
                for (int i = 0; i < signal.CountOfSamples; i++)
                {
                    TimeSpan time = TimeSpan.FromSeconds(i * (1 / signal.Frequency));
                    timeList.Add(time.Days + "д:" + time.Hours + "ч:" + time.Minutes + "м:" + time.Seconds + "с");
                }
            }
        }

        private void resize(object sender, EventArgs e)
        {
            if (signal.Oscillogram != null)
            {
                signal.Oscillogram.Width = Width;
            }
        }

        public override void Init(int channelIndex)
        {
            curChannelIndex = channelIndex;
            Text = signal.Names[channelIndex] + " - Спектограмма";
            CreateChart(channelIndex);
            Compute(channelIndex);
            DrawPicture();
            var samples = new double[signal.CountOfSamples];
            for (int i = 0; i < signal.CountOfSamples; i++)
            {
                samples[i] = i + 1;
                chart1.Series[0].Points.AddXY(i, 1);
            }
            chart1.Series[0].Points.DataBindXY(timeList, samples);
            chart1.Show();
        }

        public void Update()
        {
            Compute(curChannelIndex);
            DrawPicture();
        }

        public void Zoom(double x0, double x1)
        {
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(x0, x1);
        }

        private string GetAxisYLabelFormat(double min, double max)
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

            return "N3";
        }

        private void CreateChart(int channelIndex)
        {
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = false;
            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisX.Maximum = signal.CountOfSamples;
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = signal.Frequency / 2;
            chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format = GetAxisYLabelFormat(0, signal.Frequency);
            chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "N0";
            chart1.ChartAreas["ChartArea1"].BackImageAlignment = ChartImageAlignmentStyle.Center;
            chart1.ChartAreas["ChartArea1"].BackImageWrapMode = ChartImageWrapMode.Scaled;
            chart1.ChartAreas["ChartArea1"].AxisY.Title = signal.Names[channelIndex] + " - частота Гц ";
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[0].Color = Color.Transparent;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.White;

            chart1.AxisScrollBarClicked += scroller;

            if (signal.Oscillogram != null)
            {
                Zoom(signal.BeginRangeOsci, signal.EndRangeOsci);
            }

        }

        private void scroller(object sender, ScrollBarEventArgs e)
        {
        }

        private double[] GetPointsRange(in double[] arr, int start, int end)
        {
            var points = new double[end - start];
            for (int i = start, j = 0; i < end; i++, j++)
            {
                points[j] = arr[i];
            }
            return points;
        }


        protected override void Compute(int channelIndex)
        {
            int Ns = width;
            int K = height;

            double Section_Base = (signal.EndRangeOsci - signal.BeginRangeOsci) / Ns;


            int Section_N = (int) (Section_Base * coeff_n);

            int NN;
            int L;
            if (Section_N <= 2 * K)
            {
                NN = 2 * K;
                L = 1;
            }
            else
            {
                L = Section_N / (2 * K);
                if (Section_N % (2 * K) != 0)
                {
                    L += 1;
                }

                NN = L * 2 * K;
            }

            A = new double[Ns * K];
            double[] x = new double[NN];
            var points = new double[signal.CountOfSamples];
            for (int i = 0; i < signal.CountOfSamples; i++)
            {
                points[i] = signal.Points[channelIndex, i].Y;
            }

            var pointsFragment = GetPointsRange(points, signal.BeginRangeOsci, signal.EndRangeOsci);

            for (int ns = 0; ns < Ns; ns++)
            {
                int n0 = (int) (ns * Section_Base);
                for (int i = 0; i < Section_N; i++)
                {
                    try
                    {
                        x[i] = pointsFragment[n0 + i];
                    }
                    catch
                    {
                    }
                }

                double s = 0;
                for (int i = 0; i < Section_N; i++)
                {
                    s += x[i];
                }

                s *= (double) 1 / Section_N;
                for (int i = 0; i < Section_N; i++)
                {
                    x[i] -= s;
                }

                for (int i = 0; i < Section_N; i++)
                {
                    double w = 0.54 - 0.46 * Math.Cos((2 * Math.PI * i) / (Section_N - 1));
                    x[i] *= w;
                }

                for (int i = Section_N; i < NN; i++)
                {
                    x[i] = 0;
                }

                double[] amplitude = new double[K];
                if (L == 1)
                {
                    Complex[] fft = new Complex[NN];
                    for (int i = 0; i < NN; i++)
                    {
                        fft[i] = new Complex(x[i], 0);
                    }

                    Fourier.Forward(fft, FourierOptions.NoScaling);

                    for (int i = 0; i < K; i++)
                    {
                        amplitude[i] = (1.0 / K) *
                                       Math.Abs(Math.Sqrt(Math.Pow(fft[i].Real, 2) + Math.Pow(fft[i].Imaginary, 2)));
                    }

                    for (int i = 0; i < K; i++)
                    {
                        amplitude[i] *= (1 / signal.Frequency);
                    }
                }
                
                for (int i = 0; i < K; i++)
                {
                    A[ns + i * Ns] = amplitude[i];
                }
            }

            maxA = A.Max();
        }

        private void InitColorPallete()
        {
            grey = new byte[256][];
            for (var i = 0; i < 256; i++) grey[i] = new byte[] {(byte) i, (byte) i, (byte) i};

            cool = new byte[256][];
            for (var i = 0; i <= 85; i++) cool[i] = new byte[] {255, (byte) (i * 3), 0};
            for (var i = 86; i <= 170; i++) cool[i] = new byte[] {0, 255, (byte) ((i - 85) * 3)};
            for (var i = 171; i <= 255; i++) cool[i] = new byte[] {0, 0, (byte) ((i - 170) * 3)};

            hot = new byte[256][];
            for (var i = 0; i <= 85; i++) hot[i] = new byte[] {0, 0, (byte) (i * 3)};
            for (var i = 86; i <= 170; i++) hot[i] = new byte[] {0, (byte) ((i - 85) * 3), 255};
            for (var i = 171; i <= 255; i++) hot[i] = new byte[] {(byte) ((i - 170) * 3), 255, 255};


            Palletes = new List<byte[][]>(3);
            Palletes.Add(grey);
            Palletes.Add(cool);
            Palletes.Add(hot);
        }

        private void DrawPicture()
        {
            Coeff = trackBar1.Value;
            if (chart1.Images.Count != 0)
            {
                chart1.Images.RemoveAt(0);
            }

            colorPlots = new char[width * height];
            for (int i = 0; i < colorPlots.Length; i++)
                colorPlots[i] = (char) Math.Min(255, (int) (A[i] / maxA * Coeff * 256));

            curCollorPallete = GetColorPallete(signal.PalleteMode);
            bitmap = new Bitmap(width, height, width * 3, PixelFormat.Format24bppRgb,
                Marshal.UnsafeAddrOfPinnedArrayElement(curCollorPallete, 0));
            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            chart1.Images.Add(new NamedImage("spectogram_image", bitmap));
            chart1.ChartAreas["ChartArea1"].BackImage = chart1.Images[0].Name;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = gridMode;
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = gridMode;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Coeff = trackBar1.Value;
            DrawPicture();
        }

        private void grey_button_Click(object sender, EventArgs e)
        {
            signal.PalleteMode = 0;
            DrawPicture();
        }

        private void hot_button_Click_1(object sender, EventArgs e)
        {
            signal.PalleteMode = 1;
            DrawPicture();
        }

        private void cool_button_Click(object sender, EventArgs e)
        {
            signal.PalleteMode = 2;
            DrawPicture();
        }

        private byte[] GetColorPallete(int index)
        {
            curCollorPallete = new byte[width * height * 3];
            for (int i = 0; i < curCollorPallete.Length; i += 3)
            for (int j = 0; j < 3; j++)
                curCollorPallete[i + j] = Palletes[index][colorPlots[i / 3]][j];

            return curCollorPallete;
        }

        private void Spectrogram_FormClosed(object sender, FormClosedEventArgs e)
        {
            signal.Spectrogram = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            gridMode = !gridMode;
            DrawPicture();
        }


        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            coeff_n = trackBar2.Value;
            Update();
        }
    }
}