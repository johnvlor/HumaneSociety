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
                "\n3. Look up room status");
            input = Console.ReadLine();
        }

        public void DisplayRoomMenu()
        {
            Console.WriteLine("\nEnter an option:" +
                "\n1. By Room Number" +
                "\n2. By Animal" +
                "\n3. See all occupied rooms");
            input = Console.ReadLine();
        }

        public void GetMenuOption()
        {
            switch (input)
            {
                case "1":
                    //GetAnimalInformation();
                    //AddToDatabase();
                    humaneSociety.DisplayQuery();
                    break;
                case "2":
                    humaneSociety.UpdateStatus();
                    break;
                case "3":
                    DisplayRoomMenu();
                    GetRoomMenuOption();
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
                    humaneSociety.LookUpRoomNumber();
                    break;
                case "2":
                    humaneSociety.LookUpRoomByAnimal();
                    break;
                case "3":
                    humaneSociety.LookUpAllOccupiedRooms();
                    break;
                default:
                    break;
            }
        }
    }
}
