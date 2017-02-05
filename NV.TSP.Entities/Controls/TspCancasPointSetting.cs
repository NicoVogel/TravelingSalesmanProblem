using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using TSP.Entities;

namespace TSP.Controls
{
    public class TspCancasPointSetting
    {
        /// <summary>
        /// Default diameter
        /// </summary>
        public const double DefaultDiameter = 5.0;
        // public const double DefaultBorderSize = 0;



        private double m_diameter;
        private Brush m_color;


        #region Properties


        /// <summary>
        /// Diameter of the point
        /// </summary>
        public double Diameter
        {
            get { return m_diameter; }
            set { m_diameter = value; }
        }
        ///// <summary>
        ///// Border size of the point
        ///// </summary>
        //public double BorderSize
        //{
        //    get { return m_borderSize; }
        //    set { m_borderSize = value; }
        //}
        /// <summary>
        /// Default color
        /// </summary>
        public Brush DefaultColor
        {
            get { return Brushes.Red; }
        }
        /// <summary>
        /// Default color
        /// </summary>
        public Brush DefaultColorIntersections
        {
            get { return Brushes.Blue; }
        }
        /// <summary>
        /// Color of the point
        /// </summary>
        public Brush Color
        {
            get
            {
                if (m_color == null)
                    m_color = DefaultColor;
                return m_color;
            }
            set { m_color = value; }
        }



        #endregion


        /// <summary>
        /// Create a new instance of <see cref="TspCancasPointSetting"/> with default values
        /// </summary>
        public TspCancasPointSetting()
        {
            Diameter = DefaultDiameter;
            Color = DefaultColor;
        }



        public System.Windows.Shapes.Ellipse CreatePoint(Point p)
        {
            var e = new System.Windows.Shapes.Ellipse();
            e.Width = Diameter;
            e.Height = Diameter;
            e.StrokeThickness = 0;

            e.Fill = Color;
            return e;
        }
    }
}
