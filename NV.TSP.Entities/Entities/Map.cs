using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Entities
{
    /// <summary>
    /// Diese Klasse ist die container Klasse für alle Punkte und Linien.
    /// Fuer jede Generation wir eine neue Karte erstellt.
    /// </summary>
    public class Map
    {
        private List<Line> m_lines;
        private List<Line> m_newlines;
        private List<Line> m_oldlines;
        private List<Line> m_curlines;
        private int m_generation;


        #region Properties
        

        /// <summary>
        /// Alle Linien aus dieser Karte
        /// </summary>
        public List<Line> Lines
        {
            get
            {
                if (m_lines == null)
                    m_lines = new List<Line>();
                return m_lines;
            }
            set { m_lines = value; }
        }
        /// <summary>
        /// Die momentane Generation der Karte
        /// </summary>
        public int Generation
        {
            get { return m_generation; }
            set { m_generation = value; }
        }
        /// <summary>
        /// Die fitness der Karte
        /// </summary>
        public int Fitness
        {
            get { return this.GetFitness(); }
        }
        /// <summary>
        /// Die anzahl an Schnitstellen
        /// </summary>
        public int IntersectionCount
        {
            get { return this.GetIntersectionAmount(); }
        }
        /// <summary>
        /// Get the interseption from this map
        /// </summary>
        public List<Point> Interseptions
        {
            get { return this.GetInterseptions(); }
        }
        /// <summary>
        /// Die gesamtstrecke
        /// </summary>
        public double Distance
        {
            get { return this.GetDistance(); }
        }



        /// <summary>
        /// All lines that are the same from the last version to this version
        /// </summary>
        public List<Line> CurLines
        {
            get
            {
                if (m_curlines == null)
                    m_curlines = new List<Line>();
                return m_curlines;
            }
            set { m_curlines = value; }
        }
        /// <summary>
        /// All lines that are new from the last version to this version
        /// </summary>
        public List<Line> NewLines
        {
            get
            {
                if (m_newlines == null)
                    m_newlines = new List<Line>();
                return m_newlines;
            }
            set { m_newlines = value; }
        }
        /// <summary>
        /// All lines that were exchanged from the last version to this version
        /// </summary>
        public List<Line> OldLines
        {
            get
            {
                if (m_oldlines == null)
                    m_oldlines = new List<Line>();
                return m_oldlines;
            }
            set { m_oldlines = value; }
        }




        #endregion


        /// <summary>
        /// Erzeuge eine neue Karte
        /// </summary>
        public Map()
        {
            Generation = 0;
        }


        #region Public Methods

        
        /// <summary>
        /// Gebe alle linien zurück die diesen punkt beinhalten
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<Line> GetLineByPoint(Point p)
        {
            return Lines.Where(x => x.B == p || x.A == p).ToList();
        }


        /// <summary>
        /// Clone this map
        /// </summary>
        /// <returns></returns>
        public Map Clone()
        {
            var m = new Map();
            cloneList(Lines, m.Lines);
            cloneList(NewLines, m.NewLines);
            cloneList(OldLines, m.OldLines);
            cloneList(CurLines, m.CurLines);
            m.Generation = Generation;
            return m;
        }


        /// <summary>
        /// Fill the properties <see cref="OldLines"/>, <see cref="CurLines"/> and <see cref="NewLines"/>.
        /// <para>
        /// <see cref="OldLines"/> -> get the removed lines. These are from the <paramref name="previousMap"/>
        /// </para>
        /// <see cref="CurLines"/> -> get all unchanged lines
        /// <para></para>
        /// <see cref="NewLines"/> -> get all new lines
        /// </summary>
        /// <param name="previousMap"></param>
        public void DefineVersionDifferences(Map previousMap)
        {
            NewLines.Clear();
            OldLines = new List<Line>(previousMap.Lines);
            CurLines.Clear();
            
            foreach (var line in Lines)
            {
                // search for a line that hast the same A index and B index
                var oldLine = OldLines.Where(x => x.A.Index == line.A.Index && x.B.Index == line.B.Index).FirstOrDefault();
                if (oldLine != null)
                {
                    // if one exist, then the line didn't change and so it can be deleted
                    OldLines.Remove(oldLine);
                    CurLines.Add(oldLine);      // add the unchanged lines to the current line
                }
                else
                    // else this is a line that was changed
                    NewLines.Add(line);
            }
        }


        #endregion


        private void cloneList(List<Line> from, List<Line> to)
        {
            foreach (var line in from)
            {
                to.Add(line.Clone());
            }
        }



    }
}
