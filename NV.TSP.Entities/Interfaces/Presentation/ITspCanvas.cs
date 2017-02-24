using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

using TSP.Entities;
using TSP.Controls;

namespace TSP.Interfaces.Presentation
{
    public interface ITspCanvas
    {

        #region Properties
        

        /// <summary>
        /// Const -> the default scale value
        /// </summary>
        double DefaultScale { get; }
        /// <summary>
        /// Show the intersections
        /// </summary>
        bool ShowIntersections { get; set; }
        /// <summary>
        /// The settings for points
        /// </summary>
        TspCancasPointSetting PointSettings { get; set; }
        /// <summary>
        /// The settings for intersections 
        /// </summary>
        TspCancasPointSetting IntersetionSettings { get; set; }
        /// <summary>
        /// The settings for lines
        /// </summary>
        TspCanvasLineSetting LineSettings { get; set; }
        /// <summary>
        /// The settings for the lines that have been replaced
        /// </summary>
        TspCanvasLineSetting OldLineSettings { get; set; }
        /// <summary>
        /// The settings for the lines that are new
        /// </summary>
        TspCanvasLineSetting NewLineSettings { get; set; }



        /// <summary>
        /// Contains the relation between the entity points and the displayed points.
        /// <para>Should only be used to read and not to edit</para>
        /// </summary>
        Dictionary<Point, System.Windows.Shapes.Ellipse> Points { get; }
        /// <summary>
        /// Contains the relation between the entity line and the displayed line.
        /// <para>Should only be used to read and notn to edit</para>
        /// </summary>
        Dictionary<TSP.Entities.Line, System.Windows.Shapes.Line> Lines { get; }
        /// <summary>
        /// the scale that is multiplied with the coordinates
        /// <para>by updating the scale all coordinates get updated as well</para>
        /// </summary>
        double Scale { get; set; }


        #endregion

        #region Point Methods
        

        /// <summary>
        /// Draw points and create the relations.
        /// </summary>
        /// <param name="points"></param>
        void DrawPoints(List<Point> points);
        /// <summary>
        /// Remove a point and the relation
        /// </summary>
        /// <param name="p"></param>
        void RemovePoint(Point p);
        /// <summary>
        /// Remove all points from the canvas
        /// </summary>
        void RemoveAllPoints();


        #endregion

        #region Line Methods


        /// <summary>
        /// Draw lines and create the relations
        /// </summary>
        /// <param name="lines"></param>
        void DrawLines(List<TSP.Entities.Line> lines);
        /// <summary>
        /// Draw the new lines
        /// </summary>
        /// <param name="lines"></param>
        void DrawNewLines(List<TSP.Entities.Line> lines);
        /// <summary>
        /// Remove a line and the relation
        /// </summary>
        /// <param name="l"></param>
        void RemoveLine(TSP.Entities.Line l);
        /// <summary>
        /// Remove all lines from the canvas
        /// </summary>
        void RemoveAllLines();


        #endregion

        #region Map Methods


        /// <summary>
        /// Draw a map
        /// </summary>
        void DrawMap(Map m);
        /// <summary>
        /// Remove a map
        /// </summary>
        void RemoveMap();


        #endregion

        #region Redraw Methods


        /// <summary>
        /// Redraw all points and lines
        /// </summary>
        void RedrawAll();
        /// <summary>
        /// Redraw all points
        /// </summary>
        void RedrawPoints();
        /// <summary>
        /// Redraw all lines
        /// </summary>
        void RedrawLines();


        #endregion

    }
}
