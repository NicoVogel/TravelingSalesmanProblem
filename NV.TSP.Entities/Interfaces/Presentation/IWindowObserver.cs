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
        SaveEntity Values
        {
            get;
            set;
        }

        Map ShortestMap { get; }
        Map BestMap { get; }

        int CurrentAge
        {
            get;
            set;
        }

        Log CurrentLog { get; }


        void NewBest(Map m);
        void NewShortest(Map m);
        void LoadPoints(List<Point> points, List<Line> lines);
    }
}
