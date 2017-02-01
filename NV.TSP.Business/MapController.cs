using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Entities;
using TSP.Entities.Interfaces.Presentation;
using TSP.DataAccess;

namespace TSP.Business
{
    public class MapController
    {
        private IWindowObserver m_winObs;
        private FileManager m_fileMgr;
        private Random m_rnd;


        #region Properties


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
        /// <summary>
        /// 
        /// </summary>
        public string EmptyLine
        {
            get
            {
                return "\r" + new string(' ', Console.BufferWidth - 1) + "\r";
            }
        }
        /// <summary>
        /// Public accessor
        /// </summary>
        public IWindowObserver WinObs
        {
            get { return m_winObs; }
            private set { m_winObs = value; }
        }

        #endregion


        public MapController(IWindowObserver winObs)
        {
            m_rnd = new Random();
            WinObs = winObs;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="etheration"></param>
        public void Process(int etheration = 1)
        {
            int count = 0;
            Map best = null;
            Map bestCopy = null;
            Map shortest = null;
            for (int i = 0; i < etheration; i++)
            {
                best = WinObs.BestMap;
                bestCopy = best.Clone();
                shortest = WinObs.ShortestMap;

                count++;
                int swapCount = 1;
                WinObs.CurrentAge++;
                Console.Write(EmptyLine + WinObs.CurrentLog.Text + "\t" + i + "/" + etheration);

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
                    singleSwap(bestCopy);


                if (bestCopy.Fitness < best.Fitness)
                {
                    WinObs.NewBest(bestCopy);
                    Console.WriteLine(EmptyLine + WinObs.CurrentLog.Text);
                    count = 0;
                }
                else
                {
                    WinObs.CurrentLog.Fitness = bestCopy.Fitness;
                    WinObs.CurrentLog.Distance = bestCopy.Distance;
                    WinObs.CurrentLog.Intersections = bestCopy.GetIntersectionAmount();
                }
                if (bestCopy.Distance < WinObs.ShortestMap.Distance)
                {
                    WinObs.NewShortest(bestCopy);
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


            var points = FileMgr.LoadPoints(path);
            var lines = firstConnection(points);

            WinObs.LoadPoints(points, lines);


            //CurrentMap.Generation = 1;
            //CurrentMap.Logs.Add(new Log(CurrentMap.Generation, 0, CurrentMap.Fitness, CurrentMap.Distance));
            //CurrentMap.Logs.First().Intersections = CurrentMap.GetIntersectionAmount();
            //Console.WriteLine(EmptyLine + CurrentMap.Logs.First().Text);
            //CurrentLog = new Log(CurrentMap.Generation + 1, 0, CurrentMap.Fitness, CurrentMap.Distance);
            //CurrentLog.Intersections = CurrentMap.GetIntersectionAmount();
            //BestMap = new Map(CurrentMap);

        }
        /// <summary>
        /// Speicher die momentan beste karte
        /// </summary>
        /// <param name="path"></param>
        public void SaveMap(string path)
        {
            FileMgr.SaveMap(WinObs.Values, path);
        }
        /// <summary>
        /// Lade eine Karte und ueberschreibe die momentanen Informationen
        /// </summary>
        /// <param name="path"></param>
        public void LoadMap(string path)
        {
            WinObs.Values = FileMgr.LoadMap(path);
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
