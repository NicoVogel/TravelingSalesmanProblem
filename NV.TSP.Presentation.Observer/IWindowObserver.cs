using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Entities;

namespace TSP.Presentation.Observer
{
    public interface IWindowObserver
    {
        /// <summary>
        /// read the best map in the observer
        /// </summary>
        /// <returns></returns>
        Map GetBestMap();
        /// <summary>
        /// read the shortest map in the observer
        /// </summary>
        /// <returns></returns>
        Map GetShortestMap();
        /// <summary>
        /// override the best map in the observer
        /// </summary>
        /// <param name="m"></param>
        void SetNewBestMap(Map m);
        /// <summary>
        /// override the shortest map in the observer
        /// </summary>
        /// <param name="m"></param>
        void SetNewShortestMap(Map m);
    }
}
