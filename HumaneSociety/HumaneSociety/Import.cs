using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HumaneSociety
{
    class Import
    {
        private DataClassesDataContext db;

        public Import()
        {
            db = new DataClassesDataContext();
        }

        public void ImportCSVFile()
        {
            var query = File.ReadLines(@"C:\\Users\Lor\Documents\Projects\C#\HumaneSociety\HumaneSociety.csv").Select(l => l.Split(',')).Select(a => new 
                         {
                             name = a[0],
                             category = a[1],
                             gender = a[2],
                             age = a[3],
                             shots = a[4],
                             food = a[5],
                             status = a[6],
                             room_id = a[7],
                             adoption_fee_id = a[9]
                         }).Skip(1).ToList();

            if (!query.Any())
            {
                Console.WriteLine("empty");
            }
            else
            {
                try
                {
                    foreach (var q in query)
                    {
                        Animal importAnimals = new Animal();
                        importAnimals.Name = q.name;
                        importAnimals.Category = q.category;
                        importAnimals.Gender = q.gender;
                        importAnimals.Age = q.age;
                        importAnimals.Shots = q.shots;
                        importAnimals.Food = q.food;
                        importAnimals.Status = q.status;
                        importAnimals.Room_Id = int.Parse(q.room_id);
                        importAnimals.Adoption_Fee_Id = int.Parse(q.adoption_fee_id);

                        db.Animals.InsertOnSubmit(importAnimals);
                        db.SubmitChanges();
                    }
                    Console.WriteLine("Data imported successfully.");
                }
                catch(Exception e)
                {
                    Console.WriteLine("\nUnable to import data into the system.");
                    Console.WriteLine(e);
                }                
            }            
        }
    }
}
