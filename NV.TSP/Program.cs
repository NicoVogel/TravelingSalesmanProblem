using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Presentation;

namespace TSP
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = Console.WindowWidth + 40;
            Console.SetWindowSize(width, Console.WindowHeight);
            var pc = new PresentationController();
            

        }
    }
}
