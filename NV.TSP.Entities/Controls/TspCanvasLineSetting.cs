using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TSP.Controls
{
    public class TspCanvasLineSetting
    {
        private double m_width;
        private Brush m_color;


        public TspCanvasLineSetting DefaultLine
        {
            get
            {
                var lineSetting = new TspCanvasLineSetting();
                lineSetting.Width = 1.0;
                lineSetting.Color = Brushes.Black;
                return lineSetting;
            }
        }
        public TspCanvasLineSetting DefaultOldLine
        {
            get
            {
                var lineSetting = new TspCanvasLineSetting();
                lineSetting.Width = 2.0;
                lineSetting.Color = Brushes.Green;
                return lineSetting;
            }
        }
        public TspCanvasLineSetting DefaultNewLine
        {
            get
            {
                var lineSetting = new TspCanvasLineSetting();
                lineSetting.Width = 1.0;
                lineSetting.Color = Brushes.Yellow;
                return lineSetting;
            }
        }



        public double Width
        {
            get { return m_width; }
            set { m_width = value; }
        }
        public Brush Color
        {
            get { return m_color; }
            set { m_color = value; }
        }


        public TspCanvasLineSetting()
        {
            
        }



        public System.Windows.Shapes.Line CreateLine(TSP.Entities.Line l)
        {
            var line = new System.Windows.Shapes.Line();
            line.StrokeThickness = Width;
            line.Stroke = Color;
            line.Fill = Color;
            return line;
        }
    }
}
