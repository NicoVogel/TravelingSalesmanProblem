using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Entities.Interfaces.Presentation
{
    public interface IMainWindow
    {

        /// <summary>
        /// public accessor
        /// </summary>
        ITspCanvas TspCan { get; }
        /// <summary>
        /// get the checked value from the raidiobutton which is responsible for displaying the best map
        /// </summary>
        bool BestIsSelected { get; }
        /// <summary>
        /// get the checked value from the raidiobutton which is responsible for displaying the shortest map
        /// </summary>
        bool ShortestIsSelected { get; }

    }
}
