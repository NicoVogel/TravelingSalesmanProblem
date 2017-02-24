using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using TSP.Entities;
using TSP.Interfaces.Presentation;

namespace TSP.Controls.TspTreeView
{
    public class TreeViewStructureViewModel : BaseViewModel, ITspTreeView
    {
        public event SelectedChanged SelectedCheckBoxChanged;

        protected virtual void OnSelectedCheckBoxChanged(TreeViewDataViewModel sender, int generation)
        {
            if (SelectedCheckBoxChanged != null)
                SelectedCheckBoxChanged(sender, generation);
        }


        private ObservableCollection<TreeViewDataViewModel> m_items;


        /// <summary>
        /// Th
        /// </summary>
        public ObservableCollection<TreeViewDataViewModel> Items
        {
            get
            {
                if (m_items == null)
                    m_items = new ObservableCollection<TreeViewDataViewModel>();
                return m_items;
            }
            set { m_items = value; }
        }


        /// <summary>
        /// Default constructor
        /// </summary>
        public TreeViewStructureViewModel()
        {

        }


        #region Interface TreeView Methods




        public void AddGeneration(Log l)
        {
            // Add a new log to the list
            Items.Add(new TreeViewDataViewModel(l, this.checkboxChanged));
        }


        public void AddGenerations(List<Log> logs)
        {
            foreach (var log in logs)
            {
                AddGeneration(log);
            }
        }
        public void RemoveGeneration(int generation)
        {
            // find the item with the same generation
            var item = Items.Where(x => x.Value is int && ((int)x.Value) == generation).FirstOrDefault();
            if (item != null)
            {
                // if there is one, remove it from the list
                Items.Remove(item);
            }
        }

        public void RemoveAllGenerations()
        {
            // clear all items
            Items.Clear();
        }


        public void UncheckAllCheckboxes()
        {
            foreach (var viewModel in Items)
            {
                viewModel.IsChecked = false;
            }
        }


        #endregion


        private void checkboxChanged(int generation)
        {
            // get all view models that are not the newly selected one
            var viewModles = Items.Where(x => x.Value is int && ((int)x.Value) != generation);
            foreach (var viewModel in viewModles)
            {
                // uncheck all checkboxes except for the event sender
                viewModel.IsChecked = false;
            }
            // get the event sender
            var eventSender = Items.Where(x => x.Value is int && ((int)x.Value) == generation).FirstOrDefault();

            // throw a new event with the event sender and the generation
            OnSelectedCheckBoxChanged(eventSender, generation);
        }
    }
}
