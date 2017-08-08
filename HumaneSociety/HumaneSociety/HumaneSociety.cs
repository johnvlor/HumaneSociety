using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace HumaneSociety
{
    class HumaneSociety
    {

        public void Run()
        {
            DataClassesDataContext db = new DataClassesDataContext();

            var animals =
                from a in db.Animals
                where a.Category == "Dog"
                select a;

            foreach(var x in animals)
            {
                Console.WriteLine("Name: "+ x.Name);
                Console.WriteLine("Category: " + x.Category);
                Console.WriteLine("Age: " + x.Age);
                Console.WriteLine("Status: " + x.Status);
            }

        }
    }
}
