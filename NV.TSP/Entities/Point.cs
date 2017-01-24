using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Entities
{
    /// <summary>
    /// Diese Klasse ist ein Punkt auf einer Karte.
    /// </summary>
    public class Point
    {
        private int m_x;
        private int m_y;
        private int m_index;


        #region Properties

        /// <summary>
        /// Der index des Punkes auf der Karte.
        /// Momentan existiert kein nutzen.
        /// </summary>
        public int Index
        {
            get { return m_index; }
            set { m_index = value; }
        }
        /// <summary>
        /// Y Koordinate des Punktes
        /// </summary>
        public int Y
        {
            get { return m_y; }
            set { m_y = value; }
        }
        /// <summary>
        /// X Koordinate des Punktes
        /// </summary>
        public int X
        {
            get { return m_x; }
            set { m_x = value; }
        }


        #endregion


        /// <summary>
        /// Erzeuge einen neuen Punkt
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point()
        {

        }
    }
}
