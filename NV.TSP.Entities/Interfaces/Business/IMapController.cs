using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Entities.Interfaces.Business
{
    public interface IMapController
    {
        bool StopProcess { get; set; }


        void Process();


        /// <summary>
        /// Liesst punklte aus einer Datei. Diese ueberschreiben die momentanen Informationen
        /// </summary>
        /// <param name="path"></param>
        void ReadPoints(string path);
        /// <summary>
        /// Speicher die momentan beste karte
        /// </summary>
        /// <param name="path"></param>
        void SaveMap(string path);
        /// <summary>
        /// Lade eine Karte und ueberschreibe die momentanen Informationen
        /// </summary>
        /// <param name="path"></param>
        void LoadMap(string path);
    }
}
