using Badges;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BadgesConsoleApp
{
    public class ProgramUI
    {
        private Badge_Repo _repo = new Badge_Repo();

        public void Run()
        {
            SeedBadges();
            Menu();
        }

        public void SeedBadges()
        {
            Badge badge1 = new Badge(12345, new List<string> { "A7" });
            Badge badge2 = new Badge(22345, new List<string> { "A1", "A4", "B1", "B2" });
            Badge badge3 = new Badge(32345, new List<string> { "A4", "A5" });
            _repo.AddBadge(badge1);
            _repo.AddBadge(badge2);
            _repo.AddBadge(badge3);
        }

        public void Menu()
        {
            bool continueToRun = true;
            while (continueToRun)
            {
                Console.Clear();
                Header();
                Console.WriteLine("MAIN MENU");
                Console.WriteLine("1. Add a Badge");
                Console.WriteLine("2. Edit a Badge");
                Console.WriteLine("3. Delete all Door access from Badge");
                Console.WriteLine("4. View all Badges");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Enter a number");
                string menuSelection = Console.ReadLine();

                switch (menuSelection)
                {
                    case "1":
                        AddBadge();
                        break;
                    case "2":
                        EditBadge();
                        break;
                    case "3":
                        DeleteAllDoorsOnBadge();
                        break;
                    case "4":
                        ViewAll();
                        break;
                    case "5":
                        continueToRun = false;
                        break;
                    default:
                        break;
                }

            }
        }
        public void AddBadge()
        {
            Console.Clear();
            Header();
            Console.WriteLine("ADD A BADGE");
            Console.WriteLine("Enter the New Badge Number");
            int badgeNum = Convert.ToInt32(Console.ReadLine());
            if (_repo.GetBadgeByID(badgeNum) != null)
            {
                Console.WriteLine("Badge number already exists in system. Update instead of Add");
                Console.WriteLine("Press any key to return");
            }
            else
            {
                Badge newBadge = new Badge(badgeNum);
                bool looper = true;
                List<string> doors = new List<string>();
                while (looper)
                {
                    Console.WriteLine("Please enter Door that Badge #" + badgeNum + "needs acess to:");
                    doors.Add(Console.ReadLine());
                    Console.WriteLine("Are there more doors needed (Y/N)?");
                    string moreDoor = Console.ReadLine();
                    if (moreDoors.ToLower() == "n")
                    {
                        looper = false;
                    }
                }
                newBadge.Doors = doors;
                string doorResult = string.Join(",", doors);
                bool wasAdded = _repo.AddBadge(newBadge);
                if (wasAdded == true)
                {
                    Console.WriteLine($"Badge #{newBadge.BadgeID} has been added with Door Access: {doorResult}.");

                }
                else
                {
                    Console.WriteLine("Something went wrong. Please Try Again.");
                }
                Console.WriteLine("Press any key to return");
            }
            Console.ReadKey();
        }
        public void EditBadge()
        {
            Console.Clear();
            Header();
            Console.WriteLine("UPDATE A BADGE");
            Console.WriteLine("What Badge do you want to Update?" );
            int badgeNum = Convert.ToInt32(Console.ReadLine());
            Badge badgeToUpdate = _repo.GetABadgeByID(badgeNum);
            List<string> doorsOnBadge = new List<string>();
            if (badgeToUpdate != null)
            {
                doorsOnBadge = badgeToUpdate.Doors;
                bool looper = true;
                while (looper)
                {
                    Console.Clear();
                    Header();
                    Console.WriteLine("UPDATE A BADGE");
                    string doorsResult = string.Join(",", badgeToUpdate.Doors);
                    Console.WriteLine($"Badge #{badgeToUpdate.BadgeID} has access to doors: {doorsResult}");
                    Console.WriteLine("What update would you like to do?");
                    Console.WriteLine("1. Remove a door");
                    Console.WriteLine("2. Add a door");
                    Console.WriteLine("3. Finalize Badge");
                    string menuSelect = Console.ReadLine();
                    switch (menuSelect)
                    {
                        case "1":
                            Console.WriteLine("What door would you like to remove");
                            string doorToRemove = Console.ReadLine();
                            doorsOnBadge.Remove(doorToRemove);
                            badgeToUpdate.Doors = doorsOnBadge;
                            Console.WriteLine("Door has been removed");
                            doorsResult = string.Join(",", badgeToUpdate.Doors);
                            Console.WriteLine($"Badge #{badgeToUpdate.BadgeID} has access to doors : {doorsResult}.");
                            Console.WriteLine("Press any key to Continue");
                            Console.ReadKey();
                            break;
                        case "2":
                            Console.WriteLine("What door would you like to Add");
                            string doorToAdd = Console.ReadLine();
                            doorsOnBadge.Add(doorToAdd);
                            badgeToUpdate.Doors = doorsOnBadge;
                            Console.WriteLine("Door has been Added");
                            doorsResult = string.Join(",", badgeToUpdate.Doors);
                            Console.WriteLine($"Badge #{badgeToUpdate.BadgeID} has access to doors : {doorsResult}.");
                            Console.WriteLine("Press any key to Continue");
                            Console.ReadKey();
                            break;
                        case "3":
                            looper = false;
                            break;
                    }
                }
                bool wasUpdate = _repo.UpdateExistingBadge(badgeNum, badgeToUpdate);
                if (wasUpdate == true)
                {
                    Console.WriteLine("Badge has been Successfully Updated!");
                }
                else
                {
                    Console.WriteLine("Oh No! Something went wrong! Try Again.");
                }
            }
            else
            {
                Console.WriteLine("Badge ID not found");
            }
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
        }

        public void DeleteAllDoorsOnBadge()
        {
            Console.Clear();
            Header();
            Console.WriteLine("DELETE ALL DOORS FROM BADGE");
            Console.WriteLine("What badge # Do you want to clear?");
            int badgeNum = Convert.ToInt32(Console.ReadLine());
            Badge badgeToUpdate = _repo.GetABadgeByID(badgeNum);
            List<string> doorsOnBadge = new List<string>();
            if (badgeToUpdate != null)
            {
                string doorsResult = string.Join(",", badgeToUpdate.Doors);
                Console.WriteLine($"Badge #{badgeToUpdate.BadgeID} has access to doors: {doorsResult}.");
                Console.WriteLine($"Do you want to clear all access from Badge #{badgeToUpdate.BadgeID}? (Y/N)");
                string deleteAll = Console.ReadLine();
                if (deleteAll.ToLower() == "y")
                {
                    badgeToUpdate.Doors.Clear();
                    bool wasUpdate = _repo.UpdateExistingBadge(badgeNum, badgeToUpdate);
                    if (wasUpdate == true)
                    {
                        Console.WriteLine($"All Doors have been cleared from badge #{badgeNum}");
                    }
                    else
                    {
                        Console.WriteLine("Oh no! Something went wrong! Try Again.");
                    }
                }
                else
                {
                    Console.WriteLine("Cancelled!");
                }
            }
            else
            {
                Console.WriteLine("Badge ID not found.");
            }
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
        }
        public void ViewAll()
        {
            Console.Clear();
            Header();
            Console.WriteLine("VIEW ALL BADGES");
            Console.WriteLine();
            Console.WriteLine("Badge#   Door Access:" );
            Dictionary<int, List<string>> badges = _repo.GetAllBadges();
            foreach (KeyValuePair<int, List <string>> badge in badges)
            {
                string doorsResult = string.Join(",", badge.Value);
                Console.WriteLine($"{badge.Key}     {doorsResult}");
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
        }

        public void Header()
        {
            Console.WriteLine(@"

  _  __                         _          _____                      _ _         
 | |/ /                        | |        / ____|                    (_) |        
 | ' / ___  _ __ ___   ___   __| | ___   | (___   ___  ___ _   _ _ __ _| |_ _   _ 
 |  < / _ \| '_ ` _ \ / _ \ / _` |/ _ \   \___ \ / _ \/ __| | | | '__| | __| | | |
 | . \ (_) | | | | | | (_) | (_| | (_) |  ____) |  __/ (__| |_| | |  | | |_| |_| |
 |_|\_\___/|_| |_| |_|\___/ \__,_|\___/  |_____/ \___|\___|\__,_|_|  |_|\__|\__, |
                                                                             __/ |
                                                                            |___/ 
");
        }
    }
}