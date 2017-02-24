using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;


using TSP.Entities;
using TSP.Entities.Data;
using TSP.Interfaces.Business;
using TSP.Interfaces.Presentation;
using TSP.Interfaces.Data;
using TSP.DataAccess;
using System.Collections.ObjectModel;

namespace TSP.Business
{


    public class PresentationController : IValueShare, IPresentationController
    {
        private List<Map> m_maps;
        private Map m_best;
        private Map m_shortest;
        private List<Point> m_points;
        private ObservableCollection<Log> m_logs;
        private Log m_log;
        private IMapController m_mc;
        private IMainWindow m_main;
        private IFileManager m_fileMgr;
        private ITspTreeView m_treeView;
        private Thread m_thread;
        private bool m_unsavedMaps;


        #region Properties


        public ITspTreeView TreeView
        {
            get { return m_treeView; }
            set { m_treeView = value; }
        }



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
        public ObservableCollection<Log> Logs
        {
            get
            {
                if (m_logs == null)
                {
                    m_logs = new ObservableCollection<Log>();
                    m_logs.CollectionChanged += Logs_CollectionChanged;
                }
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
        /// <summary>
        /// if threads are running it returns true
        /// </summary>
        public bool ThreadsAreRunning { get { return m_thread != null && m_thread.IsAlive; } }
        /// <summary>
        /// This indidates if there are unsaved maps
        /// </summary>
        public bool HasUnsavedInformation
        {
            get { return m_unsavedMaps; }
            private set { m_unsavedMaps = value; }
        }


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
            get { return CurrentLog.Age; }
            set { CurrentLog.Age = value; }
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

            // override the generation number and clone the new best map
            m.Generation = CurrentLog.Generation;
            m.DefineVersionDifferences(BestMap);
            BestMap = m.Clone();
            Maps.Add(BestMap);

            this.TreeView.AddGeneration(CurrentLog);

            // reset the values for the next log
            Console.WriteLine(CurrentLog.Text);
            CurrentLog = CurrentLog.Clone();
            CurrentLog.Generation = m.Generation + 1;
            CurrentLog.Age = 0;
            Logs.Add(CurrentLog);

            // set the age to 0
            CurrentAge = CurrentLog.Age;


            // draw
            if (MainWind.BestIsSelected)
                drawLines();

            HasUnsavedInformation = true;
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

            HasUnsavedInformation = true;
        }


        #endregion

        #region PC Interface Methods


        /// <summary>
        /// Load a file which conatins points
        /// </summary>
        /// <param name="path"></param>
        public void LoadFilePoints(string path)
        {
            ClearCurrentState();

            Points = FileMgr.LoadPoints(path);
            BestMap = new Map().FirstConnection(Points);
            ShortestMap = BestMap.Clone();

            CurrentLog = new Log(BestMap.Generation + 1, 0, BestMap.Fitness, BestMap.Distance);
            Logs.Add(new Log(BestMap.Generation, 0, BestMap.Fitness, BestMap.Distance));
            Logs.Add(CurrentLog);
            CurrentAge = 0;
            Maps.Add(BestMap);

            Console.Clear();
            Console.WriteLine(Logs.First().Text);
            loadCalls();
        }
        /// <summary>
        /// Load a file which contains a map
        /// </summary>
        /// <param name="path"></param>
        public void LoadFileMap(string path)
        {
            ClearCurrentState();

            var read = FileMgr.LoadFile(path);
            Points = read.GetPoints();
            Maps = read.GetMaps();
            foreach (var log in read.GetLogs())
            {
                Logs.Add(log);
            }
            

            BestMap = Maps.OrderByDescending(x => x.Fitness).FirstOrDefault();
            ShortestMap = Maps.OrderByDescending(x => x.Distance).FirstOrDefault();

            int nextGen = Maps.Max(x => x.Generation);
            CurrentLog = new Log(nextGen, 0, BestMap.Fitness, BestMap.Distance);
            CurrentAge = 0;

            Console.Clear();
            Console.WriteLine(Logs.Last().Text);
            loadCalls();
        }
        /// <summary>
        /// Save a file at the given path
        /// </summary>
        /// <param name="path"></param>
        public void SaveFile(string path)
        {
            var mf = new MapFile();
            mf.Fill(Maps, Logs.ToList(), Points);
            FileMgr.SaveFile(path, mf);

            HasUnsavedInformation = false;
        }
        /// <summary>
        /// start the process to solve the tsp problem
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
        /// stop the process to solve the tsp problem
        /// </summary>
        public void Stop()
        {
            if (m_thread != null && m_thread.IsAlive)
                MC.StopProcess = true;
            else
                throw new Exception("Der Prozess läuft gerade nicht");
        }
        /// <summary>
        /// clear the current state. Maps, Logs, Point will be cleared
        /// </summary>
        public void ClearCurrentState()
        {
            Maps.Clear();
            Logs.Clear();
            Points.Clear();
            HasUnsavedInformation = false;
        }
        /// <summary>
        /// Redraw the current map
        /// </summary>
        public void Redraw()
        {
            drawLines();
        }

        #endregion

        private void drawPoints()
        {
            if (Points.Count > 0)
            {
                // clear the old points
                MainWind.TspCan.RemoveAllPoints();
                MainWind.TspCan.DrawPoints(Points);
            }
        }

        private void drawLines()
        {
            Map m = null;
            if (MainWind.BestIsSelected)
                m = BestMap;
            else if (MainWind.ShortestIsSelected)
                m = ShortestMap;

            if (m != null)
            {
                if (m.CurLines.Count == 0)
                    m.CurLines = m.Lines;

                MainWind.TspCan.RemoveMap();
                MainWind.TspCan.DrawMap(m);
            }
        }

        private void loadCalls()
        {
            MainWind.ActivateActionsWithLoadedMap = true;
            // draw
            drawPoints();
            drawLines();

            HasUnsavedInformation = false;
        }




        /// <summary>
        /// updates the tree view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // ignore if the treeview is not set
            if (TreeView == null)
                return;

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // add the added item to the treeview
                var logObject = e.NewItems == null ? null : e.NewItems.Count == 0 ? null : e.NewItems[0];
                if (logObject is Log)
                    TreeView.AddGeneration(logObject as Log);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                // remove the removed item to the treeview
                var logObject = e.OldItems == null ? null : e.OldItems.Count == 0 ? null : e.OldItems[0];
                if (logObject is Log)
                    TreeView.RemoveGeneration((logObject as Log).Generation);
            }
            else if (e.Action == NotifyCollectionChangedAction.Move ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                // rebuild the tree view
                TreeView.RemoveAllGenerations();
                TreeView.AddGenerations(Logs.ToList());
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
                // clear tree view
                TreeView.RemoveAllGenerations();
        }

    }
}
