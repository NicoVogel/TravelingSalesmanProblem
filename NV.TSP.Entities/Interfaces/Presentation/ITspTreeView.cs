using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Entities;

namespace TSP.Interfaces.Presentation
{
    public interface ITspTreeView
    {



        void AddGeneration(Map m, Log l);

        void RemoveGeneration(Map m);

        void RemoveAllGenerations();
    }
}
