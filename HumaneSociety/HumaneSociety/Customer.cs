using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Customer
    {
        private DataClassesDataContext db;
        public Adopter newAdopter;
        private HumaneSociety humaneSociety;

        public Customer()
        {
            db = new DataClassesDataContext();
            newAdopter = new Adopter();
            humaneSociety = new HumaneSociety();
        }

        public void CreateProfile()
        {
            Console.WriteLine("\nPlease fill out the below form");

            EnterCustomerFirstName();
            EnterCustomerLastName();
            EnterCustomerAddress();
            EnterCustomerPhone();           
            EnterPreviousPetOwner();
            EnterHomeownerRenter();
            EnterCustomerPetPreference();
        }

        public void EnterCustomerFirstName()
        {
            Console.WriteLine("First Name: ");
            newAdopter.First_Name = Console.ReadLine().ToLower();

            if (newAdopter.First_Name == "")
            {
                Console.WriteLine("Invalid input.  Please try again.");
                EnterCustomerFirstName();
                return;
            }
        }

        public void EnterCustomerLastName()
        {
            Console.WriteLine("Last Name: ");
            newAdopter.Last_Name = Console.ReadLine().ToLower();

            if (newAdopter.Last_Name == "")
            {
                Console.WriteLine("Invalid input.  Please try again.");
                EnterCustomerLastName();
                return;
            }
        }

        public void EnterCustomerAddress()
        {
            Console.WriteLine("Address: ");
            newAdopter.Address = Console.ReadLine().ToLower();

            if (newAdopter.Address == "")
            {
                Console.WriteLine("Invalid input.  Please try again.");
                EnterCustomerAddress();
                return;
            }
        }

        public void EnterCustomerPhone()
        {
            Console.WriteLine("Phone: ");
            newAdopter.Phone = Console.ReadLine();

            if (newAdopter.Phone == "")
            {
                Console.WriteLine("Invalid input.  Please try again.");
                EnterCustomerPhone();
                return;
            }
        }

        public void EnterHomeownerRenter()
        {
            Console.WriteLine("Homeowner or Renter: ");
            newAdopter.Homeowner_Renter = Console.ReadLine().ToLower();

            if (newAdopter.Homeowner_Renter != "homeowner" && newAdopter.Homeowner_Renter != "renter")
            {
                Console.WriteLine("Invalid input.  Please enter Homeowner or Renter.");
                EnterHomeownerRenter();
                return;
            }
        }

        public void EnterPreviousPetOwner()
        {
            string input;

            Console.WriteLine("Previously Owned a Pet: ");
            input = Console.ReadLine().ToLower();

            if (input == "yes")
            {
                newAdopter.Previous_Pet_Owner = true;
            }
            else if (input == "no")
            {
                newAdopter.Previous_Pet_Owner = false;
            }
            else
            {
                Console.WriteLine("Invalid input.  Please enter Yes or No.");
                EnterPreviousPetOwner();
                return;
            }
        }

        public void EnterCustomerPetPreference()
        {
            Console.WriteLine("Pet Preference: ");
            newAdopter.Animal_Preference = Console.ReadLine().ToLower();
        }

        public void AddCustomerToDatabase()
        {
            try
            {
                db.Adopters.InsertOnSubmit(newAdopter);
                db.SubmitChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SearchAllAvailableAnimals()
        {
            var animals = db.Animals;

            if (!animals.Any())
            {
                Console.WriteLine("This data does not exist.");
            }
            else
            {
                Console.WriteLine("\nAll Available Animals for Adoption");
                foreach (var a in animals)
                {
                    Console.WriteLine();
                    Console.WriteLine("Name: " + a.Name);
                    Console.WriteLine("Category: " + a.Category);
                    Console.WriteLine("Gender: " + a.Gender);
                    Console.WriteLine("Age: " + a.Age);
                    Console.WriteLine("Shots: " + a.Shots);
                    Console.WriteLine("Food: " + a.Food);
                    Console.WriteLine("Adoption Fee: $" + a.AdoptionFee.Adoption_Fee);
                }
            }
        }

        public void SearchByName()
        {
            humaneSociety.EnterAnimalName();

            var animals = db.Animals.Where(a => a.Name == humaneSociety.newAnimals.Name);

            if (!animals.Any())
            {
                Console.WriteLine("This data does not exist.");
            }
            else
            {
                foreach (var a in animals)
                {
                    Console.WriteLine();
                    Console.WriteLine("Name: " + a.Name);
                    Console.WriteLine("Category: " + a.Category);
                    Console.WriteLine("Gender: " + a.Gender);
                    Console.WriteLine("Age: " + a.Age);
                    Console.WriteLine("Shots: " + a.Shots);
                    Console.WriteLine("Food: " + a.Food);
                    Console.WriteLine("Adoption Fee: $" + a.AdoptionFee.Adoption_Fee);
                }
            }
        }

        public void SearchAnimalsInACategory()
        {
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
                }
            }
        }        

        public void SearchByAdditionalGender()
        {
            humaneSociety.EnterAnimalGender();

            var animals = db.Animals.Where(a => a.Category == humaneSociety.newAnimals.Category && a.Gender == humaneSociety.newAnimals.Gender);

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
                }
            }
        }

        public void SearchByAdditionalAge()
        {
            humaneSociety.EnterAnimalAge();

            var animals = db.Animals.Where(a => a.Category == humaneSociety.newAnimals.Category && a.Age == humaneSociety.newAnimals.Age);

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
                }
            }
        }

        public void GetAgeList()
        {
            var animals = db.Animals.Where(a => a.Category == humaneSociety.newAnimals.Category);

            Console.WriteLine("\nCurrent list of Pet ages for {0}.", humaneSociety.newAnimals.Category);
            foreach(var a in animals)
            {
                Console.WriteLine(a.Age);
            }
        }

        public void SearchByAdditionalShots()
        {
            humaneSociety.EnterAnimalShots();

            var animals = db.Animals.Where(a => a.Category == humaneSociety.newAnimals.Category && a.Shots == humaneSociety.newAnimals.Shots);

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
                }
            }
        }

        public void SearchByAdditionalFood()
        {
            humaneSociety.EnterAnimalFood();

            var animals = db.Animals.Where(a => a.Category == humaneSociety.newAnimals.Category && a.Food == humaneSociety.newAnimals.Food);

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
                }
            }
        }

        public void GetFoodList()
        {
            var animals = db.Animals.Where(a => a.Category == humaneSociety.newAnimals.Category);

            Console.WriteLine("\nCurrent list of Pet food diets for {0}.", humaneSociety.newAnimals.Category);
            foreach (var a in animals)
            {
                Console.WriteLine(a.Food);
            }
        }

    }
}
