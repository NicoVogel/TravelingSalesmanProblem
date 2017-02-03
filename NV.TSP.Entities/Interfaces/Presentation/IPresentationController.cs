using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Entities.Interfaces.Presentation
{
    public interface IPresentationController
    {
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
