//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using TSP.Entities.Math;
//using TSP.Entities.Interfaces.Business;

//namespace TSP.Entities
//{
//    /// <summary>
//    /// Diese Klasse ist die container Klasse für alle Punkte und Linien.
//    /// Fuer jede Generation wir eine neue Karte erstellt.
//    /// </summary>
//    public class Map
//    {
//        private List<Point> m_points;
//        private List<Line> m_lines;
//        private List<Log> m_logs;
//        private int m_generation;
//        private IMathHelper m_math;


//        #region Properties


//        /// <summary>
//        /// Alle Punkte aus dieser Karte
//        /// </summary>
//        public List<Point> Points
//        {
//            get
//            {
//                if (m_points == null)
//                    m_points = new List<Point>();
//                return m_points;
//            }
//            set { m_points = value; }
//        }
//        /// <summary>
//        /// Alle Linien aus dieser Karte
//        /// </summary>
//        public List<Line> Lines
//        {
//            get
//            {
//                if (m_lines == null)
//                    m_lines = new List<Line>();
//                return m_lines;
//            }
//            set { m_lines = value; }
//        }
//        /// <summary>
//        /// Die momentane Generation der Karte
//        /// </summary>
//        public int Generation
//        {
//            get { return m_generation; }
//            set { m_generation = value; }
//        }
//        /// <summary>
//        /// Die fitness der Karte
//        /// </summary>
//        public int Fitness
//        {
//            get { return GetFitness(); }
//        }
//        /// <summary>
//        /// Die anzahl an Schnitstellen
//        /// </summary>
//        public int Intersection
//        {
//            get { return GetIntersectionAmount(); }
//        }
//        /// <summary>
//        /// Die gesamtstrecke
//        /// </summary>
//        public double Distance
//        {
//            get { return GetDistance(); }
//        }
//        /// <summary>
//        /// Hier sind alle logs referenziert
//        /// </summary>
//        public List<Log> Logs
//        {
//            get
//            {
//                if (m_logs == null)
//                    m_logs = new List<Log>();
//                return m_logs;
//            }
//            set { m_logs = value; }
//        }

//        #endregion


//        /// <summary>
//        /// Erzeuge eine neue Karte
//        /// </summary>
//        public Map()
//        {
//            Generation = 0;
//            m_math = new MathHelper();
//        }
//        /// <summary>
//        /// Klone die uebergebene Karte und erhoehe die generation um 1
//        /// </summary>
//        /// <param name="map"></param>
//        public Map(Map map)
//        {
//            Points = map.Points;
//            Logs = map.Logs;
//            Lines = new List<Line>();
//            foreach (var line in map.Lines)
//            {
//                Lines.Add((Line)line.Clone());
//            }
//            Generation = map.Generation;
//        }


//        #region Public Methods


//        /// <summary>
//        /// Bekomme die Fitness von dieser Karte
//        /// </summary>
//        /// <returns></returns>
//        public int GetFitness()
//        {
//            return m_math.GetFitness(GetIntersectionAmount(), GetDistance(), Points.Count);
//        }


//        /// <summary>
//        /// Bekomme die gesamtstecke von dierser Karte
//        /// </summary>
//        /// <returns></returns>
//        public double GetDistance()
//        {
//            double distance = 0;
//            foreach (var line in Lines)
//            {
//                distance += m_math.GetDistance(line);
//            }
//            return distance;
//        }


//        /// <summary>
//        /// Bekomme die Anzahl der Schnitstellen von dieser Karte
//        /// </summary>
//        /// <returns></returns>
//        public int GetIntersectionAmount()
//        {
//            int intersections = 0;
//            for (int i = 0; i < Lines.Count; i++)
//            {
//                for (int k = i + 1; k < Lines.Count; k++)
//                {
//                    if (m_math.HasIntersection(Lines[i], Lines[k]))
//                        intersections++;
//                }
//            }
//            return intersections;
//        }


//        /// <summary>
//        /// Gebe die linie zurueck die den Punkt als B Punkt hat
//        /// </summary>
//        /// <param name="p"></param>
//        /// <returns></returns>
//        public Line GetLineByPointA(Point p)
//        {
//            return Lines.Where(x => x.A == p).FirstOrDefault();
//        }
//        #endregion

//    }
//}
