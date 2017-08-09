using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Menu
    {
        string input;
        HumaneSociety humaneSociety;

        public Menu()
        {
            humaneSociety = new HumaneSociety();
        }

        public void Run()
        {
            DisplayMenu();
            GetMenuOption();
        }

        public void DisplayMenu()
        {
            Console.WriteLine("Humane Society Pet System");
            Console.WriteLine("Enter an option:" +
                "\n1. Add an animal to the system" +
                "\n2. Update adoption status" +
                "\n3. Room" +
                "\n4. Shots");
            input = Console.ReadLine();
        }

        public void DisplayRoomMenu()
        {
            Console.WriteLine("\nLook up a room status:" +
                "\n1. By Room Number" +
                "\n2. By Animal" +
                "\n3. Display all occupied rooms");
            input = Console.ReadLine();
        }

        public void DisplayShotsMenu()
        {
            Console.WriteLine("\nLook up shot status:" +
                "\n1. Update Shot status" +
                "\n2. Display all animals with shots" +
                "\n3. Display all animals that still needs shots");
            input = Console.ReadLine();
        }

        public void GetMenuOption()
        {
            switch (input)
            {
                case "1":
                    humaneSociety.GetAnimalInformation();
                    humaneSociety.AddToDatabase();
                    humaneSociety.DisplayQuery();
                    break;
                case "2":
                    humaneSociety.UpdateAdoptionStatus();
                    humaneSociety.RemoveRoomNumber();
                    break;
                case "3":
                    DisplayRoomMenu();
                    GetRoomMenuOption();
                    break;
                case "4":
                    DisplayShotsMenu();
                    GetShotsMenuOption();
                    break;
                default:
                    break;
            }
        }

        public void GetRoomMenuOption()
        {
            switch (input)
            {
                case "1":
                    humaneSociety.GetRoomNumber();
                    break;
                case "2":
                    humaneSociety.GetRoomByAnimal();
                    break;
                case "3":
                    humaneSociety.GetAllOccupiedRooms();
                    break;
                default:
                    break;
            }
        }

        public void GetShotsMenuOption()
        {
            switch (input)
            {
                case "1":
                    humaneSociety.UpdateShots();
                    break;
                case "2":
                    humaneSociety.GetAnimalsWithShots();
                    break;
                case "3":
                    humaneSociety.GetAnimalsWithoutShots();
                    break;
                default:
                    break;
            }
        }
    }
}
