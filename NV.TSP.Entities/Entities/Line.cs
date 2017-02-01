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
    public class Line
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


        #endregion


        /// <summary>
        /// Erzeuge eine neue linie
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public Line(Point a, Point b) : this()
        {
            A = a;
            B = b;
        }

        public Line() { }


    }
}
