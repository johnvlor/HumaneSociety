using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;


namespace HumaneSociety
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Run();

            Console.ReadKey();
        }
    }
}
