using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP.Entities;
using TSP.Entities.Interfaces.Presentation;

namespace TSP.Presentation
{
    public class PresentationController : IWindowObserver
    {
        private Map m_best;
        private Map m_shotest;
        private List<Point> m_points;
        private List<Log> m_logs;
        private Log m_log;
        private int m_curAge;

        private MainWindow m_main;


        /// <summary>
        /// 
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
        /// 
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


        public Map BestMap
        {
            get { return m_best; }
            private set { m_best = value; }
        }

        public int CurrentAge
        {
            get { return m_curAge; }
            set { m_curAge = value; }
        }

        public Map ShortestMap
        {
            get { return m_shotest; }
            private set { m_shotest = value; }
        }

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
            }
        }

        public Log CurrentLog
        {
            get { return m_log; }
            private set { m_log = value; }
        }

        #endregion


        public PresentationController()
        {
            m_main.Show();
        }

        public void NewBest(Map m)
        {
            CurrentLog.Distance = m.Distance;
            CurrentLog.Fitness = m.Fitness;
            CurrentLog.Intersections = m.GetIntersectionAmount();
            Logs.Add(CurrentLog);
            m.Generation = CurrentLog.Generation;
            BestMap = m.Clone();
            
            CurrentLog = new Log(m.Generation + 1, 0, m.Fitness, m.Distance);
            CurrentLog.Intersections = m.GetIntersectionAmount();
        }
    }
}
