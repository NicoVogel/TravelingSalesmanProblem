using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Entities
{
    public class SaveEntity
    {
        private List<Point> m_points;
        private Map m_best;
        private Map m_shortest;
        private List<Log> m_logs;


        #region Properties


        public List<Log> Logs
        {
            get
            {
                if (m_logs == null)
                    m_logs = new List<Log>();
                return m_logs;
            }
            set { m_logs = value; }
        }
        
        public List<Point> Points
        {
            get
            {
                if (m_points == null)
                    m_points = new List<Point>();
                return m_points;
            }
            set { m_points = value; }
        }

        public Map ShortestMap
        {
            get
            {
                if (m_shortest == null)
                    m_shortest = new Map();
                return m_shortest;
            }
            set { m_shortest = value; }
        }
        
        public Map BestMap
        {
            get
            {
                if (m_best == null)
                    m_best = new Map();
                return m_best;
            }
            set { m_best = value; }
        }


        #endregion


        public SaveEntity(Map best, Map shortest, List<Log> logs, List<Point> points) : this()
        {
            Logs = logs;
            Points = points;
            BestMap = best;
            ShortestMap = shortest;
        }


        public SaveEntity() { }



        public void CorrectReferences()
        {
            correctObjects(Points, BestMap.Lines);
            correctObjects(Points, ShortestMap.Lines);
        }

        private void correctObjects(List<Point> points, List<Line> lines)
        {

            foreach (var p in points)
            {
                var line1 = lines.Where(x => x.A.Index == p.Index).FirstOrDefault();
                if (line1 != null)
                    line1.A = p;
                var line2 = lines.Where(x => x.B.Index == p.Index).FirstOrDefault();
                if (line2 != null)
                    line2.B = p;
            }
        }
    }
}
