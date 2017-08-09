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
        Animal newAnimals;
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
            newAnimals.Category = Console.ReadLine();

            Console.WriteLine("Gender: ");
            newAnimals.Gender = Console.ReadLine();

            Console.WriteLine("Age: ");
            newAnimals.Age = Console.ReadLine();

            Console.WriteLine("Shots: ");
            newAnimals.Shots = Console.ReadLine();

            Console.WriteLine("Food: ");
            newAnimals.Food = Console.ReadLine();

            //Console.WriteLine("Status: ");
            //newAnimals.Status = Console.ReadLine();

            EnterRoomInformation();

            return newAnimals;
        }

        
        public void AddToDatabase(Animal newAnimals)
        {
            db.Animals.InsertOnSubmit(newAnimals);
            db.SubmitChanges();
        }

        public void DisplayQuery()
        {
            var animals = db.Animals.Where(a => a.Category == "Dog");
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
                Console.WriteLine("Room: " + x.Room.Room_Number);
                Console.WriteLine("Shots: " + x.Shots);
                Console.WriteLine("Food: " + x.Food);
            }

        }

        public void UpdateStatus()
        {
            string nameInput;
            int tagInput;

            Console.WriteLine("Enter Animal's Name: ");
            nameInput = Console.ReadLine();

            Console.WriteLine("Enter Animal's Tag Id: ");
            tagInput = Convert.ToInt32(Console.ReadLine());

            var animals = db.Animals.Where(a => a.Name == nameInput && a.Animal_Id == tagInput);

            foreach (var a in animals)
                a.Status = "Adopted";

            db.SubmitChanges();

            Console.WriteLine("Adoption status changed.");
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
            var rooms =
                from a in db.Rooms
                select a;

            foreach (var a in rooms)
            {
                if (newRoom.Room_Number == a.Room_Number)
                {
                    Console.WriteLine("Room is not available.  Please choose another one.");
                    EnterRoomInformation();
                    return;
                }
            }
        }

        public void GetAvailableRoom()
        {
            var rooms = 
                from a in db.Rooms
                select a;
          
            for (int i = 10; i <= 30; i++)
            {
                foreach (var a in rooms)
                {
                    if (i != a.Room_Number)
                    {
                        Console.WriteLine("Rooms available: " + i);
                        break;
                    }
                }
            }
        }

        public void LookUpRoomNumber()
        {
            int roomInput;

            Console.WriteLine("Enter Room Number: ");
            roomInput = Convert.ToInt32(Console.ReadLine());

            var animals = db.Animals.Where(a => a.Room.Room_Number == roomInput);

            foreach (var a in animals)
            {
                Console.WriteLine("Room {0} is occupied by {1}.", a.Room.Room_Number, a.Name);
            }
        }

        public void LookUpRoomByAnimal()
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

        public void LookUpAllOccupiedRooms()
        {
            var animals =
                from a in db.Animals
                where a.Room.Room_Number != null
                select a;

            foreach (var a in animals)
            {
                 Console.WriteLine("Room {0} is occupied by {1}.", a.Room.Room_Number, a.Name);
            }
        }
    }
}
