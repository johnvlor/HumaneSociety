﻿using System;
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
            HumaneSociety humaneSociety = new HumaneSociety();
            humaneSociety.Run();

            Console.ReadKey();
        }
    }
}
