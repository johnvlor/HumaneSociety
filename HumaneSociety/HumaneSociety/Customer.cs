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

        public Customer()
        {
            db = new DataClassesDataContext();
            newAdopter = new Adopter();
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
    }
}
