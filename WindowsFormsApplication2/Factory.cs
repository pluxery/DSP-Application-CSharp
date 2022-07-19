using System.Drawing;
using MathNet.Numerics.LinearAlgebra;

namespace WindowsApp
{
    public abstract class Factory
    {
        protected static Signal signal = Signal.GetInstance();
        public abstract AbstractGraphic Create(int channelIndex);
    }

    public class FactoryFft : Factory
    {
        public override AbstractGraphic Create(int channelIndex)
        {
            if (signal.Fft == null)
            {
                var fft = new Fft(signal.MainForm);
                fft.Init(channelIndex);
                fft.Show();
                return fft;
            }

            signal.Fft.Init(channelIndex);
            signal.Fft.Show();
            return signal.Fft;
        }
    }
    public class FactoryStatistic : Factory
    {
        public override AbstractGraphic Create(int channelIndex)
        {
            var statistic = new Statistic(signal.MainForm);
            statistic.Init(channelIndex);
            statistic.Show();
            return statistic;
        }
    }
    public class FactoryOscillogram : Factory
    {
        public override AbstractGraphic Create(int channelIndex)
        {
            if (signal.Oscillogram == null)
            {
                var oscillogram = new Oscillogram(signal.MainForm);
                oscillogram.Init(channelIndex);
                oscillogram.Show();
                return oscillogram;
            }

            signal.Oscillogram.Init(channelIndex);
            signal.Oscillogram.Show();
            return signal.Oscillogram;
        }
    }
    
}