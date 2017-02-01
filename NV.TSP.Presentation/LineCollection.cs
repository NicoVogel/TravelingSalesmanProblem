using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace TSP.Presentation
{
    /// <summary>
    /// There is no real need for this class excapt for readability.
    /// Because I also have a entity called "Line" it is easier to read with this class
    /// </summary>
    public class LineCollection
    {
        private List<System.Windows.Shapes.Line> m_lines;
        private Canvas m_can;


        #region Properties


        /// <summary>
        /// Get all Shape Lines
        /// </summary>
        public List<System.Windows.Shapes.Line> ShapeLines
        {
            get
            {
                if (m_lines == null)
                    m_lines = new List<System.Windows.Shapes.Line>();
                return m_lines;
            }
            private set { m_lines = value; }
        }
        /// <summary>
        /// create Lines entities out of the shape lines
        /// </summary>
        public List<TSP.Entities.Line> Lines
        {
            get
            {
                var lines = new List<TSP.Entities.Line>();
                foreach (var l in ShapeLines)
                {
                    var a = new TSP.Entities.Point((int)l.X1, (int)l.Y1);
                    var b = new TSP.Entities.Point((int)l.X2, (int)l.Y2);
                    lines.Add(new TSP.Entities.Line(a,b));
                }
                return lines;
            }
        }
        /// <summary>
        /// Public accessor for the related canvas
        /// </summary>
        public Canvas ObjCanvas
        {
            get { return m_can; }
            private set { m_can = value; }
        }


        #endregion


        /// <summary>
        /// create new <see cref="LineCollection"/>
        /// </summary>
        /// <param name="can"></param>
        public LineCollection(Canvas can)
        {
            ObjCanvas = can;
        }


        /// <summary>
        /// Add a line to this list
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public System.Windows.Shapes.Line AddLine(TSP.Entities.Line l)
        {
            var line = new System.Windows.Shapes.Line();

            line.Stroke = Brushes.Black;
            line.Fill = Brushes.Black;

            line.X1 = l.A.X;
            line.Y1 = l.A.Y;
            line.X2 = l.B.X;
            line.Y2 = l.B.Y;

            ObjCanvas.Children.Add(line);

            return line;
        }



        /// <summary>
        /// Update the canvas with the new lines
        /// </summary>
        /// <param name="updatedMap"></param>
        public void UpdateNewLines(TSP.Entities.Map updatedMap)
        {
            // contain the two old lines
            var currentLines = new List<System.Windows.Shapes.Line>(this.ShapeLines);

            // contain the two new lines
            var changedLines = new List<Entities.Line>();



            // find the changed lines
            foreach (var nLine in updatedMap.Lines)
            {
                System.Windows.Shapes.Line sameLine = null;
                for (int i = 0; i < currentLines.Count; i++)
                {
                    if (currentLines[i].X1 == nLine.A.X &&
                        currentLines[i].Y1 == nLine.A.Y &&
                        currentLines[i].X2 == nLine.B.X &&
                        currentLines[i].Y2 == nLine.B.Y)
                    {
                        sameLine = currentLines[i];
                        break;
                    }
                }
                if (sameLine == null)
                    changedLines.Add(nLine);
                else
                    currentLines.Remove(sameLine);
            }


            // remove old lines
            if (currentLines.Count > 0)
            {
                foreach (var line in currentLines)
                {
                    ObjCanvas.Children.Remove(line);
                }
            }

            // add new lines
            if(changedLines.Count > 0)
            {
                foreach (var line in changedLines)
                {
                    AddLine(line);
                }
            }

        }

    }
}
