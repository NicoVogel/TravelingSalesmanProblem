using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace TSP.Presentation
{
    public class TspCanvas : Canvas, TSP.Entities.Interfaces.Presentation.ITspCanvas
    {
        private double m_defaultScale = 0.1;
        public const int m_defaultPointSize = 5;

        private Dictionary<TSP.Entities.Point, Ellipse> m_points;
        private Dictionary<TSP.Entities.Line, Line> m_lines;
        private double m_scale;
        private int m_pointSize;


        #region Properties


        public double DefaultScale { get { return m_defaultScale; } }
        public int DefaultPointSize { get { return m_defaultPointSize; } }


        /// <summary>
        /// Contains the relation between the entity points and the displayed points.
        /// <para>Should only be used to read and not to edit</para>
        /// </summary>
        public Dictionary<TSP.Entities.Point, Ellipse> Points
        {
            get
            {
                if (m_points == null)
                    m_points = new Dictionary<TSP.Entities.Point, Ellipse>();
                return m_points;
            }
            private set { m_points = value; }
        }
        /// <summary>
        /// Contains the relation between the entity line and the displayed line.
        /// <para>Should only be used to read and notn to edit</para>
        /// </summary>
        public Dictionary<TSP.Entities.Line, Line> Lines
        {
            get
            {
                if (m_lines == null)
                    m_lines = new Dictionary<TSP.Entities.Line, Line>();
                return m_lines;
            }
            private set { m_lines = value; }
        }
        /// <summary>
        /// the scale that is multiplied with the coordinates
        /// <para>by updating the scale all coordinates get updated as well</para>
        /// </summary>
        public double Scale
        {
            get { return m_scale; }
            set
            {
                foreach (var line in Lines)
                {
                    line.Value.X1 = (line.Key.A.X / Scale) * value;
                    line.Value.X2 = (line.Key.B.X / Scale) * value;
                    line.Value.Y1 = (line.Key.A.Y / Scale) * value;
                    line.Value.Y2 = (line.Key.B.Y / Scale) * value;
                }
                foreach (var point in Points)
                {
                    ellipsPosition(point.Key, point.Value);
                }
                m_scale = value;
            }
        }
        /// <summary>
        /// the diametar of a point
        /// <para>by updating the size all points get updated as well</para>
        /// </summary>
        public int PointSize
        {
            get { return m_pointSize; }
            set
            {
                foreach (var point in Points)
                {
                    ellipsPosition(point.Key, point.Value);
                }
                m_pointSize = value;
            }
        }


        #endregion


        public TspCanvas() : base()
        {
            Scale = DefaultScale;
            PointSize = DefaultPointSize;
        }


        #region Public Methods


        /// <summary>
        /// Draw a point and create a relation
        /// </summary>
        /// <param name="p"></param>
        public void DrawPoint(TSP.Entities.Point p)
        {
            this.Dispatcher.Invoke(() =>
            {
                var e = new Ellipse();
                e.Width = PointSize;
                e.Height = PointSize;
                e.StrokeThickness = 0;
                e.Fill = Brushes.Red;
                ellipsPosition(p, e);

                this.Children.Add(e);
                Points.Add(p, e);
            });
        }
        /// <summary>
        /// Remove a point and the relation
        /// </summary>
        /// <param name="p"></param>
        public void RemovePoint(TSP.Entities.Point p)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (Points.Keys.Any(x => x == p))
                {
                    var e = Points[p];
                    this.Children.Remove(e);
                    Points.Remove(p);
                }
            });
        }
        /// <summary>
        /// Remove all points from the canvas
        /// </summary>
        public void RemoveAllPoints()
        {
            this.Dispatcher.Invoke(() =>
            {
                foreach (var e in Points.Values)
                {
                    this.Children.Remove(e);
                }
                Points.Clear();
            });
        }
        /// <summary>
        /// Draw a line and create a relation
        /// </summary>
        /// <param name="l"></param>
        public void DrawLine(TSP.Entities.Line l)
        {
            this.Dispatcher.Invoke(() =>
            {
                var line = new System.Windows.Shapes.Line();

                line.Stroke = Brushes.Black;
                line.Fill = Brushes.Black;

                line.X1 = l.A.X * Scale;
                line.Y1 = l.A.Y * Scale;
                line.X2 = l.B.X * Scale;
                line.Y2 = l.B.Y * Scale;

                this.Children.Add(line);

                Lines.Add(l, line);
            });
        }
        /// <summary>
        /// Remove a line and the relation
        /// </summary>
        /// <param name="l"></param>
        public void RemoveLine(TSP.Entities.Line l)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (Lines.Keys.Any(x => x == l))
                {
                    var line = Lines[l];
                    this.Children.Remove(line);
                    Lines.Remove(l);
                }
            });
        }
        /// <summary>
        /// Remove all lines from the canvas
        /// </summary>
        public void RemoveAllLines()
        {
            this.Dispatcher.Invoke(() =>
            {
                foreach (var l in Lines.Values)
                {
                    this.Children.Remove(l);
                }
                Lines.Clear();
            });
        }

        #endregion

        #region Private Methods


        private void ellipsPosition(TSP.Entities.Point p, Ellipse e)
        {
            double left = (p.X * Scale) - (PointSize / 2);
            double top = (p.Y * Scale) - (PointSize / 2);
            e.Margin = new System.Windows.Thickness(left, top, 0, 0);
        }

        
        #endregion

    }
}
