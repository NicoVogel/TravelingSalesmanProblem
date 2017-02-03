using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Interfaces.Business
{
    public interface IPresentationController
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
        /// Load a file which conatins points
        /// </summary>
        /// <param name="path"></param>
        void LoadFilePoints(string path);
        /// <summary>
        /// Load a file which contains a map
        /// </summary>
        /// <param name="path"></param>
        void LoadFileMap(string path);
        /// <summary>
        /// Save a file at the given path
        /// </summary>
        /// <param name="path"></param>
        void SaveFile(string path);
        /// <summary>
        /// the method for the endless run to progress
        /// </summary>
        void Run();

        void Stop();

    }
}
