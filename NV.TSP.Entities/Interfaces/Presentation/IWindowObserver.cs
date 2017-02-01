using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Entities;

namespace TSP.Entities.Interfaces.Presentation
{
    public interface IWindowObserver
    {
        /// <summary>
        /// Load or save the current state through a <see cref="SaveEntity"/>
        /// </summary>
        SaveEntity Values
        {
            get;
            set;
        }
        /// <summary>
        /// public accessor
        /// </summary>
        Map ShortestMap { get; }
        /// <summary>
        /// public accessor
        /// </summary>
        Map BestMap { get; }
        /// <summary>
        /// public accessor
        /// </summary>
        int CurrentAge
        {
            get;
            set;
        }
        /// <summary>
        /// public accessor
        /// </summary>
        Log CurrentLog { get; }



        /// <summary>
        /// Set the new best map
        /// </summary>
        /// <param name="m"></param>
        void NewBest(Map m);
        /// <summary>
        /// Set the new shortest map
        /// </summary>
        /// <param name="m"></param>
        void NewShortest(Map m);
        /// <summary>
        /// load the points from a file and create the start entities
        /// </summary>
        /// <param name="points"></param>
        /// <param name="lines"></param>
        void LoadPoints(List<Point> points, List<Line> lines);
    }
}
