using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Entities;

namespace TSP.Interfaces.Business
{
    public interface IMathHelper
    {


        #region Const


        /// <summary>
        /// Diese Zahl wird mit der Schnitstellen anzahl multipliziert um einen groesseren einfluss auf die Fitness zu haben.
        /// </summary>
        double IntersectionMultiplicator { get; }
        /// <summary>
        /// Diese Zahl wird mit der Gesamtstrecke multipliziert um den einfluss auf die Fitness zu veraendern
        /// </summary>
        double DistanceMultiplicator { get; }


        #endregion

        #region Distance


        /// <summary>
        /// Berechne die Stecke von einer Linie
        /// </summary>
        /// <param name="ab"></param>
        /// <returns></returns>
        double GetDistance(Line ab);
        /// <summary>
        /// Berechne die Stecke zwischen zwei punkten
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        double GetDistance(Point a, Point b);
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
        double GetDistance(int x1, int y1, int x2, int y2);

        #endregion

        #region Intersection


        /// <summary>
        /// Pruefe ob eine Schnitstelle zwischen den beiden Linien besteht
        /// </summary>
        /// <param name="ab"></param>
        /// <param name="cd"></param>
        /// <returns></returns>
        bool HasIntersection(Line ab, Line cd);
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
        bool HasIntersection(Point a, Point b, Point c, Point d);
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
        bool HasIntersection(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4);
        /// <summary>
        /// Get the intersection point. It is null if there is no intersection
        /// </summary>
        /// <param name="ab"></param>
        /// <param name="cd"></param>
        /// <returns></returns>
        Point GetIntersection(Line ab, Line cd);
        /// <summary>
        /// Get the intersection point. It is null if there is no intersection
        /// <para></para>
        /// Linie 1 -> AB; 
        /// Linie 2 -> CD
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        Point GetIntersection(Point a, Point b, Point c, Point d);
        /// <summary>
        /// Get the intersection point. It is null if there is no intersection
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
        Point GetIntersection(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4);

        #endregion


        /// <summary>
        /// Berechnet die Fitness anhand der uebergebenen informationen
        /// </summary>
        /// <param name="intersections"></param>
        /// <param name="distance"></param>
        /// <param name="pointCount"></param>
        /// <returns></returns>
        int GetFitness(int intersections, double distance, int pointCount);

    }
}
