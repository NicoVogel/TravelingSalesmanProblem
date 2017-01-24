using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    class Program
    {
        static void Main(string[] args)
        {
            MapController mc = new MapController();

            int width = Console.WindowWidth + 40;
            Console.SetWindowSize(width, Console.WindowHeight);
            mc.ReadPoints(@"E:\temp\att532.txt");
            mc.LoadMap(@"E:\temp\test4.xml");
            mc.Process(1000000);
            mc.SaveMap(@"E:\temp\test4.xml");

            ////mc.ReadPoints(@"E:\temp\smalTest.txt");
            //mc.LoadMap(@"E:\temp\smalResult.xml");
            //mc.Process(10000);
            //mc.SaveMap(@"E:\temp\smalResult.xml");
        }
    }
}
