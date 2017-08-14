using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Menu
    {
        private string input;
        private HumaneSociety humaneSociety;
        private Customer customer;
        private Import import;

        public Menu()
        {
            humaneSociety = new HumaneSociety();
            customer = new Customer();
            import = new Import();
        }

        public void Run()
        {
            DisplayMainMenu();
            GetMainMenuOption();
        }

        public void DisplayMainMenu()
        {
            Console.WriteLine("\nHumane Society Pet System");
            Console.WriteLine("Log in as Employee or Customer" +
                "\n1. Employee" +
                "\n2. Customer");
            input = Console.ReadLine();

            if (input != "1" && input != "2")
            {
                Console.WriteLine("Invalid input.  Please choose an option provided.");
                DisplayMainMenu();
                return;               
            }

        }

        public void DisplayEmployeeMenu()
        {           
            Console.WriteLine("\nEnter an option:" +
                "\n1. Add an animal to the system" +
                "\n2. Adoption Process" +
                "\n3. Room" +
                "\n4. Shots" +
                "\n5. Animal Category" +
                "\n6. Food" +
                "\n7. Customer" +
                "\n8. Report" +
                "\n9. Import CSV File");
            input = Console.ReadLine();

            if (input != "1" && input != "2" && input != "3" && input != "4" && input != "5" && input != "6" && input != "7" && input != "8" && input != "9")
            {
                Console.WriteLine("Invalid input.  Please choose an option provided.");
                DisplayEmployeeMenu();
                return;
            }
        }

        public void DisplayAdoptionMenu()
        {
            Console.WriteLine("\nEnter an option:" +
                "\n1. Start Adoption Process" +
                "\n2. Look up an Adopted Animal" +
                "\n3. Display all Adopted animals");
            input = Console.ReadLine();

            if (input != "1" && input != "2" && input != "3")
            {
                Console.WriteLine("Invalid input.  Please choose an option provided.");
                DisplayAdoptionMenu();
                return;
            }
        }

        public void DisplayRoomMenu()
        {
            Console.WriteLine("\nLook up a Room status:" +
                "\n1. By Room Number" +
                "\n2. By Animal" +
                "\n3. Display all occupied rooms" +
                "\n4. Display all available rooms");
            input = Console.ReadLine();

            if (input != "1" && input != "2" && input != "3" && input != "3")
            {
                Console.WriteLine("Invalid input.  Please choose an option provided.");
                DisplayRoomMenu();
                return;
            }

        }

        public void DisplayShotsMenu()
        {
            Console.WriteLine("\nLook up Shots status:" +
                "\n1. Update Shot status" +
                "\n2. Display all animals with shots" +
                "\n3. Display all animals that still needs shots");
            input = Console.ReadLine();

            if (input != "1" && input != "2" && input != "3")
            {
                Console.WriteLine("Invalid input.  Please choose an option provided.");
                DisplayShotsMenu();
                return;
            }
        }

        public void DisplayCategoryMenu()
        {
            Console.WriteLine("\nLook up Animal Category:" +
                "\n1. Display animal categories." +
                "\n2. Display all animals of a category");
            input = Console.ReadLine();

            if (input != "1" && input != "2")
            {
                Console.WriteLine("Invalid input.  Please choose an option provided.");
                DisplayCategoryMenu();
                return;
            }
        }

        public void DisplayFoodMenu()
        {
            Console.WriteLine("\nLook up Food Status:" +
                "\n1. By Animal" +
                "\n2. Update Food Amount");
            input = Console.ReadLine();

            if (input != "1" && input != "2")
            {
                Console.WriteLine("Invalid input.  Please choose an option provided.");
                DisplayFoodMenu();
                return;
            }
        }

        public void DisplayAdoptingCustomerMenu()
        {
            Console.WriteLine("\nLook up Adopting Customer:" +
                "\n1. By Name" +
                "\n2. By Animal Preference" +
                "\n3. Display all potential adopting customers");
            input = Console.ReadLine();

            if (input != "1" && input != "2" && input != "3")
            {
                Console.WriteLine("Invalid input.  Please choose an option provided.");
                DisplayAdoptingCustomerMenu();
                return;
            }
        }

        public void DisplayReportMenu()
        {
            Console.WriteLine("\nEnter an option:" +
                "\n1. All available animals" +
                "\n2. All adopted animals");
            input = Console.ReadLine();

            if (input != "1" && input != "2")
            {
                Console.WriteLine("Invalid input.  Please choose an option provided.");
                DisplayReportMenu();
                return;
            }
        }

        public void DisplayCustomerMenu()
        {
            Console.WriteLine("\nEnter an option:" +
                "\n1. Create a profile" +
                "\n2. Search for Pets");
            input = Console.ReadLine();

            if (input != "1" && input != "2")
            {
                Console.WriteLine("Invalid input.  Please choose an option provided.");
                DisplayCustomerMenu();
                return;
            }
        }

        public void DisplayCustomerSearchMenu()
        {
            Console.WriteLine("\nEnter a search option:" +
                "\n1. All available Pets" +
                "\n2. Pet Category");
            input = Console.ReadLine();

            if (input != "1" && input != "2")
            {
                Console.WriteLine("Invalid input.  Please choose an option provided.");
                DisplayCustomerSearchMenu();
                return;
            }
        }

        public void DisplayCustomerAddition()
        {
            Console.WriteLine("\nThank you for your submission.  We'll be in contact with you to followup.");
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
                    DisplayReportMenu();
                    GetReportMenuOption();
                    break;
                case "9":
                    import.ImportCSVFile();
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
                    customer.AddCustomerToDatabase();
                    DisplayCustomerAddition();
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
                    humaneSociety.GetAnimalCategories();
                    customer.SearchAnimalsInACategory();
                    PromptToNarrowSearch();
                    break;
                default:
                    break;
            }
        }

        public void GetReportMenuOption()
        {
            switch (input)
            {
                case "1":
                    humaneSociety.GetAllAvailableAnimals();
                    break;
                case "2":
                    humaneSociety.GetAllAdoptedAnimals();
                    break;
                default:
                    break;
            }
        }

        public void PromptToNarrowSearch()
        {
            Console.WriteLine("\nWould you like to narrow your search criteria?  Please enter yes or no.");
            input = Console.ReadLine().ToLower();

            if (input == "yes")
            {
                NarrowSearch();
            }
            else if (input == "no")
            {
                Console.WriteLine("Thank you for searching.");
            }
            else
            {
                Console.WriteLine("Invalid input.  Please enter yes or no.");
                PromptToNarrowSearch();
                return;
            }
        }

        public void NarrowSearch()
        {
            Console.WriteLine("\nPlease enter next search criteria." +
                "\n1. gender" +
                "\n2. age" +
                "\n3. shots" +
                "\n4. food");
            input = Console.ReadLine().ToLower();

            if (input == "1" || input == "2" || input == "3" || input == "4")
            {
                SearchCriteria();
            }
            else
            {
                Console.WriteLine("Invalid input.  Please choose one of the options provided.");
                NarrowSearch();
                return;
            }
        }

        public void SearchCriteria()
        {
            switch (input)
            {
                case "1":
                    customer.SearchByAdditionalGender();
                    break;
                case "2":
                    customer.GetAgeList();
                    customer.SearchByAdditionalAge();
                    break;
                case "3":
                    customer.SearchByAdditionalShots();
                    break;
                case "4":
                    customer.GetFoodList();
                    customer.SearchByAdditionalFood();
                    break;
                default:
                    break;
            }
        }
    }
}
