using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data;

namespace HumaneSociety
{
    class HumaneSociety
    {
        public Animal newAnimals;
        Room newRoom;
        AdoptionFee newAdoptionFee;
        DataClassesDataContext db;

        public HumaneSociety()
        {
            db = new DataClassesDataContext();
            newAnimals = new Animal();
            newRoom = new Room();
            newAdoptionFee = new AdoptionFee();
        }

        public Animal AddAnimalInformation()
        {
            //Console.WriteLine("Name: ");
            //newAnimals.Name = Console.ReadLine();
            EnterAnimalName();

            //Console.WriteLine("Category: ");
            //newAnimals.Category = Console.ReadLine().ToLower();
            EnterAnimalCategory();

            //Console.WriteLine("Gender: ");
            //newAnimals.Gender = Console.ReadLine().ToLower();
            EnterAnimalGender();

            //Console.WriteLine("Age: ");
            //newAnimals.Age = Console.ReadLine().ToLower();
            EnterAnimalAge();

            //Console.WriteLine("Shots: ");
            //newAnimals.Shots = Console.ReadLine().ToLower();
            EnterAnimalShots();

            //Console.WriteLine("Food: ");
            //newAnimals.Food = Console.ReadLine().ToLower();
            EnterAnimalFood();

            newAnimals.Status = "Available";

            EnterAdoptionFee();

            return newAnimals;
        }

        public void EnterAnimalId()
        {
            Console.WriteLine("Animal's Tag Id: ");
            newAnimals.Animal_Id = Convert.ToInt32(Console.ReadLine());
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
            Console.WriteLine("Adoption Fee Pricing - $50, $75, $100, $200" +
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

            if (newAdoptionFee.Adoption_Fee != 200 || newAdoptionFee.Adoption_Fee != 100 || newAdoptionFee.Adoption_Fee != 75 || newAdoptionFee.Adoption_Fee != 50)
            {
                Console.WriteLine("Invalid input.  Please follow the Fee pricing $50, $75, $100, $200");
                EnterAdoptionFee();
                return;
            }
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
                Console.WriteLine("Adoption Fee: " + a.AdoptionFee.Adoption_Fee);
            }

        }

       public void StartAdoptionProcess()
        {
            //string nameInput;
            //int tagInput;
            string customerFirstNameInput;
            string customerLastNameInput;
            int idInput;

            //Console.WriteLine("Enter Animal's Name: ");
            //nameInput = Console.ReadLine();

            //Console.WriteLine("Enter Animal's Tag Id: ");
            //tagInput = Convert.ToInt32(Console.ReadLine());

            EnterAnimalName();
            EnterAnimalId();

            Console.WriteLine("Enter Adopting Customer's First Name: ");
            customerFirstNameInput = Console.ReadLine();

            Console.WriteLine("Enter Adopting Customer's Last Name: ");
            customerLastNameInput = Console.ReadLine();

            Console.WriteLine("Enter Customer's Identification Number: ");
            idInput = Convert.ToInt32(Console.ReadLine());

            UpdateAdoptionStatus(idInput);
            CollectAdoptionFee(idInput);
        }

        public void UpdateAdoptionStatus(int idInput)
        {
            var animals = db.Animals.Where(a => a.Animal_Id == newAnimals.Animal_Id);

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

                    Console.WriteLine("{0} has been adopted." +
                        "\nAdoption status updated.", a.Name);
                }

                db.SubmitChanges();
            }
        }

        public void CollectAdoptionFee(int idInput)
        {
            var animals =
                from a in db.Animals
                join c in db.Adopters on a.Adopter_Id equals c.Adopter_Id
                where a.Animal_Id == newAnimals.Animal_Id && c.Adopter_Id == idInput
                select a;

            foreach (var a in animals)
            {
                a.Adopter.Paid_Amount = a.AdoptionFee.Adoption_Fee;
                Console.WriteLine("Adoption Fee Paid: ${0}", a.Adopter.Paid_Amount);
            }

            db.SubmitChanges();
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
                    Console.WriteLine("{0} - adopted by {1} {2}", a.Name, a.Adopter.First_Name, a.Adopter.Last_Name);
                }
            }            
        }

        public void GetAdoptedAnimal()
        {
            //string nameInput;
            //int tagInput;

            //Console.WriteLine("Enter Animal's Name: ");
            //nameInput = Console.ReadLine();

            //Console.WriteLine("Enter Animal's Tag Id: ");
            //tagInput = Convert.ToInt32(Console.ReadLine());

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
                    Console.WriteLine("\n{0} - adopted by {1} {2}", a.Name, a.Adopter.First_Name, a.Adopter.Last_Name);
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
                Console.WriteLine("Room is available.");
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
            //int roomInput;

            //Console.WriteLine("Enter Room Number: ");
            //roomInput = Convert.ToInt32(Console.ReadLine());

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
                    Console.WriteLine("\nRoom {0} is occupied by {1}.", a.Room.Room_Number, a.Name);
                }
            }
        }

        public void GetRoomByAnimal()
        {
            //string nameInput;
            //int tagInput;

            //Console.WriteLine("Enter Animal's Name: ");
            //nameInput = Console.ReadLine();

            //Console.WriteLine("Enter Animal's Tag Id: ");
            //tagInput = Convert.ToInt32(Console.ReadLine());

            EnterAnimalName();
            EnterAnimalId();

            var animals = db.Animals.Where(a => a.Name == newAnimals.Name && a.Animal_Id == newAnimals.Animal_Id);

            foreach (var a in animals)
            {
                Console.WriteLine("\nRoom {0} is occupied by {1}.", a.Room.Room_Number, a.Name);
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
            //string nameInput;
            //int tagInput;

            //Console.WriteLine("Enter Animal's Name: ");
            //nameInput = Console.ReadLine();

            //Console.WriteLine("Enter Animal's Tag Id: ");
            //tagInput = Convert.ToInt32(Console.ReadLine());

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
            var animals = db.Animals.Select(a => a.Category).Distinct();

            Console.WriteLine("\nCategory of Animals:");
            foreach (var a in animals)
            {
                Console.WriteLine(a);
            }
        }

        public void GetAnimalsInACategory()
        {
            //string categoryInput;
            int count = 0;

            //Console.WriteLine("Enter category of an animal: ");
            //categoryInput = Console.ReadLine();

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
            //string nameInput;
            //int tagInput;

            //Console.WriteLine("Enter Animal's Name: ");
            //nameInput = Console.ReadLine();

            //Console.WriteLine("Enter Animal's Tag Id: ");
            //tagInput = Convert.ToInt32(Console.ReadLine());

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
                    Console.WriteLine("{0} will need to consume {1} of food a week.", a.Name, a.Food);
                }
            }
        }

        public void UpdateFoodDiet()
        {
            //string nameInput;
            //int tagInput;
            //string foodInput;

            //Console.WriteLine("Enter Animal's Name: ");
            //nameInput = Console.ReadLine();

            //Console.WriteLine("Enter Animal's Tag Id: ");
            //tagInput = Convert.ToInt32(Console.ReadLine());

            //Console.WriteLine("Enter new Food amount: ");
            //foodInput = Console.ReadLine();

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

        public void ImportCSVFile()
        {
            var query = (from line in File.ReadLines(@"C:\\Users\Lor\Documents\Projects\C#\HumaneSociety\HumaneSociety.csv")
                        let csvLines = line.Split(';')
                        from csvLine in csvLines
                        where !String.IsNullOrWhiteSpace(csvLine)
                        let data = csvLine.Split(',')
                        select new
                        {
                            name = data[0],
                            category = data[1],
                            gender = data[2],
                            age = data[3],
                            shots = data[4],
                            food = data[5],
                            status = data[6],
                            room_id = data[7],
                            //adopter_id = data[8],
                            adoption_fee_id = data[9]
                        }).Skip(1);

            var animals = db.Animals;

            foreach (var q in query)
            {
                //Console.WriteLine(q.name);
                //Console.WriteLine(q.category);
                //Console.WriteLine(q.adopter_id);
                newAnimals.Name = q.name;
                newAnimals.Category = q.category;
                newAnimals.Gender = q.gender;
                newAnimals.Age = q.age;
                newAnimals.Shots = q.shots;
                newAnimals.Food = q.food;
                newAnimals.Status = q.status;
                newAnimals.Room_Id = int.Parse(q.room_id);
                //newAnimals.Adopter_Id = q.adopter_id;
                newAnimals.Adoption_Fee_Id = int.Parse(q.adoption_fee_id);

                //db.Animals.InsertOnSubmit(newAnimals);
                db.SubmitChanges();
            }

        }

    }
}
