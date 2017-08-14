using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class HumaneSociety
    {
        public Animal newAnimals;
        private Room newRoom;
        private AdoptionFee newAdoptionFee;
        private Adopter newAdopter;
        private Register register;
        private DataClassesDataContext db;

        public HumaneSociety()
        {
            db = new DataClassesDataContext();
            newAnimals = new Animal();
            newRoom = new Room();
            newAdoptionFee = new AdoptionFee();
            newAdopter = new Adopter();
            register = new Register();
        }

        public Animal AddAnimalInformation()
        {
            Console.WriteLine("\nFill out the below form");
            EnterAnimalName();
            EnterAnimalCategory();
            EnterAnimalGender();
            EnterAnimalAge();
            EnterAnimalShots();
            EnterAnimalFood();
            newAnimals.Status = "Available";
            EnterAdoptionFee();

            return newAnimals;
        }

        public void EnterAnimalId()
        {
            Console.WriteLine("Animal's Tag Id: ");

            try
            {
                newAnimals.Animal_Id = Convert.ToInt32(Console.ReadLine());
            }
            catch(FormatException)
            {
                Console.WriteLine("Invalid input.  Please try again.");
                EnterAnimalId();
                return;
            }
        }

        public void EnterAnimalName()
        {
            Console.WriteLine("Animal's Name: ");
            newAnimals.Name = Console.ReadLine().ToLower();
        }

        public void EnterAnimalCategory()
        {
            Console.WriteLine("Category: ");
            newAnimals.Category = Console.ReadLine().ToLower();
        }

        public void EnterAnimalGender()
        {
            Console.WriteLine("Gender: ");
            newAnimals.Gender = Console.ReadLine().ToLower();

            if(newAnimals.Gender != "male" && newAnimals.Gender != "female")
            {
                Console.WriteLine("Please try again.  Enter either male or female.");
                EnterAnimalGender();
                return;
            }
        }

        public void EnterAnimalAge()
        {
            Console.WriteLine("Age: ");
            newAnimals.Age = Console.ReadLine().ToLower();
        }

        public void EnterAnimalShots()
        {
            Console.WriteLine("Shots (yes or no): ");
            newAnimals.Shots = Console.ReadLine().ToLower();

            if (newAnimals.Shots != "yes" && newAnimals.Shots != "no")
            {
                Console.WriteLine("Please try again.  Enter either yes or no.");
                EnterAnimalShots();
                return;
            }
        }

        public void EnterAnimalFood()
        {
            Console.WriteLine("Food: ");
            newAnimals.Food = Console.ReadLine().ToLower();
        }

        public void EnterRoomNumber()
        {
            Console.WriteLine("Room Number: ");
            try
            {
                newRoom.Room_Number = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.  Please enter a number.");
                EnterRoomNumber();
                return;
            }
        }

        public void EnterAdoptionFee()
        {
            Console.WriteLine("\nAdoption Fee Pricing - $50, $75, $100, $200" +
                "\nEnter Amount:");
            try
            {
                newAdoptionFee.Adoption_Fee = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.  Please enter a number.");
                EnterAdoptionFee();
                return;
            }

            if (newAdoptionFee.Adoption_Fee != 200 && newAdoptionFee.Adoption_Fee != 100 && newAdoptionFee.Adoption_Fee != 75 && newAdoptionFee.Adoption_Fee != 50)
            {
                Console.WriteLine("Invalid input.  Please follow the Fee pricing $50, $75, $100, $200");
                EnterAdoptionFee();
                return;
            }
        }

        public void AddToDatabase()
        {
            try
            {
                db.Animals.InsertOnSubmit(newAnimals);
                db.SubmitChanges();

                Console.WriteLine("\nNew animal has been added to the system.");
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to add animal to the system.");
                Console.WriteLine(e);
            }
        }

       public void StartAdoptionProcess()
        {
            GetAdoptionAnimalName();
            GetAdoptorsName();
            UpdateAdoptionStatus();
            register.CollectAdoptionFee(newAdopter, newAnimals);
        }

        public void GetAdoptionAnimalName()
        {
            EnterAnimalName();
            EnterAnimalId();

            var animals = db.Animals.Where(a => a.Animal_Id == newAnimals.Animal_Id && a.Name == newAnimals.Name);

            if (!animals.Any())
            {
                Console.WriteLine("Animal name does not match the tag id.  Please try again." );
                GetAdoptionAnimalName();
                return;
            }
        }

        public void GetAdoptorsName()
        {
            Console.WriteLine("Enter Adopting Customer's First Name: ");
            newAdopter.First_Name = Console.ReadLine().ToLower();

            Console.WriteLine("Enter Adopting Customer's Last Name: ");
            newAdopter.Last_Name = Console.ReadLine().ToLower();

            Console.WriteLine("Enter Adopting Customer's Id: ");
            newAdopter.Adopter_Id = Convert.ToInt32(Console.ReadLine());

            var customers = db.Adopters.Where(c => c.First_Name == newAdopter.First_Name && c.Last_Name == newAdopter.Last_Name && c.Adopter_Id == newAdopter.Adopter_Id);

            if (!customers.Any())
            {
                Console.WriteLine("Customer record does not exist.  Please verify input data and try again.");
                GetAdoptorsName();
                return;
            }
        }

        public void UpdateAdoptionStatus()
        {
            var animals = db.Animals.Where(a => a.Animal_Id == newAnimals.Animal_Id && a.Name == newAnimals.Name);

            if (!animals.Any())
            {
                Console.WriteLine("This record does not exist.  Please verify and try again.");
                StartAdoptionProcess();
                return;
            }
            else
            {
                foreach (var a in animals)
                {
                    a.Status = "Adopted";
                    a.Adopter_Id = newAdopter.Adopter_Id;

                    Console.WriteLine("{0} has been adopted by {1} {2}." +
                        "\nAdoption status updated.", a.Name, a.Adopter.First_Name, a.Adopter.Last_Name);
                }

                db.SubmitChanges();
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
                    Console.WriteLine("{0} ({3}) - adopted by {1} {2}", a.Name, a.Adopter.First_Name, a.Adopter.Last_Name, a.Category);
                }
            }            
        }

        public void GetAdoptedAnimal()
        {
            EnterAnimalName();
            EnterAnimalId();

            var animals =
                from a in db.Animals
                join c in db.Adopters on a.Adopter_Id equals c.Adopter_Id
                where (a.Name == newAnimals.Name && a.Animal_Id == newAnimals.Animal_Id && a.Status == "Adopted")
                select a;

            if (!animals.Any())
            {
                Console.WriteLine("This record does not exist.  Please try again.");
                GetAdoptedAnimal();
                return;
            }
            else
            {
                foreach (var a in animals)
                {
                    Console.WriteLine("\n{0} ({3}) - adopted by {1} {2}", a.Name, a.Adopter.First_Name, a.Adopter.Last_Name, a.Category);
                }
            }
        }

        public void AddRoomInformation()
        {
            Console.WriteLine("Room Number: ");
            try
            {
                newRoom.Room_Number = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.  Please enter a number.");
                AddRoomInformation();
                return;
            }
            CheckRoomStatus();
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
                        AddRoomInformation();
                        return;
                    }
                }
                Console.WriteLine("Room is available and added to the record.");
            }
        }

        public void AddRoom()
        {
            var rooms = db.Rooms.Where(r => r.Room_Number == newRoom.Room_Number);

            if (!rooms.Any())
            {
                Console.WriteLine("This record does not exist.");
            }
            else
            {
                foreach (var r in rooms)
                {
                    newAnimals.Room = r;
                }
            }

            db.SubmitChanges();
        }

        public void AddAdoptionFee()
        {
            var adoptionFees = db.AdoptionFees.Where(f => f.Adoption_Fee == newAdoptionFee.Adoption_Fee);

            if (!adoptionFees.Any())
            {
                Console.WriteLine("This record does not exist.");
            }
            else
            {
                foreach (var f in adoptionFees)
                {
                    newAnimals.AdoptionFee = f;
                }
            }

            db.SubmitChanges();
        }

        public void GetAvailableRooms()
        {
            var rooms = db.Rooms.Where(a => !db.Animals.Any(r => r.Room_Id == a.Room_Id));

            Console.WriteLine("\nAvailable Rooms");
            foreach (var r in rooms)
            {
                Console.WriteLine("Room: " + r.Room_Number);
            }
        }

        public void GetRoomNumber()
        {
            EnterRoomNumber();

            var animals = db.Animals.Where(a => a.Room.Room_Number == newRoom.Room_Number);

            if (!animals.Any())
            {
                Console.WriteLine("Room is not occupied and available.");
            }
            else
            {
                foreach (var a in animals)
                {
                    Console.WriteLine("\nRoom {0} is occupied by {1} - Tag Id {2}.", a.Room.Room_Number, a.Name, a.Animal_Id);
                }
            }
        }

        public void GetRoomByAnimal()
        {
            EnterAnimalName();
            EnterAnimalId();

            var animals = db.Animals.Where(a => a.Name == newAnimals.Name && a.Animal_Id == newAnimals.Animal_Id);

            foreach (var a in animals)
            {
                Console.WriteLine("\nRoom {0} is occupied by {1} - Tag Id {2}.", a.Room.Room_Number, a.Name, a.Animal_Id);
            }
        }

        public void GetAllOccupiedRooms()
        {
            var animals = db.Animals.Where(a => a.Room.Room_Number != null);

            foreach (var a in animals)
            {
                 Console.WriteLine("Room {0} is occupied by {1} - Tag Id {2}.", a.Room.Room_Number, a.Name, a.Animal_Id);
            }
        }

        public void UpdateShots()
        {
            EnterAnimalName();
            EnterAnimalId();

            var animals = db.Animals.Where(a => a.Name == newAnimals.Name && a.Animal_Id == newAnimals.Animal_Id);

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
            var animals =
                from a in db.Animals
                join r in db.Rooms on a.Room_Id equals r.Room_Id
                where a.Shots == "yes"
                select a;

            Console.WriteLine("\nAnimals that already received their shots");
            foreach (var a in animals)
            {
                Console.WriteLine("\nTag ID: " + a.Animal_Id);
                Console.WriteLine("Name: " + a.Name);
                Console.WriteLine("Category: " + a.Category);
                Console.WriteLine("Gender: " + a.Gender);
                Console.WriteLine("Age: " + a.Age);
                Console.WriteLine("Room: " + a.Room.Room_Number);
            }
        }

        public void GetAnimalsWithoutShots()
        {
            var animals =
                from a in db.Animals
                join r in db.Rooms on a.Room_Id equals r.Room_Id
                where a.Shots == "no"
                select a;

            Console.WriteLine("\nAnimals that still needs their shots");
            foreach (var a in animals)
            {
                Console.WriteLine("\nTag ID: " + a.Animal_Id);
                Console.WriteLine("Name: " + a.Name);
                Console.WriteLine("Category: " + a.Category);
                Console.WriteLine("Gender: " + a.Gender);
                Console.WriteLine("Age: " + a.Age);
                Console.WriteLine("Room: " + a.Room.Room_Number);
            }
        }

        public void GetAnimalCategories()
        {
            var animals = db.Animals.Select(a => a.Category).Distinct();

            Console.WriteLine("\nCategory of Animals:");
            foreach (var a in animals)
            {
                Console.WriteLine(a);
            }
        }

        public void GetAnimalsInACategory()
        {
            int count = 0;

            EnterAnimalCategory();

            var animals = db.Animals.Where(a => a.Category == newAnimals.Category);

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
            EnterAnimalName();
            EnterAnimalId();

            var animals = db.Animals.Where(a => a.Name == newAnimals.Name && a.Animal_Id == newAnimals.Animal_Id);

            if (!animals.Any())
            {
                Console.WriteLine("This record does not exist.");
            }
            else
            {
                foreach (var a in animals)
                {
                    Console.WriteLine("\n{0} will need to consume {1} of food a week.", a.Name, a.Food);
                }
            }
        }

        public void UpdateFoodDiet()
        {
            EnterAnimalName();
            EnterAnimalId();
            Console.WriteLine("\nEnter new Food amount: ");
            EnterAnimalFood();

            var animals = db.Animals.Where(a => a.Name == newAnimals.Name && a.Animal_Id == newAnimals.Animal_Id);

            if (!animals.Any())
            {
                Console.WriteLine("This record does not exist.");
            }
            else
            {
                foreach (var a in animals)
                {
                    a.Food = newAnimals.Food;
                    Console.WriteLine("\n{0}'s food amount has been updated to {1} a week.", a.Name, a.Food);
                }
                db.SubmitChanges();
            }
        }

        public void GetCustomer()
        {
            Console.WriteLine("Enter Adopting Customer's First Name: ");
            newAdopter.First_Name = Console.ReadLine().ToLower();

            Console.WriteLine("Enter Adopting Customer's Last Name: ");
            newAdopter.Last_Name = Console.ReadLine().ToLower();

            var customers = db.Adopters.Where(c => c.First_Name == newAdopter.First_Name && c.Last_Name == newAdopter.Last_Name);

            if (!customers.Any())
            {
                Console.WriteLine("This record does not exist.  Please verify data input.");
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
            Console.WriteLine("\nEnter Animal Preference Category: ");
            newAdopter.Animal_Preference = Console.ReadLine().ToLower();

            var customers = db.Adopters.Where(c => c.Animal_Preference == newAdopter.Animal_Preference);

            if (!customers.Any())
            {
                Console.WriteLine("No Customers interested in adopting a {0} at this time.");
            }
            else
            {
                Console.WriteLine("\nCustomers interested in adopting a {0}.", newAdopter.Animal_Preference);
                foreach (var c in customers)
                {
                    Console.WriteLine("Customer Name: {0} {1}", c.First_Name, c.Last_Name);
                }              
            }
        }

        public void GetAllAvailableAnimals()
        {
            int count = 0;

            var animals =
                from a in db.Animals
                join r in db.Rooms on a.Room_Id equals r.Room_Id
                where a.Status == "Available"
                select a;

            Console.WriteLine("List of all available animals");
            foreach (var a in animals)
            {
                Console.WriteLine();
                Console.WriteLine("Tag ID: " + a.Animal_Id);
                Console.WriteLine("Name: " + a.Name);
                Console.WriteLine("Category: " + a.Category);
                Console.WriteLine("Gender: " + a.Gender);
                Console.WriteLine("Age: " + a.Age);
                Console.WriteLine("Room: " + a.Room.Room_Number);
                Console.WriteLine("Shots: " + a.Shots);
                Console.WriteLine("Food: " + a.Food);
                Console.WriteLine("Adoption Fee: " + a.AdoptionFee.Adoption_Fee);
                count++;
            }
            Console.WriteLine("\nTotal available animals: {0}", count);
        }

    }
}
