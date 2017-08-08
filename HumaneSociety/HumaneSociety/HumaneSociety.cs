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
        Animal newAnimals = new Animal();
        DataClassesDataContext db = new DataClassesDataContext();

        public HumaneSociety()
        {

        }

        public void Run()
        {
            AddAnimal();
            AddToDatabase();
            DisplayQuery();

            
            //newAnimals.Name = "Skittles";
            //newAnimals.Category = "Cat";
            //newAnimals.Status = "Available";
            

            //var cat =
            //    from a in db.Animals
            //    where a.Category == "Cat"
            //    select a;

            //foreach (var c in cat)
            //    c.Age = "6 months";

            ////db.Animals.InsertOnSubmit(newAnimals);
            //db.SubmitChanges();



        }

        public Animal AddAnimal()
        {
            Console.WriteLine("Add an animal to the system.");
            Console.WriteLine("Name: ");
            newAnimals.Name = Console.ReadLine();

            Console.WriteLine("Category: ");
            newAnimals.Category = Console.ReadLine();

            Console.WriteLine("Gender: ");
            newAnimals.Gender = Console.ReadLine();

            Console.WriteLine("Age: ");
            newAnimals.Age = Console.ReadLine();

            Console.WriteLine("Status: ");
            newAnimals.Status = Console.ReadLine();

            Console.WriteLine("Room: ");
            newAnimals.Room = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Shots: ");
            newAnimals.Shots = Console.ReadLine();

            Console.WriteLine("Food: ");
            newAnimals.Food = Console.ReadLine();

            return newAnimals;
        }

        public void AddToDatabase()
        {
            db.Animals.InsertOnSubmit(newAnimals);
            db.SubmitChanges();
        }

        public void DisplayQuery()
        {
            var animals =
                from a in db.Animals
                select a;

            foreach (var x in animals)
            {
                Console.WriteLine("Name: " + x.Name);
                Console.WriteLine("Category: " + x.Category);
                Console.WriteLine("Age: " + x.Age);
                Console.WriteLine("Status: " + x.Status);
                Console.WriteLine("Gender: " + x.Gender);
                Console.WriteLine("Room: " + x.Room);
                Console.WriteLine("Shots: " + x.Shots);
                Console.WriteLine("Food: " + x.Food);
                Console.WriteLine();
            }

        }
    }
}
