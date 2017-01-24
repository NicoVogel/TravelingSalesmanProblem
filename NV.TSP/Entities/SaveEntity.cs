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
        private List<Line> m_bestLine;
        private List<Line> m_shortLine;
        private List<Log> m_log;
        private int m_bestG;
        private int m_shortG;

        public List<Log> Log
        {
            get
            {
                if (m_log == null)
                    m_log = new List<Log>();
                return m_log;
            }
            set { m_log = value; }
        }

        public int BestG
        {
            get { return m_bestG; }
            set { m_bestG = value; }
        }

        public int ShortG
        {
            get { return m_shortG; }
            set { m_shortG = value; }
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

        public List<Line> ShortestLine
        {
            get
            {
                if (m_shortLine == null)
                    m_shortLine = new List<Line>();
                return m_shortLine;
            }
            set { m_shortLine = value; }
        }


        public List<Line> BestLine
        {
            get
            {
                if (m_bestLine == null)
                    m_bestLine = new List<Line>();
                return m_bestLine;
            }
            set { m_bestLine = value; }
        }

        public Map BestMap
        {
            get
            {
                var m = new Map();
                m.Points = Points;
                m.Lines = BestLine;
                m.Logs = Log;
                m.Generation = BestG;
                correctObjects(m.Points, m.Lines);
                return m;
            }
        }

        public Map ShortestMap
        {
            get
            {
                var m = new Map();
                m.Points = Points;
                m.Lines = ShortestLine;
                m.Logs = Log;
                m.Generation = ShortG;
                correctObjects(m.Points, m.Lines);
                return m;
            }
        }





        public SaveEntity(Map best, Map shortest)
        {
            Log = best.Logs;
            Points = best.Points;
            BestG = best.Generation;
            BestLine = best.Lines;
            ShortG = shortest.Generation;
            ShortestLine = shortest.Lines;
        }


        public SaveEntity()
        {

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
