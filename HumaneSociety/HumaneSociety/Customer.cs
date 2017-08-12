using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Customer
    {
        DataClassesDataContext db;
        Adopter newAdopter;
        HumaneSociety humaneSociety;

        public Customer()
        {
            db = new DataClassesDataContext();
            newAdopter = new Adopter();
            humaneSociety = new HumaneSociety();
        }

        public void CreateProfile()
        {
            Console.WriteLine("\nPlease fill out the below form");
            Console.WriteLine("First Name: ");
            newAdopter.First_Name = Console.ReadLine();

            Console.WriteLine("Last Name: ");
            newAdopter.Last_Name = Console.ReadLine();

            Console.WriteLine("Address: ");
            newAdopter.Address = Console.ReadLine();

            Console.WriteLine("Phone: ");
            newAdopter.Phone = Console.ReadLine();

            GetPreviousPetOwner();

            Console.WriteLine("Homeowner or Renter: ");
            newAdopter.Homeowner_Renter = Console.ReadLine();

            Console.WriteLine("Pet Preference: ");
            newAdopter.Animal_Preference = Console.ReadLine();
        }

        public void GetPreviousPetOwner()
        {
            string userInput;

            Console.WriteLine("Previously Owned a Pet: ");
            userInput = Console.ReadLine().ToLower();

            if (userInput == "yes")
            {
                newAdopter.Previous_Pet_Owner = true;
            }
            else if (userInput == "no")
            {
                newAdopter.Previous_Pet_Owner = false;
            }
            else
            {
                Console.WriteLine("Invalid input.  Please enter Yes or No.");
                GetPreviousPetOwner();
                return;
            }
        }

        public void AddCustomerToDatabase()
        {
            db.Adopters.InsertOnSubmit(newAdopter);
            db.SubmitChanges();
        }

        public void SearchAllAvailableAnimals()
        {
            var animals = db.Animals;

            Console.WriteLine("All Available Animals for Adoption");
            foreach (var a in animals)
            {
                Console.WriteLine();
                Console.WriteLine("Tag ID: " + a.Animal_Id);
                Console.WriteLine("Name: " + a.Name);
                Console.WriteLine("Category: " + a.Category);
                Console.WriteLine("Gender: " + a.Gender);
                Console.WriteLine("Age: " + a.Age);
                Console.WriteLine("Shots: " + a.Shots);
                Console.WriteLine("Food: " + a.Food);
                Console.WriteLine("Adoption Fee: $" + a.AdoptionFee.Adoption_Fee);
            }
        }

        public void SearchAnimalsInACategory()
        {
            int count = 0;

            humaneSociety.EnterAnimalCategory();

            var animals = db.Animals.Where(a => a.Category == humaneSociety.newAnimals.Category);

            if (!animals.Any())
            {
                Console.WriteLine("This data does not exist.");
            }
            else
            {
                foreach (var a in animals)
                {
                    Console.WriteLine("\nName: " + a.Name);
                    Console.WriteLine("Category: " + a.Category);
                    Console.WriteLine("Gender: " + a.Gender);
                    Console.WriteLine("Age: " + a.Age);
                    Console.WriteLine("Shots: " + a.Shots);
                    Console.WriteLine("Food: " + a.Food);
                    Console.WriteLine("Adoption Fee: $" + a.AdoptionFee.Adoption_Fee);
                    count++;
                }
                Console.WriteLine("\nTotal: {0}", count);
            }
        }
    }
}
