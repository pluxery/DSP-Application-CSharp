using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsApp
{
    public  class AbstractGraphic:Form //без ключевого слова abstract т.к. designer-конструктор будет выдавать ошибку 
    {
        protected bool localScaleMode=false;
        protected bool gridMode =false;
        protected bool markerMode=true;
        public List<int> channelIndexes = new List<int>();
        
        public static Signal signal = Signal.GetInstance();
        protected Chart curChart;
        public List<Chart> charts = new List<Chart>();
        
        protected const int margin = 30;
        
        protected AbstractGraphic(){}
        
        public virtual void Init(int channelIndex)
        {
        }

        protected virtual Chart CreateChart(int channelIndex)
        {
            return new Chart();
            
        }

        protected virtual void Compute(int channelIndex){}

        protected virtual void SetScale(){}
        
        protected double Min(in DataPointCollection points, int beginIndex, int endIndex)
        {
            double min = Double.MaxValue;
            for (int i = beginIndex; i < endIndex; i++)
            {
                if (min > points[i].YValues[0])
                    min = points[i].YValues[0];
            }

            return min;
        }

        protected double Max(in DataPointCollection points, int beginIndex, int endIndex)
        {
            double max = Double.MinValue;
            for (int i = beginIndex; i < endIndex; i++)
            {
                if (max < points[i].YValues[0])
                    max = points[i].YValues[0];
            }

            return max;
        }

    }
}