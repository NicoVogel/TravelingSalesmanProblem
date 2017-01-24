using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Entities;

namespace TSP
{
    public class MapController
    {
        private Map m_bestMap;
        private Map m_shortMap;
        private Map m_curMap;
        private Log m_curlog;
        private FileManager m_fileMgr;
        private Random m_rnd;


        #region Properties


        /// <summary>
        /// 
        /// </summary>
        public Log CurrentLog
        {
            get
            {
                if (m_curlog == null)
                    m_curlog = new Log();
                return m_curlog;
            }
            set { m_curlog = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Map CurrentMap
        {
            get
            {
                if (m_curMap == null)
                    m_curMap = new Map(BestMap);
                return m_curMap;
            }
            set { m_curMap = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Map BestMap
        {
            get
            {
                if (m_bestMap == null)
                    m_bestMap = new Map();
                return m_bestMap;
            }
            set { m_bestMap = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Map ShortestMap
        {
            get
            {
                if (m_shortMap == null)
                    m_shortMap = new Map();
                return m_shortMap;
            }
            set { m_shortMap = value; }
        }
        /// <summary>
        /// Public accessor fuer den File Manager
        /// </summary>
        public FileManager FileMgr
        {
            get
            {
                if (m_fileMgr == null)
                    m_fileMgr = new FileManager();
                return m_fileMgr;
            }
            private set { m_fileMgr = value; }
        }
        public string EmptyLine
        {
            get
            {
                return "\r" + new string(' ', Console.BufferWidth-1) + "\r";
            }
        }

        #endregion


        public MapController()
        {
            m_rnd = new Random();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="etheration"></param>
        public void Process(int etheration = 1)
        {
            int count = 0;
            for (int i = 0; i < etheration; i++)
            {
                count++;
                int swapCount = 1;
                CurrentLog.Age++;
                Console.Write(EmptyLine + CurrentLog.Text + "\t" + i + "/" + etheration);

                switch (count)
                {
                    case 10:
                    case 20:
                    case 30:
                    case 40:
                    case 60:
                    case 70:
                    case 80:
                    case 90:
                        swapCount = 2;
                        break;
                    case 50:
                        swapCount = 4;
                        count = 0;
                        break;
                    default:
                        break;
                }
                for (int k = 0; k < swapCount; k++)
                    singleSwap(CurrentMap);


                if (CurrentMap.Fitness < BestMap.Fitness)
                {
                    newBest();
                    count = 0;
                }
                else
                {
                    CurrentLog.Fitness = CurrentMap.Fitness;
                    CurrentLog.Distance = CurrentMap.Distance;
                    CurrentLog.Intersections = CurrentMap.GetIntersectionAmount();
                    CurrentMap = new Map(BestMap);
                }
                if(CurrentMap.Distance < ShortestMap.Distance)
                {
                    ShortestMap = new Map(CurrentMap);
                }
            }
        }


        #region IO


        /// <summary>
        /// Liesst punklte aus einer Datei. Diese ueberschreiben die momentanen Informationen
        /// </summary>
        /// <param name="path"></param>
        public void ReadPoints(string path)
        {
            CurrentMap = new Map();

            CurrentMap.Points = FileMgr.LoadPoints(path);
            CurrentMap.Lines = firstConnection(CurrentMap.Points);
            CurrentMap.Generation = 1;
            CurrentMap.Logs.Add(new Log(CurrentMap.Generation, 0, CurrentMap.Fitness, CurrentMap.Distance));
            CurrentMap.Logs.First().Intersections = CurrentMap.GetIntersectionAmount();
            Console.WriteLine(EmptyLine + CurrentMap.Logs.First().Text);
            CurrentLog = new Log(CurrentMap.Generation + 1, 0, CurrentMap.Fitness, CurrentMap.Distance);
            CurrentLog.Intersections = CurrentMap.GetIntersectionAmount();
            BestMap = new Map(CurrentMap);
        }
        /// <summary>
        /// Speicher die momentan beste karte
        /// </summary>
        /// <param name="path"></param>
        public void SaveMap(string path)
        {
            var entity = new SaveEntity(BestMap, ShortestMap);
            FileMgr.SaveMap(entity, path);
        }
        /// <summary>
        /// Lade eine Karte und ueberschreibe die momentanen Informationen
        /// </summary>
        /// <param name="path"></param>
        public void LoadMap(string path)
        {
            var read = FileMgr.LoadMap(path);
            CurrentMap = read.BestMap;
            ShortestMap = read.ShortestMap;
            //foreach (var p in CurrentMap.Points)
            //{
            //    var line1 = CurrentMap.Lines.Where(x => x.A.Index == p.Index).FirstOrDefault();
            //    if (line1 != null)
            //        line1.A = p;
            //    var line2 = CurrentMap.Lines.Where(x => x.B.Index == p.Index).FirstOrDefault();
            //    if (line2 != null)
            //        line2.B = p;
            //}
            Console.WriteLine(EmptyLine + CurrentMap.Logs.Last().Text);
            BestMap = new Map(CurrentMap);
            CurrentLog = new Log(CurrentMap.Generation + 1, 0);
        }


        #endregion

        #region Mutation


        /// <summary>
        /// Erstellt die verbindung anhand von der entfernung der einzelnen punkten
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private List<Line> firstConnection(List<Point> points)
        {
            var lines = new List<Line>();
            // klone die liste damit man diese schrumpfen lassen kann
            var clonePoints = new List<Point>(points);
            // der erste muss festgehalten werden, damit man diesen als endpunkt nutzen kann
            var first = clonePoints.First();
            var current = first;
            clonePoints.Remove(first);
            do
            {
                Point next = null;
                double distance = 0;
                foreach (var p in clonePoints)
                {
                    // suche nach dem punkt der am naechsten zu "current" ist.
                    var d = MathHelper.GetDistance(current, p);
                    if (next == null || distance > d)
                    {
                        distance = d;
                        next = p;
                    }
                }
                if (next != null)
                {
                    // wenn man einen gefunden hat wird eine linie hinzugefuegt
                    // danach wir next der neue current und dieser punkt verschwindet aus der liste
                    lines.Add(new Line(current, next));
                    current = next;
                    clonePoints.Remove(next);
                }
                else
                {
                    // wenn man keinen mehr findet, dann muss nur noch der letzte mit dem ersten punkt verbunden werden
                    lines.Add(new Line(current, first));
                    break;
                }


            } while (clonePoints.Count >= 0);

            return lines;
        }

        /// <summary>
        /// Tausche zwei verbindungen
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private void singleSwap(Map map)
        {
            var line1 = map.Lines[m_rnd.Next(0, map.Lines.Count)];
            Line line2 = null;
            do
            {
                line2 = map.Lines[m_rnd.Next(0, map.Lines.Count)];
            } while (line1.A == line2.A ||
                        line1.A == line2.B ||
                        line1.B == line2.A ||
                        line1.B == line2.B);

            // helper
            var h = line1.B;

            line1.B = line2.B;
            line2.B = h;

            if (hasLoop(map))
            {
                line2.B = line1.B;
                line1.B = h;
                h = line1.A;
                line1.A = line2.A;
                line2.A = h;
            }
        }


        #endregion



        private void newBest()
        {
            CurrentLog.Distance = CurrentMap.Distance;
            CurrentLog.Fitness = CurrentMap.Fitness;
            CurrentLog.Intersections = CurrentMap.GetIntersectionAmount();
            CurrentMap.Logs.Add(CurrentLog);
            CurrentMap.Generation = CurrentLog.Generation;
            BestMap = new Map(CurrentMap);

            Console.WriteLine(EmptyLine + CurrentLog.Text);
            CurrentLog = new Log(CurrentMap.Generation + 1, 0, CurrentMap.Fitness, CurrentMap.Distance);
            CurrentLog.Intersections = CurrentMap.GetIntersectionAmount();
        }


        /// <summary>
        /// Pruefe ob ein rundlauf entstanden ist und dadurch nicht mehr alle punkte angesteuert werden
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private bool hasLoop(Map map)
        {
            var startPoint = map.Lines.First().A;
            int counter = 0;
            var pointB = map.Lines.First().B;
            do
            {
                counter++;
                pointB = map.GetLineByPointA(pointB).B;
                if (counter == map.Lines.Count + 10)
                    throw new Exception("Something is wrong");
            } while (pointB != startPoint);

            if (counter == map.Lines.Count)
                return true;
            else
                return false;
        }

    }
}
