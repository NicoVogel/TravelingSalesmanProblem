using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using TSP.Entities;
using TSP.Entities.Data;


namespace TSP.Interfaces.Data
{
    public interface IFileManager
    {

        /// <summary>
        /// get the extesion for the map files
        /// </summary>
        string MapExtension { get; }
        /// <summary>
        /// get the extsion for point files
        /// </summary>
        string PointExtension { get; }



        /// <summary>
        /// Load a file which contains maps
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        MapFile LoadFile(string path);
        /// <summary>
        /// Save a file which contains maps
        /// </summary>
        /// <param name="path"></param>
        /// <param name="mf"></param>
        void SaveFile(string path, MapFile mf);
        /// <summary>
        /// Load a file which contains points
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        List<Point> LoadPoints(string path);
        /// <summary>
        /// Save a file which contains points
        /// </summary>
        /// <param name="path"></param>
        /// <param name="points"></param>
        void SaveFile(string path, List<Point> points);


    }
}
