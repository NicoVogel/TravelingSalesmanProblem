using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Entities;

namespace TSP.Interfaces.Business
{
    public interface IValueShare
    {
        /// <summary>
        /// public accessor
        /// </summary>
        Map ShortestMap { get; }
        /// <summary>
        /// public accessor
        /// </summary>
        Map BestMap { get; }
        /// <summary>
        /// public accessor
        /// </summary>
        int CurrentAge
        {
            get;
            set;
        }
        /// <summary>
        /// public accessor
        /// </summary>
        Log CurrentLog { get; }



        /// <summary>
        /// Set the new best map
        /// </summary>
        /// <param name="m"></param>
        void NewBest(Map m);
        /// <summary>
        /// Set the new shortest map
        /// </summary>
        /// <param name="m"></param>
        void NewShortest(Map m);
    }
}
