using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Controls.TspTreeView;

namespace TSP.Presentation
{
    public class ViewModelLocator
    {
        private readonly TreeViewStructureViewModel _viewModel = new TreeViewStructureViewModel();

        public TreeViewStructureViewModel Main
        {
            get { return _viewModel; }
        }
    }

}
