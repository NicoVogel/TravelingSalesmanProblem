using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Entities;
using TSP.Entities.Math;
using TSP.Interfaces.Business;
using TSP.Interfaces.Presentation;
using TSP.DataAccess;

namespace TSP.Business
{
    public class MapController : IMapController
    {
        private IValueShare m_winObs;
        private Random m_rnd;
        private bool m_stopProcess;


        #region Properties


        /// <summary>
        /// 
        /// </summary>
        public bool StopProcess
        {
            get { return m_stopProcess; }
            set { m_stopProcess = value; }
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
        public IValueShare WinObs
        {
            get { return m_winObs; }
            private set { m_winObs = value; }
        }


        #endregion


        public MapController(IValueShare winObs)
        {
            m_rnd = new Random();
            WinObs = winObs;
        }
        

        /// <summary>
        /// 
        /// </summary>
        public void Process()
        {
            StopProcess = false;
            int count = 0;
            Map best = null;
            Map bestCopy = null;
            Map shortest = null;
            while (StopProcess == false)
            {
                best = WinObs.BestMap;
                bestCopy = best.Clone();
                shortest = WinObs.ShortestMap;

                count++;
                int swapCount = 1;
                WinObs.CurrentAge++;
                Console.Write(EmptyLine + WinObs.CurrentLog.Text);

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
        

        #region Mutation


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
                h = line2.A;
                line2.A = line1.B;
                line1.B = h;
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
            var lastLine = map.Lines.First();
            var startPoint = map.Lines.First().A;
            int counter = 0;
            var pointB = map.Lines.First().B;
            do
            {
                counter++;
                //Line firstChoice = null;
                //Line secondChoice = null;
                //var next = map.GetLineByPointA(pointB);


                var next = map.GetLineByPoint(pointB);
                Line nextLine = null;


                if (next.Count == 2)
                {
                    if (next[0] == lastLine)
                        nextLine = next[1];
                    else if (next[1] == lastLine)
                        nextLine = next[0];
                    else
                        throw new Exception("hasloop: line connection error. -> 'firstChoice' and 'secondChoice' are not equal to 'lastLine'");
                }
                else
                {
                    throw new Exception("hasloop: line connection error. -> next.count = " + next.Count + " is not allowed to be grather than 2.");
                }

                //if (next.Count == 0)
                //{
                //    next = map.GetLineByPointB(pointB);
                //    if (next.Count == 2)
                //    {
                //        firstChoice = next[0];
                //        secondChoice = next[1];
                //    }
                //    else
                //        throw new Exception("hasloop: line connection error. -> ByPointA == 0 /// ByPointB != 2");
                //}
                //else if (next.Count == 1)
                //{
                //    firstChoice = next[0];
                //    var second = map.GetLineByPointB(pointB);
                //    if (second != null && second.Count == 1)
                //    {
                //        secondChoice = second[0];
                //    }
                //    else
                //        throw new Exception("hasloop: line connection error. -> firstChoice is set /// ByPointB == null or !=1");
                //}
                //else if(next.Count == 2)
                //{
                //    firstChoice = next[0];
                //    secondChoice = next[1];
                //}
                //else
                //{
                //    throw new Exception("hasloop: line connection error. -> next.count = " + next.Count + " is not allowed to be grather than 2.");
                //}


                //if (firstChoice == lastLine)
                //    nextLine = secondChoice;
                //else if (secondChoice == lastLine)
                //    nextLine = firstChoice;
                //else
                //    throw new Exception("hasloop: line connection error. -> 'firstChoice' and 'secondChoice' are not equal to 'lastLine'");

                lastLine = nextLine;

                if (nextLine.A == pointB)
                    pointB = nextLine.B;
                else if (nextLine.B == pointB)
                    pointB = nextLine.A;
                else
                    throw new Exception("hasloop: line connection error. -> the decided next line does not contain 'pointB'");

                
                if (counter == map.Lines.Count + 10)
                    throw new Exception("Something is wrong");
            } while (pointB != startPoint);

            if (counter == (map.Lines.Count - 1))
                return false;
            else
                return true;
        }

    }
}
