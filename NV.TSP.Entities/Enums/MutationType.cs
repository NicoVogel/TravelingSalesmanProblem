using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Entities.Enums
{
    /// <summary>
    /// these are the different types of mutations that do exist
    /// </summary>
    public enum MutationType
    {
        /// <summary>
        /// Default
        /// <para>Try to randomly change the map</para>
        /// </summary>
        Random,
        /// <summary>
        /// Try to change the map by randomly check neighboring
        /// </summary>
        Neighboring,
        /// <summary>
        /// Try to change the map by removing the intersections
        /// </summary>
        Intersections
    }
}
