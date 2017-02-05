using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

using TSP.Entities;
using TSP.Interfaces.Presentation;

namespace TSP.Controls
{

    public class TspCanvas : Canvas, ITspCanvas
    {
        private double m_defaultScale = 0.1;

        private Dictionary<TSP.Entities.Point, System.Windows.Shapes.Ellipse> m_points;
        private Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> m_lines;
        private Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> m_oldLines;
        private Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> m_newLines;
        private double m_scale;
        private bool m_showIntrsections;
        private TspCancasPointSetting m_normalPoint;
        private TspCancasPointSetting m_intersetcionPoint;
        private TspCanvasLineSetting m_normalLine;
        private TspCanvasLineSetting m_oldLine;
        private TspCanvasLineSetting m_newLine;
        private Dictionary<TSP.Entities.Point, System.Windows.Shapes.Ellipse> m_intersections;

        private Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> m_mapOld;
        private Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> m_mapCur;
        private Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> m_mapNew;
        private Map m_currentMap;




        #region Properties


        /// <summary>
        /// Const -> the default scale value
        /// </summary>
        public double DefaultScale { get { return m_defaultScale; } }
        /// <summary>
        /// Show the intersections
        /// </summary>
        public bool ShowIntersections
        {
            get { return m_showIntrsections; }
            set
            {
                if (value && ShowIntersections)
                {
                    // update the existing intersectins
                    removeIntersections();
                    drawIntersections();
                }
                else if (value)
                    drawIntersections();
                else
                    removeIntersections();
                m_showIntrsections = value;
            }
        }
        /// <summary>
        /// The settings for points
        /// </summary>
        public TspCancasPointSetting PointSettings
        {
            get
            {
                if (m_normalPoint == null)
                    m_normalPoint = new TspCancasPointSetting();
                return m_normalPoint;
            }
            set { m_normalPoint = value; }
        }
        /// <summary>
        /// The settings for intersections 
        /// </summary>
        public TspCancasPointSetting IntersetionSettings
        {
            get
            {
                if (m_intersetcionPoint == null)
                    m_intersetcionPoint = new TspCancasPointSetting();
                return m_intersetcionPoint;
            }
            set { m_intersetcionPoint = value; }
        }
        /// <summary>
        /// The settings for lines
        /// </summary>
        public TspCanvasLineSetting LineSettings
        {
            get
            {
                if (m_normalLine == null)
                    m_normalLine = new TspCanvasLineSetting();
                return m_normalLine;
            }
            set { m_normalLine = value; }
        }
        /// <summary>
        /// The settings for the lines that have been replaced
        /// </summary>
        public TspCanvasLineSetting OldLineSettings
        {
            get
            {
                if (m_oldLine == null)
                    m_oldLine = new TspCanvasLineSetting();
                return m_oldLine;
            }
            set { m_oldLine = value; }
        }
        /// <summary>
        /// The settings for the lines that are new
        /// </summary>
        public TspCanvasLineSetting NewLineSettings
        {
            get
            {
                if (m_newLine == null)
                    m_newLine = new TspCanvasLineSetting();
                return m_newLine;
            }
            set { m_newLine = value; }
        }



        /// <summary>
        /// Contains the relation between the entity points and the displayed points.
        /// <para>Should only be used to read and not to edit</para>
        /// </summary>
        public Dictionary<TSP.Entities.Point, System.Windows.Shapes.Ellipse> Points
        {
            get
            {
                if (m_points == null)
                    m_points = new Dictionary<TSP.Entities.Point, System.Windows.Shapes.Ellipse>();
                return m_points;
            }
            private set { m_points = value; }
        }
        /// <summary>
        /// Contains the relation between the entity line and the displayed line.
        /// <para>Should only be used to read and notn to edit</para>
        /// </summary>
        public Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> Lines
        {
            get
            {
                if (m_lines == null)
                    m_lines = new Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line>();
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
            set { m_scale = value; }
        }
        /// <summary>
        /// public accessor
        /// </summary>
        public Dictionary<TSP.Entities.Point, System.Windows.Shapes.Ellipse> Intersections
        {
            get
            {
                if (m_intersections == null)
                    m_intersections = new Dictionary<Point, System.Windows.Shapes.Ellipse>();
                return m_intersections;
            }
            private set { m_intersections = value; }
        }



        /// <summary>
        /// public accessor
        /// </summary>
        public Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> OldLines
        {
            get
            {
                if (m_oldLines == null)
                    m_oldLines = new Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line>();
                return m_oldLines;
            }
            private set { m_oldLines = value; }
        }
        /// <summary>
        /// public accessor
        /// </summary>
        public Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> NewLines
        {
            get
            {
                if (m_newLines == null)
                    m_newLines = new Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line>();
                return m_newLines;
            }
            private set { m_newLines = value; }
        }








        /// <summary>
        /// public accessor
        /// </summary>
        public Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> MapOldLines
        {
            get
            {
                if (m_mapOld == null)
                    m_mapOld = new Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line>();
                return m_mapOld;
            }
            private set { m_mapOld = value; }
        }
        /// <summary>
        /// public accessor
        /// </summary>
        public Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> MapCurLines
        {
            get
            {
                if (m_mapCur == null)
                    m_mapCur = new Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line>();
                return m_mapCur;
            }
            private set { m_mapCur = value; }
        }
        /// <summary>
        /// public accessor
        /// </summary>
        public Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> MapNewLines
        {
            get
            {
                if (m_mapNew == null)
                    m_mapNew = new Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line>();
                return m_mapNew;
            }
            private set { m_mapNew = value; }
        }
        /// <summary>
        /// public accessor
        /// </summary>
        public Map CurrentMap
        {
            get { return m_currentMap; }
            private set { m_currentMap = value; }
        }

        #endregion


        public TspCanvas() : base()
        {
            Scale = DefaultScale;
            IntersetionSettings.Color = IntersetionSettings.DefaultColorIntersections;
            LineSettings = LineSettings.DefaultLine;
            OldLineSettings = OldLineSettings.DefaultOldLine;
            NewLineSettings = NewLineSettings.DefaultNewLine;
        }


        #region Public Methods

        #region Point Methods


        /// <summary>
        /// Draw points and create the relations
        /// </summary>
        /// <param name="p"></param>
        public void DrawPoints(List<TSP.Entities.Point> points)
        {
            this.Dispatcher.Invoke(() =>
            {
                createPoints(points, PointSettings, Points);
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
                removeAllPoints(Points);
                removeAllPoints(Intersections);
            });
        }


        #endregion

        #region Line Methods


        /// <summary>
        /// Draw lines and create the relations
        /// </summary>
        /// <param name="lines"></param>
        public void DrawLines(List<TSP.Entities.Line> lines)
        {
            this.Dispatcher.Invoke(() =>
            {
                createLines(lines, LineSettings, Lines);
            });
        }
        /// <summary>
        /// Draw the new lines
        /// </summary>
        /// <param name="lines"></param>
        public void DrawNewLines(List<TSP.Entities.Line> lines)
        {
            this.Dispatcher.Invoke(() =>
            {
                // Clear the OldLines
                removeAllLines(OldLines);


                // Change the NewLines to normal lines
                var newLinesToNormal = new List<TSP.Entities.Line>(NewLines.Keys);
                removeAllLines(NewLines);
                createLines(newLinesToNormal, LineSettings, Lines);


                // Find the lines for the new and old list
                var newLines = new List<TSP.Entities.Line>();
                var oldLines = new List<TSP.Entities.Line>(Lines.Keys);
                filterLines(lines, oldLines, newLines);


                // Create the new OldLine and NewLine lines
                createLines(oldLines, OldLineSettings, OldLines);
                createLines(newLines, NewLineSettings, NewLines);

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
                else if (OldLines.Keys.Any(x => x == l))
                {
                    var line = OldLines[l];
                    this.Children.Remove(line);
                    OldLines.Remove(l);
                }
                else if (NewLines.Keys.Any(x => x == l))
                {
                    var line = NewLines[l];
                    this.Children.Remove(line);
                    NewLines.Remove(l);
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
                removeAllLines(Lines);
                removeAllLines(OldLines);
                removeAllLines(NewLines);
            });
        }


        #endregion

        #region Map Methods


        /// <summary>
        /// Draw a map
        /// </summary>
        public void DrawMap(Map m)
        {
            this.Dispatcher.Invoke(() =>
            {
                CurrentMap = m;
                createLines(m.OldLines, OldLineSettings, MapOldLines);
                createLines(m.CurLines, LineSettings, MapCurLines);
                createLines(m.NewLines, NewLineSettings, MapNewLines);
            });
        }
        /// <summary>
        /// Remove a map
        /// </summary>
        public void RemoveMap()
        {
            this.Dispatcher.Invoke(() =>
            {
                removeAllLines(MapOldLines);
                removeAllLines(MapCurLines);
                removeAllLines(MapNewLines);
                removeAllLines(Lines);
            });
        }


        #endregion

        #region Redraw Methods


        /// <summary>
        /// Redraw all points and lines
        /// </summary>
        public void RedrawAll()
        {
            RedrawPoints();
            RedrawLines();
        }
        /// <summary>
        /// Redraw all points
        /// </summary>
        public void RedrawPoints()
        {
            this.Dispatcher.Invoke(() =>
            {
                // Redraw points
                //foreach (var key in Points.Keys)
                //{
                //    var val = Points[key];
                //    this.Children.Remove(val);

                //    var e = PointSettings.CreatePoint(key);
                //    ellipsPosition(key, e, PointSettings.Diameter);
                //    this.Children.Add(e);
                //    Points[key] = e;
                //}
                createPoints(new List<Point>(Points.Keys), PointSettings, Points);

                // redraw intersection
                if (ShowIntersections)
                {
                    //foreach (var key in Intersections.Keys)
                    //{
                    //    var val = Intersections[key];
                    //    this.Children.Remove(val);

                    //    var e = IntersetionPointSettings.CreatePoint(key);
                    //    ellipsPosition(key, e, IntersetionPointSettings.Diameter);
                    //    this.Children.Add(e);
                    //    Intersections[key] = e;
                    //}
                    createPoints(new List<Point>(Intersections.Keys), IntersetionSettings, Intersections);
                }
            });
        }
        /// <summary>
        /// Redraw all lines
        /// </summary>
        public void RedrawLines()
        {
            this.Dispatcher.Invoke(() =>
            {
                createLines(new List<Line>(Lines.Keys), LineSettings, Lines);
                createLines(new List<Line>(OldLines.Keys), OldLineSettings, OldLines);
                createLines(new List<Line>(NewLines.Keys), NewLineSettings, NewLines);
            });
        }


        #endregion

        #endregion

        #region Private Methods



        private void ellipsPosition(TSP.Entities.Point p, System.Windows.Shapes.Ellipse e, double width)
        {
            double left = (p.X * Scale) - (width / 2);
            double top = (p.Y * Scale) - (width / 2);
            e.Margin = new System.Windows.Thickness(left, top, 0, 0);
        }




        private void drawIntersections()
        {
            this.Dispatcher.Invoke(() =>
            {
                var m = CurrentMap;
                if (m == null)
                    m = new Map() { Lines = new List<Line>(Lines.Keys) };
                var intersections = m.Interseptions;

                foreach (var inter in intersections)
                {
                    var e = IntersetionSettings.CreatePoint(inter);
                    ellipsPosition(inter, e, IntersetionSettings.Diameter);
                    this.Children.Add(e);

                    Intersections.Add(inter, e);
                }

            });
        }

        private void removeIntersections()
        {
            this.Dispatcher.Invoke(() =>
            {
                foreach (var e in Intersections.Values)
                {
                    this.Children.Remove(e);
                }
                Intersections.Clear();
            });
        }



        private void filterLines(List<TSP.Entities.Line> NewLines, List<TSP.Entities.Line> oldSet, List<TSP.Entities.Line> newSet)
        {
            foreach (var line in NewLines)
            {
                // search for a line that hast the same A index and B index
                var oldLine = oldSet.Where(x => x.A.Index == line.A.Index && x.B.Index == line.B.Index).FirstOrDefault();
                if (oldLine != null)
                    // if one exist, then the line didn't change and so it can be deleted
                    oldSet.Remove(oldLine);
                else
                    // else this is a line that was changed
                    newSet.Add(line);
            }
            foreach (var line in oldSet)
            {
                var key = Lines.Keys.Where(x => x.A.Index == line.A.Index && x.B.Index == line.B.Index).FirstOrDefault();
                if (key != null)
                    Lines.Remove(key);
            }
        }

        private void createLines(List<TSP.Entities.Line> lines, TspCanvasLineSetting setting, Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> destination)
        {
            foreach (var l in lines)
            {
                var line = setting.CreateLine(l);
                line.X1 = l.A.X * Scale;
                line.Y1 = l.A.Y * Scale;
                line.X2 = l.B.X * Scale;
                line.Y2 = l.B.Y * Scale;
                this.Children.Add(line);
                destination.Add(l, line);
            }
        }

        private void createPoints(List<TSP.Entities.Point> points, TspCancasPointSetting setting, Dictionary<TSP.Entities.Point, System.Windows.Shapes.Ellipse> destination)
        {
            foreach (var p in points)
            {
                var e = setting.CreatePoint(p);
                ellipsPosition(p, e, setting.Diameter);
                this.Children.Add(e);
                destination.Add(p, e);
            }
        }

        private void removeAllLines(Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> lines)
        {
            foreach (var l in lines.Values)
            {
                this.Children.Remove(l);
            }
            lines.Clear();
        }

        private void removeAllMapLines(Dictionary<TSP.Entities.Map, System.Windows.Shapes.Line> lines)
        {
            foreach (var l in lines.Values)
            {
                this.Children.Remove(l);
            }
            lines.Clear();
        }

        private void removeAllPoints(Dictionary<TSP.Entities.Point, System.Windows.Shapes.Ellipse> points)
        {
            foreach (var e in points.Values)
            {
                this.Children.Remove(e);
            }
            points.Clear();
        }


        #endregion

    }
}
