using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Entities.Math;
using TSP.Interfaces.Business;

namespace TSP.Entities
{
    public static class MathExtensions
    {
        private static IMathHelper m_math = new MathHelper();

        #region Line


        /// <summary>
        /// Die Laenge der linie
        /// </summary>
        public static double GetDistance(this Line line)
        {
            return m_math.GetDistance(line);
        }
        /// <summary>
        /// Klone diese linie
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static Line Clone(this Line line)
        {
            return new Line(line.A, line.B);
        }


        #endregion

        #region Map



        /// <summary>
        /// Bekomme die Fitness von dieser Karte
        /// </summary>
        /// <returns></returns>
        public static int GetFitness(this Map m)
        {
            return m_math.GetFitness(m.GetIntersectionAmount(), m.GetDistance(), m.Lines.Count);
        }


        /// <summary>
        /// Bekomme die gesamtstecke von dierser Karte
        /// </summary>
        /// <returns></returns>
        public static double GetDistance(this Map m)
        {
            double distance = 0;
            foreach (var line in m.Lines)
            {
                distance += m_math.GetDistance(line);
            }
            return distance;
        }


        /// <summary>
        /// Bekomme die Anzahl der Schnitstellen von dieser Karte
        /// </summary>
        /// <returns></returns>
        public static int GetIntersectionAmount(this Map m)
        {
            int intersections = 0;
            for (int i = 0; i < m.Lines.Count; i++)
            {
                for (int k = i + 1; k < m.Lines.Count; k++)
                {
                    if (m_math.HasIntersection(m.Lines[i], m.Lines[k]))
                        intersections++;
                }
            }
            return intersections;
        }


        /// <summary>
        /// Erstellt die verbindung anhand von der entfernung der einzelnen punkten
        /// </summary>
        /// <param name="map"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Map FirstConnection(this Map map, List<Point> points)
        {
            var lines = map.Lines;
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
                    var d = m_math.GetDistance(current, p);
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
            return map;
        }


        #endregion

    }
}
