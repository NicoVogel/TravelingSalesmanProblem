using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Entities.Enums;
using TSP.Interfaces.Presentation;

namespace TSP.Interfaces.Business
{
    public interface IPresentationController
    {

        ITspTreeView TreeView { get; set; }


        /// <summary>
        /// get the extesion for the map files
        /// </summary>
        string MapExtension { get; }
        /// <summary>
        /// get the extsion for point files
        /// </summary>
        string PointExtension { get; }
        /// <summary>
        /// if threads are running it returns true
        /// </summary>
        bool ThreadsAreRunning { get; }
        /// <summary>
        /// This indidates if there are unsaved maps
        /// </summary>
        bool HasUnsavedInformation { get; }
        

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
        /// start the process to solve the tsp problem
        /// </summary>
        void Run();
        /// <summary>
        /// stop the process to solve the tsp problem
        /// </summary>
        void Stop();
        /// <summary>
        /// clear the current state. Maps, Logs, Point will be cleared
        /// </summary>
        void ClearCurrentState();
        /// <summary>
        /// Redraw the current map
        /// </summary>
        void Redraw();

    }
}
