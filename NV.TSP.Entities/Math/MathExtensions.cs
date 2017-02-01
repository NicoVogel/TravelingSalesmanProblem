using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Entities.Math;
using TSP.Entities.Interfaces.Business;

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

        #endregion

    }
}
