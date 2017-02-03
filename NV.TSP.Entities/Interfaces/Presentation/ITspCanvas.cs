using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

using TSP.Entities;

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
        /// Const -> the default point size
        /// </summary>
        int DefaultPointSize { get; }


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
        /// <summary>
        /// the diametar of a point
        /// <para>by updating the size all points get updated as well</para>
        /// </summary>
        int PointSize { get; set; }


        #endregion



        /// <summary>
        /// Draw a point and create a relation
        /// </summary>
        /// <param name="p"></param>
        void DrawPoint(Point p);
        /// <summary>
        /// Remove a point and the relation
        /// </summary>
        /// <param name="p"></param>
        void RemovePoint(Point p);
        /// <summary>
        /// Remove all points from the canvas
        /// </summary>
        void RemoveAllPoints();
        /// <summary>
        /// Draw a line and create a relation
        /// </summary>
        /// <param name="l"></param>
        void DrawLine(TSP.Entities.Line l);
        /// <summary>
        /// Remove a line and the relation
        /// </summary>
        /// <param name="l"></param>
        void RemoveLine(TSP.Entities.Line l);
        /// <summary>
        /// Remove all lines from the canvas
        /// </summary>
        void RemoveAllLines();


    }
}
