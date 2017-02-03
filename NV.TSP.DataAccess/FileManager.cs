using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSP.Entities;
using TSP.Entities.Data;
using TSP.Interfaces.Data;
using TSP.DataAccess.Properties;
using TSP.Exceptions;

using FileParser;
using FileParser.Parser;

namespace TSP.DataAccess
{
    /// <summary>
    /// Ist zustaendig eine Datei zu laden oder zu speichern
    /// </summary>
    public class FileManager : IFileManager
    {
        public const string cMapExtension = "map";
        public const string cPointsExtension = "txt";
        public const char PointCoordinateSeperator = ' ';

        private FPDataManager m_fileMgr;


        #region Properties


        /// <summary>
        /// public accessor
        /// </summary>
        public FPDataManager FileMgr
        {
            get
            {
                if (m_fileMgr == null)
                {
                    FileMgr = new FPDataManager();
                    var txt = new FPTextSaveLoad();
                    var xml = new FPXmlSaveLoad();
                    xml.SetExtention(MapExtension);
                    txt.SetExtention(PointExtension);

                    FileMgr.SaveLoad = new Dictionary<string, ISaveLoad>();
                    FileMgr.SaveLoad.Add(xml.Extension, xml);
                    FileMgr.SaveLoad.Add(txt.Extension, txt);
                }
                return m_fileMgr;
            }
            private set { m_fileMgr = value; }
        }
        /// <summary>
        /// get the extesion for the map files
        /// </summary>
        public string MapExtension { get { return cMapExtension; } }
        /// <summary>
        /// get the extsion for point files
        /// </summary>
        public string PointExtension { get { return cPointsExtension; } }


        #endregion


        /// <summary>
        /// Crate a new instance of <see cref="FileManager"/>
        /// </summary>
        public FileManager() { }


        #region Public Methods


        /// <summary>
        /// Load a file which contains maps
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public MapFile LoadFile(string path)
        {
            try
            {
                return FileMgr.Load<MapFile>(path, MapExtension);
            }
            catch (Exception ex)
            {
                throw new TspDataException(DiagnosticEvents.LoadMapError, String.Format(Resources.ExLoadMapError, path, ex.Message), ex);
            }
        }
        /// <summary>
        /// Load a file which contains points
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<Point> LoadPoints(string path)
        {
            var points = new List<Point>();
            var txtReader = FileMgr.SaveLoad[PointExtension] as FPTextSaveLoad;
            string[] r = null;


            if (txtReader.LoadRows(path, out r))
            {
                var read = r.ToList();
                if (read.Count != 0)
                {
                    for (int i = 0; i < read.Count(); i++)
                    {
                        var line = read[i];
                        var parts = line.Split(PointCoordinateSeperator);
                        if (parts.Count() > 1)
                        {
                            int x = 0;
                            int y = 0;
                            if (!Int32.TryParse(parts[0], out x))
                                throw new TspDataException(DiagnosticEvents.LoadPointNoNumber, String.Format(Resources.ExLoadPointNoNumber, "X", i, path, parts[0]));
                            else if (!Int32.TryParse(parts[1], out y))
                                throw new TspDataException(DiagnosticEvents.LoadPointNoNumber, String.Format(Resources.ExLoadPointNoNumber, "Y", i, path, parts[1]));
                            else
                            {
                                points.Add(new Point(x, y)
                                {
                                    Index = i + 1
                                });
                            }
                        }
                    }
                }
                else
                    throw new TspDataException(DiagnosticEvents.LoadPointEmpty, String.Format(Resources.ExLoadPointEmpty, path));
            }
            else
            {
                try
                {
                    FileMgr.Load<string>(path, PointExtension);
                    throw new TspDataException(DiagnosticEvents.LoadPointError, String.Format(Resources.ExLoadPointError, path, Resources.ExLoadPointErrorUnknown));
                }
                catch (Exception ex)
                {
                    throw new TspDataException(DiagnosticEvents.LoadPointError, String.Format(Resources.ExLoadPointError, path, ex.Message), ex);
                }
            }
            return points;
        }
        /// <summary>
        /// Save a file which contains points
        /// </summary>
        /// <param name="path"></param>
        /// <param name="points"></param>
        public void SaveFile(string path, List<Point> points)
        {
            var builder = new StringBuilder();
            var pointsClone = points.OrderBy(x => x.Index).ToList();
            foreach (var p in pointsClone)
            {
                builder.AppendLine(p.X.ToString() + PointCoordinateSeperator + p.Y.ToString());
            }
            try
            {
                FileMgr.Save(builder.ToString(), path, PointExtension);
            }
            catch (Exception ex)
            {
                throw new TspDataException(DiagnosticEvents.SavePointError, String.Format(Resources.ExSavePointError, path, ex.Message), ex);
            }
        }
        /// <summary>
        /// Save a file which contains maps
        /// </summary>
        /// <param name="path"></param>
        /// <param name="mf"></param>
        public void SaveFile(string path, MapFile mf)
        {
            try
            {
                FileMgr.Save(mf, path, MapExtension);
            }
            catch (Exception ex)
            {
                throw new TspDataException(DiagnosticEvents.SaveMapError, String.Format(Resources.ExSaveMapError, path, ex.Message), ex);
            }
        }


        #endregion


    }
}