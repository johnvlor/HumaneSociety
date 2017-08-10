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
        public Animal newAnimals;
        Room newRoom;
        DataClassesDataContext db;

        public HumaneSociety()
        {
            db = new DataClassesDataContext();
            newAnimals = new Animal();
            newRoom = new Room();
        }

        public Animal GetAnimalInformation()
        {
            Console.WriteLine("Name: ");
            newAnimals.Name = Console.ReadLine();

            Console.WriteLine("Category: ");
            newAnimals.Category = Console.ReadLine().ToLower();

            Console.WriteLine("Gender: ");
            newAnimals.Gender = Console.ReadLine().ToLower();

            Console.WriteLine("Age: ");
            newAnimals.Age = Console.ReadLine().ToLower();

            Console.WriteLine("Shots: ");
            newAnimals.Shots = Console.ReadLine().ToLower();

            Console.WriteLine("Food: ");
            newAnimals.Food = Console.ReadLine().ToLower();

            newAnimals.Status = "Available";

            EnterRoomInformation();

            return newAnimals;
        }

        
        public void AddToDatabase()
        {
            db.Animals.InsertOnSubmit(newAnimals);
            db.SubmitChanges();
        }

        public void DisplayQuery()
        {
            var animals = /*db.Animals;*/
                from a in db.Animals
                join r in db.Rooms on a.Room_Id equals r.Room_Id
                select a;

            foreach (var a in animals)
            {
                Console.WriteLine();
                Console.WriteLine("Tag ID: " + a.Animal_Id);
                Console.WriteLine("Name: " + a.Name);
                Console.WriteLine("Category: " + a.Category);
                Console.WriteLine("Gender: " + a.Gender);
                Console.WriteLine("Age: " + a.Age);
                Console.WriteLine("Status: " + a.Status);
                Console.WriteLine("Room: " + a.Room.Room_Number);
                Console.WriteLine("Shots: " + a.Shots);
                Console.WriteLine("Food: " + a.Food);
            }

        }

       public void StartAdoptionProcess()
        {
            string nameInput;
            int tagInput;
            string customerFirstNameInput;
            string customerLastNameInput;
            int idInput;

            Console.WriteLine("Enter Animal's Name: ");
            nameInput = Console.ReadLine();

            Console.WriteLine("Enter Animal's Tag Id: ");
            tagInput = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Adopting Customer's First Name: ");
            customerFirstNameInput = Console.ReadLine();

            Console.WriteLine("Enter Adopting Customer's Last Name: ");
            customerLastNameInput = Console.ReadLine();

            Console.WriteLine("Enter Customer's Identification Number: ");
            idInput = Convert.ToInt32(Console.ReadLine());

            UpdateAdoptionStatus(nameInput, tagInput, idInput);
        }

        public void UpdateAdoptionStatus(string nameInput, int tagInput, int idInput)
        {
            var animals = db.Animals.Where(a => a.Name == nameInput && a.Animal_Id == tagInput);

            if (!animals.Any())
            {
                Console.WriteLine("This record does not exist.");
            }
            else
            {
                foreach (var a in animals)
                {
                    a.Status = "Adopted";
                    a.Adopter_Id = idInput;
                }

                db.SubmitChanges();

                Console.WriteLine("Adoption status updated.");
            }
        }

        public void RemoveRoomNumber()
        {
            var animals = db.Animals.Where(a => a.Status == "Adopted");

            foreach (var a in animals)
            {
                a.Room_Id = null;
            }

            db.SubmitChanges();

            Console.WriteLine("Room is now available.");
        }

        public void GetAllAdoptedAnimals()
        {
            var animals = 
                from a in db.Animals
                join c in db.Adopters on a.Adopter_Id equals c.Adopter_Id
                where a.Status == "Adopted"
                select a;

            Console.WriteLine("\nList of adopted animals.");
            if (!animals.Any())
            {
                Console.WriteLine("This record does not exist.");
            }
            else
            {
                foreach (var a in animals)
                {
                    Console.WriteLine("{0} adopted by {1} {2}", a.Name, a.Adopter.First_Name, a.Adopter.Last_Name);
                }
            }
            
        }

        public void GetAdoptedAnimal()
        {
            string nameInput;
            int tagInput;

            Console.WriteLine("Enter Animal's Name: ");
            nameInput = Console.ReadLine();

            Console.WriteLine("Enter Animal's Tag Id: ");
            tagInput = Convert.ToInt32(Console.ReadLine());

            var animals =
                from a in db.Animals
                join c in db.Adopters on a.Adopter_Id equals c.Adopter_Id
                where (a.Name == nameInput && a.Animal_Id == tagInput && a.Status == "Adopted")
                select a;

            if (!animals.Any())
            {
                Console.WriteLine("This record does not exist.");
            }
            else
            {
                foreach (var a in animals)
                {
                    Console.WriteLine("\n{0} adopted by {1} {2}", a.Name, a.Adopter.First_Name, a.Adopter.Last_Name);
                }
            }

        }

        public void EnterRoomInformation()
        {
            Console.WriteLine("Room: ");
            try
            {
                newRoom.Room_Number = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.  Please enter a number.");
                EnterRoomInformation();
                return;
            }
            CheckRoomStatus();

            newAnimals.Room = newRoom;
        }

        public void CheckRoomStatus()
        {
            var rooms = db.Rooms.Where(a => db.Animals.Any(r => r.Room_Id == a.Room_Id));

            if (!rooms.Any())
            {
                Console.WriteLine("This record does not exist.");
            }
            else
            {
                foreach (var r in rooms)
                {
                    if (newRoom.Room_Number == r.Room_Number)
                    {
                        
                        Console.WriteLine("Room is not available.  Please choose another one.");
                        EnterRoomInformation();
                        return;
                    }                  
                }
                Console.WriteLine("Room is available.");
            }
        }

        public void GetAvailableRooms()
        {
            var rooms = db.Rooms.Where(a => !db.Animals.Any(r => r.Room_Id == a.Room_Id));

            foreach (var r in rooms)
            {
                Console.WriteLine("Room: " + r.Room_Number);
            }
        }

        public void GetRoomNumber()
        {
            int roomInput;

            Console.WriteLine("Enter Room Number: ");
            roomInput = Convert.ToInt32(Console.ReadLine());

            var animals = db.Animals.Where(a => a.Room.Room_Number == roomInput);

            if (!animals.Any())
            {
                Console.WriteLine("Room is not occupied and available.");
            }
            else
            {
                foreach (var a in animals)
                {
                    Console.WriteLine("Room {0} is occupied by {1}.", a.Room.Room_Number, a.Name);
                }
            }
        }

        public void GetRoomByAnimal()
        {
            string nameInput;
            int tagInput;

            Console.WriteLine("Enter Animal's Name: ");
            nameInput = Console.ReadLine();

            Console.WriteLine("Enter Animal's Tag Id: ");
            tagInput = Convert.ToInt32(Console.ReadLine());

            var animals = db.Animals.Where(a => a.Name == nameInput && a.Animal_Id == tagInput);

            foreach (var a in animals)
            {
                Console.WriteLine("Room {0} is occupied by {1}.", a.Room.Room_Number, a.Name);
            }
        }

        public void GetAllOccupiedRooms()
        {
            var animals = db.Animals.Where(a => a.Room.Room_Number != null);

            foreach (var a in animals)
            {
                 Console.WriteLine("Room {0} is occupied by {1}.", a.Room.Room_Number, a.Name);
            }
        }

        public void UpdateShots()
        {
            string nameInput;
            int tagInput;

            Console.WriteLine("Enter Animal's Name: ");
            nameInput = Console.ReadLine();

            Console.WriteLine("Enter Animal's Tag Id: ");
            tagInput = Convert.ToInt32(Console.ReadLine());

            var animals = db.Animals.Where(a => a.Name == nameInput && a.Animal_Id == tagInput);

            if (!animals.Any())
            {
                Console.WriteLine("This record does not exist.");
            }
            else
            {
                foreach (var a in animals)
                {
                    if (a.Shots == "yes")
                    {
                        Console.WriteLine("Shots is not needed.");
                    }
                    else
                    {
                        a.Shots = "yes";
                        db.SubmitChanges();

                        Console.WriteLine("Shots status changed.");
                    }
                }
            }          
        }

        public void GetAnimalsWithShots()
        {
            var animals = db.Animals.Where(a => a.Shots == "yes");

            foreach (var a in animals)
            {
                Console.WriteLine(a.Name);
            }
        }

        public void GetAnimalsWithoutShots()
        {
            var animals = db.Animals.Where(a => a.Shots == "no");

            foreach (var a in animals)
            {
                Console.WriteLine(a.Name);
            }
        }

        public void GetAnimalCategories()
        {
            var animals =
                (from a in db.Animals
                select a.Category).Distinct();

            foreach (var a in animals)
            {
                Console.WriteLine("Animal Category: {0}", a);
            }
        }

        public void GetAnimalsInACategory()
        {
            string categoryInput;
            int count = 0;

            Console.WriteLine("Enter category of an animal: ");
            categoryInput = Console.ReadLine();

            var animals = db.Animals.Where(a => a.Category == categoryInput);

            if (!animals.Any())
            {
                Console.WriteLine("This data does not exist.");
            }
            else
            {
                foreach (var a in animals)
                {
                    Console.WriteLine("Animal: {0}", a.Name);
                    count++;
                }
                Console.WriteLine("\nTotal: {0}", count);
            }
        }

        public void GetFood()
        {
            string nameInput;
            int tagInput;

            Console.WriteLine("Enter Animal's Name: ");
            nameInput = Console.ReadLine();

            Console.WriteLine("Enter Animal's Tag Id: ");
            tagInput = Convert.ToInt32(Console.ReadLine());

            var animals = db.Animals.Where(a => a.Name == nameInput && a.Animal_Id == tagInput);

            if (!animals.Any())
            {
                Console.WriteLine("This record does not exist.");
            }
            else
            {
                foreach (var a in animals)
                {
                    Console.WriteLine("{0} will need to consume {1} of food a week.", a.Name, a.Food);
                }
            }
        }

        public void UpdateFoodDiet()
        {
            string nameInput;
            int tagInput;
            string foodInput;

            Console.WriteLine("Enter Animal's Name: ");
            nameInput = Console.ReadLine();

            Console.WriteLine("Enter Animal's Tag Id: ");
            tagInput = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter new Food amount: ");
            foodInput = Console.ReadLine();

            var animals = db.Animals.Where(a => a.Name == nameInput && a.Animal_Id == tagInput);

            if (!animals.Any())
            {
                Console.WriteLine("This record does not exist.");
            }
            else
            {
                foreach (var a in animals)
                {
                    a.Food = foodInput;
                }
                db.SubmitChanges();

                Console.WriteLine("Food amount changed.");
            }
        }

        public void GetCustomer()
        {
            string firstNameInput;
            string lastNameInput;

            Console.WriteLine("Enter Adopting Customer's First Name: ");
            firstNameInput = Console.ReadLine().ToLower();

            Console.WriteLine("Enter Adopting Customer's Last Name: ");
            lastNameInput = Console.ReadLine().ToLower();

            var customers = db.Adopters.Where(c => c.First_Name == firstNameInput && c.Last_Name == lastNameInput);

            if (!customers.Any())
            {
                Console.WriteLine("This record does not exist.");
            }
            else
            {
                foreach (var c in customers)
                {
                    Console.WriteLine("\nCustomer Name: {0} {1}", c.First_Name, c.Last_Name);
                    Console.WriteLine("Address: {0}", c.Address);
                    Console.WriteLine("Phone: {0}", c.Phone);
                    Console.WriteLine("Previously Owned a Pet: {0}", c.Previous_Pet_Owner);
                    Console.WriteLine("Homeowner or Renter: {0}", c.Homeowner_Renter);
                    Console.WriteLine("Pet Preference: {0}", c.Animal_Preference);
                }
            }
        }

        public void GetAllCustomers()
        {
            var customers = db.Adopters;

            foreach (var c in customers)
            {
                Console.WriteLine("\nCustomer Name: {0} {1}", c.First_Name, c.Last_Name);
                Console.WriteLine("Address: {0}", c.Address);
                Console.WriteLine("Phone: {0}", c.Phone);
                Console.WriteLine("Previously Owned a Pet: {0}", c.Previous_Pet_Owner);
                Console.WriteLine("Homeowner or Renter: {0}", c.Homeowner_Renter);
                Console.WriteLine("Pet Preference: {0}", c.Animal_Preference);
            }
        }

        public void GetCustomerAnimalPreference()
        {
            string preferenceInput;

            Console.WriteLine("Enter Animal Preference Category: ");
            preferenceInput = Console.ReadLine().ToLower();

            var customers = db.Adopters.Where(c => c.Animal_Preference == preferenceInput);

            if (!customers.Any())
            {
                Console.WriteLine("This record does not exist.");
            }
            else
            {
                Console.WriteLine("\nCustomers interested in adopting {0}.", preferenceInput);
                foreach (var c in customers)
                {
                    Console.WriteLine("Customer Name: {0} {1}", c.First_Name, c.Last_Name);
                }              
            }
        }
    }
}
