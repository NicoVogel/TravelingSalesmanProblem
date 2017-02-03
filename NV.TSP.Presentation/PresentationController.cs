using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


using TSP.Business;
using TSP.Entities;
using TSP.Entities.Interfaces.Business;
using TSP.Entities.Interfaces.Presentation;


namespace TSP.Presentation
{
    

    public class PresentationController : IWindowObserver, IPresentationController
    {
        private Map m_best;
        private Map m_shotest;
        private List<Point> m_points;
        private List<Log> m_logs;
        private Log m_log;
        private int m_curAge;
        private IMapController m_mc;
        private IMainWindow m_main;
        private Thread m_thread;

        
        #region Properties


        /// <summary>
        /// public accessor
        /// </summary>
        public IMapController MC
        {
            get
            {
                if (m_mc == null)
                    m_mc = new MapController(this);
                return m_mc;
            }
            private set { m_mc = value; }
        }
        /// <summary>
        /// public accessor
        /// </summary>
        public IMainWindow MainWind
        {
            get { return m_main; }
            private set { m_main = value; }
        }
        /// <summary>
        /// public accessor
        /// </summary>
        public List<Point> Points
        {
            get
            {
                if (m_points == null)
                    m_points = new List<Point>();
                return m_points;
            }
            private set { m_points = value; }
        }
        /// <summary>
        /// public accessor
        /// </summary>
        public List<Log> Logs
        {
            get
            {
                if (m_logs == null)
                    m_logs = new List<Log>();
                return m_logs;
            }
            private set { m_logs = value; }
        }


        #region Window Observer Interface Properties


        /// <summary>
        /// public accessor
        /// </summary>
        public Map BestMap
        {
            get { return m_best; }
            private set { m_best = value; }
        }
        /// <summary>
        /// public accessor
        /// </summary>
        public int CurrentAge
        {
            get { return m_curAge; }
            set { m_curAge = value; }
        }
        /// <summary>
        /// public accessor
        /// </summary>
        public Map ShortestMap
        {
            get { return m_shotest; }
            private set { m_shotest = value; }
        }
        /// <summary>
        /// Load or save the current state through a <see cref="SaveEntity"/>
        /// </summary>
        public SaveEntity Values
        {
            get
            {
                return new SaveEntity(BestMap, ShortestMap, Logs, Points);
            }
            set
            {
                value.CorrectReferences();
                BestMap = value.BestMap;
                ShortestMap = value.ShortestMap;
                Logs = value.Logs;
                Points = value.Points;

                // draw
                drawPoints();
                drawLines();
            }
        }
        /// <summary>
        /// public accessor
        /// </summary>
        public Log CurrentLog
        {
            get { return m_log; }
            private set { m_log = value; }
        }


        #endregion

        #endregion


        /// <summary>
        /// create new instance of <see cref="PresentationController"/>
        /// </summary>
        /// <param name="m"></param>
        public PresentationController(IMainWindow m)
        {
            MainWind = m;

        }


        #region IWindowObserver Interface Methods


        /// <summary>
        /// Set the new best map
        /// </summary>
        /// <param name="m"></param>
        public void NewBest(Map m)
        {
            //set the values for the next log
            CurrentLog.Distance = m.Distance;
            CurrentLog.Fitness = m.Fitness;
            CurrentLog.Intersections = m.GetIntersectionAmount();
            CurrentLog.Age = CurrentAge;
            Logs.Add(CurrentLog);

            // override the generation number and clone the new best map
            m.Generation = CurrentLog.Generation;
            BestMap = m.Clone();

            // create the new log
            CurrentLog = new Log(m.Generation + 1, 0, m.Fitness, m.Distance);
            CurrentLog.Intersections = m.GetIntersectionAmount();

            // set the age to 0
            CurrentAge = CurrentLog.Age;


            // draw
            if (MainWind.BestIsSelected)
                drawLines();
        }
        /// <summary>
        /// Set the new shortest map
        /// </summary>
        /// <param name="m"></param>
        public void NewShortest(Map m)
        {
            ShortestMap = m.Clone();

            // draw
            if (MainWind.ShortestIsSelected)
                drawLines();
        }
        /// <summary>
        /// load the points from a file and create the start entities
        /// </summary>
        /// <param name="points"></param>
        /// <param name="lines"></param>
        public void LoadPoints(List<Point> points, List<Line> lines)
        {
            Points = points;
            BestMap = new Map() { Lines = lines };
            ShortestMap = BestMap.Clone();
            CurrentLog = new Log(BestMap.Generation + 1, 0, BestMap.Fitness, BestMap.Distance);
            Logs.Add(CurrentLog);
            CurrentAge = 0;

            // draw
            drawPoints();
            drawLines();
        }


        #endregion



        #region PC Interface Methods


        public void LoadFilePoints(string path)
        {
            MC.ReadPoints(path);
        }
        public void LoadFileMap(string path)
        {
            MC.LoadMap(path);
        }

        public void SaveFile(string path)
        {
            MC.SaveMap(path);
        }

        public void Run()
        {
            if (m_thread != null && m_thread.IsAlive)
                throw new Exception("Der Prozess läuft schon");
            else
            {
                m_thread = new Thread(MC.Process);
                m_thread.Start();
            }
        }

        public void Stop()
        {
            if (m_thread != null && m_thread.IsAlive)
                MC.StopProcess = true;
            else
                throw new Exception("Der Prozess läuft gerade nicht");
        }


        #endregion

        private void drawPoints()
        {
            if (Points.Count > 0)
            {
                // clear the old points
                MainWind.TspCan.RemoveAllPoints();

                // set the new points
                foreach (var p in Points)
                {
                    MainWind.TspCan.DrawPoint(p);
                }
            }
        }

        private void drawLines()
        {
            List<Line> lines = null;
            if (MainWind.BestIsSelected)
                lines = BestMap.Lines;
            else if (MainWind.ShortestIsSelected)
                lines = ShortestMap.Lines;

            if (lines != null && lines.Count > 0)
            {
                // clear the old lines
                MainWind.TspCan.RemoveAllLines();

                // set the new line
                foreach (var l in lines)
                {
                    MainWind.TspCan.DrawLine(l);
                }
            }
        }
    }
}
