using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Entities
{
    /// <summary>
    /// Diese Klasse ist nur als container gedacht.
    /// </summary>
    public class Line : ICloneable
    {
        private Point m_p1;
        private Point m_p2;


        #region Property

        /// <summary>
        /// Der erste Punkt
        /// </summary>
        public Point A
        {
            get { return m_p2; }
            set { m_p2 = value; }
        }
        /// <summary>
        /// Der zweite Punkt
        /// </summary>
        public Point B
        {
            get { return m_p1; }
            set { m_p1 = value; }
        }
        /// <summary>
        /// Die Laenge der linie
        /// </summary>
        public double Distance
        {
            get { return MathHelper.GetDistance(this); }
        }


        #endregion


        /// <summary>
        /// Erzeuge eine neue linie
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public Line(Point a, Point b)
        {
            A = a;
            B = b;
        }

        public Line()
        {

        }

        public object Clone()
        {
            return new Line(A, B);
        }
    }
}
