using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Entities;
using TSP.Entities.Interfaces.Business;

namespace TSP.Entities.Math
{
    /// <summary>
    /// Diese Klasse uebernimmt die rechnungen
    /// </summary>
    public class MathHelper : IMathHelper
    {
        #region Const


        /// <summary>
        /// Diese Zahl wird mit der Schnitstellen anzahl multipliziert um einen groesseren einfluss auf die Fitness zu haben.
        /// </summary>
        public double IntersectionMultiplicator
        {
            get
            {
                return 10;
            }
        }
        /// <summary>
        /// Diese Zahl wird mit der Gesamtstrecke multipliziert um den einfluss auf die Fitness zu veraendern
        /// </summary>
        public double DistanceMultiplicator
        {
            get
            {
                return 60;
            }
        }

        #endregion

        #region Distance


        /// <summary>
        /// Berechne die Stecke von einer Linie
        /// </summary>
        /// <param name="ab"></param>
        /// <returns></returns>
        public double GetDistance(Line ab)
        {
            return GetDistance(ab.A, ab.B);
        }
        /// <summary>
        /// Berechne die Stecke zwischen zwei punkten
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double GetDistance(Point a, Point b)
        {
            return GetDistance(a.X, a.Y, b.X, b.Y);
        }
        /// <summary>
        /// Berechne die Strecke zwischen Koordinaten
        /// <para></para>
        /// A(x1,y1);
        /// B(x2,y2)
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public double GetDistance(int x1, int y1, int x2, int y2)
        {
            // c = root(a² + b²)
            double a = System.Math.Pow(x1 - x2, 2);    // a²
            double b = System.Math.Pow(y1 - y2, 2);    // b²
            return System.Math.Sqrt(a + b);            // root (a²+b²)
        }


        #endregion

        #region Intersection


        /// <summary>
        /// Pruefe ob eine Schnitstelle zwischen den beiden Linien besteht
        /// </summary>
        /// <param name="ab"></param>
        /// <param name="cd"></param>
        /// <returns></returns>
        public bool HasIntersection(Line ab, Line cd)
        {
            return HasIntersection(ab.A, ab.B, cd.A, cd.B);
        }
        /// <summary>
        /// Pruefe ob eine Schnitstelle zwischen den beiden Punkten besteht. 
        /// <para></para>
        /// Linie 1 -> AB; 
        /// Linie 2 -> CD
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public bool HasIntersection(Point a, Point b, Point c, Point d)
        {
            return HasIntersection(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y);
        }
        /// <summary>
        /// Pruefe ob eine Schnitstelle zwischen den Koordinaten besteht.
        /// <para>A(x1,y1);
        /// B(x2,y2);
        /// C(x3,y3);
        /// D(x4,y4)
        /// </para>
        /// Linie 1 -> AB;
        /// Linie 2 -> CD
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="x4"></param>
        /// <param name="y4"></param>
        /// <returns></returns>
        public bool HasIntersection(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            // A = (y4 - y3)  
            // B = (x4 - x3)
            //
            // Ax1 = A * x1
            // By1 = B * y1
            // 
            //        Ax1 - Ax3 - By1 + By3
            //  t = -----------------------
            //      - Ax2 + Ax1 + By2 - By1 
            //
            // Koordinaten von dem schnittpunkt
            // x = x1 + t * (x2 - x1)
            // y = y1 + t * (y2 - y2)

            try
            {
                int A = y4 - y3;
                int B = x4 - x3;

                int Ax1 = A * x1;
                int By1 = B * y1;

                double nominator = Ax1 - (A * x3) - By1 + (B * y3);
                double denominator = -(A * x2) + Ax1 + (B * y2) - By1;

                if (denominator == 0)
                    return false;

                double t = nominator / denominator;
                double x = x1 + t * (x2 - x1);
                double y = y1 + t * (y2 - y1);

                int Ax = System.Math.Min(x1, x2);  // linie 1. der kleinere x wert
                int Bx = System.Math.Max(x1, x2);  // linie 1. der groessere x wert
                int Ay = System.Math.Min(y1, y2);  // linie 1. der kleinere y wert
                int By = System.Math.Max(y1, y2);  // linie 1. der groessere y wert
                    
                int Cx = System.Math.Min(x3, x4);  // linie 2. der kleinere x wert
                int Dx = System.Math.Max(x3, x4);  // linie 2. der groessere x wert
                int Cy = System.Math.Min(y3, y4);  // linie 2. der kleinere y wert
                int Dy = System.Math.Max(y3, y4);  // linie 2. der groessere y wert

                if (x >= Ax && x <= Bx &&   // zwischen Ax und Bx
                    x >= Cx && x <= Dx &&   // zwischen Cx und Dx
                    y >= Ay && y <= By &&   // zwischen Ay und By
                    y >= Cy && y <= Dy)     // zwischen Cy und Dy
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        #endregion


        /// <summary>
        /// Berechnet die Fitness anhand der uebergebenen informationen
        /// </summary>
        /// <param name="intersections"></param>
        /// <param name="distance"></param>
        /// <param name="pointCount"></param>
        /// <returns></returns>
        public int GetFitness(int intersections, double distance, int pointCount)
        {
            //// weniger ist besser und 0 ist fertig (nicht erreibar)
            //int fitness = 0;
            //// wird benoetigt damit die intersections in einer relation zur laenge stehen,
            //// sonst wuerden die schnittpunkte nicht einen aehnliche gewichtung bei verschiedenen punkten haben.
            //double avgDistance = distance / pointCount;

            //fitness = (int)(avgDistance * intersections * IntersectionMultiplicator);
            //fitness += (int)(distance * DistanceMultiplicator / pointCount);
            //return fitness;

            int fitness = 0;
            fitness = (int)(intersections * IntersectionMultiplicator);
            fitness += (int)(distance * DistanceMultiplicator / pointCount);
            return fitness;



        }

    }


}
