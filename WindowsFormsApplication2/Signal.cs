using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


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
        public Oscillogram Oscillogram { get; set; }
        public Statistic Statistic { get; set; }
        public List<Statistic> Statistics = new List<Statistic>();
        public int BeginRangeOsci { get; private set; }
        public int EndRangeOsci { get; private set; } = instance != null ? GetInstance().CountOfSamples : 0;
        public int BeginRangeFft { get; set; }
        public int EndRangeFft { get; set; }
        public Navigation Navigation { get; private set; }
        public Spectrogram Spectrogram { get; set; }
        public ModelInputParams ModelInputParams { get; private set; }

        public Model Model { get; private set; }

        public String Textmod { get; set; }
        Hashtable hash = new Hashtable();
        public Fft Fft { get; set; }


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

        public void SetModelInputParams(ModelInputParams modelInputParams)
        {
            ModelInputParams = modelInputParams;
            if (ModelInputParams != null)
                ModelInputParams.MdiParent = MainForm;
        }

        public void SetModel(Model model)
        {
            Model = model;
            if (Model != null)
                Model.MdiParent = MainForm;
        }

        public void SetNavigation(Navigation nav)
        {
            Navigation = nav;
            MainForm.SignalInformation(nav == null ? false : true);
        }

        public void SetOscillogram(Oscillogram osc)
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
                Navigation.Show();
            }
            else
            {
                Navigation.Init(CountOfChannels);
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

        public void SetBeginRangeOsci(int st)
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

        public void SetEndRangeOsci(int value)
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
                Oscillogram.Zoom(BeginRangeOsci, EndRangeOsci);
            if (Model != null)
                Model.Zoom(BeginRangeOsci, EndRangeOsci);
        }

        public void SetEndRangeFFT(int value)
        {
            if (value <= 0)
            {
                EndRangeFft = CountOfSamples/2;
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
                Fft.Zoom(Convert.ToDouble(BeginRangeFft), Convert.ToDouble(EndRangeFft));
        }
        
        public void CloseOscillogram()
        {
            if (Oscillogram != null)
                Oscillogram.Close();
            BeginRangeOsci = 0;
            EndRangeOsci = CountOfSamples;
            MainForm.osc(false);
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
            if (CheckHash(objectName))
                hash[objectName] = obj;
            else
                hash.Add(objectName, obj);
        }

        public bool CheckHash(String objectName)
        {
            return hash.ContainsKey(objectName);
        }

        public void CreateCorrelation(int channelIndex)//create Factory
        {
            if (Correlation == null)
            {
                Correlation = new Correlation(MainForm);
            }
            Correlation.Init(channelIndex);
            Correlation.Show();
        }
    }
    
}