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

            //Console.WriteLine("Status: ");
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
            var animals = db.Animals;
                //from a in db.Animals
                //select a;

            foreach (var x in animals)
            {
                Console.WriteLine();
                Console.WriteLine("Tag ID: " + x.Animal_Id);
                Console.WriteLine("Name: " + x.Name);
                Console.WriteLine("Category: " + x.Category);
                Console.WriteLine("Gender: " + x.Gender);
                Console.WriteLine("Age: " + x.Age);
                Console.WriteLine("Status: " + x.Status);
                //Console.WriteLine("Room: " + x.Room.Room_Number);
                Console.WriteLine("Shots: " + x.Shots);
                Console.WriteLine("Food: " + x.Food);
            }

        }

        public void UpdateAdoptionStatus()
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
                    a.Status = "Adopted";
                }

                db.SubmitChanges();

                Console.WriteLine("Adoption status changed.");
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

            Console.WriteLine("Room Number is now available.");
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
            //newAnimals.Room.Room_Number
            newAnimals.Room = newRoom;
        }

        public void CheckRoomStatus()
        {
            var rooms = db.Rooms.Where(a => db.Animals.Any(r => r.Room_Id == a.Room_Id));
            //from a in db.Rooms
            //    join r in db.Animals on a.Room_Id equals r.Room_Id
            //    where a.Room_Number == newRoom.Room_Number && r.Room_Id != a.Room_Id
            //    select a;
            //from a in db.Rooms
            //where a.Room_Number == newRoom.Room_Number
            //select a;

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
    }
}
