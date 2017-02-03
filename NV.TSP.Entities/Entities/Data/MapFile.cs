using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Exceptions;
using TSP.Entities.Properties;

namespace TSP.Entities.Data
{
    /// <summary>
    /// This class is used to convert the data to how it is stored and back to normal
    /// </summary>
    public class MapFile
    {
        public const char MapAgeSeperator = '?';
        public const char MapIndexSeperator = ',';
        public const char PointCoordinateSeperator = ',';


        private List<string> m_maps;
        private List<string> m_points;


        #region Properties


        /// <summary>
        /// Get the Maps strings
        /// </summary>
        public List<string> Maps { get { return m_maps; } }
        /// <summary>
        /// Get the Point strings
        /// </summary>
        public List<string> Points { get { return m_points; } }


        #endregion


        /// <summary>
        /// create a new empty <see cref="MapFile"/>
        /// </summary>
        public MapFile()
        {
            m_maps = new List<string>();
            m_points = new List<string>();
        }


        #region Public Methods


        /// <summary>
        /// Fill this object with information
        /// </summary>
        /// <param name="maps"></param>
        /// <param name="logs"></param>
        /// <param name="points"></param>
        /// <exception cref="TspDataException"></exception>
        public void Fill(List<Map> maps, List<Log> logs, List<Point> points)
        {
            var mapsOrderd = maps.OrderBy(x => x.Generation);
            var pointsOrderd = points.OrderBy(x => x.Index);

            foreach (var map in mapsOrderd)
            {
                var log = logs.Where(x => x.Generation == map.Generation).FirstOrDefault();
                m_maps.Add(getMapAndLogAsString(map, log));
            }
            foreach (var point in pointsOrderd)
            {
                m_points.Add(getPointAsString(point));
            }

        }
        /// <summary>
        /// Get the <see cref="Map"/> entites
        /// </summary>
        /// <returns></returns>
        /// <exception cref="TspDataException"></exception>
        public List<Map> GetMaps()
        {
            var maps = new List<Map>();
            if (Maps.Count != 0)
            {
                Map m = null;
                Log l = null;
                for (int i = 0; i < Maps.Count; i++)
                {
                    getMapOrLog(Maps[i], i, out m, out l);
                }
                maps.Add(m);
            }

            return maps;
        }
        /// <summary>
        /// Get the <see cref="Log"/> entities
        /// </summary>
        /// <returns></returns>
        /// <exception cref="TspDataException"></exception>
        public List<Log> GetLogs()
        {
            var logs = new List<Log>();
            if (Maps.Count != 0)
            {
                Map m = null;
                Log l = null;
                for (int i = 0; i < Maps.Count; i++)
                {
                    getMapOrLog(Maps[i], i, out m, out l);
                }
                logs.Add(l);
            }

            return logs;
        }
        /// <summary>
        /// Get the <see cref="Point"/> entities
        /// </summary>
        /// <returns></returns>
        /// <exception cref="TspDataException"></exception>
        public List<Point> GetPoints()
        {
            var points = new List<Point>();
            if (Points.Count != 0)
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    if (!String.IsNullOrEmpty(Points[i]))
                    {
                        var pointParts = Points[i].Split(PointCoordinateSeperator);
                        if (pointParts.Count() == 2)
                        {
                            int x = 0;
                            int y = 0;

                            if (!Int32.TryParse(pointParts[0], out x))
                                throw new TspDataException(DiagnosticEvents.ReadPointNoNumber, String.Format(Resources.ExReadPointNoNumber, "X", i, pointParts[0]));
                            if (!Int32.TryParse(pointParts[1], out y))
                                throw new TspDataException(DiagnosticEvents.ReadPointNoNumber, String.Format(Resources.ExReadPointNoNumber, "Y", i, pointParts[1]));

                            points.Add(new Point()
                            {
                                Index = i + 1,
                                X = x,
                                Y = y
                            });
                        }
                        else
                            throw new TspDataException(DiagnosticEvents.ReadPointStructureError, String.Format(Resources.ExReadPointStructureError, i, Points[i]));
                    }
                    else throw new TspDataException(DiagnosticEvents.ReadPointStringEmpty, String.Format(Resources.ExReadPointStringEmpty, i));
                }
            }

            return points;
        }


        #endregion

        #region Private Methods


        /// <summary>
        /// convert a map and its log to a string
        /// </summary>
        /// <param name="map"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        /// <exception cref="TspDataException"></exception>
        private string getMapAndLogAsString(Map map, Log log)
        {

            // Strucure of the result:
            // 
            // <age>?<index 1>,<index 2>,...<index x> 

            var storeMap = new StringBuilder();

            if (map.Lines.Count > 0)
            {
                var lineClone = new List<Line>(map.Lines);
                var curLine = lineClone.First();
                var curPoint = curLine.B;

                if (log == null)
                    storeMap.Append("0" + MapAgeSeperator);
                else
                    storeMap.Append(log.Age + MapAgeSeperator);
                storeMap.Append(curLine.A.Index + MapIndexSeperator + curLine.B.Index);
                lineClone.Remove(curLine);


                while (lineClone.Count != 0)
                {
                    storeMap.Append(MapIndexSeperator);
                    var nextlines = map.GetLineByPoint(curPoint);


                    if (nextlines.Count == 2)
                    {
                        if (nextlines[0] == curLine)
                            curLine = nextlines[1];
                        else
                            curLine = nextlines[0];


                        if (curLine.A == curPoint)
                            curPoint = curLine.B;
                        else
                            curPoint = curLine.A;

                        // remove the current point
                        lineClone.Remove(curLine);
                        storeMap.Append(curPoint.Index);


                    }
                    else
                        throw new TspDataException(DiagnosticEvents.MapLinesStructureError, String.Format(Resources.ExMapLinesStructureError, curPoint.Index, curPoint.X, curPoint.Y, map.Generation, nextlines.Count));
                }
            }
            else
                throw new TspDataException(DiagnosticEvents.ConvertMapToIndexList, Resources.ExConvertMapToIndexList);


            return storeMap.ToString();
        }
        /// <summary>
        /// convert a point to a string
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <exception cref="TspDataException"></exception>
        private string getPointAsString(Point point)
        {
            return point.X.ToString() + PointCoordinateSeperator + point.Y.ToString();
        }
        /// <summary>
        /// convert a read string to a map and a log.
        /// <para>This require points</para>
        /// </summary>
        /// <param name="mapLogLine"></param>
        /// <param name="index"></param>
        /// <param name="map"></param>
        /// <param name="log"></param>
        /// <exception cref="TspDataException"></exception>
        private void getMapOrLog(string mapLogLine, int index, out Map map, out Log log)
        {
            map = new Map();
            map.Generation = index + 1;
            log = new Log();

            if (!String.IsNullOrEmpty(mapLogLine))
            {
                var logMap = mapLogLine.Split(MapAgeSeperator).ToList();
                if (logMap.Count == 2)
                {
                    int age = 0;
                    if (!Int32.TryParse(logMap[0], out age))
                        throw new TspDataException(DiagnosticEvents.ReadMapAgeNoNumberError, String.Format(Resources.ExReadMapAgeNoNumberError, index, logMap[0]));

                    var pointIndices = logMap[1].Split(MapIndexSeperator);

                    if (pointIndices.Count() > 1)
                    {
                        var indices = new List<int>();
                        foreach (var i in pointIndices)
                        {
                            int number = 0;
                            if (!Int32.TryParse(i, out number))
                                throw new TspDataException(DiagnosticEvents.ReadMapIndiceNoNumberError, String.Format(Resources.ExReadMapIndiceNoNumberError, index, Array.IndexOf(pointIndices, i), i));
                            indices.Add(number);
                        }

                        var points = GetPoints();
                        int indexA = indices.First();
                        var pointA = points.Where(x => x.Index == indexA).FirstOrDefault();
                        for (int i = 1; i < indices.Count; i++)
                        {
                            int indexB = indices[i];
                            var pointB = points.Where(x => x.Index == indexB).FirstOrDefault();
                            map.Lines.Add(new Line(pointA, pointB));
                            indexA = indexB;
                            pointA = pointB;
                        }
                        var firstPoint = points.Where(x => x.Index == indices.First()).FirstOrDefault();
                        map.Lines.Add(new Line(pointA, firstPoint));

                        log.Distance = map.Distance;
                        log.Fitness = map.Fitness;
                        log.Generation = map.Generation;
                        log.Intersections = map.Intersection;
                    }
                    else
                        throw new TspDataException(DiagnosticEvents.ReadMapIndicesEmpty, String.Format(Resources.ExReadMapIndicesEmpty, index, logMap[1]));
                }
                else
                    throw new TspDataException(DiagnosticEvents.ReadMapAgeSturctureError, String.Format(Resources.ExReadMapAgeSturctureError, MapAgeSeperator, index, mapLogLine));
            }
            else
                throw new TspDataException(DiagnosticEvents.ReadMapStringEmpty, String.Format(Resources.ExReadMapStringEmpty, index));
        }


        #endregion

    }
}
