using System;
using System.Collections;
using System.Drawing;


namespace WindowsApp
{
    public class Signal
    {
        public bool WasNavigation { get; set; }
        private static Signal instance;
        public MainForm MainForm { get; set; }
        public SignalInfo SignalInformation { get; private set; }
        public int CountOfChannels { get; set; }
        public int CountOfSamples { get; set; }
        public double Frequency { get; set; }
        public String Path { get; set; }

        public int FFTMode { get; set; } = 2;
        public int PalleteMode { get; set; } = 2;

        private bool nav_was_del;

        public String[] Names { get; set; }

        public DateTime DateBegin { get; set; }
        public DateTime DateStartOfSignal { get; private set; }
        public PointF[,] Points { get; set; }
        public oscillogram Oscillogram { get; private set; }
        public double BeginRangeOsci { get; private set; }
        public double EndRangeOsci { get; private set; } = instance != null ? GetInstance().CountOfSamples : 0;
        public double BeginRangeFft { get; set; }
        public double EndRangeFft { get; set; }
        public Navigation Navigation { get; private set; }
        public Spectrogram Spectrogram { get; set; }
        public ModelInputParams ModelInputParams { get; private set; }

        public Model Model { get; private set; }

        public String Textmod { get; set; }
        Hashtable hash = new Hashtable();
        public FFT Fft { get; set; }


        public Correlation Correlation { get; set; }
        public int Smooth;

        public static Signal GetInstance()
        {
            if (instance != null)
                return instance;
            instance = new Signal();
            return instance;
        }

        public void SetSignalInfo()
        {
            if (Navigation != null)
                SignalInformation = new SignalInfo();
            else
                SignalInformation = null;
        }

        public void SetModelling(ModelInputParams modelInputParams)
        {
            ModelInputParams = modelInputParams;
            if (ModelInputParams != null)
                ModelInputParams.MdiParent = MainForm;
        }

        public void SetModel(Model model)
        {
            this.Model = model;
            if (this.Model != null)
                this.Model.MdiParent = MainForm;
        }

        public void SetNavigation(Navigation nav)
        {
            Navigation = nav;
            MainForm.SignalInformation(nav == null ? false : true);
        }

        public void SetOscillogram(oscillogram osc)
        {
            if (osc == null)
                MainForm.osc(false);
            else
                MainForm.osc(true);
            Oscillogram = osc;
        }

        public void SetDateStartOfSignal()
        {
            DateStartOfSignal = DateBegin.Add(TimeSpan.FromSeconds((1 / Frequency) * CountOfSamples));
        }

        public void SetPoints(PointF[,] points)
        {
            Points = points;
        }


        public void AddChannel(PointF[] points, String name)
        {
            if (Points == null)
            {
                if (points != null)
                {
                    Points = new PointF[1, CountOfSamples];
                    for (int i = 0; i < CountOfSamples; i++)
                        Points[0, i] = points[i];
                }
            }
            else
            {
                PointF[,] newPoints = new PointF[CountOfChannels + 1, CountOfSamples];
                for (int i = 0; i < CountOfChannels; i++)
                for (int j = 0; j < CountOfSamples; j++)
                    newPoints[i, j] = Points[i, j];

                for (int i = 0; i < CountOfSamples; i++)
                    newPoints[CountOfChannels, i] = points[i];
                Points = newPoints;
            }

            if (Names != null)
            {
                String[] newNames = new String[CountOfChannels + 1];
                Names.CopyTo(newNames, 0);
                newNames[CountOfChannels] = name;
                Names = newNames;
            }
            else
            {
                Names = new string[1];
                Path = "Модели Сигналов";
                Names[0] = name;
            }

            CountOfChannels += 1;
            if (Navigation == null)
            {
                Navigation = new Navigation(MainForm);
                Navigation.MdiParent = MainForm;
                Navigation.Show();
            }
            else
            {
                Navigation.PlotSignal();
                SetNavigation(Navigation);
                MainForm.CallItem();
            }
        }

        public double Min(int channelIndex, int beginRange, int endRange)
        {
            double min = Points[channelIndex, beginRange].Y;
            for (int i = beginRange; i < endRange; i++)
            {
                min = min > Points[channelIndex, i].Y ? Points[channelIndex, i].Y : min;
            }

            return min;
        }

        public double Max(int channelIndex, int beginRange, int endRange)
        {
            double max = Points[channelIndex, beginRange].Y;
            for (int i = beginRange; i < endRange; i++)
            {
                max = max < Points[channelIndex, i].Y ? Points[channelIndex, i].Y : max;
            }

            return max;
        }

        public void SetBeginRangeOsci(double st)
        {
            BeginRangeOsci = st;
            if (Navigation != null)
                if (Points != null)
                    Navigation.DrawRangeLines(0);
        }

        public void SetDelNav(bool model_st)
        {
            nav_was_del = model_st;
        }

        public void SetEndRangeOsci(double value)
        {
            EndRangeOsci = value;
            if (Navigation != null)
                if (Points != null)
                    Navigation.DrawRangeLines(1);
            if (BeginRangeOsci > EndRangeOsci)
            {
                EndRangeOsci = BeginRangeOsci;
                BeginRangeOsci = value;
            }

            if (EndRangeOsci - BeginRangeOsci < 1)
            {
                EndRangeOsci = BeginRangeOsci + 1;
            }

            if (Oscillogram != null)
                Oscillogram.ZoomCharts(BeginRangeOsci, EndRangeOsci);
            if (Model != null)
                Model.ZoomCharts(BeginRangeOsci, EndRangeOsci);
        }

        public void setEndRangeFFT(double value)
        {
            if (value <= 0)
            {
                EndRangeFft = Frequency;
            }
            else
            {
                EndRangeFft = value;
            }

            if (BeginRangeFft > EndRangeFft)
            {
                EndRangeFft = BeginRangeFft;
                BeginRangeFft = value;
            }


            if (Fft != null)
                Fft.ZoomCharts(Convert.ToDouble(BeginRangeFft), Convert.ToDouble(EndRangeFft));
        }


        public void CloseOscillogram()
        {
            if (Oscillogram != null)
                Oscillogram.Close();
            BeginRangeOsci = 0;
            EndRangeOsci = CountOfSamples;
            MainForm.osc(false);
        }

        public void CreateSpectogram(int channelIndex)
        {
            if (Spectrogram == null)
            {
                Spectrogram = new Spectrogram(MainForm);
                Spectrogram.MdiParent = MainForm;
                try
                {
                    Spectrogram.Owner = MainForm;
                }
                catch (ArgumentException e)
                {
                }
            }
            else
            {
                Spectrogram.Close();
                Spectrogram = new Spectrogram(MainForm);
                Spectrogram.MdiParent = MainForm;
                try
                {
                    Spectrogram.Owner = MainForm;
                }
                catch (ArgumentException e)
                {
                }
            }

            Spectrogram.Init(channelIndex);
            Spectrogram.Show();
        }

        public void CreateOscillogram(int channelIndex)
        {
            if (Oscillogram == null)
            {
                SetOscillogram(new oscillogram(MainForm));
                Oscillogram.MdiParent = MainForm;

                try
                {
                    Oscillogram.Owner = MainForm;
                }
                catch (ArgumentException argEx)
                {
                    //MessageBox.Show("Error: Could not do this. Original error: " + argEx.Message);
                }
            }

            Oscillogram.Init(channelIndex, Min(channelIndex, 0, CountOfSamples), Max(channelIndex, 0, CountOfSamples));
            Oscillogram.Show();
        }

        public Object GetHash(String objectName)
        {
            return hash[objectName];
        }

        public void RemoveHash(String objectName)
        {
            hash.Remove(objectName);
        }

        public void SetHash(String objectName, Object obj)
        {
            if (ChechHash(objectName))
                hash[objectName] = obj;
            else
                hash.Add(objectName, obj);
        }

        public bool ChechHash(String objectName)
        {
            return hash.ContainsKey(objectName);
        }

        public void CreateStatistic(int channelIndex)
        {
            Statistic statistic = (Statistic) GetHash("stat");
            if (statistic == null)
            {
                statistic = new Statistic(MainForm);
                statistic.MdiParent = MainForm;
                try
                {
                    statistic.Owner = MainForm;
                }
                catch (ArgumentException argEx)
                {
                }
            }

            statistic.Init(channelIndex);
            statistic.Show();
            SetHash("stat", statistic);
        }

        public void CreateFFT(int channelIndex)
        {
            if (Fft == null)
            {
                Fft = new FFT(MainForm);
                Fft.MdiParent = MainForm;

                try
                {
                    Fft.Owner = MainForm;
                }
                catch (ArgumentException argEx)
                {
                    //MessageBox.Show("Error: Could not do this. Original error: " + argEx.Message);
                }
            }

            Fft.Init(channelIndex);
            Fft.Show();
        }


        public void CreateCorrelation(int channelIndex)
        {
            if (Correlation == null)
            {
                Correlation = new Correlation(MainForm);
                Correlation.MdiParent = MainForm;

                try
                {
                    Correlation.Owner = MainForm;
                }
                catch (ArgumentException argEx)
                {
                    //MessageBox.Show("Error: Could not do this. Original error: " + argEx.Message);
                }
            }

            Correlation.Init(channelIndex);
            Correlation.Show();
        }
    }
}