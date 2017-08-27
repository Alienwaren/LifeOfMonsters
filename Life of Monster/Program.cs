using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life_of_Monster
{
    class Program
    {
        static int Main(string[] args)
        {
            MainLoop loop = new MainLoop();
            return loop.Loop();
           
        }
    }
}
