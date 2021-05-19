using Claims;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ClaimsConsoleApp
{
    public class Program_UI
    {
        private Claim_Repo _repo = new Claim_Repo();
        public void Run()
        {
            SeedClaims();
            Menu();
        }

        public void Header()
        {
            Console.WriteLine(@"
              _  __                         _          _____ _       _               
             | |/ /                        | |        / ____| |     (_)              
             | ' / ___  _ __ ___   ___   __| | ___   | |    | | __ _ _ _ __ ___  ___ 
             |  < / _ \| '_ ` _ \ / _ \ / _` |/ _ \  | |    | |/ _` | | '_ ` _ \/ __|
             | . \ (_) | | | | | | (_) | (_| | (_) | | |____| | (_| | | | | | | \__ \
             |_|\_\___/|_| |_| |_|\___/ \__,_|\___/   \_____|_|\__,_|_|_| |_| |_|___/
 ");
        }
        public void SeedClaims()
        {
            Claim claim1 = new Claim(1, ClaimType.Car, "Car Accident on 465", 400.00, new DateTime(2018, 4, 25), new DateTime(2018, 4, 27));
            Claim claim2 = new Claim(2, ClaimType.Home, "House fire in Kitchen", 4000.00, new DateTime(2018, 4, 11), new DateTime(2018, 4, 12));
            Claim claim3 = new Claim(3, ClaimType.Theft, "Stolen Pancakes", 4.00, new DateTime(2018, 4, 27), new DateTime(2018, 6, 01));

            _repo.AddClaim(claim1);
            _repo.AddClaim(claim2);
            _repo.AddClaim(claim3);
        }
        public void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Header();
                Console.WriteLine("Main Claims Menu");
                Console.WriteLine("1. See All Claims");
                Console.WriteLine("2. Handle Next Claim");
                Console.WriteLine("3. Enter New Claim");
                Console.WriteLine("4. Exit");
                Console.WriteLine();
                Console.WriteLine("Please enter number to continue");
                string menuSelection = Console.ReadLine();

                switch (menuSelection)
                {
                    case "1":
                        SeeAllClaims();
                        break;
                    case "2":
                        NextClaim();
                        break;
                    case "3":
                        NewClaim();
                        break;
                    case "4":
                        keepRunning = false;
                        break;
                    default:
                        break;
                }
            }
        }
        public void SeeAllClaims()
        {
            Console.Clear();
            Header();
            Queue<Claim> allClaims = _repo.GetAllClaims();
            Console.WriteLine("{0,-10} {1,6} {2,-25} {3,-12} {4,15} {5,12} {6,10}", "ClaimID", "Type", "Description", "ClaimAmount", "DateOfIncident", "DateOfClaim", "IsValid");
            foreach (Claim claim in allClaims)
            {
                Console.WriteLine("{0,-10} {1,6}    {2,-25}  ${3,-12:N2} {4,-15} {5,-15} {6,6}", claim.ClaimID, claim.Type, claim.ClaimDescription, claim.ClaimAmount, claim.DateOfIncident.ToString("MM/dd/yy"), claim.DateOfClaim.ToString("MM/dd/yy"), claim.IsValid);
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadKey();
        }

        public void NextClaim()
        {
            Console.Clear();
            Header();
            Console.WriteLine("Process Next Claim");
            Claim claim = new Claim();
            claim = _repo.PeekClaim();
            Console.WriteLine($"Claim ID : {claim.ClaimID}");
            Console.WriteLine($"Claim Type : {claim.Type}");
            Console.WriteLine($"Claim Description : {claim.ClaimDescription}");
            Console.WriteLine($"Claim Amount : ${claim.ClaimAmount:N2}");
            Console.WriteLine($"Date of Incident : {claim.DateOfIncident.ToString("MM/dd/yy")}");
            Console.WriteLine($"Date of Claim : {claim.DateOfClaim.ToString("MM/dd/yy")}");
            Console.WriteLine($"Is Claim Valid : {claim.IsValid}");
            Console.WriteLine();
            Console.WriteLine("Would you like to process this claim now? (Y/N)");
            string processClaim = Console.ReadLine();
            if (processClaim.ToLower() == "y")
            {
                bool dequeueSuccess = _repo.DequeueClaim();
                if (dequeueSuccess == true)
                {
                    Console.WriteLine("Claim removed from queue. Press any key to return.");
                }
                else
                {
                    Console.WriteLine("Something went wrong. Press any key to return.");
                }
                Console.ReadKey();
            }
        }
        public void NewClaim()
        {
            Claim claim = new Claim();

            bool looper = true;
            while (looper)
            {
                Console.Clear();
                Header();
                Console.WriteLine("Add New Claim");
                Console.WriteLine();
                Console.WriteLine("Enter New Claim ID:");
                claim.ClaimID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Select Claim Type:");
                Console.WriteLine("1. Car");
                Console.WriteLine("2. Home");
                Console.WriteLine("3. Theft");
                bool typeSelectLoop = true;
                string eventType = "1";
                while (typeSelectLoop)
                {
                    eventType = Console.ReadLine();
                    if (eventType == "1" || eventType == "2" || eventType == "3")
                    {
                        typeSelectLoop = false;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid selection- 1,2 or 3");
                    }
                }
                claim.Type = (ClaimType)int.Parse(eventType);
                Console.WriteLine("Enter Claim Description:");
                claim.ClaimDescription = Console.ReadLine();
                Console.WriteLine("Enter Claim Amount:");
                string claimAmount = Console.ReadLine();

                if (claimAmount[0] == '$')
                {
                    claimAmount = claimAmount.Trim('$');
                }
                claim.ClaimAmount = Convert.ToDouble(claimAmount);
                DateTime date = new DateTime();
                bool dateLoop = false;
                while (!dateLoop)
                {
                    Console.WriteLine("Enter Date of Incident (MM/DD/YYYY):");
                    string dateString = Console.ReadLine();
                    dateLoop = DateTime.TryParse(dateString, out date);
                }
                claim.DateOfIncident = date;
                dateLoop = false;
                while (!dateLoop)
                {
                    Console.WriteLine("Enter the Date of Claim (MM/DD/YYYY):");
                    string dateString = Console.ReadLine();
                    dateLoop = DateTime.TryParse(dateString, out date);
                }
                claim.DateOfClaim = date;

                if (claim.IsValid == true)
                {
                    Console.WriteLine("This claim is Valid.");
                }
                else
                {
                    Console.WriteLine("This claim is not Valid.");
                }
                Console.Clear();
                Console.WriteLine("Add Claim");
                Console.WriteLine();
                Console.WriteLine($"Claim ID          :     {claim.ClaimID}");
                Console.WriteLine($"Claim Type        :     {claim.Type}");
                Console.WriteLine($"Claim Description :     {claim.ClaimDescription}");
                Console.WriteLine($"Claim Amount      :     ${claim.ClaimAmount:N2}");
                Console.WriteLine($"Date of Incident  :     {claim.DateOfIncident.ToString("MM/dd/yy")}");
                Console.WriteLine($"Claim ID          :     {claim.DateOfClaim.ToString("MM/dd/yy")}");
                Console.WriteLine($"Is Claim Valid    :     {claim.IsValid}");
                Console.WriteLine();
                Console.WriteLine("Is all info correct? Press Y to add to the Queue, press N to start over.");
                string correct = Console.ReadLine();
                if (correct.ToLower() == "y")
                {
                    looper = false;
                }

            }
            bool wasAdded = _repo.AddClaim(claim);

            if (wasAdded == true)
            {
                Console.WriteLine("New claim added to the queue.");
                Console.WriteLine("Press any key to Continue.");
            }
            else
            {
                Console.WriteLine("Something went wrong. Please Try Again");
                Console.WriteLine("Press any key to continue.");
            }
            Console.ReadKey();
        }
    }
}

