using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

using TSP.Interfaces.Presentation;

namespace TSP.Entities.Controls
{
    public class TspTreeView : TreeView, ITspTreeView
    {






        public TspTreeView()
        {
            
        }


        public void AddGeneration(Map m)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllGenerations()
        {
            throw new NotImplementedException();
        }

        public void RemoveGeneration(Map m)
        {
            throw new NotImplementedException();
        }
    }
}
