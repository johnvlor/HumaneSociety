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
        Customer customer;

        public Menu()
        {
            humaneSociety = new HumaneSociety();
            customer = new Customer();
        }

        public void Run()
        {
            DisplayMainMenu();
            GetMainMenuOption();
        }

        public void DisplayMainMenu()
        {
            Console.WriteLine("Humane Society Pet System");
            Console.WriteLine("Log in as Employee or Customer" +
                "\n1. Employee" +
                "\n2. Customer");
            input = Console.ReadLine();
        }

        public void DisplayEmployeeMenu()
        {           
            Console.WriteLine("Enter an option:" +
                "\n1. Add an animal to the system" +
                "\n2. Adoption Process" +
                "\n3. Room" +
                "\n4. Shots" +
                "\n5. Animal Category" +
                "\n6. Food" +
                "\n7. Customer" +
                "\n8. Import CSV File");
            input = Console.ReadLine();
        }

        public void DisplayAdoptionMenu()
        {
            Console.WriteLine("\nEnter an option:" +
                "\n1. Start Adoption Process" +
                "\n2. Look up an Adopted Animal" +
                "\n3. Display all Adopted animals");
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
                "\n1. Display animal categories." +
                "\n2. Display all animals of a category");
            input = Console.ReadLine();
        }

        public void DisplayFoodMenu()
        {
            Console.WriteLine("\nLook up Food Status:" +
                "\n1. By Animal" +
                "\n2. Update Food Amount");
            input = Console.ReadLine();
        }

        public void DisplayAdoptingCustomerMenu()
        {
            Console.WriteLine("\nLook up Adopting Customer:" +
                "\n1. By Name" +
                "\n2. By Animal Preference" +
                "\n3. Display all potential adopting customers");
            input = Console.ReadLine();
        }

        public void DisplayCustomerMenu()
        {
            Console.WriteLine("\nEnter an option:" +
                "\n1. Create a profile" +
                "\n2. Search for Pets");
            input = Console.ReadLine();
        }

        public void DisplayCustomerSearchMenu()
        {
            Console.WriteLine("\nEnter a search option:" +
                "\n1. All available Pets" +
                "\n2. Pet Category");
            input = Console.ReadLine();
        }

        public void GetMainMenuOption()
        {
            switch (input)
            {
                case "1":
                    DisplayEmployeeMenu();
                    GetEmployeeMenuOption();
                    break;
                case "2":
                    DisplayCustomerMenu();
                    GetCustomerMenuOption();
                    break;
                default:
                    break;
            }
        }

        public void GetEmployeeMenuOption()
        {
            switch (input)
            {
                case "1":
                    humaneSociety.AddAnimalInformation();
                    humaneSociety.AddToDatabase();
                    humaneSociety.AddRoomInformation();
                    humaneSociety.AddRoom();
                    humaneSociety.AddAdoptionFee();
                    humaneSociety.DisplayQuery();
                    break;
                case "2":
                    DisplayAdoptionMenu();
                    GetAdoptionMenuOption();
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
                case "7":
                    DisplayAdoptingCustomerMenu();
                    GetAdoptingCustomerMenuOption();
                    break;
                case "8":
                    humaneSociety.ImportCSVFile();
                    break;
                default:
                    break;
            }
        }

        public void GetAdoptionMenuOption()
        {
            switch (input)
            {
                case "1":
                    humaneSociety.StartAdoptionProcess();
                    humaneSociety.RemoveRoomNumber();
                    break;
                case "2":
                    humaneSociety.GetAdoptedAnimal();
                    break;
                case "3":
                    humaneSociety.GetAllAdoptedAnimals();
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

        public void GetAdoptingCustomerMenuOption()
        {
            switch (input)
            {
                case "1":
                    humaneSociety.GetCustomer();
                    break;
                case "2":
                    humaneSociety.GetCustomerAnimalPreference();
                    break;
                case "3":
                    humaneSociety.GetAllCustomers();
                    break;
                default:
                    break;
            }
        }

        public void GetCustomerMenuOption()
        {
            switch (input)
            {
                case "1":
                    customer.CreateProfile();
                    //customer.AddCustomerToDatabase();
                    break;
                case "2":
                    DisplayCustomerSearchMenu();
                    GetCustomerSearchMenuOption();
                    break;
                default:
                    break;
            }
        }

        public void GetCustomerSearchMenuOption()
        {
            switch (input)
            {
                case "1":
                    customer.SearchAllAvailableAnimals();
                    break;
                case "2":
                    customer.SearchAnimalsInACategory();
                    break;
                default:
                    break;
            }
        }
    }
}
