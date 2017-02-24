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
        
        event SelectedChanged SelectedCheckBoxChanged;

        void AddGeneration(Log log);

        void AddGenerations(List<Log> logs);
        void RemoveGeneration(int generation);

        void RemoveAllGenerations();

        void UncheckAllCheckboxes();
    }

    public delegate void SelectedChanged(object sender, int generation);
}
