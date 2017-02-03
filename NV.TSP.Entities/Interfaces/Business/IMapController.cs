using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Interfaces.Business
{
    public interface IMapController
    {
        bool StopProcess { get; set; }


        void Process();

        
    }
}
