using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using TSP.Entities;

namespace TSP.Controls.TspTreeView
{
    public class TreeViewDataViewModel : BaseViewModel
    {

        private Action<int> m_checkboxChecked;


        #region Properties


        /// <summary>
        /// The displayed name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// contain the value
        /// </summary>
        public object Value { get; set; }

        public bool IsChecked { get; set; }
        /// <summary>
        /// The child informations Distanc, Fitness, Age, Interstection
        /// </summary>
        public ObservableCollection<TreeViewDataViewModel> Children { get; set; }
        /// <summary>
        /// Only show a checkbox if this is the parent
        /// </summary>
        public bool ShowCheckbox { get { return Children != null; } }


        #endregion

        #region Command


        public ICommand OnCheckedCommand { get; set; }


        #endregion

        #region Constructor


        /// <summary>
        /// create a child data type property
        /// </summary>
        /// <param name="parentStructure"></param>
        /// <param name="type"></param>
        /// <param name="log"></param>
        public TreeViewDataViewModel(Log m, Action<int> checkboxChecked)
        {
            m_checkboxChecked = checkboxChecked;
            OnCheckedCommand = new RelayCommand(this.CheckboxChecked);
            Children = new ObservableCollection<TreeViewDataViewModel>();
            Name = "Generation: ";
            Value = m.Generation;
            Children.Add(new TreeViewDataViewModel("Distance: ", m.Distance));
            Children.Add(new TreeViewDataViewModel("Fitness: ", m.Fitness));
            Children.Add(new TreeViewDataViewModel("Age: ", m.Age));
            Children.Add(new TreeViewDataViewModel("Intersections: ", m.Intersections));
        }

        public TreeViewDataViewModel(string title, object value)
        {
            m_checkboxChecked = null;
            Name = title;
            Value = value;
            OnCheckedCommand = null;
            Children = null;
        }


        #endregion

        #region Methods


        /// <summary>
        /// this 
        /// </summary>
        /// <param name="generation"></param>
        private void CheckboxChecked()
        {
            if (m_checkboxChecked == null)
                return;

            if (Value is int)
                m_checkboxChecked((int)Value);
        }

        #endregion

    }
}
