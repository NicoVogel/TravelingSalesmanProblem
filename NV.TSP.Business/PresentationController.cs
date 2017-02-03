using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


using TSP.Entities;
using TSP.Entities.Data;
using TSP.Interfaces.Business;
using TSP.Interfaces.Presentation;
using TSP.Interfaces.Data;
using TSP.DataAccess;


namespace TSP.Business
{
    

    public class PresentationController : IValueShare, IPresentationController
    {
        private List<Map> m_maps;
        private Map m_best;
        private Map m_shortest;
        private List<Point> m_points;
        private List<Log> m_logs;
        private Log m_log;
        private int m_curAge;
        private IMapController m_mc;
        private IMainWindow m_main;
        private IFileManager m_fileMgr;
        private Thread m_thread;


        #region Properties


        /// <summary>
        /// Public accessor fuer den File Manager
        /// </summary>
        public IFileManager FileMgr
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
        /// <summary>
        /// public accessor
        /// </summary>
        public List<Map> Maps
        {
            get
            {
                if (m_maps == null)
                    m_maps = new List<Map>();
                return m_maps;
            }
            private set { m_maps = value; }
        }


        #region PC Interface Properties

        
        /// <summary>
        /// get the extesion for the map files
        /// </summary>
        public string MapExtension { get { return FileMgr.MapExtension; } }
        /// <summary>
        /// get the extsion for point files
        /// </summary>
        public string PointExtension { get { return FileMgr.PointExtension; } }


        #endregion
        
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
            get { return m_shortest; }
            private set { m_shortest = value; }
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
            Maps.Add(BestMap);

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
            Maps.Add(ShortestMap);

            // draw
            if (MainWind.ShortestIsSelected)
                drawLines();
        }


        #endregion



        #region PC Interface Methods


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void LoadFilePoints(string path)
        {
            Points = FileMgr.LoadPoints(path);
            BestMap = new Map().FirstConnection(Points);
            ShortestMap = BestMap.Clone();
            CurrentLog = new Log(BestMap.Generation + 1, 0, BestMap.Fitness, BestMap.Distance);
            Logs.Add(CurrentLog);
            CurrentAge = 0;
            Maps.Add(BestMap);
            
            Console.Clear();

            // draw
            drawPoints();
            drawLines();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void LoadFileMap(string path)
        {
            var read = FileMgr.LoadFile(path);
            Points = read.GetPoints();
            Maps = read.GetMaps();
            Logs = read.GetLogs();

            BestMap = Maps.OrderByDescending(x => x.Fitness).FirstOrDefault();
            ShortestMap = Maps.OrderByDescending(x => x.Distance).FirstOrDefault();

            int nextGen = Maps.Max(x => x.Generation);
            CurrentLog = new Log(nextGen, 0, BestMap.Fitness, BestMap.Distance);
            CurrentAge = 0;

            Console.Clear();

            // draw
            drawPoints();
            drawLines();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void SaveFile(string path)
        {
            var mf = new MapFile();
            mf.Fill(Maps, Logs, Points);
            FileMgr.SaveFile(path, mf);
        }
        /// <summary>
        /// 
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
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
