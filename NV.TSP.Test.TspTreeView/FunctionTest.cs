using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;



using TSP.Controls.TspTreeView;
using TSP.Entities;
using TSP.Interfaces.Presentation;
using TSP.Presentation;
using System.Windows;

namespace NV.TSP.Test.TspTreeView
{
    [TestClass]
    public class FunctionTest
    {
        [TestMethod]
        public void AddGeneration()
        {
            // create a new tree view view models
            var ctx = new TreeViewStructureViewModel();

            // create a value that has the same interface which is used in the business layer
            ITspTreeView treeView = ctx;


            // create dummy log
            var log1 = new Log()
            {
                Generation = 1,
                Distance = 10000,
                Age = 100,
                Fitness = 18000,
                Intersections = 90
            };


            // add the first dummy to the tree view
            treeView.AddGeneration(log1);

            // assert that the items in the view model are no longer null
            Assert.IsNotNull(ctx.Items);

            // there is only one item in the list
            Assert.IsTrue(ctx.Items.Count == 1);

        }



        [TestMethod]
        public void AddGenerationThroughUI()
        {
            // create a new tree view view models
            var window = new MainWindow();

            // create the reference to the tree view view model from the wpf
            var ctx = window.DataContext as TreeViewStructureViewModel;

            // create a value that has the same interface which is used in the business layer
            ITspTreeView treeView = ctx;


            // create dummy log
            var log1 = new Log()
            {
                Generation = 1,
                Distance = 10000,
                Age = 100,
                Fitness = 18000,
                Intersections = 90
            };


            // add the first dummy to the tree view
            treeView.AddGeneration(log1);

            // assert that the items in the view model are no longer null
            Assert.IsNotNull(ctx.Items);

            // there is only one item in the list
            Assert.IsTrue(ctx.Items.Count == 1);
        }


        
    }
}
