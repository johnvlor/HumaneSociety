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
                "\n4. Shots" +
                "\n5. Animal Category" +
                "\n6. Food");
            input = Console.ReadLine();
        }

        public void DisplayRoomMenu()
        {
            Console.WriteLine("\nLook up a Room status:" +
                "\n1. By Room Number" +
                "\n2. By Animal" +
                "\n3. Display all occupied rooms" +
                "\n4. Display all available rooms");
            input = Console.ReadLine();
        }

        public void DisplayShotsMenu()
        {
            Console.WriteLine("\nLook up Shots status:" +
                "\n1. Update Shot status" +
                "\n2. Display all animals with shots" +
                "\n3. Display all animals that still needs shots");
            input = Console.ReadLine();
        }

        public void DisplayCategoryMenu()
        {
            Console.WriteLine("\nLook up Animal Category:" +
                "\n1. Display categories." +
                "\n2. Display all animals of a category");
            input = Console.ReadLine();
        }

        public void DisplayFoodMenu()
        {
            Console.WriteLine("\nLook up Food Status:" +
                "\n1. By Animal." +
                "\n2. Update Food Amount");
            input = Console.ReadLine();
        }

        public void GetMenuOption()
        {
            switch (input)
            {
                case "1":
                    humaneSociety.GetAnimalInformation();
                    //humaneSociety.AddToDatabase();
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
                case "5":
                    DisplayCategoryMenu();
                    GetCategoryMenuOption();
                    break;
                case "6":
                    DisplayFoodMenu();
                    GetFoodMenuOption();
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
                case "4":
                    humaneSociety.GetAvailableRooms();
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

        public void GetCategoryMenuOption()
        {
            switch (input)
            {
                case "1":
                    humaneSociety.GetAnimalCategories();
                    break;
                case "2":
                    humaneSociety.GetAnimalsInACategory();
                    break;
                default:
                    break;
            }
        }

        public void GetFoodMenuOption()
        {
            switch (input)
            {
                case "1":
                    humaneSociety.GetFood();
                    break;
                case "2":
                    humaneSociety.UpdateFoodDiet();
                    break;
                default:
                    break;
            }
        }
    }
}
