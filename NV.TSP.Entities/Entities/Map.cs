using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Entities
{
    /// <summary>
    /// Diese Klasse ist die container Klasse für alle Punkte und Linien.
    /// Fuer jede Generation wir eine neue Karte erstellt.
    /// </summary>
    public class Map
    {
        private List<Line> m_lines;
        private int m_generation;


        #region Properties
        
        /// <summary>
        /// Alle Linien aus dieser Karte
        /// </summary>
        public List<Line> Lines
        {
            get
            {
                if (m_lines == null)
                    m_lines = new List<Line>();
                return m_lines;
            }
            set { m_lines = value; }
        }
        /// <summary>
        /// Die momentane Generation der Karte
        /// </summary>
        public int Generation
        {
            get { return m_generation; }
            set { m_generation = value; }
        }
        /// <summary>
        /// Die fitness der Karte
        /// </summary>
        public int Fitness
        {
            get { return this.GetFitness(); }
        }
        /// <summary>
        /// Die anzahl an Schnitstellen
        /// </summary>
        public int Intersection
        {
            get { return this.GetIntersectionAmount(); }
        }
        /// <summary>
        /// Die gesamtstrecke
        /// </summary>
        public double Distance
        {
            get { return this.GetDistance(); }
        }

        #endregion


        /// <summary>
        /// Erzeuge eine neue Karte
        /// </summary>
        public Map()
        {
            Generation = 0;
        }


        #region Public Methods


        /// <summary>
        /// Gebe die linie zurueck die den Punkt als B Punkt hat
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<Line> GetLineByPointA(Point p)
        {
            return Lines.Where(x => x.A == p).ToList();
        }
        /// <summary>
        /// Gebe die linie zurueck die den Punkt als A Punkt hat
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<Line> GetLineByPointB(Point p)
        {
            return Lines.Where(x => x.B == p).ToList();
        }


        /// <summary>
        /// Klone diese karte
        /// </summary>
        /// <returns></returns>
        public Map Clone()
        {
            var m = new Map();
            foreach (var line in Lines)
            {
                m.Lines.Add(line.Clone());
            }
            m.Generation = Generation;
            return m;
        }

        #endregion


    }
}
