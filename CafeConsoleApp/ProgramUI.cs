using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe;

namespace CafeConsoleApp
{
    public class ProgramUI
    {
        private MenuRepo _menu = new MenuRepo();
        public void Run()
        {
            SeedItemMenu();
            MainMenu();
        }
        public void SeedItemMenu()
        {
            MenuItem meal1 = new MenuItem(01,
                "Fish and Chips",
                "Delicious North Atlantic Whitefish and fries",
                "3pc fish, housemade fries, homemade pickles and tarter" ,
                10);
            MenuItem meal2 = new MenuItem(02,
                "Personal Pizza",
                "Fresh Flatbread pizza with your choice of toppings" ,
                "Pepperoni, Sausage, Mushrooms, Onions, Banana Peppers" ,
                9);
            MenuItem meal3 = new MenuItem(03,
                "Fried Chicken Meal", 
                "3 Pcs of Southern Fried Chicken and choice of side",
                "Fries, Tots, Coleslaw, Baked beans",
                11);
            _menu.AddMenuItem(meal1);
            _menu.AddMenuItem(meal2);
            _menu.AddMenuItem(meal3);
        }
        public void MainMenu()
        {
            bool continueToRun = true;
            while (continueToRun)
            {
                Console.Clear();
                Header();
                Console.WriteLine("Main Menu");
                Console.WriteLine("1. View Cafe Menu Items");
                Console.WriteLine("2. Add New Menu Item");
                Console.WriteLine("3. Update Existing Item");
                Console.WriteLine("4. Delete Menu Item");
                Console.WriteLine("5. Exit Cafe Menu");
                Console.WriteLine("Please choose an option");
                string menuSelection = Console.ReadLine();

                switch (menuSelection)
                {
                    case "1":
                        ShowMenu();
                        break;
                    case "2":
                        AddNewItem();
                        break;
                    case "3":
                        UpdateMenuItem();
                        break;
                    case "4":
                        DeleteItem();
                        break;
                    case "5":
                        continueToRun = false;
                        break;
                    default:
                        break;
                }

            }
        }
        public void Header()
        {
            Console.WriteLine(@"
            ______                               _____        __     
            |  _  \                             /  __ \      / _|    
            | | | |_ __ __ _  __ _  ___  _ __   | /  \/ __ _| |_ ___ 
            | | | | '__/ _` |/ _` |/ _ \| '_ \  | |    / _` |  _/ _ \
            | |/ /| | | (_| | (_| | (_) | | | | | \__/\ (_| | ||  __/
            |___/ |_|  \__,_|\__, |\___/|_| |_|  \____/\__,_|_| \___|
                              __/ |                                  
                             |___/                                   
            ");
        }
        public void ShowMenu()
        {
            Console.Clear();
            Header();
            Console.WriteLine("Display All Menu Items");
            List<MenuItem> menuItems = _menu.GetMenu();

            foreach (MenuItem item in menuItems)
            {
                Console.WriteLine("Meal #:" + item.MealNumber);
                Console.WriteLine("Meal Name:" + item.MealName);
                Console.WriteLine("Meal Description:" + item.MealDescription);
                Console.WriteLine("Meal Ingredients:" + item.MealIngredients);
                Console.WriteLine("");
            }
            Console.WriteLine("Press enter to return to main menu");
            Console.ReadKey();
        }
        public void AddNewItem()
        {
            MenuItem newItem = new MenuItem();
            bool looper = true;

            while (looper)
            {
                Console.Clear();
                Header();
                Console.WriteLine("Adding New Menu Item");
                Console.WriteLine("Enter Meal #:");
                newItem.MealNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Meal Name:");
                newItem.MealName = Console.ReadLine();
                Console.WriteLine("Enter Meal Description:");
                newItem.MealDescription = Console.ReadLine();
                Console.WriteLine("Enter list of meal ingredients:");
                newItem.MealIngredients = Console.ReadLine();
                Console.WriteLine("Enter price of meal:");
                newItem.MealPrice = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("-- New Item --");
                Console.WriteLine("Meal #:" + newItem.MealNumber);
                Console.WriteLine("Meal Name:" + newItem.MealName);
                Console.WriteLine("Meal Description:" + newItem.MealDescription);
                Console.WriteLine("Meal Ingredients:" + newItem.MealIngredients);
                Console.WriteLine("Meal Price:$" + newItem.MealPrice);
                Console.WriteLine("");
                Console.WriteLine("Is the new menu item correct? Enter Y to continue adding New Item to the menu. Enter N to start over creating New Item.");
                string isCorrect = Console.ReadLine();
                if (isCorrect.ToLower() == "y")
                {
                    looper = false;
                }
            }
            bool wasAdded = _menu.AddMenuItem(newItem);
            if (wasAdded == true)
            {
                Console.WriteLine("Menu Item added successfully");
                Console.WriteLine("Press any key to return to the main menu");
            }
            else
            {
                Console.WriteLine("Something went wrong. Your new item was not added to the menu. Please try again.");
                Console.WriteLine("Press any key to return to the main menu");
            }

            Console.ReadKey();
        }
        public void UpdateMenuItem()
        {
            MenuItem oldItem = new MenuItem();
            Console.Clear();
            Header();
            Console.WriteLine("Update Menu Item");
            Console.WriteLine("Enter the number of the meal you want to update:");
            int oldMenuNum = Convert.ToInt32(Console.ReadLine());
            oldItem = _menu.GetByMealNumber(oldMenuNum);
            bool looper = true;
            while (looper)
            {
                Console.Clear();
                Console.WriteLine("Menu Item");
                Console.WriteLine("1> Meal #:" + oldItem.MealNumber);
                Console.WriteLine("2> Meal Name:" + oldItem.MealName);
                Console.WriteLine("3> Meal Description:" + oldItem.MealDescription);
                Console.WriteLine("4> Meal Ingredients:" + oldItem.MealIngredients);
                Console.WriteLine("5> Meal Price:$" + oldItem.MealPrice);
                Console.WriteLine("6> Done Editing");
                Console.WriteLine("Please enter the number for the value you'd like to edit");
                string menuSelection = Console.ReadLine();
                switch (menuSelection)
                {
                    case "1":
                        Console.WriteLine("Enter new Meal #:");
                        oldItem.MealNumber = Convert.ToInt32(Console.ReadLine());
                        break;
                    case "2":
                        Console.WriteLine("Enter new Name:");
                        oldItem.MealName = Console.ReadLine();
                        break;
                    case "3":
                        Console.WriteLine("Enter new meal Description:");
                        oldItem.MealDescription = Console.ReadLine();
                        break;
                    case "4":
                        Console.WriteLine("Enter new Ingredients:");
                        oldItem.MealIngredients = Console.ReadLine();
                        break;
                    case "5":
                        Console.WriteLine("Enter new Price:");
                        oldItem.MealPrice = Convert.ToDouble(Console.ReadLine());
                        break;
                    case "6":
                        looper = false;
                        break;
                }
            }
            bool wasUpdate = _menu.UpdateExistingMenuItem(oldMenuNum, oldItem);
            if (wasUpdate == true)
            {
                Console.WriteLine("Menu Item Updated\n" +
                    "Press any key to continue");
            }
            else
            {
                Console.WriteLine("Something went wrong. Please try again.\n" +
                    "Press any key to continue");
            }
            Console.ReadKey();
        }
        
        public void DeleteItem()
        {
            MenuItem oldItem = new MenuItem();
            Console.Clear();
            Header();
            Console.WriteLine("Deleting An Item");
            Console.WriteLine("Enter number of the Meal of the item you'd like to delete:");
            int mealNum = Convert.ToInt32(Console.ReadLine());
            oldItem = _menu.GetByMealNumber(mealNum);
            Console.WriteLine("-- Menu Item To Delete --");
            Console.WriteLine("Meal #:            " + oldItem.MealNumber);
            Console.WriteLine("Meal Name:         " + oldItem.MealName);
            Console.WriteLine("Meal Description:  " + oldItem.MealDescription);
            Console.WriteLine("Meal Ingredients:  " + oldItem.MealIngredients);
            Console.WriteLine("Meal Price:        $" + oldItem.MealPrice);
            Console.WriteLine("Are you Sure you Want to Continue? Deletions CANNOT be undone!");
            Console.WriteLine("Enter Y to continue deleting this Item. Enter N to return to the main menu.");
            string deleteConfirm = Console.ReadLine();
            if (deleteConfirm.ToLower() == "y")
            {
                bool deleteResult = _menu.DeleteExistingMenuItem(oldItem);
                if (deleteResult == true)
                {
                    Console.WriteLine("Item deleted Successfully.");
                    Console.WriteLine("Press any key to continue.");

                }
                else
                {
                    Console.WriteLine("Something went wrong. Please try again.");
                    Console.WriteLine("Press any key to continue.");

                }
            }
            else
            {
                Console.WriteLine("Delete Canceled. \nPress any Key to return to main menu.");
            }
            Console.ReadKey();
        }
    }
}
